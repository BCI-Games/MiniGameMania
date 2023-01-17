using BCIEssentials.Controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MenuGameTileFactory : MonoBehaviour
{
    public GameObject prototype;
    public BehaviorType bciBehaviorType;
    public MenuGameTileAttributes[] tileInfo;

    public void Start()
    {
        prototype.GetComponent<MenuGameTile>().Init(tileInfo[0], SetBCIControllerBehavior);
        prototype.name = tileInfo[0].gameTitle;

        for(int i = 1; i < tileInfo.Length; i++)
        {
            GameObject prototypeClone = Instantiate(prototype);
            prototypeClone.transform.SetParent(transform, false);
            prototypeClone.name = tileInfo[i].gameTitle;
            prototypeClone.GetComponent<MenuGameTile>().Init(tileInfo[i], SetBCIControllerBehavior);
        }
    }

    void SetBCIControllerBehavior() => BCIController.Instance.ChangeBehavior(bciBehaviorType);
}
