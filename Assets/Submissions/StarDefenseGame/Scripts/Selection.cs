using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Submissions.StarDefense
{

    public class Selection : SPO
    {
        private SpriteRenderer displayImage;
        public SelectionType selectionType;
        public enum SelectionType
        {
            NONE,
            ROCKET,
            ELECTRIC,
            BOMB,
            SHIELD,
            BULLET,
        }


        private void Start()
        {
            displayImage = GetComponent<SpriteRenderer>();
        }


        // Turn the stimulus on
        public override float TurnOn()
        {

            //This is just for an object renderer (e.g. 3D object). Use <SpriteRenderer> for 2D
            { this.GetComponent<SpriteRenderer>().color = onColour; }


            //Return time since stim
            return Time.time;
        }

        // Turn off/reset the SPO
        public override void TurnOff()
        {
            //This is just for an object renderer (e.g. 3D object). Use <SpriteRenderer> for 2D
            { this.GetComponent<SpriteRenderer>().color = offColour; }
        }

        // What to do on selection
        public override void OnSelection()
        {
            // This is free form, do whatever you want on selection

            StartCoroutine(QuickFlash());
            GameManager.Instance.ChoiceInput(displayImage.sprite, selectionType);

            // Reset
            TurnOff();
        }
    }
}