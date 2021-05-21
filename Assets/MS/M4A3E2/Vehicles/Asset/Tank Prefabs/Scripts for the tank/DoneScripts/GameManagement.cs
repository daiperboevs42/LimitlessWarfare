using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagement : MonoBehaviour
{
    List<GameObject> listOfEnemies = new List<GameObject>();
    private bool allEnemiesAreDead = false;
    private bool isPlayerDead = false;

    // Start is called before the first frame update
    void Start()
    {
        listOfEnemies.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
        print(listOfEnemies.Count);
    }

    public void KilledEnemy(GameObject enemy)
    {
        if (listOfEnemies.Contains(enemy))
            listOfEnemies.Remove(enemy);

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


    // Update is called once per frame
    void Update()
    {
        if (allEnemiesAreDead)
        {
            //load win screen
        }
        if (isPlayerDead)
        {
            //load death screen
        }
    }
}
