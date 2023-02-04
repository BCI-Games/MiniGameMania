using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using Random = UnityEngine.Random;

public partial class MainGameController : MonoBehaviour
{
    [SerializeField] private MinigameData[] minigames;
    [SerializeField] private MinigameSlot leftSlot, rightSlot;
    private List<MinigameSlot> gameHistory = new();
    public static MainGameController Instance;
    
    [Serializable]
    public struct MinigameSlot
    {
        public Transform iconParent;
        public MinigameData data { get; set; }
        public WinState winState { get; set; }
    }

    private void Awake()
    {
        if (!Instance)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
    }

    private void Start()
    {
        GameSelection();
    }

    public void OnGameEnd(WinState winState)
    {
        if (gameHistory.Count > 0)
        {
            var lastGame = gameHistory[^1];
            lastGame.winState = winState;
        }
        // load selection scene
    }
    public void GameSelection()
    {
        var game1 = Random.Range(0, minigames.Length);
        var game2 = Random.Range(0, minigames.Length - 1);
        if (game2 >= game1)
            game2++;
        
        print(game1);
        print(game2);
        
        leftSlot.data = minigames[game1];
        rightSlot.data = minigames[game2];


        foreach (var obj in leftSlot.iconParent.GetComponentsInChildren<Transform>())
        {
            if (obj != leftSlot.iconParent)
                Destroy(obj.gameObject);
        }
        foreach (var obj in rightSlot.iconParent.GetComponentsInChildren<Transform>())
        {
            if (obj != rightSlot.iconParent)
                Destroy(obj.gameObject);
        }
        
        Instantiate(leftSlot.data.planetPrefab, leftSlot.iconParent);
        Instantiate(rightSlot.data.planetPrefab, rightSlot.iconParent);
    }

    public void EnterGame(MinigameSlot slot)
    {
        var game = slot.data;
        print(game.sceneName);
        gameHistory.Add(slot);
    }
}