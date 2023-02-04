using UnityEngine;
using static MazeGameController.Direction;

public class MazePlayer : MonoBehaviour
{
    private MazeGameController ctrl;
    public Vector2Int currPos;

    public void Init(MazeGameController ctrl, Vector2Int spawnPos)
    {
        currPos = spawnPos;
        this.ctrl = ctrl;
    }
    public bool TryMove(MazeGameController.Direction dir)
    {
        Vector2Int targetPos = currPos;
        switch (dir)
        {
            case UP:
                targetPos = currPos + Vector2Int.up;
                break;
            case DOWN:
                targetPos = currPos + Vector2Int.down;
                break;
            case LEFT:
                targetPos = currPos + Vector2Int.left;
                break;
            case RIGHT:
                targetPos = currPos + Vector2Int.right;
                break;
        }
        if ((ctrl.CellAt(targetPos) != null) && !ctrl.CellAt(targetPos).wall)
        {
            transform.position = new Vector3(targetPos.x, 0, targetPos.y);
            currPos = targetPos;
            return true;
        }

        return false;
    }
}