using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToStart : MonoBehaviour
{

    public void ToStart()
    {
        
        SceneManager.LoadScene("StartMenu");
            //SceneManager.GetSceneByBuildIndex(0));
            //SceneManager.GetActiveScene().buildIndex + 1);
    }



}
