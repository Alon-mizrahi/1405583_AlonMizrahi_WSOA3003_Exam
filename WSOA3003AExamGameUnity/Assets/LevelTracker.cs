using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelTracker : MonoBehaviour
{

    public int currentLevel = 0;
    public int highestLevel = 0;
    GameManager GM;

    public GameObject level1;
    public GameObject level2;
    public GameObject level3;
    public GameObject level4;
    public GameObject level5;


    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        if (SceneManager.GetActiveScene().name != "StartMenu")
        {
            GM = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
        }
        
        Debug.Log("GotGM: " + GM);
    }

    private void OnLevelWasLoaded(int level)
    {
        Debug.Log("RERUN ON LEVEL LOADED");
        if (GM == null && SceneManager.GetActiveScene().buildIndex != 0)
        {
            GM = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
            Debug.Log("GotGM: " + GM);
        }

        /*
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            Debug.Log("GOT LEVELS");
            level1 = GameObject.Find("Level1");
            level2 = GameObject.Find("Level2");
            level3 = GameObject.Find("Level3");
            level4 = GameObject.Find("Level4");
            level5 = GameObject.Find("Level5");

            //level1 = Resources.FindObjectsOfTypeAll<GameObject>.FirstorD

            UnlockLevels();
        }
        */
    }

    public void GetButtons()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            Debug.Log("GOT LEVELS");
            level1 = GameObject.Find("Level1");
            level2 = GameObject.Find("Level2");
            level3 = GameObject.Find("Level3");
            level4 = GameObject.Find("Level4");
            level5 = GameObject.Find("Level5");

            //level1 = Resources.FindObjectsOfTypeAll<GameObject>.FirstorD

            UnlockLevels();
        }
    }




    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name != "StartMenu")
        {
            if(GM.state == STATE.WON) { TrackLevels(); }
        }
        
    }



    void TrackLevels()
    {
        currentLevel = SceneManager.GetActiveScene().buildIndex;

        if (currentLevel > highestLevel) { highestLevel = currentLevel; }
    }


    public void UnlockLevels()
    {
        if (highestLevel == 1)
        {
            level1.GetComponent<Button>().interactable = true;
            level2.GetComponent<Button>().interactable = true;
        }
        if (highestLevel == 2)
        {
            level1.GetComponent<Button>().interactable = true;
            level2.GetComponent<Button>().interactable = true;
            level3.GetComponent<Button>().interactable = true;
        }
        if (highestLevel == 3)
        {
            level1.GetComponent<Button>().interactable = true;
            level2.GetComponent<Button>().interactable = true;
            level3.GetComponent<Button>().interactable = true;
            level4.GetComponent<Button>().interactable = true;
        }
        if (highestLevel == 4)
        {
            level1.GetComponent<Button>().interactable = true;
            level2.GetComponent<Button>().interactable = true;
            level3.GetComponent<Button>().interactable = true;
            level4.GetComponent<Button>().interactable = true;
            level5.GetComponent<Button>().interactable = true;
        }
        if (highestLevel == 5)
        {
            level1.GetComponent<Button>().interactable = true;
            level2.GetComponent<Button>().interactable = true;
            level3.GetComponent<Button>().interactable = true;
            level4.GetComponent<Button>().interactable = true;
            level5.GetComponent<Button>().interactable = true;
            //level6.GetComponent<Button>().interactable = true;
        }


    }

}
