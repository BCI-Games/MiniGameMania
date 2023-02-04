using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MazeGenerator : MonoBehaviour
{
    [SerializeField] int mazeSize;
    [SerializeField] private float yScale;
    ArrayList walls = new ArrayList ();
    public List<Cell> cells = new();

    public void MazeIt ()
    {
        // Maze Generator by Charles Crete
        // Using Prim's algorithm
        // More info: http://en.wikipedia.org/wiki/Maze_generation_algorithm#Randomized_Prim.27s_algorithm
        int max = 10000;
        int size = mazeSize; // must be odd?
        if (size % 2 == 0)
            size++;
        int sizer = (size - 1) / 2;

        for (int x = -sizer; x <= sizer; x++) {
            for (int z = -sizer; z <= sizer; z++) {
                cells.Add (new Cell (x, z));
            }
        }

        Cell startingCell = getCellAt (0, 0);
        walls.Add (startingCell);
				
        while (true) {

            Cell wall = (Cell)walls [Random.Range (0, walls.Count)];

            processWall (wall);
						
					
            if (walls.Count <= 0)
                break;
            if (--max < 0)
                break;

        }

        foreach (Cell cell in cells) {

            if (cell.wall) {
                GameObject cube = GameObject.CreatePrimitive (PrimitiveType.Cube);
                cube.transform.position = new Vector3 ((float)cell.x, (float)0.5, (float)cell.z);

                cube.transform.localScale = new Vector3 (1, yScale, 1);

            }
        }

    }

    void processWall (Cell cell)
    {
        int x = cell.x;
        int z = cell.z;
        if (cell.from == null) {
            if (Random.Range (0, 2) == 0) {
                x += Random.Range (0, 2) - 1;
            } else {
                z += Random.Range (0, 2) - 1;
            }
        } else {
			
            x += (cell.x - cell.from.x);
            z += (cell.z - cell.from.z);
        }
        Cell next = getCellAt (x, z);
        if (next == null || !next.wall)
            return; 
        cell.wall = false;
        next.wall = false;


        foreach (Cell process in getWallsAroundCell (next)) {
            process.from = next;
            walls.Add (process);
        }
        walls.Remove (cell);

    }

    public Cell getCellAt (int x, int z)
    {
        foreach (Cell cell in cells) {
            if (cell.x == x && cell.z == z)
                return cell;
        }
        return null;
    }

    ArrayList getWallsAroundCell (Cell cell)
    {
        ArrayList near = new ArrayList ();
        ArrayList check = new ArrayList ();

        check.Add (getCellAt (cell.x + 1, cell.z));
        check.Add (getCellAt (cell.x - 1, cell.z));
        check.Add (getCellAt (cell.x, cell.z + 1));
        check.Add (getCellAt (cell.x, cell.z - 1));

        foreach (Cell checking in check) {
            if (checking != null && checking.wall)
                near.Add (checking);
        }
        return near;

    }


    public class Cell
    {
        public int x { get; set; }

        public int z { get; set; }
		
        public bool wall { get; set; }

        public Cell from { get; set; }

        public Cell (int x, int z)
        {
            this.x = x;
            this.z = z;
            this.wall = true;
        }


    }
}