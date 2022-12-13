using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPOSelectionManager<T> : MonoBehaviour
{
    public System.Action<T> onComplete;

    protected CallbackSPO<T>[] selectableChildren;

    public void Init()
    {
        selectableChildren = GetComponentsInChildren<CallbackSPO<T>>();
        foreach (CallbackSPO<T> spo in selectableChildren)
            spo.callback = OnSelection;

        SetActive(false);
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
        foreach (CallbackSPO<T> spo in selectableChildren)
        {
            spo.includeMe = active;
            if (active)
                spo.Display();
        }
    }

    public void TurnOffAll()
    {
        foreach (CallbackSPO<T> spo in selectableChildren)
            spo.TurnOff();
    }

    protected virtual void OnSelection(T arg)
    {
        SetActive(false);
        onComplete(arg);
    }

    public void SelectRandom()
    {
        int randomIndex = Random.Range(0, selectableChildren.Length);
        selectableChildren[randomIndex].OnSelection();
    }
}
