using Gtec.Chain.Common.Nodes.Utilities.LDA;
using Gtec.Chain.Common.SignalProcessingPipelines;
using Gtec.Chain.Common.Templates.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using UnityEngine;
using static Gtec.Chain.Common.Nodes.InputNodes.ToWorkspace;
using static Gtec.Chain.Common.Templates.DataAcquisitionUnit.DataAcquisitionUnit;

namespace Submissions.BrainBuddy
{
    public class UIController : MonoBehaviour
    {
        public enum UIs { Menu, Training, Level, Loading, Credits, Instruction, Finished }

        public GameObject Menu;
        public GameObject Training;
        public GameObject Application;
        public GameObject MainCamera;
        public GameObject Loading;
        public GameObject Credits;
        public GameObject Instruction;
        public GameObject Finished;
        public GameObject Obstacles1;

        private const int TimeoutMs = 1000;

        private MenuController _menuController;
        private TrainingController _trainingController;
        private CreditsController _creditsController;
        private InstructionController _instructionController;
        private FlashControllerTraining _flashControllerTraining;
        private FlashControllerApplication _flashControllerApplication;
        private SequenceManager _sequenceManager;
        private States _previousState;
        private bool _changeDialog;
        private UIs _dialog;
        private bool _flashControllerApplicationStarted;
        private int _selectedClassBufLength;
        private int[] _selectedClassBuf;
        private int _selectedClassBufCnt;


