using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreEnemiesDead : MonoBehaviour
{
    List<GameObject> listOfEnemies = new List<GameObject>();

    void Start()
    {
        listOfEnemies.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
        print(listOfEnemies.Count);
    }

    public void KilledEnemy(GameObject enemy)
    {
        if (listOfEnemies.Contains(enemy))
        {
            listOfEnemies.Remove(enemy);
        }

        print(listOfEnemies.Count);
    }

    public bool AreEnemyDead()
    {
        if (listOfEnemies.Count <= 0)
        {
            // They are dead!
            return true;
        }
        else
        {
            // They're still alive dangit
            return false;
        }
    }
}
