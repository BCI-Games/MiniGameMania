using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Submissions.StarDefense
{
    public class SelectionData : MonoBehaviour
    {
        public static SelectionData Instance;
        public Selection.SelectionType[] randomSelections = new Selection.SelectionType[4];
        public Sprite[] randomSprites = new Sprite[4];

        public void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }

        }
    }
}