using UnityEngine;
using UnityEngine.UI;

public class SignalSPOSet : SignalSPOFactory
{
    public int maxCount = 8;
    public int minCount = 2;

    public Button increaseButton;
    public Button decreaseButton;

    public void IncreaseObjectCount()
    {
        GameObject spoInstance = Instantiate(prototype);
        spoInstance.transform.SetParent(transform, false);
        
        SignalSPO spo = spoInstance.GetComponentInChildren<SignalSPO>();
        spo.myIndex = count;
        spo.SetEvents(OnSelection.Invoke, OnTrainTarget.Invoke, OnStimulus.Invoke);

        childSPOs.Add(spo);
        UpdateCount();
    }

    public void DecreaseObjectCount()
    {
        Destroy(transform.GetChild(transform.childCount - 1).gameObject);
        childSPOs.RemoveAt(count - 1);

        UpdateCount();
    }

    void UpdateCount()
    {
        count = childSPOs.Count;
        increaseButton.interactable = count < maxCount;
        decreaseButton.interactable = count > minCount;
    }
}
