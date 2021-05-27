using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace LimitlessWarfare
{
    public class LevelChange : MonoBehaviour
    {
        public int nextScene;
       
        void Start()
        {

            GetComponent<Button>().onClick.AddListener(NextLevel);
        }

        public void NextLevel()
        {
            SceneManager.LoadScene(nextScene);
        }

    }
}
