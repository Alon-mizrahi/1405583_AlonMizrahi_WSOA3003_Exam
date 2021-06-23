using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartLevelSelector : MonoBehaviour
{
    GameObject[] Trackers;

    private void Start()
    {
        Trackers = GameObject.FindGameObjectsWithTag("LevelTracker");
    }


    public void SelectLevel(int level)
    {
        if (level == 1) { SceneManager.LoadScene("Level 1"); }
        if (level == 2) { SceneManager.LoadScene("Level 2"); }
        if (level == 3) { SceneManager.LoadScene("Level 3"); }
        if (level == 4) { SceneManager.LoadScene("Level 4"); }
        //if (level == 5) { SceneManager.LoadScene("Level 5"); } // dont have this yet
    }

    private void Update()
    {
        DeleteTrackers();
    }

    void DeleteTrackers()
    {

        Trackers = GameObject.FindGameObjectsWithTag("LevelTracker");

        bool isZero = true;
        if (Trackers.Length != 1)
        {

            for (int y = 0; y < Trackers.Length; y++)
            {
                if (Trackers[y].GetComponent<LevelTracker>().highestLevel != 0)
                {
                    isZero = false;
                }
            }


            if (isZero == false)
            {
                for (int i = 0; i < Trackers.Length; i++)
                {
                    for (int x = 0; x < Trackers.Length; x++)
                    {
                        if (i != x)
                        {
                            if (Trackers[i].GetComponent<LevelTracker>().highestLevel >= Trackers[x].GetComponent<LevelTracker>().highestLevel)
                            {
                                GameObject.Destroy(Trackers[x]);
                                Debug.Log("Destroyed a tracker" + Trackers[x]);
                                //break;
                            }
                        }
                    }
                }
                isZero = true;
            }
            else
            {
                for (int z = 0; z < Trackers.Length-1; z++)
                {
                    GameObject.Destroy(Trackers[z]);
                }

            }

        }
    }




}
