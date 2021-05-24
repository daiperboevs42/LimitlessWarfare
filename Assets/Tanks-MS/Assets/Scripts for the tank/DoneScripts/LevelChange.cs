using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelChange : MonoBehaviour
{
    public int nextScene;
    // Start is called before the first frame update
    void Start()
    {
        
        GetComponent<Button>().onClick.AddListener(NextLevel);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(nextScene);
    }
    
}
