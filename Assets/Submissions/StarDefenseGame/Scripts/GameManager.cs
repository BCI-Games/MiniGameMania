using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Reflection;
using UnityEngine.SceneManagement;
using static UnityEditor.FilePathAttribute;

namespace Submissions.StarDefense
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        public GameObject playerShooter;
        public GameObject opponentShooter;
        public GameObject playerShield;
        public GameObject opponentShield;

        public WallBehavior leftWall;
        public WallBehavior rightWall;

        public int turnNumber = 0;
        public int turnsToDo;
        public bool readyToProcess = true;

        public P300Controller controller;

        public Vector2 direction = Vector2.zero;
        public float power;
        public bool abilityChosen = false;
        public bool onGoingSimulation = false;
        public float maxReselectTime = 5;
        public float currentSelectTime = 0;
        public bool processingResult = false;

        public float menuReselectTime = 5;
        public float currentMenuTime = 0;

        private AudioSource audioSource;
        [SerializeField] private AudioClip[] sounds;

        public GameObject[] selections;
        public GameObject[] menuSelections;
        public static Dictionary<string, int> convertLayers = new Dictionary<string, int>()
    {
        {"PlayerRocket", 6},
        {"PlayerBomb", 9 },
        {"PlayerShield", 13},

        {"OpponentRocket", 11},
        {"OpponentBomb", 12},
        {"OpponentShield", 10},

        {"LeftWall", 7},
        {"RightWall", 8},
    };
        //ordered by appearance in selection
        //ROCKET, PICKAXE, BOMB, SHIELD
        public Sprite[] selectionIcons;
        public Dictionary<Sprite, Selection.SelectionType> typeAndImage = new Dictionary<Sprite, Selection.SelectionType>();

        public Queue<GameObject> playerUpcoming = new Queue<GameObject>();
        public Queue<GameObject> opponentUpcoming = new Queue<GameObject>();
        public GameObject displayTemplate;

        public bool gameEnded = false;
        // Start is called before the first frame update
        void Start()
        {
            if (Instance == null)
            {
                if (SelectionData.Instance != null)
                {
                    selectionIcons = SelectionData.Instance.randomSprites;
                }
                Time.timeScale = 1;
                int i = 0;
                foreach (Selection.SelectionType s in SelectionData.Instance.randomSelections)
                {
                    if (s.Equals("NONE")) { continue; }

                    typeAndImage.Add(selectionIcons[i], s);
                    i++;
                }

                audioSource = GetComponent<AudioSource>();
                Instance = this;

                if (controller == null)
                {
                    while (controller == null)
                    {
                        controller = FindObjectOfType<P300Controller>();
                    }
                }


                nextTurn();
            }
            else
            {
                Destroy(this);
            }

        }
        private void Update()
        {
            if (gameEnded == false)
            {
                if (Input.GetKeyDown(KeyCode.S))
                {
                    //Debug.Log(">>>Off");
                    abilityChosen = false;
                }
                if (processingResult)
                {
                    if (abilityChosen == false)
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
            else
            {
                currentMenuTime -= Time.deltaTime;
                if (currentMenuTime < 0)
                {
                    currentMenuTime = menuReselectTime;
                    StartCoroutine(WaitForMenuSelection());
                }

            }


        }

        public void nextTurn()
        {
            if (gameEnded == false)
            {
                turnNumber++;
                turnsToDo = 0;
                CanvasReference.Instance.turnDisplay.text = "Turn " + turnNumber;
                //amount of times to choose
                StartCoroutine(WaitToSelect());
                StartCoroutine(WaitToOppose(turnNumber, 1));
            }



        }

        public IEnumerator WaitToReSelect()
        {
            if (gameEnded == false)
            {
                Debug.Log("Reselecting::::::");
                currentSelectTime = maxReselectTime;
                abilityChosen = false;
                CanvasReference.Instance.prepare.SetActive(true);
                yield return new WaitForSeconds(1);
                CanvasReference.Instance.prepare.SetActive(false);
                onGoingSimulation = false;
                controller.StartStopStimulus();
                processingResult = true;
            }
        }


        public IEnumerator WaitToSelect()
        {
            if (gameEnded == false)
            {
                CanvasReference.Instance.prepare.SetActive(true);
                Debug.Log("Waiting to Select::::");
                currentSelectTime = maxReselectTime;
                abilityChosen = false;

                yield return new WaitForSeconds(2);

                RandomizeChoices();
                CanvasReference.Instance.prepare.SetActive(false);
                yield return new WaitForSeconds(1);
                onGoingSimulation = false;
                if (controller == null)
                {
                    while (controller == null)
                    {
                        controller = FindObjectOfType<P300Controller>();
                    }
                }
                controller.StartStopStimulus();
                processingResult = true;
            }

        }

        public void RandomizeChoices()
        {
            if (gameEnded == false)
            {
                Sprite previous = null;
                for (int i = 0; i < selections.Length; i++)
                {
                    Sprite current = null;
                    do
                    {
                        int randomIndex = UnityEngine.Random.Range(0, selectionIcons.Length);
                        current = selectionIcons[randomIndex];
                    } while (current == previous);
                    selections[i].GetComponent<SpriteRenderer>().sprite = current;
                    selections[i].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
                    selections[i].GetComponent<Selection>().selectionType = typeAndImage[current];
                    previous = current;
                }
            }

        }
        public void RandomizeOpponentChoices(int max, int index)
        {
            if (gameEnded == false)
            {
                int randomIndex = UnityEngine.Random.Range(0, typeAndImage.Count);
                Sprite current = selectionIcons[randomIndex];


                GameObject displayClone = Instantiate(displayTemplate);
                displayClone.GetComponent<UpcomingDisplay>().Setup(
                        current, typeAndImage[current], true, index - 1);
                displayClone.transform.SetParent(CanvasReference.Instance.enemyUpcoming.transform);
                displayClone.transform.localPosition = new Vector3(1000f, 0f, 0f);
                opponentUpcoming.Enqueue(displayClone);

                if (index < max)
                {
                    StartCoroutine(WaitToOppose(max, index + 1));
                }
            }


        }
        public IEnumerator WaitToOppose(int max, int index)
        {
            if (gameEnded == false)
            {
                yield return new WaitForSeconds(1);
                RandomizeOpponentChoices(max, index);
            }
        }

        public Selection.SelectionType stringToEnum(string str)
        {
            Enum.TryParse(str, out Selection.SelectionType myType);
            return myType;
        }
        public void ChoiceInput(Sprite display, Selection.SelectionType type)
        {
            if (gameEnded == false)
            {
                if (onGoingSimulation == false)
                {
                    onGoingSimulation = true;
                    if (abilityChosen == false)
                    {
                        audioSource.clip = sounds[1];//clicks
                        audioSource.Play();
                        //Debug.Log(">>>1");
                        abilityChosen = true;
                        GameObject displayClone = Instantiate(displayTemplate);
                        //Debug.Log(">>>2");
                        //Debug.Log(display.name);
                        //Debug.Log(type);
                        displayClone.GetComponent<UpcomingDisplay>().Setup(
                            display, type, false, turnsToDo);
                        //Debug.Log(">>>3");
                        displayClone.transform.SetParent(CanvasReference.Instance.playerUpcoming.transform);
                        //Debug.Log(">>>4");
                        displayClone.transform.localPosition = new Vector3(-1000f, 0f, 0f);
                        //Debug.Log(">>>5");
                        playerUpcoming.Enqueue(displayClone);
                        //Debug.Log(">>>6");
                        turnsToDo++;
                    }
                    if (turnsToDo < turnNumber)
                    {
                        //repeat
                        StartCoroutine(WaitToSelect());
                    }
                    else
                    {
                        CanvasReference.Instance.watch.SetActive(true);
                        StartCoroutine(WaitToChoose());
                    }
                }
            }


            //Debug.Log(">>>7");

        }
        public void ChoiceSelection(Selection.SelectionType type, bool isPlayer)
        {
            if (gameEnded == false)
            {
                if (type == Selection.SelectionType.ROCKET)
                {
                    ShootRocket(isPlayer);
                }
                else if (type == Selection.SelectionType.BOMB)
                {
                    ShootBomb(isPlayer);
                }
                else if (type == Selection.SelectionType.SHIELD)
                {
                    SpawnShield(isPlayer);
                }
                else if (type == Selection.SelectionType.ELECTRIC)
                {
                    BlastLaser(isPlayer);
                }
                else if (type == Selection.SelectionType.BULLET)
                {
                    ShootBullets(isPlayer);
                }
                if (isPlayer)
                {
                    if (playerUpcoming.Count > 0)
                    {
                        StartCoroutine(WaitToChoose());
                    }
                    else
                    {
                        StartCoroutine(WaitForNextTurn());
                    }
                }
            }

        }
        public IEnumerator WaitForNextTurn()
        {
            if (gameEnded == false)
            {
                yield return new WaitForSeconds(5);
                CanvasReference.Instance.watch.SetActive(false);
                nextTurn();
            }

        }
        public IEnumerator WaitToChoose()
        {
            if (gameEnded == false)
            {
                yield return new WaitForSeconds(3);
                selections[0].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
                selections[1].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
                if (playerUpcoming.Count > 0)
                {
                    playerUpcoming.Peek().GetComponent<UpcomingDisplay>().Decay();
                    ChoiceSelection(playerUpcoming.Dequeue().GetComponent<UpcomingDisplay>().selectionType, true);
                }
                foreach (GameObject display in playerUpcoming.ToArray())
                {
                    //Debug.Log(display.name);
                    display.GetComponent<UpcomingDisplay>().index -= 1;
                }

                //Debug.Log(playerUpcoming.Count);
                if (opponentUpcoming.Count > 0)
                {
                    opponentUpcoming.Peek().GetComponent<UpcomingDisplay>().Decay();
                    ChoiceSelection(opponentUpcoming.Dequeue().GetComponent<UpcomingDisplay>().selectionType, false);
                }

                foreach (GameObject display in opponentUpcoming.ToArray())
                {
                    //Debug.Log(display.name);
                    display.GetComponent<UpcomingDisplay>().index -= 1;
                }
            }

        }

        private void ShootRocket(bool isPlayer)
        {
            GameObject pooledProjectile = ObjectPooler.GetInstanceObject("Rocket");
            if (pooledProjectile != null)
            {
                float randomAngle = UnityEngine.Random.Range(-10f, 7f) - 90;
                float randomPower = UnityEngine.Random.Range(-2.5f, 4f) + power;

                string location = "none";
                GameObject shooter = null;
                if (isPlayer)
                {
                    location = "PlayerRocket";
                    shooter = playerShooter;
                }
                else
                {
                    location = "OpponentRocket";
                    shooter = opponentShooter;
                }
                Shoot(pooledProjectile, location, randomPower, randomAngle, shooter, true);
            }
        }
        private void ShootBomb(bool isPlayer)
        {
            GameObject pooledProjectile = ObjectPooler.GetInstanceObject("Bomb");
            if (pooledProjectile != null)
            {
                float randomAngle = UnityEngine.Random.Range(-10f, 10f) - 90;
                float randomPower = UnityEngine.Random.Range(-2.5f, 2.5f) + (float)(power * 0.625f);
                string location = "none";
                GameObject shooter = null;
                if (isPlayer)
                {
                    location = "PlayerBomb";
                    shooter = playerShooter;
                }
                else
                {
                    location = "OpponentBomb";
                    shooter = opponentShooter;
                }
                Shoot(pooledProjectile, location, randomPower, randomAngle, shooter, true);

            }
        }
        private void ShootBullets(bool isPlayer)
        {
            audioSource.clip = sounds[3]; //machine gun
            audioSource.Play();

            StartCoroutine(BulletDelay(isPlayer));

        }
        private IEnumerator BulletDelay(bool isPlayer)
        {
            yield return new WaitForSeconds(0.1f);
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    GameObject pooledProjectile = ObjectPooler.GetInstanceObject("Bullet");
                    if (pooledProjectile != null)
                    {
                        float randomAngle = UnityEngine.Random.Range(-10f, 10f) - 90;
                        float randomPower = UnityEngine.Random.Range(-10f, 10f) + (float)(power);
                        string location = "none";
                        GameObject shooter = null;
                        if (isPlayer)
                        {
                            location = "PlayerBullet";
                            shooter = playerShooter;
                        }
                        else
                        {
                            location = "OpponentBullet";
                            shooter = opponentShooter;
                        }
                        Shoot(pooledProjectile, location, randomPower, randomAngle, shooter, false);
                    }
                    yield return new WaitForSeconds(0.07f);
                }
                yield return new WaitForSeconds(0.3f);
            }

        }

        private void Shoot(GameObject projectile, string layerName, float power, float angle, GameObject shooter, bool sound)
        {
            if (sound)
            {
                audioSource.clip = sounds[0]; //cannon boom
                audioSource.Play();
            }

            projectile.layer = retrieveLayer(layerName);
            projectile.transform.position = shooter.transform.position; // position it at player
            projectile.transform.rotation = Quaternion.Euler(shooter.transform.rotation.eulerAngles + new Vector3(0, 0, angle));
            projectile.GetComponent<ProjectileBehavior>().resetProperties();

            projectile.SetActive(true); // activate it
            projectile.GetComponent<Rigidbody2D>().AddRelativeForce(direction * power, ForceMode2D.Impulse);
        }
        private void SpawnShield(bool isPlayer)
        {
            //Debug.Log("clicked");
            if (isPlayer)
            {
                playerShield.GetComponent<DeflectBehavior>().resetProperties();
                playerShield.SetActive(true);
            }
            else
            {
                opponentShield.GetComponent<DeflectBehavior>().resetProperties();
                opponentShield.SetActive(true);
            }

        }
        private void BlastLaser(bool isPlayer)
        {
            if (isPlayer)
            {
                playerShooter.GetComponentInChildren<ParticleSystem>().Play();
                playerShooter.GetComponentInChildren<AudioSource>().Play();
                rightWall.TakeDamage(7);
                //StartCoroutine(WaitToRemoveHealth(rightWall));
            }
            else
            {
                opponentShooter.GetComponentInChildren<ParticleSystem>().Play();
                opponentShooter.GetComponentInChildren<AudioSource>().Play();
                leftWall.TakeDamage(7);
                //StartCoroutine(WaitToRemoveHealth(leftWall));
            }
        }
        private IEnumerator WaitToRemoveHealth(WallBehavior target)
        {
            yield return new WaitForSeconds(2.25f);
            for (int i = 0; i < 20; i++)
            {
                target.TakeDamage(0.5f);
                yield return new WaitForSeconds(0.02f);
            }
        }

        public static int retrieveLayer(string layerName)
        {
            int layer = 0;
            convertLayers.TryGetValue(layerName, out layer);//opponent rocket
            return layer;
        }



        public void ChangeScene(GameMenuSelection.SelectionType type)
        {
            if (type == GameMenuSelection.SelectionType.REFRESH)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            else if (type == GameMenuSelection.SelectionType.PLAY)
            {
                ObjectPooler.SharedInstances = new Dictionary<string, ObjectPooler>();
                if (SceneManager.GetActiveScene().name.Equals("StarDefMainScene"))
                {

                    SceneManager.LoadScene("StarDefMainScene2");
                }
                else
                {
                    SceneManager.LoadScene("StarDefMainScene");
                }

            }
        }
        public void ChangeScene(string sceneName)
        {
            ObjectPooler.SharedInstances = new Dictionary<string, ObjectPooler>();
            SceneManager.LoadScene(sceneName);
        }
        public void EndGame(bool playerWins)
        {
            gameEnded = true;
            if (controller.stimOn)
            {
                controller.StartStopStimulus();
            }
            foreach (GameObject control in selections)
            {
                control.GetComponent<Selection>().includeMe = false;
            }
            foreach (GameObject control in menuSelections)
            {
                control.GetComponent<GameMenuSelection>().includeMe = true;
            }
            if (playerWins)
            {
                CanvasReference.Instance.statusText.text = "Winner";
            }
            else
            {
                CanvasReference.Instance.statusText.text = "Defeat";
            }

            CanvasReference.Instance.statusDisplay.SetActive(true);

            StartCoroutine(WaitForMenuSelection());
        }

        public IEnumerator WaitForMenuSelection()
        {
            currentMenuTime = menuReselectTime;
            yield return new WaitForSeconds(3);
            controller.StartStopStimulus();

        }

    }
}