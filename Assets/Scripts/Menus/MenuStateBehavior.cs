using UnityEngine;
using UnityEngine.UI;

public class MenuStateBehavior : MonoBehaviour
{
    public GameObject currentMenu;
    public RectTransform transitionShape;
    public GameObject transitionRaycastBlocker;

    public float transitionPeriod;
    public AnimationCurve transitionEasing;
    public Vector2 screenSize = new Vector2(450, 800);
    public Vector2 transitionBlockSize = new Vector2(600, 950);
    public float menuTravelDistance = 200;

    SlideTransitionInstance previousMenuTransition;
    SlideTransitionInstance currentMenuTransition;
    SlideTransitionInstance shapeTransition;

    bool transitionActive;
    float transitionTimer;
    float transitionShapeDiagonal;

    void Start()
    {
        foreach (Transform child in transform)
            child.gameObject.SetActive(false);
        currentMenu.SetActive(true);

        transitionRaycastBlocker.SetActive(false);

        transitionShapeDiagonal = transitionShape.sizeDelta.x * 1.42f;
        transitionShape.gameObject.SetActive(false);
    }

    void Update()
    {
        if(transitionActive)
        {
            if (transitionTimer < transitionPeriod)
            {
                transitionTimer += Time.deltaTime;
                if (transitionTimer > transitionPeriod)
                    transitionTimer = transitionPeriod;

                float lerpValue = transitionEasing.Evaluate(transitionTimer / transitionPeriod);
                previousMenuTransition.SetPosition(lerpValue);
                currentMenuTransition.SetPosition(lerpValue);
                shapeTransition.SetPosition(lerpValue);

                if (lerpValue >= 0.3f)
                {
                    previousMenuTransition.ResetTarget();
                    currentMenu.SetActive(true);
                }
            }
            else
            {
                transitionActive = false;
                transitionRaycastBlocker.SetActive(false);

                previousMenuTransition.ResetTarget();
                shapeTransition.ResetTarget();
            }
        }
    }

    public void SwitchTo(MenuTransitionSettings transitionSettings)
    {
        //transitionActive = true;
        //transitionRaycastBlocker.SetActive(true);
        //transitionTimer = 0;

        //Vector2 transitionDistance = transitionBlockSize + screenSize;
        //transitionDistance.x *= transitionSettings.direction.x;
        //transitionDistance.y *= transitionSettings.direction.y;

        //previousMenuTransition = new(currentMenu, Vector2.zero, transitionDistance);
        //currentMenu = transitionSettings.targetMenu;
        //currentMenuTransition = new(currentMenu, -transitionDistance, transitionDistance);

        transitionActive = true;
        transitionRaycastBlocker.SetActive(true);
        transitionTimer = 0;

        Vector2 menuDistance = Vector2.left * menuTravelDistance;

        previousMenuTransition = new(currentMenu, Vector2.zero, menuDistance);
        currentMenu = transitionSettings.targetMenu;
        currentMenuTransition = new(currentMenu, -menuDistance, menuDistance);
        currentMenu.SetActive(false);

        Vector2 shapeDistance = Vector2.left * (transitionShapeDiagonal + screenSize.x / 2);
        shapeTransition = new(transitionShape.gameObject, -shapeDistance / 2, shapeDistance);
    }

    class SlideTransitionInstance
    {
        public RectTransform target;
        public Vector2 startPosition;
        public Vector2 endPosition;

        public SlideTransitionInstance(GameObject targetObject, Vector2 startPos, Vector2 distance)
        {
            targetObject.SetActive(true);
            target = targetObject.GetComponent<RectTransform>();
            if(!target)
                target = targetObject.AddComponent<RectTransform>();

            target.anchoredPosition= startPos;
            startPosition = startPos;
            endPosition = startPos + distance;
        }

        public void SetPosition(float lerpValue)
        {
            target.anchoredPosition = Vector2.LerpUnclamped(startPosition, endPosition, lerpValue);
        }

        public void ResetTarget()
        {
            target.gameObject.SetActive(false);
            target.anchoredPosition = Vector2.zero;
        }
    }
}
