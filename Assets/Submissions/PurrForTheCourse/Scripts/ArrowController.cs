using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.Events;

public class RotationEvent : UnityEvent<float> { }

public class ArrowController : MonoBehaviour
{
    [SerializeField] private float timeToRotateCompletely = 3f;
    [SerializeField] private float maxDegreeOffset = 30f;
    [SerializeField] private Transform transformToRotateAround;

    private Coroutine rotateCo;
    private bool isSpeedCoRunning;

    private Coroutine velocityCo;
    private bool isVelocityCoRunning;

    private bool canRotate = true;

    private RotationEvent StartRotationEvent = new RotationEvent();
    private float angleMultiplier = -1f;

    [SerializeField] private float maxScale = 1.0f;
    [SerializeField] private float minScale = 0.3f;
    [SerializeField] private float timeToScaleJawn = 2.0f;

    private float chosenScale = 0.0f;
    private Vector3 initialScale;

    private Quaternion initialRotation;
    [SerializeField]private Renderer arrowRenderer;

    [HideInInspector] public Quaternion centerRotation;

    private float offsetInFront = 1f;
    public float getOffset() { return offsetInFront; }


    // Start is called before the first frame update
    void Awake()
    {
        centerRotation = transform.rotation;
        arrowRenderer.enabled = false;
        initialScale = new Vector3(1.0f, 1.0f, minScale);
        transform.localScale = initialScale;

        // TODO: subscribe to event to start rotating
        StartRotationEvent.AddListener(StartRotation);
        //startRotation.Invoke(timeToRotateCompletely/2f);
    }

    public Vector3 getWorldSpaceDirection()
    {
        return transform.TransformDirection(Vector3.forward);
    }

    public void SetPositionRelativeToBall()
    {
        transform.position = new Vector3(transformToRotateAround.position.x, transform.position.y,
            transform.position.z + offsetInFront);
        transform.rotation = initialRotation;
        
    }

    public void stopRotation()
    {
        canRotate = false;
        StopAllCoroutines();
    }

    public void StartRotation()  
    {
        canRotate = true;
        SetPositionRelativeToBall();
        arrowRenderer.enabled = true;
        StartRotation(timeToRotateCompletely/2f);
    }

    private void StartRotation(float secondsToRotate)
    {
        if (!canRotate)
            return;

        angleMultiplier *= -1f;

        Quaternion currentRotation = centerRotation;


        Quaternion targetRotation = currentRotation * Quaternion.Euler(0, angleMultiplier * maxDegreeOffset, 0);
        //Debug.Log("Target Rotation: " + targetRotation.eulerAngles);
        //Debug.Log("Current Rotation: " + currentRotation.eulerAngles);


        rotateCo = StartCoroutine(RotateUntil(targetRotation, angleMultiplier * Quaternion.Angle(targetRotation, currentRotation) / secondsToRotate, StartRotationEvent));
    }

    private IEnumerator RotateUntil(Quaternion targetRotation, float degreesPerSecond, RotationEvent toInvoke)
    {
        isSpeedCoRunning = true;

        do
        {
            float degreesPerFrame = degreesPerSecond * Time.deltaTime;

            transform.RotateAround(transformToRotateAround.position, Vector3.up, degreesPerFrame);
            yield return null;
        } while (Quaternion.Angle(targetRotation, transform.rotation) > 0.5f);
        

        isSpeedCoRunning = false;
        toInvoke.Invoke(timeToRotateCompletely);
    }

    public void StartChooseVelocity()
    {
        velocityCo = StartCoroutine(ChooseSpeed());
    }

    public void StopChooseVelocity()
    {
        
        arrowRenderer.enabled = false;
        initialScale = new Vector3(1.0f, 1.0f, minScale);
        transform.localScale = initialScale;
        StopAllCoroutines();
    }

    public float getForcePercentage()
    {
        Debug.Log("local scale: " + transform.localScale);
        Debug.Log("min scale: " + minScale);
        Debug.Log("max scale: " + maxScale);

        return (chosenScale - minScale) / (maxScale - minScale);
    }

    private IEnumerator ChooseSpeed()
    {

        Vector3 newScale = transform.localScale;

        while (true)
        {
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / timeToScaleJawn)
            {
                chosenScale = Mathf.Lerp(minScale, maxScale, t);
                newScale.z = chosenScale;
                transform.localScale = newScale;
                yield return null;
            }

            for (float t = 1.0f; t > 0.0f; t -= Time.deltaTime / timeToScaleJawn)
            {
                chosenScale = Mathf.Lerp(minScale, maxScale, t);
                newScale.z = chosenScale;
                transform.localScale = newScale;
                yield return null;
            }
        }
    }
}
