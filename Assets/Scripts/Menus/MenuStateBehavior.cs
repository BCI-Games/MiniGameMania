using UnityEngine;
using UnityEngine.UI;

public class MenuStateBehavior : MonoBehaviour
{
    public GameObject currentMenu;

    public float transitionPeriod;
    public AnimationCurve transitionEasing;
    public Vector2 screenSize = new Vector2(450, 800);
    public Vector2 transitionBlockSize = new Vector2(600, 950);
    public Color transitionBlockColour = Color.white;

    SlideTransitionInstance previousMenuTransition;
    SlideTransitionInstance currentMenuTransition;
    SlideTransitionInstance colourBlockTransition;

    GameObject straightTransitionColourBlock;
    GameObject diagonalTransitionColourBlock;

    bool transitionActive;
    float transitionTimer;

    void Start()
    {
        foreach (Transform child in transform)
            child.gameObject.SetActive(false);
        currentMenu.SetActive(true);

        BuildTransitionColourBlocks();
    }

    // Update is called once per frame
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
                colourBlockTransition.SetPosition(lerpValue);
            }
            else
            {
                transitionActive = false;

                previousMenuTransition.ResetTarget();
                colourBlockTransition.ResetTarget();
            }
        }
    }

    public void SwitchTo(MenuTransitionSettings transitionSettings)
    {
        //currentMenu.SetActive(false);
        //previousMenu = currentMenu;
        //currentMenu = transitionSettings.targetMenu;
        //currentMenu.SetActive(true);

        transitionActive = true;
        transitionTimer = 0;

        Vector2 transitionDistance = transitionBlockSize + screenSize;
        transitionDistance.x *= transitionSettings.direction.x;
        transitionDistance.y *= transitionSettings.direction.y;

        previousMenuTransition = new(currentMenu, Vector2.zero, transitionDistance);
        currentMenu = transitionSettings.targetMenu;
        currentMenuTransition = new(currentMenu, -transitionDistance, transitionDistance);


        GameObject relevantBlock = diagonalTransitionColourBlock;
        if (transitionSettings.slant == MenuTransitionSettings.TransitionSlant.Straight)
            relevantBlock = straightTransitionColourBlock;

        relevantBlock.SetActive(true);
        colourBlockTransition = new(relevantBlock, -transitionDistance / 2, transitionDistance);
    }

    void BuildTransitionColourBlocks()
    {
        straightTransitionColourBlock = BlockCreationHelper("Straight Transition Colour Block", transform);
        AddColourBlockImage(straightTransitionColourBlock);

        diagonalTransitionColourBlock = BlockCreationHelper("Diagonal Transition Colour Block", transform);
        GameObject hBlock = BlockCreationHelper("horizontal block", diagonalTransitionColourBlock, new Vector3(3, 1, 1));
        AddColourBlockImage(hBlock);

        GameObject vBlock = BlockCreationHelper("vertical block", diagonalTransitionColourBlock, new Vector3(1, 3, 1));
        AddColourBlockImage(vBlock);

        straightTransitionColourBlock.SetActive(false);
        diagonalTransitionColourBlock.SetActive(false);
    }

    GameObject BlockCreationHelper(string objectName, Transform parent) =>
        BlockCreationHelper(objectName, parent.gameObject, Vector3.one);
    GameObject BlockCreationHelper(string objectName, GameObject parent, Vector3 scale)
    {
        GameObject result = new(objectName);
        result.transform.SetParent(parent.transform, false);
        result.transform.localScale = scale;
        return result;
    }

    void AddColourBlockImage(GameObject hostObject)
    {
        Image blockImage = hostObject.AddComponent<Image>();
        blockImage.rectTransform.sizeDelta = transitionBlockSize;
        blockImage.color = transitionBlockColour;
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
