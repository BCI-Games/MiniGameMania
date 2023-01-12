using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Submissions.FoodPunch
{
    public class FoodController : MonoBehaviour
    {
        public Sprite[] sprites;
        public SpriteRenderer spriteRenderer;
        public Food food;

        public bool tooLow = false;
        public bool paused = false;

        float fallSpeed = 0.05f;

        void Update()
        {
            Vector3 delta = new Vector3(0, fallSpeed, 0);
            if (!paused)
            {
                transform.position -= delta;
            }
            if (transform.position.y <= -1)
            {
                tooLow = true;
            }
            if (transform.position.y <= -1.1)
            {
                Destroy(gameObject);
            }
        }

        public void setFood(Food food_)
        {
            food = food_;
            spriteRenderer.sprite = sprites[food.type];
            spriteRenderer.color = Food.colors[food.attribute];
        }
    }
}