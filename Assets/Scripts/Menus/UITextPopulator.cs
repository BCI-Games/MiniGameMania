using UnityEngine;
using TMPro;

public class UITextPopulator : MonoBehaviour
{
    public string[] memberStrings;
    public GameObject prototype;
    public bool usePrototypeInstance = true;

    void Start()
    {
        PopulateTextList();
    }

    void PopulateTextList()
    {
        int memberIndex = 0;
        if (usePrototypeInstance)
        {
            prototype.GetComponent<TextMeshProUGUI>().text = memberStrings[memberIndex];
            memberIndex++;
        }

        while(memberIndex < memberStrings.Length)
        {
            GameObject prototypeClone = Instantiate(prototype);
            SetProtoypeInstanceValues(prototypeClone, memberStrings[memberIndex]);

            memberIndex++;
        }
    }

    void SetProtoypeInstanceValues(GameObject prototypeInstance, string memberString)
    {
        prototypeInstance.transform.SetParent(transform, false);
        prototypeInstance.name = memberString;
        prototypeInstance.GetComponent<TextMeshProUGUI>().text = memberString;
    }
}
