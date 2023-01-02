using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Submissions.StarDefense
{
    public class GameMenuSelection : SPO
    {
        private SpriteRenderer displayImage;
        public SelectionType selectionType;
        public enum SelectionType
        {
            NONE,
            PLAY,
            REFRESH,
        }


        private void Start()
        {
            displayImage = GetComponent<SpriteRenderer>();
        }


        // Turn the stimulus on
        public override float TurnOn()
        {

            //This is just for an object renderer (e.g. 3D object). Use <SpriteRenderer> for 2D
            { this.GetComponent<Image>().color = onColour; }


            //Return time since stim
            return Time.time;
        }

        // Turn off/reset the SPO
        public override void TurnOff()
        {
            //This is just for an object renderer (e.g. 3D object). Use <SpriteRenderer> for 2D
            { this.GetComponent<Image>().color = offColour; }
        }

        // What to do on selection
        public override void OnSelection()
        {
            // This is free form, do whatever you want on selection

            StartCoroutine(QuickFlash());
            GameManager.Instance.ChangeScene(selectionType);

            // Reset
            TurnOff();
        }
    }
}