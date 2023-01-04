using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shapes;

namespace Submissions.StarSkirmish
{
    public class StarSkirmishSPO : SPO
    {

        //EKL Just making this work, very simple implementation.
        //Each item this is on is a button, so we are going to just call the button action.
        [SerializeField] 
        private PlayerShip myShip;
        //[SerializeField] 
        //private PlayerShip enemyShip;
        [SerializeField] 
        private Rectangle rectButtonBG;
        [SerializeField] 
        private float delayVal;


        // Start is called before the first frame update
        public override float TurnOn()
        {
            // Make the BG the color we want.
            rectButtonBG.Color = onColour;

            // Don't touch this
            // Return time since stim
            return Time.time;

        }

        public override void TurnOff()
        {
            // Make the BG the color move back to the default.
            rectButtonBG.Color = offColour;


        }

        public override void OnSelection()
        {
            //Just make sure the button goes back to its default color.
            rectButtonBG.Color = offColour;
            Debug.Log("Did this work?");

            switch(myIndex){
                //Shileds Case
                case 0:     
                    myShip.SetShieldsOnline(true); //This could be let-up early, but we aren't going to do that right now...for future work...what happens if it always thinks its on.
                    Debug.Log("Made it to case O");
                    break;

                //Reload Case
                case 1:
                    //Just doing a full reload. Can make this flow better later. EKL FIX- Might actually break it if it always thinks it is on...
                    //myShip.SetReloading(true); //This could be let-up early, but we aren't going to do that right now...for future work...what happens if it always thinks its on.
                    Debug.Log("Made it to case 1");
                    break;

                //Turret Target Case
                case 2:
                    Debug.Log("Made it to case 2");
                    myShip.ShootCannonAtBow();
                    StartCoroutine("StopCannonFire");
                    break;

                //Engine Target Case
                case 3:
                    Debug.Log("Made it to case 3");
                    myShip.ShootCannonAtStern();
                    StartCoroutine("StopCannonFire");
                    break;
            }
        }

        //private void Update()
        //{
        //    if (Input.GetKeyDown(KeyCode.G))
        //    {
        //        OnSelection();
        //    }
        //}

        private void FixedUpdate()
        {
            
            //Bad practice - but turn off reloading once it is full. Should be in the player ship script, not here...but you know. Time and everything.
            if(myShip.reloadProgress==1.000 && myShip.reloading)
            {
                myShip.SetReloading(false);
            }
            //Debug.Log("Reloading progress:" + myShip.reloadProgress);
            //Debug.Log("Reloading possible? " + myShip.reloading);

            if(myShip.shieldFuel<0.01 && myShip.shieldsOnline)
            {
                myShip.SetShieldsOnline(false);
            }
        }

        private IEnumerator StopCannonFire()
        {
            yield return new WaitForSecondsRealtime(delayVal);
            if (myShip.cannonFired)
            { 
                myShip.StopShootingCannon(); 
            }
            else
            {
                Debug.Log("Cannon hasn't shot yet!");
            }
        }

    } //End Class

} //End Namespace