using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartLevelSelector : MonoBehaviour
{
    public void SelectLevel(int level)
    {
        if (level == 1) { SceneManager.LoadScene("Level 1"); }
        if (level == 2) { SceneManager.LoadScene("Level 2"); }
        if (level == 3) { SceneManager.LoadScene("Level 3"); }
        if (level == 4) { SceneManager.LoadScene("Level 4"); }
        //if (level == 5) { SceneManager.LoadScene("Level 5"); } // dont have this yet
    }
}
