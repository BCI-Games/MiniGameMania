using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Threading.Tasks;
using HelloWorld;
using Submissions.PurrForTheCourse;

public class HitEvent : UnityEvent<float> { }

public class BallContoller : MonoBehaviour
{
    [SerializeField] private float maxImpulseSpeed = 60.0f;
    [SerializeField] private float minImpulseSpeed = 1.0f;
    private Rigidbody rb;
    [SerializeField] private float vEpsilon = 0.1f;
    [SerializeField] private float timeToQuicklySlowDown = 0.2f;

    [SerializeField] private PlayerMovementLocal playerMovementLocal;

    [SerializeField] private ArrowController arrowController;
    [SerializeField] private float timeToRest = 4.0f;

    private bool isCoRunning = false;
    private Coroutine co;
    public HitEvent onPuttBallEvent = new HitEvent();

    private void Start()
    {
        timeToRest = 1.0f;
        rb = GetComponent<Rigidbody>();
    }

    public bool getIsCoRunning() { return isCoRunning; }

    public void startShoot()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        isCoRunning = true;
        
        co = StartCoroutine(ShootBallForward());
    }


    public IEnumerator ShootBallForward()
    {
        Vector3 direction = arrowController.getWorldSpaceDirection();
        float percentage = arrowController.getForcePercentage();
        float impulseSpeed = (maxImpulseSpeed - minImpulseSpeed) * percentage + minImpulseSpeed;

        onPuttBallEvent.Invoke(percentage);

        Debug.Log("Impulse speed: " + impulseSpeed);
        Debug.Log("impulse Percentage: " + percentage);
        Debug.Log("max Impulse speed: " + maxImpulseSpeed);
        Debug.Log("min Impulse speed: " + minImpulseSpeed);

        rb.AddForce(direction * impulseSpeed, ForceMode.Impulse);



        float t = 0.0f;
        bool checkAgain = true;

        do
        {
            

            do
            {
                yield return null;
            } while (checkAgain && rb.velocity.magnitude < vEpsilon);



            do
            {
                yield return null;
            } while (rb.velocity.magnitude > vEpsilon);

            

            Debug.Log("before do while");

            t = 0.0f;
            do
            {
                Debug.Log("in do while");

                t += Time.deltaTime;
                yield return null;
                
                if (rb.velocity.magnitude > vEpsilon)
                {
                    t = 0.0f;
                    Debug.Log("do while, magnitude: " + rb.velocity.magnitude);
                    checkAgain = false;
                    break;
                }
                    
                if (t > timeToRest)
                {
                    Debug.Log("do while, time: " + t);
                    break;
                }

                Debug.Log("right at end of do while");

            } while (rb.velocity.magnitude <= vEpsilon);

            Debug.Log("after do while");

        } while (t < timeToRest);


        // slow down quickly
        
        Reset();
        isCoRunning = false;
    }

   

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "SpeedBoost")
        {
            Debug.Log("Its a speeeed!");
            
            speedUp(other.transform.right, 4);
        }

        if (other.gameObject.tag == "Rough")
        {
            Debug.Log("Its a rough!");
           
            slowDown(70);
        }

        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Win")
        {
            playerMovementLocal.isInHole = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "GameBounds")
        {
            Debug.Log("hell");
            playerMovementLocal.onOutOfBoundsCalled();
        }
    }

    public void speedUp(Vector3 direction,int speed)
    {
        rb.AddForce(direction*speed,ForceMode.Impulse);
    }

    public void slowDown(int speed)
    {
        Vector3 direction = Vector3.Normalize(this.transform.forward) * -1;
        //Vector3 test = direction * speed + rb.velocity;
        //if(test.x * rb.velocity.x <= 0 )
        //{//different sign meaning we'd be changing the x direction if we use this change
        //    test.x = 0;
        //}
        //if (test.y * rb.velocity.y <= 0)
        //{//different sign meaning we'd be changing the x direction if we use this change
        //    test.y = 0;
        //}
        //if (test.z * rb.velocity.z <= 0)
        //{//different sign meaning we'd be changing the x direction if we use this change
        //    test.z = 0;
        //}
        
        //rb.velocity = test;
        rb.AddForce(direction*speed, ForceMode.Acceleration);
    }

    public void Reset(bool resetPosition = false)
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        if (resetPosition)
        {
            transform.localPosition = Vector3.zero;
        }
    }
}
