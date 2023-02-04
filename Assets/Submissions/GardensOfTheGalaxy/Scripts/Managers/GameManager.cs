using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Submissions.GardensOTG
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        public int numPlayers = 0;
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
                return;
            }
            Instance = this;
        }

        public void LaunchGame()
        {
            // launches game

        }

        public void SetNumPlayers(int numPlayers)
        {
            this.numPlayers = numPlayers;
        }
    }
}