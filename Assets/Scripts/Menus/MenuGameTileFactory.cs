using BCIEssentials.Controllers;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;
using TMPro;

public class MenuGameTileFactory : MonoBehaviour
{
    public string categoryName;
    public GameObject prototype;
    public BehaviorType bciBehaviorType;
    public MenuGameTileAttributes[] tileInfo;

    [Header("game info references")]
    public GameObject gameInfoObject;
    public TextMeshProUGUI gameInfoTitle;
    public TextMeshProUGUI gameInfoBody;

    [Header("preview texture attributes")]
    public int previewTextureSizeMultiplier = 2;
    public int previewTextureDepth = 16;

    int previewWidth;
    int previewHeight;

    public void Start()
    {
        Vector2 gridCellSize = GetComponent<GridLayoutGroup>().cellSize;
        previewWidth = (int)(gridCellSize.x * previewTextureSizeMultiplier);
        previewHeight = (int)(gridCellSize.y * previewTextureSizeMultiplier);

        int[] tileOrder = GetTileOrder();

        for(int i = 0; i < tileInfo.Length; i++)
        {
            int tileIndex = tileOrder[i];
            MenuGameTileAttributes tileAttributes = tileInfo[tileIndex];

            GameObject tileObject = prototype;

            if (i != 0)
            {
                tileObject = Instantiate(prototype);
                tileObject.transform.SetParent(transform, false);
            }

            tileObject.name = tileAttributes.gameTitle;
            InitializeGameTile(tileObject, tileAttributes, tileIndex);
        }

        SaveTileOrder();
        gameInfoObject.SetActive(false);
    }

    void OnDisable()
    {
        gameInfoObject.SetActive(false);
    }

    int[] GetTileOrder()
    {
        int[] tileOrder = new int[tileInfo.Length];
        for(int i = 0; i < tileOrder.Length; i++)
            tileOrder[i] = i;

        if (PlayerPrefs.HasKey(categoryName))
        {
            string savedTileOrderString = PlayerPrefs.GetString(categoryName);
            string[] savedTileOrderStrings = savedTileOrderString.Split(',');

            int[] savedTileOrder = savedTileOrderStrings.Select(s => int.Parse(s)).ToArray();
            // basic checks for save validity
            if (savedTileOrder.Length == tileOrder.Length &&
                savedTileOrder.Length == savedTileOrder.Distinct().Count())
                tileOrder = savedTileOrder;
        }

        return tileOrder;
    }

    void SetBCIControllerBehavior() => BCIController.Instance.ChangeBehavior(bciBehaviorType);

    void ShowGameInfo(string title, string gameInfo)
    {
        gameInfoObject.SetActive(true);
        gameInfoTitle.text = "How to play " + title;
        gameInfoBody.text = gameInfo;
    }
    public void HideGameInfo() => gameInfoObject.SetActive(false);

    void InitializeGameTile(GameObject tileObject, MenuGameTileAttributes tileInfo, int tileIndex)
    {
        tileObject.name = tileInfo.gameTitle;

        MenuGameTile tile = tileObject.GetComponent<MenuGameTile>();
        tile.Init(tileInfo, previewWidth, previewHeight, previewTextureDepth,
            SetBCIControllerBehavior, ShowGameInfo);
        tile.tileIndex = tileIndex;
    }

    public void SaveTileOrder(ReorderableList.ReorderableListEventStruct eventArgs = default)
    {
        int[] tileOrder = new int[transform.childCount];

        for(int i = 0; i < tileOrder.Length; i++)
        {
            Transform tileTransform = transform.GetChild(i);
            MenuGameTile tile = tileTransform.GetComponent<MenuGameTile>();
            if(tile == null)
                tile = eventArgs.DroppedObject.GetComponent<MenuGameTile>();

            tileOrder[i] = tile.tileIndex;
        }

        PlayerPrefs.SetString(categoryName, string.Join(',', tileOrder));
    }

    public void OnTileGrabbed(ReorderableList.ReorderableListEventStruct eventArgs)
    {
        MenuGameTile grabbedTile = eventArgs.SourceObject.GetComponent<MenuGameTile>();
        grabbedTile?.OnGrab();
    }

    public void OnTileDropped(ReorderableList.ReorderableListEventStruct eventArgs)
    {
        MenuGameTile droppedTile = eventArgs.DroppedObject.GetComponent<MenuGameTile>();
        droppedTile?.OnDrop();
    }
}
