using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace LimitlessWarfare
{
    public class RestartLevel : MonoBehaviour
    {
       
        void Start()
        {
            GetComponent<Button>().onClick.AddListener(Restart);
        }


        public void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            
        }
    }
}
