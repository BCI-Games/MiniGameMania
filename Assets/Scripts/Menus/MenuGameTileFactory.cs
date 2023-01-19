using BCIEssentials.Controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MenuGameTileFactory : MonoBehaviour
{
    public GameObject prototype;
    public BehaviorType bciBehaviorType;
    public MenuGameTileAttributes[] tileInfo;

    public int previewTextureSizeMultiplier = 2;
    public int previewTextureDepth = 16;

    int previewWidth;
    int previewHeight;

    public void Start()
    {
        Vector2 gridCellSize = GetComponent<GridLayoutGroup>().cellSize;
        previewWidth = (int)(gridCellSize.x * previewTextureSizeMultiplier);
        previewHeight = (int)(gridCellSize.y * previewTextureSizeMultiplier);

        InitializeGameTile(prototype, tileInfo[0]);
        prototype.name = tileInfo[0].gameTitle;

        for(int i = 1; i < tileInfo.Length; i++)
        {
            GameObject prototypeClone = Instantiate(prototype);
            prototypeClone.transform.SetParent(transform, false);
            prototypeClone.name = tileInfo[i].gameTitle;
            InitializeGameTile(prototypeClone, tileInfo[i]);
        }
    }

    void SetBCIControllerBehavior() => BCIController.Instance.ChangeBehavior(bciBehaviorType);

    void InitializeGameTile(GameObject tileObject, MenuGameTileAttributes tileInfo)
    {
        MenuGameTile tile = tileObject.GetComponent<MenuGameTile>();
        tile.Init(tileInfo, previewWidth, previewHeight, previewTextureDepth, SetBCIControllerBehavior);
    }
}
