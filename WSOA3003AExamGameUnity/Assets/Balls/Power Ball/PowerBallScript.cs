using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This scripts selects the power of the powerball and sets an enum based off of that.
/// control unique interactions here
/// </summary>

//these are different abilitys. enum ensures only one at a time
//add to these
public enum POWER { NORMAL, STICKY, GOTHOUGH }

public class PowerBallScript : MonoBehaviour
{

    GameManager GM;

    public POWER power;

    GameObject Normal_UI;
    GameObject Sticky_UI;
    GameObject Through_UI;

    public Material Mat_Normal;
    public Material Mat_Sticky;
    public Material Mat_Through;

    public Color Selected = new Color(225,225,0,225);
    public Color UnSelected = new Color(176,176,176,128);


    // Start is called before the first frame update
    void Start()
    {
        power = POWER.NORMAL;
        
        GM = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();

        Normal_UI = GameObject.FindGameObjectWithTag("UI_Normal");
        Sticky_UI = GameObject.FindGameObjectWithTag("UI_Sticky");
        Through_UI = GameObject.FindGameObjectWithTag("UI_Through");
        Normal_UI.GetComponent<Outline>().effectColor = Selected;
    }


    public void SelectPower(string PowerType)
    {
        Debug.Log("power: " + PowerType);


        if (PowerType == "NormalBall")
        {
            Normal_UI.GetComponent<Outline>().effectColor = Selected;
            Sticky_UI.GetComponent<Outline>().effectColor = UnSelected;
            Through_UI.GetComponent<Outline>().effectColor = UnSelected;

            NormalBall();
        }
        if (PowerType == "ThroughBall")
        {
            Normal_UI.GetComponent<Outline>().effectColor = UnSelected;
            Sticky_UI.GetComponent<Outline>().effectColor = UnSelected;
            Through_UI.GetComponent<Outline>().effectColor = Selected;

            ThroughBall();
        }
        if (PowerType == "StickyBall")
        {
            Normal_UI.GetComponent<Outline>().effectColor = UnSelected;
            Sticky_UI.GetComponent<Outline>().effectColor = Selected;
            Through_UI.GetComponent<Outline>().effectColor = UnSelected;

            StickyBall();
        }
    }


    void NormalBall()
    {
        Debug.Log("Normal Ball Selected");
        power = POWER.NORMAL;
        gameObject.GetComponent<MeshRenderer>().material = Mat_Normal;

        ToggleThroughWalls(true);
    }

    void StickyBall()
    {
        Debug.Log("Sticky Ball Selected");
        power = POWER.STICKY;
        gameObject.GetComponent<MeshRenderer>().material = Mat_Sticky;

        ToggleThroughWalls(true);
    }

    void ThroughBall()
    {
        Debug.Log("Through Ball Selected");
        power = POWER.GOTHOUGH;
        gameObject.GetComponent<MeshRenderer>().material = Mat_Through;

        ToggleThroughWalls(false);
    }

    //THROUGH BALL
    //pass in false to disable | pass in true to enable
    void ToggleThroughWalls(bool isOn) 
    {
        GameObject[] ThroughWalls = GameObject.FindGameObjectsWithTag("ThroughWall");

        for(int i = 0; i < ThroughWalls.Length; i++)
        {
            ThroughWalls[i].GetComponent<BoxCollider>().enabled = isOn;
        }

    }
    

    //STICKY BALL
    private void OnCollisionEnter(Collision collision)
    {
        if (power == POWER.STICKY)
        {
            if (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "ThroughWall")
            {
                gameObject.GetComponent<Rigidbody>().velocity = new Vector3 (0f,0f,0f);
                //gameObject.GetComponent<Rigidbody>().isKinematic = true;
                //gameObject.GetComponent<Rigidbody>().isKinematic = false;
            }    
        }
    }


}