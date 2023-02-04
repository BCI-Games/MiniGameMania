using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Random = UnityEngine.Random;

public class FishingGameController : MonoBehaviour
{
    public int numRows = 8;
    public int numCols = 8;

    public GameObject[] fishPrefabs;
    public GameObject waterSquarePrefab;

    public List<List<GameObject>> grid;
    public List<Fish> allFish;
    public List<List<bool>> isFish;
    public Fish chosenFish = null;

    public GameObject crane;

    void Start()
    {
        SpawnGrid();
    }
    private void SpawnGrid()
    {
        isFish = new List<List<bool>>();

        for (int r = 0; r < numRows; r++)
        {
            isFish.Add(new List<bool>());
            int randCol = Random.Range(0, numCols);
            for (int c = 0; c < numCols; c++)
            {
                // spawn water
                GameObject waterObject = Instantiate(waterSquarePrefab);
                waterObject.transform.position = new Vector3(c, r, 1);


                if (c == randCol)
                {
                    isFish[r].Add(true);
                    GameObject fishObject = Instantiate(fishPrefabs[Random.Range(0, fishPrefabs.Length)]);
                    fishObject.transform.position = new Vector3(c, r, 0);
                    Fish fish = fishObject.GetComponent<Fish>();
                    fish.rowIndex = r;
                    fish.colIndex = c;
                    allFish.Add(fish);
                    fishObject.GetComponent<SPO_ShaderColor>().myIndex = r;
                    fishObject.tag = "BCI";
                }
                else
                {
                    isFish[r].Add(false);
                }
            }
        }
    }
    public void MoveChosenFish()
    {

    }
    public void MoveOtherFish()
    {
        Assert.IsTrue(chosenFish != null);

        // move all fish except player's fish
        foreach(Fish fish in allFish)
        {
            // if not the same fish
            if (fish.rowIndex != chosenFish.rowIndex)
            {
                // find random col to move to
                int randCol = Random.Range(0, numCols);
                fish.MoveTo(Vector3.zero, MoveFishComplete);
            }
        }
    }
    private void MoveFishComplete()
    {
        print("Move Fish Complete");
    
    }
    public Fish RandomFishSelection()
    {
        return allFish[Random.Range(0, allFish.Count)];
    }
    public void ChooseFish(Fish fish)
    {
        chosenFish = fish;
    }
    public void RemoveColumn()
    {
        if (chosenFish.colIndex == 0)
        {
            // remove last column
            foreach(List<GameObject> rowGrid in grid)
            {
                RemoveLastColumn();
            }
        }
        else if (chosenFish.colIndex == numCols - 1)
        {
            // remove first column
            foreach (List<GameObject> rowGrid in grid)
            {
                RemoveFirstColumn();
            }
        }
        else
        {
            bool coinFlip = Random.Range(0, 1) > 0.5f ? true : false;

            if (coinFlip)
            {
                RemoveLastColumn();
            }
            else
            {
                RemoveFirstColumn();
            }
        }
        numCols--;
    }
    public void StartMoveCrane(int rowIndex, int colIndex)
    {
        StartCoroutine(MoveCraneAndFish(rowIndex, colIndex, 4f, 4f, null));
    }

    IEnumerator MoveCraneAndFish(int rowIndex, int colIndex, float timeX, float timeY, Action onComplete)
    {
        Vector3 startingPos = crane.transform.position;
        Vector3 finalPos = Vector3.zero;
        float elapsedTime = 0;

        while (elapsedTime < timeX)
        {
            crane.transform.position = Vector3.Lerp(startingPos, finalPos, (elapsedTime / timeX));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        startingPos = finalPos;
        finalPos = Vector3.zero;
        elapsedTime = 0;
        while (elapsedTime < timeY)
        {
            crane.transform.position = Vector3.Lerp(startingPos, finalPos, (elapsedTime / timeY));
            yield return null;
        }
        onComplete.Invoke();
    }

    #region Helper Functions
    // helper functions
    private void RemoveFirstColumn()
    {
        // remove first column
        foreach (List<GameObject> rowGrid in grid)
        {
            rowGrid.RemoveAt(0);
        }
    }
    private void RemoveLastColumn()
    {
        // remove last column
        foreach (List<GameObject> rowGrid in grid)
        {
            rowGrid.RemoveAt(numCols - 1);
        }
    }
    #endregion


    /*
    private IEnumerator SmoothLerp(float time)
    {
        Vector3 startingPos = initialWayPoint.transform.position;
        Vector3 finalPos = finalWayPoint.transform.position;
        float elapsedTime = 0;

        while (elapsedTime < time)
        {
            transform.position = Vector3.Lerp(startingPos, finalPos, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        onComplete.Invoke();
    }
    */
}
