using System.Collections;
using System.Collections.Generic;
using BCIEssentials.Controllers;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Submissions.StarDefense
{
    public class MenuManager : MonoBehaviour
    {
        public static MenuManager Instance;

        public GameObject titleSection;
        public GameObject setupSection;

        public GameObject[] existingPrefabs;

        public GameObject[] displayRolls;

        public BCIController controller;

        public bool menuChosen = false;
        public float maxReselectTime = 5;
        public float currentSelectTime = 0;
        public bool processingResult = false;
        public bool allowedToRefresh = true;

        private AudioSource audioSource;
        [SerializeField] private AudioClip[] sounds;

        public void Start()
        {
            if (Instance == null)
            {
                Time.timeScale = 1;
                audioSource = GetComponent<AudioSource>();
                controller = BCIController.Instance;
                allowedToRefresh = true;
                RefreshChoices();
                allowedToRefresh = false;
                if (allowedToRefresh)
                {
                    StartCoroutine(WaitToReSelect());
                }

                Instance = this;
            }

        }
        public void Update()
        {

            if (allowedToRefresh && processingResult)
            {
                if (menuChosen == false)
                {
                    currentSelectTime -= Time.deltaTime;
                    if (currentSelectTime < 0)
                    {
                        Debug.Log("Nothing was chosen");
                        processingResult = false;
                        currentSelectTime = maxReselectTime;
                        StartCoroutine(WaitToReSelect());
                    }
                }
            }
        }
        public void Output(RefreshMenu.SelectionType type)
        {
            if (type != RefreshMenu.SelectionType.NONE)
            {
                audioSource.clip = sounds[0];//clicks
                audioSource.Play();
            }
            if (type == RefreshMenu.SelectionType.REFRESH)
            {
                RefreshChoices();
            }
            else if (type == RefreshMenu.SelectionType.PLAY)
            {
                SceneSwap("StarDefMainScene");
            }
        }
        public IEnumerator WaitToReSelect()
        {
            Debug.Log("Reselecting::::::");
            menuChosen = false;
            yield return new WaitForSeconds(2);
            currentSelectTime = maxReselectTime;
            controller.StartStopStimulus();
            processingResult = true;


        }
        public void RefreshChoices()
        {
            if (allowedToRefresh == true)
            {
                for (int x = 0; x < 4; x++)
                {
                    SelectionData.Instance.randomSelections[x] = Selection.SelectionType.NONE;
                }
                for (int i = 0; i < 4; i++)
                {
                    int randomIndex = UnityEngine.Random.Range(0, existingPrefabs.Length);
                    GameObject display = existingPrefabs[randomIndex];
                    bool contains = true;
                    while (contains)
                    {
                        bool found = false;
                        Selection.SelectionType[] data = SelectionData.Instance.randomSelections;
                        for (int a = 0; a < data.Length; a++)
                        {
                            if (data[a] == Selection.SelectionType.NONE)
                            {
                                break;
                            }
                            if (data[a] == display.GetComponent<AbilityStorage>().selectionType)
                            {
                                found = true;
                                break;
                            }
                        }
                        if (found)
                        {
                            randomIndex = UnityEngine.Random.Range(0, existingPrefabs.Length);
                            display = existingPrefabs[randomIndex];
                        }
                        else
                        {
                            contains = false;
                            displayRolls[i].GetComponent<Image>().sprite = display.GetComponent<AbilityStorage>().sprite;
                            SelectionData.Instance.randomSelections[i] = display.GetComponent<AbilityStorage>().selectionType;
                            SelectionData.Instance.randomSprites[i] = display.GetComponent<AbilityStorage>().sprite;
                        }
                    }
                }

            }
        }
        public void SceneSwap(string myScene)
        {
            SceneManager.LoadScene(myScene);
        }
        public void LoadSelection(string type)
        {
            audioSource.clip = sounds[0];//clicks
            audioSource.Play();
            allowedToRefresh = false;
            if (type.Equals("title"))
            {
                if (controller.ActiveBehavior.stimOn)
                {
                    controller.StartStopStimulus();
                }

                titleSection.SetActive(true);
                setupSection.SetActive(false);
            }
            else if (type.Equals("setup"))
            {
                allowedToRefresh = true;
                titleSection.SetActive(false);
                setupSection.SetActive(true);
                StartCoroutine(WaitToReSelect());
            }
        }

        public void QuitApp()
        {
            Application.Quit();
        }

    }
}