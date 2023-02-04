using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Submissions.GardensOTG
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance { get; private set; }

        private List<GameObject> activeCanvasGroup = new List<GameObject>();

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
                return;
            }
            Instance = this;
        }
        public void DisplayNewCanvas(Canvas canvas)
        {
            foreach (GameObject go in activeCanvasGroup)
            {
                go.SetActive(false);
            }
            canvas.gameObject.SetActive(true);
            activeCanvasGroup.Add(canvas.gameObject);
        }
        public void OverlayCanvas(Canvas canvas)
        {
            canvas.gameObject.SetActive(true);
            activeCanvasGroup.Add(canvas.gameObject);
        }
        public void HideCanvas(Canvas canvas)
        {
            canvas.gameObject.SetActive(false);
        }
    }
}