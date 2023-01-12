using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Submissions.FoodPunch
{
    public class Food
    {
        public int type; //meat, veg, candy
        public int attribute; //spicy, cold, sweet
        public bool desired;
        Color color;

        public static string[] types = new string[] { "meat", "veggies", "sweet" };
        public static string[] attributes = new string[] { "spicy", "cold", "icky" };
        public static Color[] colors = new Color[] { new Color(180, 0, 0), new Color(0, 110, 255), new Color(200, 200, 0) };

        public Food(bool desired_, bool useAttribute, int desiredNumber_)
        {
            desired = desired_;
            type = Random.Range(0, 3);
            attribute = Random.Range(0, 3);
            if (desired)
            {
                if (useAttribute)
                {
                    attribute = desiredNumber_;
                    type = Random.Range(0, 3);
                }
                else
                {
                    type = desiredNumber_;
                    attribute = Random.Range(0, 3);
                }
            }
            else
            {
                if (useAttribute)
                {
                    attribute = Randomizer.RangeNot(0, 3, desiredNumber_);
                }
                else
                {
                    type = Randomizer.RangeNot(0, 3, desiredNumber_);
                }
            }
        }
    }
}