using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabManager : MonoBehaviour
{
    public GameObject[] tabs;
    public GameObject startingTab;
    GameObject activeTab;

    void Start()
    {
        foreach (GameObject tabContent in tabs)
            tabContent.SetActive(false);

        startingTab.SetActive(true);
    }

    public void SelectTab(GameObject selectedTab)
    {
        activeTab.SetActive(false);
        activeTab = selectedTab;
        activeTab.SetActive(true);
    }
}
