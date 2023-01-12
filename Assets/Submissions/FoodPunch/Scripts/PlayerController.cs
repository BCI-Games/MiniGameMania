using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Submissions.FoodPunch
{
    public class PlayerController : MonoBehaviour
    {
        public CrowdController crowdController;
        public MeterController leftMeter;
        public MeterController rightMeter;
        public FoodController foodPrefab;

        public List<FoodController> leftList;
        public List<FoodController> rightList;

        public bool isBCI = false;
        public int scoreModifier = 1;

        public int score = 0;

        float leftValue = 0f;
        float rightValue = 0f;
        float deflationSpeed = 0.3f;
        float threshold = 70;
        float jumpAmount = 10;

        float spawnHeight = 7f;
        public float position = -4.5f;
        float dist = 2f;

        int respawnTime = 2 * 60;
        int prevSpawnTime = 0;
        int frame = 0;

        bool paused = false;

        public bool gameEnded = false;

        void Start()
        {
            leftMeter.setThreshold(threshold);
            rightMeter.setThreshold(threshold);
        }

        void Update()
        {
            if (!gameEnded)
            {
                if (!paused)
                {
                    if (frame - prevSpawnTime >= respawnTime)
                    {
                        prevSpawnTime = frame;
                        spawnFood();
                    }
                    frame++;

                }

                leftValue = Mathf.Round(updatedValue(leftValue) * 10f) / 10f;
                rightValue = Mathf.Round(updatedValue(rightValue) * 10f) / 10f;

                leftMeter.setValue(leftValue);
                rightMeter.setValue(rightValue);

                if ((leftList[0].transform.position.y <= 1 || rightList[0].transform.position.y <= 1))
                {
                    if (leftValue >= threshold)
                    {
                        scoreFood(leftList);
                    }
                    if (rightValue >= threshold)
                    {
                        scoreFood(rightList);
                    }
                }


                if (!isBCI)
                {
                    if (Input.GetKeyDown(KeyCode.LeftArrow))
                    {
                        left();
                    }
                    if (Input.GetKeyDown(KeyCode.RightArrow))
                    {
                        right();
                    }
                }

                if (leftList[0].tooLow || rightList[0].tooLow)
                {
                    Destroy(leftList[0].gameObject);
                    Destroy(rightList[0].gameObject);
                    leftList.RemoveAt(0);
                    rightList.RemoveAt(0);
                }

                if (leftList[0].transform.position.y <= -0.9f)
                {
                    pause();
                }
            }
        }

        public void scoreFood(List<FoodController> list)
        {
            unpause();
            leftValue = 0;
            rightValue = 0;

            score += 3 * scoreModifier;
            if (list[0].food.desired)
            {
                score += 2 * scoreModifier;
            }

            Destroy(list[0].gameObject);
            leftList.RemoveAt(0);
            rightList.RemoveAt(0);
        }

        public void left()
        {
            if (leftValue <= 100)
            {
                leftValue += jumpAmount;
            }
            if (leftValue > 100)
            {
                leftValue = 100;
            }
        }

        public void right()
        {
            if (rightValue <= 100)
            {
                rightValue += jumpAmount;
            }
            if (rightValue > 100)
            {
                rightValue = 100;
            }
        }

        float updatedValue(float value)
        {
            if (value - deflationSpeed >= 0f)
            {
                return value - deflationSpeed;
            }
            else return value;
        }

        public void spawnFood()
        {
            unpause();
            FoodController leftFood = Instantiate(foodPrefab, new Vector3(position - dist, spawnHeight, 0), Quaternion.identity);
            FoodController rightFood = Instantiate(foodPrefab, new Vector3(position + dist, spawnHeight, 0), Quaternion.identity);

            if (Randomizer.RandomBool())
            {
                leftFood.setFood(new Food(true, crowdController.useAttribute, crowdController.desired));
                rightFood.setFood(new Food(false, crowdController.useAttribute, crowdController.desired));
            }
            else
            {
                leftFood.setFood(new Food(false, crowdController.useAttribute, crowdController.desired));
                rightFood.setFood(new Food(true, crowdController.useAttribute, crowdController.desired));
            }

            leftList.Add(leftFood);
            rightList.Add(rightFood);
        }

        void pauseUnpause(List<FoodController> list, bool paused)
        {
            foreach (FoodController controller in list)
            {
                controller.paused = paused;
            }
        }

        public void pause()
        {
            paused = true;
            pauseUnpause(leftList, paused);
            pauseUnpause(rightList, paused);
        }

        public void unpause()
        {
            paused = false;
            pauseUnpause(leftList, paused);
            pauseUnpause(rightList, paused);
        }

        public void clearFood()
        {
            foreach (FoodController controller in leftList)
            {
                Destroy(controller.gameObject);
            }
            foreach (FoodController controller in rightList)
            {
                Destroy(controller.gameObject);
            }
            leftList = new List<FoodController>();
            rightList = new List<FoodController>();
        }
    }
}