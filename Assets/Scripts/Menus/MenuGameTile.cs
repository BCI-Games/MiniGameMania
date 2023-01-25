using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class MenuGameTile : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TextMeshProUGUI titleText;
    public Button playButton;
    public RawImage previewImage;
    public VideoPlayer previewVideoPlayer;
    public GameObject hoverIcon;

    [HideInInspector]
    public int tileIndex;

    string targetScene;
    bool grabbed = false;

    Texture previewStill;
    RenderTexture previewVideoTexture;

    System.Action onLoadGame;

    public void Init(MenuGameTileAttributes attributes,
        int renderTextureWidth, int renderTextureHeight,int renderTextureDepth,
        System.Action loadAction)
    {
        titleText.text = attributes.gameTitle;

        targetScene = attributes.targetScene;
        playButton.onClick.AddListener(LoadGame);
        onLoadGame= loadAction;

        previewStill = attributes.previewStill;

        previewVideoTexture = new RenderTexture(renderTextureWidth, renderTextureHeight, renderTextureDepth);

        previewImage = GetComponentInChildren<RawImage>(true);
        previewImage.texture = previewStill;

        previewVideoPlayer = GetComponentInChildren<VideoPlayer>(true);
        previewVideoPlayer.targetTexture = previewVideoTexture;
        previewVideoPlayer.clip = attributes.previewClip;

        hoverIcon.SetActive(false);
    }

    void LoadGame()
    {
        onLoadGame();
        SceneManager.LoadScene(targetScene);
    }

    private void OnDisable()
    {
        ResetPreview();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        StartPreview();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ResetPreview();
    }

    void StartPreview()
    {
        if (grabbed)
            return;

        previewImage.texture = previewVideoTexture;
        previewVideoPlayer.Play();

        hoverIcon.SetActive(true);
    }

    void ResetPreview()
    {
        if (grabbed)
            return;

        previewImage.texture = previewStill;
        previewVideoPlayer.Stop();

        hoverIcon.SetActive(false);
    }

    public void OnGrab()
    {
        grabbed = true;
    }

    public void OnDrop()
    {
        grabbed = false;
    }
}

[System.Serializable]
public struct MenuGameTileAttributes
{
    public string gameTitle;
    public string targetScene;

    public Texture previewStill;
    public VideoClip previewClip;
}
