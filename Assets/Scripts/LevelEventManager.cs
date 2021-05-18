using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEventManager : MonoBehaviour
{
    public int currentScene;
    public int nextScene;
    // Start is called before the first frame update
    void ShowGameOver(bool win)
    {
        if (win)
        {
            SceneManager.LoadScene(nextScene);
        }
        else if (!win)
        {
            SceneManager.LoadScene(currentScene);
        }
    }
}
