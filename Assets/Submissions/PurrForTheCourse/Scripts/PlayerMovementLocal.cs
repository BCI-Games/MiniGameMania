using Unity.Netcode;
using UnityEngine;
using System.Collections.Generic;
using Submissions.PurrForTheCourse;


namespace HelloWorld

    //Right now we randomly move the object every frame.
{
    public class PlayerMovementLocal : NetworkBehaviour
    {
        public NetworkVariable<Vector3> Position = new NetworkVariable<Vector3>(new Vector3(0f, 0f, 0f));
        private NetworkVariable<bool> isTurn = new NetworkVariable<bool>(false);
        public static int mode;
        public int uniqueID;
        public int turnCount = 0;
        public bool isInHole = false;
        public bool isOutOfBounds = false;
        public Vector3 lastGoodPosition;

        private bool wasBoolTrue = false;
        private Coroutine turnCo;
        [SerializeField] private PlayerController playerController;
        



        protected virtual void Awake()
        {
            //Add me when I awake - this is called on both the clients and the host, so everyone will know me
            //ActivePlayers.Add(this);
        }
        protected virtual void OnDestroy()
        {
            //Cleanup - this is important since static members will persist until application is quit
            //ActivePlayers.Remove(this);
        }

       

        public void Move()
        {
            
            //you are a client, so call a server function to change the synced position variable.
                
               

            SubmitPositionIncrementRequest();
            
        }

      

      
        void SubmitPositionIncrementRequest()
        {
            //Call a func that calculates what you wanna do to the position and on the next frame when update is called we'll move it there
            Position.Value += new Vector3(Random.Range(-20f, 20f), 0f, Random.Range(-20f, 20f));
        }

      
        void SubmitPositionRequest(Vector3 newPosition)
        {
            //Call a func that calculates what you wanna do to the position and on the next frame when update is called we'll move it there
            Position.Value = newPosition;
        }



        
        public void SubmitTurnRequest(bool p)
        {
            isTurn.Value = p;

            if (isTurn.Value)
            {
                lastGoodPosition = this.transform.position;
                turnCount += 1;
                wasBoolTrue = true;
                playerController.onHitEvent.AddListener(onHitEventCalled);
                playerController.onDoneTurnEvent.AddListener(onDoneEventCalled);
                turnCo = StartCoroutine(playerController.GameLoopEnum());
            }
            else
            {
                playerController.StopDaCoroutines();
                lastGoodPosition = this.transform.position;
            }
        }

        



        public bool getBoolTurn()
        {
            return isTurn.Value;

        }

        public bool getIsInHole()
        {
            return isInHole;
        }

       

       

        public void MoveAbsolute(Vector3 v)
        {
           
            SubmitPositionRequest(v);

            
        }


        static Vector3 GetRandomPositionOnPlane()
        {
            return new Vector3(Random.Range(-20f, 20f), 0f, Random.Range(-20f, 20f));
        }

        public Vector3 getPosition()
        {
            return Position.Value;
        }

        public int getTurnCount()
        {
            return turnCount;
        }

        public void onOutOfBoundsCalled()
        {
            transform.position = lastGoodPosition;
            Position.Value = lastGoodPosition;
            
            isOutOfBounds = false;
            SubmitTurnRequest(false);
            Debug.Log("Outofboundsboi");
       

        }



        public int getMode()
        {
            return mode;
        }

        private void onHitEventCalled()
        {
            // AHMEDDDDD
            Debug.Log("Hit got callllleeeeddd");
        }

        private void onDoneEventCalled()
        {
            playerController.StopAllCoroutines();
            GameManagerLocal.SetTimeRemainingZero();

            Debug.Log("turn is done baybeeeee");
        }

        void Update()
        {
            transform.position = Position.Value;
          
            //if (getBoolTurn() && !wasBoolTrue)
            //{
            //    wasBoolTrue = true;
            //    playerController.onHitEvent.AddListener(onHitEventCalled);
            //    playerController.onDoneTurnEvent.AddListener(onDoneEventCalled);
            //    turnCo = StartCoroutine(playerController.GameLoopEnum());
            //    //SubmitPositionIncrementRequest();
            //}

            //if (!getBoolTurn() && wasBoolTrue)
            //{
            //    playerController.StopDaCoroutines();

            //    wasBoolTrue = false;
            //}
        }
    }
}