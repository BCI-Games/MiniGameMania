using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelector : SPOSelectionManager<GameObject>
{
    public void SetUnitColour(Color colour)
    {
        foreach (CallbackSPO<GameObject> spo in selectableChildren)
        {
            (spo as UnitCardSPO).SetUnitColour(colour);
        }
    }
}
