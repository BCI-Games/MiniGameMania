using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SignalSPOFactory : MonoBehaviour
{
    public GameObject prototype;
    public int count;

    public UnityEvent<SPO> OnSelection;
    public UnityEvent<SPO> OnTrainTarget;
    public UnityEvent<SPO> OnStimulus;

    protected List<SignalSPO> childSPOs;

    void Start()
    {
        childSPOs = new();

        for(int i = 0; i < count; i++)
        {
            GameObject spoInstance = prototype;
            
            if(i != 0)
            {
                spoInstance = Instantiate(prototype);
                spoInstance.transform.SetParent(transform, false);
            }

            SignalSPO spo = spoInstance.GetComponentInChildren<SignalSPO>();
            spo.myIndex = i;
            spo.SetEvents(OnSelection.Invoke, OnTrainTarget.Invoke, OnStimulus.Invoke);

            childSPOs.Add(spo);
        }
    }

    public void DisableChildrenTargetIndicators()
    {
        foreach (SignalSPO spo in childSPOs)
            spo.DisableTargetIndicator();
    }
}