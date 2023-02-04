using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "Data/MinigameData", order = 1)]
public class MinigameData : ScriptableObject
{
    public string sceneName;
    public GameObject planetPrefab;
}