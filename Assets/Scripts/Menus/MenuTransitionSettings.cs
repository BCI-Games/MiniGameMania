using UnityEngine;

public class MenuTransitionSettings : MonoBehaviour
{
    public enum TransitionSlant { Straight, Diagonal }

    public GameObject targetMenu;

    public TransitionSlant slant;
    public Vector2Int direction = Vector2Int.up;
}
