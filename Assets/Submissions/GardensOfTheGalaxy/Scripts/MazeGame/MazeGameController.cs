using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MazeGameController : MinigameController
{
    [SerializeField] private int totalTurns;
    [SerializeField] private MazePlayer player1Prefab, player2Prefab;
    [SerializeField] private GameObject goalPrefab;
    [SerializeField] private MazeGenerator mazeGenerator;
    [SerializeField] private TextMeshProUGUI turnIndicator;

    private MazePlayer player1, player2;
    private Vector2Int goalPos;
    
    private int currentTurn;
    private int awaitingPlayer;

    public enum Direction
    {
        UP,
        DOWN,
        LEFT,
        RIGHT
    }
    
    private void Start()
    {
        mazeGenerator.MazeIt();
        foreach (var cell in mazeGenerator.cells)
        {
            if (!cell.wall)
            {
                player1 = Instantiate(player1Prefab, new Vector3(cell.x, 0, cell.z), Quaternion.identity);
                player1.Init(this, new Vector2Int(cell.x, cell.z));
                break;
            }
        }

        for (int i = mazeGenerator.cells.Count - 1; i >= 0; i--)
        {
            var cell = mazeGenerator.cells[i];
            if (!cell.wall)
            {
                player2 = Instantiate(player2Prefab, new Vector3(cell.x, 0, -cell.z), Quaternion.identity);
                player2.Init(this, new Vector2Int(cell.x, -cell.z));
                goalPos = new Vector2Int(0, cell.z);
                Instantiate(goalPrefab, new Vector3(goalPos.x, 0, goalPos.y), Quaternion.identity);
                break;
            }
        }
        
        
        NextTurn();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
            MovePlayer(2, Direction.UP);
        if (Input.GetKeyDown(KeyCode.DownArrow))
            MovePlayer(2, Direction.DOWN);
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            MovePlayer(2, Direction.LEFT);
        if (Input.GetKeyDown(KeyCode.RightArrow))
            MovePlayer(2, Direction.RIGHT);
        
        if (Input.GetKeyDown(KeyCode.W))
            MovePlayer(1, Direction.UP);
        if (Input.GetKeyDown(KeyCode.S))
            MovePlayer(1, Direction.DOWN);
        if (Input.GetKeyDown(KeyCode.A))
            MovePlayer(1, Direction.LEFT);
        if (Input.GetKeyDown(KeyCode.D))
            MovePlayer(1, Direction.RIGHT);
    }

    void NextTurn()
    {
        if (awaitingPlayer == 1)
            awaitingPlayer = 2;
        else
            awaitingPlayer = 1;
        turnIndicator.text = "Player " + awaitingPlayer + "'s Turn";
    }

    void MovePlayer(int playerId, Direction dir)
    {
        if (awaitingPlayer == playerId)
        {
            var player = (playerId == 1) ? player1 : player2;
            if (player.TryMove(dir))
            {
                if (player1.currPos == player2.currPos)
                {
                    player1.transform.position += Vector3.left * 0.1f;
                    player2.transform.position -= Vector3.left * 0.1f;
                }
                if (player1.currPos == goalPos && player2.currPos == goalPos)
                {
                    awaitingPlayer = 0;
                    turnIndicator.text = "You Won!";
                }
                else
                {
                    NextTurn();
                }
            }
        }
    }

    public MazeGenerator.Cell CellAt(Vector2Int pos)
    {
        return mazeGenerator.getCellAt(pos.x, pos.y);
    }
}