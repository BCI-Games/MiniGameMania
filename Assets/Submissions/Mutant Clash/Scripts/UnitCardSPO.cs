using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitCardSPO : CallbackSPO<GameObject>
{
    [Header("unit card settings")]
    public float selectScale = 2;

    [Header("sprites")]
    public UnitCardStatBar speedBar;
    public UnitCardStatBar healthBar;
    public UnitCardStatBar costBar;

    [Header("references")]
    public GameObject unitPrefab;
    public SpriteRenderer characterSpriteRenderer;

    public Image baseImage;
    public CanvasGroup canvasGroup;

    void Start()
    {
        //canvasGroup = GetComponentInChildren<CanvasGroup>(true);
        //baseImage = GetComponent<Image>();

        UnitBehavior targetUnit = unitPrefab.GetComponent<UnitBehavior>();

        characterSpriteRenderer.sprite = targetUnit.idleSprite;

        UnitStats displayStats = targetUnit.stats;

        speedBar.SetValue(displayStats.speed);
        healthBar.SetValue(displayStats.health);
        costBar.SetValue(displayStats.cost);

    }

    public void SetUnitColour(Color colour)
    {
        characterSpriteRenderer.color = colour;
    }

    public override float TurnOn()
    {
        transform.localScale = Vector3.one * selectScale;
        baseImage.color = onColour;
        canvasGroup.alpha = 1;

        return Time.deltaTime;
    }

    public override void TurnOff()
    {
        transform.localScale = Vector3.one;
        baseImage.color = offColour;
        canvasGroup.alpha = offColour.a;
    }

    public override void OnSelection()
    {
        // ready unit for spawn, waiting on location
        callback(unitPrefab);
    }
}

[System.Serializable]
public struct UnitStats
{
    public int speed;
    public int health;
    public int cost;
}
