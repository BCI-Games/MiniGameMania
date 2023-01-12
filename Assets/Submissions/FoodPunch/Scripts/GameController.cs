using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using BCIEssentials;

namespace Submissions.FoodPunch
{
    public class GameController : MonoBehaviour
    {
        public MIControllerBehavior bci_controller;
        public CrowdController crowdController;
        public Transform pointer;

        public TMP_Text crowdText;

        public GameObject win1;
        public GameObject win2;

        public int score = 0;

        public string currentRequest = "";

        public PlayerController bciPlayer;
        public PlayerController kbPlayer;
        int prevTime = 0;

        int frame = 0;

        bool gameEnded = false;

        void Start()
        {
            bci_controller = Object.FindObjectOfType<MIControllerBehavior>();
            bci_controller.StartStopStimulus();
            crowdController = gameObject.GetComponent<CrowdController>();
            bciPlayer.crowdController = crowdController;
            kbPlayer.crowdController = crowdController;

            newRound();
        }

        public void left()
        {
            bciPlayer.left();
        }

        public void right()
        {
            bciPlayer.right();
        }

        void Update()
        {
            if (!gameEnded)
            {
                score = (-bciPlayer.score + -kbPlayer.score);

                pointer.position = new Vector3(score * 1.67f / 50f, -4.4f, -2f);


                if (frame - prevTime >= 15 * 60)
                {
                    bciPlayer.pause();
                    kbPlayer.pause();
                    bciPlayer.clearFood();
                    kbPlayer.clearFood();
                    newRound();
                    prevTime = frame;
                }


                if (frame >= 2 * 60 * 60)
                {
                    bciPlayer.pause();
                    kbPlayer.pause();
                    bciPlayer.clearFood();
                    kbPlayer.clearFood();
                    bciPlayer.gameEnded = true;
                    kbPlayer.gameEnded = true;
                    end();
                }

                frame++;
            }
        }

        void newRound()
        {
            crowdController.generateNewFood();

            crowdText.text = crowdController.getRequest();

            bciPlayer.spawnFood();
            kbPlayer.spawnFood();
        }

        void end()
        {
            gameEnded = true;
            if (score < 0)
            {
                Destroy(kbPlayer.gameObject);
                crowdText.text = "Player 1 wins!";
                win1.SetActive(true);
            }
            else
            {
                Destroy(bciPlayer.gameObject);
                crowdText.text = "Player 2 wins!";
                win2.SetActive(true);
            }
        }
    }
}