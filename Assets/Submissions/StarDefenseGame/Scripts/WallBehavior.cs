using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Submissions.StarDefense
{
    public class WallBehavior : MonoBehaviour
    {
        public int maxHp = 50;
        public float currentHp;

        // Start is called before the first frame update
        void Start()
        {
            currentHp = maxHp;
            if (name.Equals("RightWall"))
            {
                CanvasReference.Instance.rightWallHealth.maxValue = maxHp;
                CanvasReference.Instance.rightWallHealth.value = maxHp;
            }
            else if (name.Equals("LeftWall"))
            {
                CanvasReference.Instance.leftWallHealth.maxValue = maxHp;
                CanvasReference.Instance.leftWallHealth.value = maxHp;
            }
        }

        public void TakeDamage(float damage)
        {
            currentHp -= damage;

            if (GameManager.Instance.gameEnded == false)
            {
                if (name.Equals("RightWall"))
                {
                    if (currentHp <= 0)
                    {
                        currentHp = 0;
                        GameManager.Instance.EndGame(true);
                        Debug.Log("You win");
                    }
                    CanvasReference.Instance.rightWallHealth.value = currentHp;
                }
                else if (name.Equals("LeftWall"))
                {
                    if (currentHp <= 0)
                    {
                        currentHp = 0;
                        GameManager.Instance.EndGame(false);
                        Debug.Log("You lose");
                    }
                    CanvasReference.Instance.leftWallHealth.value = currentHp;
                }
            }


        }
    }
}