using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Submissions.StudioSomething;

public class StationManager : MonoBehaviour
{
    public static StationManager Instance;
    public Color[] Colors;
    public GameObject[] Stations;
    public string[] KeyCodes;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public Vector3 GetStationLocation(int index)
    {
        if (index >= 0 && index <= Stations.Length - 1)
        {
            return Stations[index].GetComponent<StationSPO>().Zone.transform.position;
        }
        Debug.Log("Invalid Move");
        return Vector3.zero;

    }

    public void RefreshStations()
    {

        Stations = GameObject.FindGameObjectsWithTag("BCI");
        for (int i = 0; i < Stations.Length; i++)
        {
            Stations[i].GetComponent<Renderer>().material.color = Colors[i];
            StationSPO spo = Stations[i].GetComponent<StationSPO>();
            spo.onColour = Colors[i];
            spo.offColour = Color.white * 0.2f;
            spo.Label.text = KeyCodes[i];
            if (PlayerController.Keyboard == null)
            {
                spo.LetterCanvas.SetActive(false);
            }
            else
            {
                spo.LetterCanvas.SetActive(true);
            }

        }

    }

    public void RemoveOutlines()
    {
        foreach (GameObject station in Stations)
            station.GetComponent<Outline>().enabled = false;
    }


}