        void ShowUI(UIs ui)
        {
            switch (ui)
            {
                case UIs.Menu:
                    Menu.SetActive(true);
                    Training.SetActive(false);
                    Application.SetActive(false);
                    Loading.SetActive(false);
                    Credits.SetActive(false);
                    Instruction.SetActive(false);
                    Finished.SetActive(false);
                    break;
                case UIs.Training:
                    Menu.SetActive(false);
                    Training.SetActive(true);
                    Application.SetActive(false);
                    Loading.SetActive(false);
                    Credits.SetActive(false);
                    Instruction.SetActive(false);
                    Finished.SetActive(false);
                    break;
                case UIs.Level:
                    Menu.SetActive(false);
                    Training.SetActive(false);
                    Application.SetActive(true);
                    Loading.SetActive(false);
                    Credits.SetActive(false);
                    Instruction.SetActive(false);
                    Finished.SetActive(false);
                    break;
                case UIs.Loading:
                    Menu.SetActive(false);
                    Training.SetActive(false);
                    Application.SetActive(false);
                    Loading.SetActive(true);
                    Credits.SetActive(false);
                    Instruction.SetActive(false);
                    Finished.SetActive(false);
                    break;
                case UIs.Credits:
                    Menu.SetActive(false);
                    Training.SetActive(false);
                    Application.SetActive(false);
                    Loading.SetActive(false);
                    Credits.SetActive(true);
                    Instruction.SetActive(false);
                    Finished.SetActive(false);
                    break;
                case UIs.Instruction:
                    Menu.SetActive(false);
                    Training.SetActive(false);
                    Application.SetActive(false);
                    Loading.SetActive(false);
                    Credits.SetActive(false);
                    Instruction.SetActive(true);
                    Finished.SetActive(false);
                    break;
                case UIs.Finished:
                    Menu.SetActive(false);
                    Training.SetActive(false);
                    Application.SetActive(false);
                    Loading.SetActive(false);
                    Credits.SetActive(false);
                    Instruction.SetActive(false);
                    Finished.SetActive(true);
                    break;
                default:
                    break;
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            try
            {
                _selectedClassBufLength = 3;
                _selectedClassBuf = new int[_selectedClassBufLength];
                _selectedClassBufCnt = 0;

                _flashControllerApplicationStarted = false;
                _previousState = States.Disconnected;

                _menuController = Menu.GetComponent<MenuController>();
                _trainingController = Training.GetComponent<TrainingController>();
                _creditsController = Credits.GetComponent<CreditsController>();
                _instructionController = Instruction.GetComponent<InstructionController>();
                _flashControllerTraining = Training.GetComponentInChildren<FlashControllerTraining>();
                _flashControllerApplication = Application.GetComponentInChildren<FlashControllerApplication>();

                //UI events
                _menuController.BtnConnectClick += OnBtnConnectClick;
                _menuController.BtnCreditsClick += OnBtnCreditsClick;
                _creditsController.BtnCreditsBackClick += OnBtnCreditsBackClick;
                _instructionController.BtnInstructionsContinueClick += OnBtnInstructionsContinueClick;
                _trainingController.BtnStartStopTrainingClick += OnTrainingStartStopClick;

                //flash controller training events
                _flashControllerTraining.FlashingStarted += OnFlashingStartedTraining;
                _flashControllerTraining.FlashingStopped += OnFlashingStoppedTraining;
                _flashControllerTraining.Trigger += OnTrigger;

                //flash controller application events
                _flashControllerApplication.FlashingStarted += OnFlashingStartedApplication;
                _flashControllerApplication.FlashingStopped += OnFlashingStoppedApplication;
                _flashControllerApplication.Trigger += OnTrigger;

                _sequenceManager = new SequenceManager(_flashControllerTraining.NumberOfClasses);

                //BCI Manager events
                BCIManager.Instance.RuntimeExceptionOccured += OnRuntimeExceptionOccured;
                BCIManager.Instance.ModeChanged += OnModeChanged;
                BCIManager.Instance.ScoreValueAvailable += OnScoreValueAvailable;
                BCIManager.Instance.SignalQualityAvailable += OnSignalQualityAvailable;
                BCIManager.Instance.ClassifierCalculated += OnClassifierAvailable;
                BCIManager.Instance.StateChanged += OnBCIStateChanged;

                if (_menuController == null)
                    throw new Exception("Could not get menu controller.");

                if (_flashControllerTraining == null)
                    throw new Exception("Could not get flash controller training.");

                if (_flashControllerApplication == null)
                    throw new Exception("Could not get flash controller application.");
            }
            catch (Exception ex)
            {
                UnityEngine.Debug.Log(String.Format("UI controller initialization failed.\n{0}\n{1}", ex.Message, ex.StackTrace));
            }

            //initiali ui
            ShowUI(UIs.Menu);
            //TODO JUST FOR DEBUG REMOVE AFTERWARDS
            //ShowUI(UIs.Level);
        }

        // Update is called once per frame
        void Update()
        {
            if (_changeDialog)
            {
                ShowUI(_dialog);
                _changeDialog = false;
            }

            if (_dialog == UIs.Level && _flashControllerApplication.Initialized && !_flashControllerApplicationStarted)
            {
                _flashControllerApplication.StartFlashing();
                _flashControllerApplicationStarted = true;
            }

            //simulate bci selection with numpad
            if (Input.GetKeyDown(KeyCode.Keypad1))
            {
                _selectedClassBuf[_selectedClassBufCnt % _selectedClassBufLength] = 1;
                _selectedClassBufCnt++;
            }
            if (Input.GetKeyDown(KeyCode.Keypad2))
            {
                _selectedClassBuf[_selectedClassBufCnt % _selectedClassBufLength] = 2;
                _selectedClassBufCnt++;
            }
            if (Input.GetKeyDown(KeyCode.Keypad3))
            {
                _selectedClassBuf[_selectedClassBufCnt % _selectedClassBufLength] = 3;
                _selectedClassBufCnt++;
            }
            if (Input.GetKeyDown(KeyCode.Keypad4))
            {
                _selectedClassBuf[_selectedClassBufCnt % _selectedClassBufLength] = 4;
                _selectedClassBufCnt++;
            }
            if (Input.GetKeyDown(KeyCode.Keypad5))
            {
                _selectedClassBuf[_selectedClassBufCnt % _selectedClassBufLength] = 5;
                _selectedClassBufCnt++;
            }
            if (Input.GetKeyDown(KeyCode.Keypad6))
            {
                _selectedClassBuf[_selectedClassBufCnt % _selectedClassBufLength] = 6;
                _selectedClassBufCnt++;
            }

            int selectedClass = -1;
            for (int i = 1; i < _selectedClassBuf.Length; i++)
            {
                if (_selectedClassBuf[i] == _selectedClassBuf[i - 1])
                    selectedClass = _selectedClassBuf[i];
                else
                {
                    selectedClass = -1;
                    break;
                }
            }

            if (selectedClass > 0)
            {
                UnityEngine.Debug.Log(String.Format("Selected: {0}", selectedClass));

                if (selectedClass == 1)
                    Obstacles1.SetActive(false);
                if (selectedClass == 2)
                    Obstacles1.SetActive(true);

                for (int i = 1; i < _selectedClassBuf.Length; i++)
                {
                    _selectedClassBuf[i] = 0;
                    _selectedClassBufCnt = 0;
                }
            }

            if (FinishedController.Instance.FinishReached)
            {
                _changeDialog = true;
                _dialog = UIs.Finished;
            }
        }

        private void OnDestroy()
        {

        }

        private void OnApplicationQuit()
        {
            //UI events
            _menuController.BtnConnectClick -= OnBtnConnectClick;
            _menuController.BtnCreditsClick -= OnBtnCreditsClick;
            _creditsController.BtnCreditsBackClick -= OnBtnCreditsBackClick;
            _trainingController.BtnStartStopTrainingClick -= OnTrainingStartStopClick;

            //flash controller training events
            _flashControllerTraining.FlashingStarted -= OnFlashingStartedTraining;
            _flashControllerTraining.FlashingStopped -= OnFlashingStoppedTraining;
            _flashControllerTraining.Trigger -= OnTrigger;

            //flash controller application events
            _flashControllerApplication.FlashingStarted -= OnFlashingStartedApplication;
            _flashControllerApplication.FlashingStopped -= OnFlashingStoppedApplication;
            _flashControllerApplication.Trigger -= OnTrigger;

            BCIManager.Instance.Uninitialize();

            BCIManager.Instance.RuntimeExceptionOccured -= OnRuntimeExceptionOccured;
            BCIManager.Instance.ModeChanged -= OnModeChanged;
            BCIManager.Instance.ScoreValueAvailable -= OnScoreValueAvailable;
            BCIManager.Instance.SignalQualityAvailable -= OnSignalQualityAvailable;
            BCIManager.Instance.ClassifierCalculated -= OnClassifierAvailable;
            BCIManager.Instance.StateChanged -= OnBCIStateChanged;
        }

        private void OnClassifierAvailable(object sender, EventArgs e)
        {
            UnityEngine.Debug.Log("Classifier calculated.");

            Dictionary<int, Accuracy> accuracy = BCIManager.Instance.Accuracy();
            foreach (KeyValuePair<int, Accuracy> kvp in accuracy)
                UnityEngine.Debug.Log(String.Format("Trials:{0}/ Accuracy: {1}%", kvp.Key, kvp.Value.Mean));

            BCIManager.Instance.Configure(ERPPipeline.Mode.Application);

            _changeDialog = true;
            _dialog = UIs.Level;
        }

        private void OnSignalQualityAvailable(object sender, EventArgs e)
        {
            //TODO
        }

        private void OnScoreValueAvailable(object sender, EventArgs e)
        {
            ToWorkspaceEventArgs ea = (ToWorkspaceEventArgs)e;

            //convert scores to bool array
            double[] scores = new double[ea.Data.GetLength(0)];
            for (int i = 0; i < ea.Data.GetLength(0); i++)
                scores[i] = ea.Data[i, 1];
            bool[] sequence = new bool[scores.Length];
            int maxValPos = 0;
            double maxVal = 0;
            for (int i = 0; i < scores.Length; i++)
            {
                if (i == 0)
                {
                    maxVal = scores[i];
                    maxValPos = i;
                }

                if (scores[i] > maxVal)
                {
                    maxVal = scores[i];
                    maxValPos = i;
                }
            }

            for (int i = 0; i < scores.Length; i++)
            {
                if (i == maxValPos)
                    sequence[i] = true;
                else
                    sequence[i] = false;
            }

            int selectedClass = _sequenceManager.GetSequenceID(sequence);
            UnityEngine.Debug.Log(String.Format("Selected Class: {0}", selectedClass));

            _selectedClassBuf[_selectedClassBufCnt % _selectedClassBufLength] = selectedClass;
            _selectedClassBufCnt++;
        }

        private void OnModeChanged(object sender, EventArgs e)
        {
            ModeChangedEventArgs ea = (ModeChangedEventArgs)e;
            UnityEngine.Debug.Log(String.Format("Changed mode to: {0}", ea.Mode));
        }

        private void OnRuntimeExceptionOccured(object sender, EventArgs e)
        {
            RuntimeExceptionEventArgs ea = (RuntimeExceptionEventArgs)e;
            UnityEngine.Debug.Log(String.Format("A runtime exception occured.\n{0}\n{1}", ea.Exception.Message, ea.Exception.StackTrace));
        }

        private void OnFlashingStartedTraining(object sender, EventArgs e)
        {
            UnityEngine.Debug.Log("Started flashing for training.");
        }

        private void OnFlashingStoppedTraining(object sender, EventArgs e)
        {
            UnityEngine.Debug.Log("Stopped flashing for training.");
            Stopwatch sw = new Stopwatch();
            sw.Start();
            while (BCIManager.Instance.TargetCount != _flashControllerTraining.NumberOfTrials && sw.ElapsedMilliseconds < TimeoutMs)
                Thread.Sleep(1);
            sw.Stop();

            if (sw.ElapsedMilliseconds > TimeoutMs)
                UnityEngine.Debug.Log(String.Format("Did not receive all triggers. {0}/{1} triggers received", BCIManager.Instance.TargetCount, _flashControllerTraining.NumberOfTrials));

            BCIManager.Instance.Train();
        }

        private void OnFlashingStartedApplication(object sender, EventArgs e)
        {
            UnityEngine.Debug.Log("Started flashing for application.");
        }

        private void OnFlashingStoppedApplication(object sender, EventArgs e)
        {
            UnityEngine.Debug.Log("Stopped flashing for training.");
        }

        private void OnTrigger(object sender, EventArgs e)
        {
            TriggerEventArgs ea = (TriggerEventArgs)e;
            if (BCIManager.Instance.Initialized)
                BCIManager.Instance.SetTrigger(ea.IsTarget, ea.Id, ea.Trial, ea.IsLastOfTrial);
        }

        private void OnTrainingStartStopClick(object sender, EventArgs e)
        {
            if (_trainingController.TrainingStart)
                _flashControllerTraining.StartFlashing();
            else
                _flashControllerTraining.StopFlashing();
        }

        private void OnBtnCreditsBackClick(object sender, EventArgs e)
        {
            _changeDialog = true;
            _dialog = UIs.Menu;
        }

        private void OnBtnCreditsClick(object sender, EventArgs e)
        {
            _changeDialog = true;
            _dialog = UIs.Credits;
        }

        private void OnBtnConnectClick(object sender, EventArgs e)
        {
            ShowUI(UIs.Loading);

            new Thread(() =>
            {
                try
                {
                    BCIManager.Instance.Initialize(_menuController.SelectedSerial);
                }
                catch (Exception ex)
                {
                    try
                    {
                        BCIManager.Instance.Uninitialize();
                    }
                    catch
                    {
                    //DO NOTHING 
                }

                    UnityEngine.Debug.Log(String.Format("Device initialization failed.\n{0}\n{1}", ex.Message, ex.StackTrace));
                }
            }).Start();
        }

        private void OnBCIStateChanged(object sender, EventArgs e)
        {
            StateChangedEventArgs ea = (StateChangedEventArgs)e;
            UnityEngine.Debug.Log(String.Format("Device state changed to '{0}'", ea.State));

            //switch to training dialog after connection
            if ((ea.State == States.Connected || ea.State == States.Acquiring) &&
                (_previousState == States.Connecting || _previousState == States.Disconnected))
            {
                _changeDialog = true;
                _dialog = UIs.Instruction;
            }

            if (ea.State == States.Disconnected)
            {
                _changeDialog = true;
                _dialog = UIs.Menu;
            }

            //store last state
            _previousState = ea.State;
        }

        private void OnBtnInstructionsContinueClick(object sender, EventArgs e)
        {
            _changeDialog = true;
            _dialog = UIs.Training;
        }
    }
}