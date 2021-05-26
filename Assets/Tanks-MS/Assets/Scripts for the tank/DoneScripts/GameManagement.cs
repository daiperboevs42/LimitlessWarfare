using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LimitlessWarfare {
    public class GameManagement : MonoBehaviour
    {
        #region Variables
        [Header("Enemy Properties")]
        List<GameObject> listOfEnemies = new List<GameObject>();
        private bool allEnemiesAreDead = false;
        private bool isPlayerDead = false;

        [Header("Canvas Properties")]
       
        public GameObject winCanvas;
        public GameObject restartCanvas;
        public GameObject winWinCanvas;
        public GameObject canvasHolder;

        public bool lastLevel = false;
        private bool isGameOver = false;
        public int nextScene;
        #endregion


        #region Builtin Methods
        void Start()
        {
            listOfEnemies.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
            print(listOfEnemies.Count);
            winCanvas.GetComponentInChildren<LevelChange>().nextScene = nextScene;

        }
        #endregion


        #region Custom Methods
        public void KilledEnemy(GameObject enemy)
        {
            if (listOfEnemies.Contains(enemy))
                listOfEnemies.Remove(enemy);
            AreEnemiesDead();
            print(listOfEnemies.Count);
        }

        public bool AreEnemiesDead()
        {
            if (listOfEnemies.Count <= 0)
            {
                allEnemiesAreDead = true;
                return true;
            }
            else {
                allEnemiesAreDead = false;
                return false;
            }
        }

        public bool IsPlayerDead()
        {
            isPlayerDead = true;
            return true;
        }


        void Update()
        {
            if (!isGameOver)
            {
                if (allEnemiesAreDead && !lastLevel)
                {

                    //load win screen
                    canvasHolder = Instantiate(winCanvas);
                    isGameOver = true;
                    Debug.Log("THEY DEAD");
                }
                if (isPlayerDead)
                {
                    //load death screen
                    canvasHolder = Instantiate(restartCanvas);
                    isGameOver = true;
                    Debug.Log("YOU DIED");
                }
                if (allEnemiesAreDead && lastLevel)
                {
                    canvasHolder = Instantiate(winWinCanvas);
                    isGameOver = true;
                }
            }
        }

    }
    #endregion
}