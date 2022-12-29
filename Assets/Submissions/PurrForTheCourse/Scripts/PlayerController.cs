using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Threading.Tasks;
using Cinemachine;

enum State
{
    Aiming
}

namespace Submissions.PurrForTheCourse
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private ArrowController arrowController;
        [SerializeField] private BallContoller ballController;
        [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
        [SerializeField] private Transform transformToLookAt;


        private Rigidbody rb;

        public UnityEvent StopArrow = new UnityEvent();

        public UnityEvent FireBallNow = new UnityEvent();

        private Task ArrowTask;

        private bool FireCalled = false;
        private bool isMyTurn = true;

        private Transform FaceTowards;

        private bool isIdle = false;

        private bool isBallDoneMoving;

        public UnityEvent onHitEvent = new UnityEvent();
        public UnityEvent onDoneTurnEvent = new UnityEvent();

        private bool isTurnRunning = false;
        private bool isHit = false;




        // Start is called before the first frame update
        void Start()
        {

            rb = GetComponent<Rigidbody>();
            //StartCoroutine(GameLoopEnum());
        }

        public void SetupAiming()
        {
            // start aiming
            transform.LookAt(FaceTowards);
            arrowController.StartRotation();
        }

        private void RotateCameraToFacePosition(Vector3 position)
        {
            Vector3 diffPosition = (ballController.transform.position - position).normalized;
            diffPosition.y = 0.0f;

            Vector3 currentOffset = cinemachineVirtualCamera.transform.position - ballController.transform.position;
            currentOffset.y = 0f;
            float magnitude = currentOffset.magnitude;

            var transposer = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>();
            var newOffset = diffPosition * magnitude;
            newOffset.y = transposer.m_FollowOffset.y;
            transposer.m_FollowOffset = newOffset;

        }

        private void RotateArrowToFacePosition(Vector3 position)
        {
            float offset = arrowController.getOffset();

            Vector3 diffPosition = (position - ballController.transform.position).normalized;
            diffPosition.y = 0.0f;

            diffPosition *= offset;

            arrowController.transform.position = ballController.transform.position + diffPosition;
            arrowController.transform.rotation = Quaternion.LookRotation(diffPosition);
            arrowController.centerRotation = arrowController.transform.rotation;
        }

        private void SetBallDoneMovingTrue()
        {
            isBallDoneMoving = true;
        }

        public bool getIsTurnRunning()
        {
            return isTurnRunning;
        }

        public bool getIsHit()
        {
            return isHit;
        }

        public void StopDaCoroutines()
        {
            arrowController.stopRotation();
            isIdle = false;
            isTurnRunning = false;
            FireCalled = false;
            isHit = false;
            cinemachineVirtualCamera.Priority = 0;
            ballController.StopAllCoroutines();

            StopAllCoroutines();
        }

        public IEnumerator GameLoopEnum()
        {
            GameManagerLocal game = FindObjectOfType<GameManagerLocal>();
            RotateCameraToFacePosition(game.getTransformToLookAtAfterTurnEnds().position);

            cinemachineVirtualCamera.Priority = 1;

            isTurnRunning = true;
            // wait until fire is called
            isIdle = true;
            
            SetupAiming();

            isIdle = true;

            // wait for rotation to be done
            while (!FireCalled)
                yield return null;

            // stop the rotation
            arrowController.stopRotation();
            FireCalled = false;

            arrowController.StartChooseVelocity();

            // wait for aiming to be done
            while (!FireCalled) 
                yield return null;

            arrowController.StopChooseVelocity();
            isIdle = false;
            FireCalled = false;

            isHit = true;
            ballController.startShoot();

            onHitEvent.Invoke();

            do
            {
                yield return new WaitForEndOfFrame();
            } while (!ballController.getIsCoRunning());

            do
            {
                yield return new WaitForEndOfFrame();
            } while (ballController.getIsCoRunning());

            isHit = false;
            cinemachineVirtualCamera.Priority = 0;


            RotateCameraToFacePosition(game.getTransformToLookAtAfterTurnEnds().position);
            RotateArrowToFacePosition(game.getTransformToLookAtAfterTurnEnds().position);

            onDoneTurnEvent.Invoke();
            isTurnRunning = false;


        }

        void Update()
        {
            if (Input.GetKeyDown("space"))
            {
                print("space key was pressed");
                ShootBallTowardsArrow();
            }
        }

        public void ShootBallTowardsArrow()
        {
            Debug.Log("hehe");
            if (isIdle)
                FireCalled = true;
        }

        public void ResetBall(bool resetPosition = false)
        {
            ballController.Reset(resetPosition);
        }
    }
}
