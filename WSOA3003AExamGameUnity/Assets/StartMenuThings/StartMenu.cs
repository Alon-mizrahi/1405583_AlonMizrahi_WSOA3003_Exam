using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public GameObject StartDispaly;
    public GameObject SettingsDispaly;
    public GameObject LevelsDispaly;


    public void BackBtn()
    {
        StartDispaly.SetActive(true);
        SettingsDispaly.SetActive(false);
        LevelsDispaly.SetActive(false);
    }

    public void PlayBtn()
    {
        //go to first level or better, go to current level //level tracker script

        //just going to first level
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Debug.Log("clicked play");
    }

    //hide start display settings
    public void SettingsBtn()
    {
        StartDispaly.SetActive(false);
        SettingsDispaly.SetActive(true);
        LevelsDispaly.SetActive(false);
    }

    //hide settings and start, display levels
    public void LevelsBtn()
    {
        StartDispaly.SetActive(false);
        SettingsDispaly.SetActive(false);
        LevelsDispaly.SetActive(true);
    }


}
