using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Submissions.BCIBurgers
{
    public class LevelManager : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void LoadLevel(int levelToLoad)
        {
            SceneManager.LoadScene(levelToLoad);
        }



        public void QuitGame()
        {
            Application.Quit();
        }
    }
}