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

    public bool isWentThrough = false;
    public bool isStartNexttoWall = false;

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

    }


    void StickyBall()
    {
        Debug.Log("Sticky Ball Selected");
        power = POWER.STICKY;
        gameObject.GetComponent<MeshRenderer>().material = Mat_Sticky;

    }


    void ThroughBall()
    {
        Debug.Log("Through Ball Selected");
        power = POWER.GOTHOUGH;
        gameObject.GetComponent<MeshRenderer>().material = Mat_Through;
    
    }

    //STICKY BALL
    private void OnCollisionEnter(Collision collision)
    {
        if (power == POWER.STICKY && collision.gameObject.tag == "Wall")
        {
                gameObject.GetComponent<Rigidbody>().isKinematic = true;
                gameObject.GetComponent<Rigidbody>().isKinematic = false;
        }
    }


    //THROUGH BALL
    //first wall
    //enter trigger -> disable box collider
    //exit -> trigger turn on box collider
    //check for single wall passing though -> can only do one

    private void OnTriggerEnter(Collider other)
    {
        if (power == POWER.GOTHOUGH && other.gameObject.tag == "Wall" && isWentThrough == false)
        {
                BoxCollider[] col = other.gameObject.GetComponentsInChildren<BoxCollider>();
                col[1].enabled = false;
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Wall" && GM.state == STATE.BALLSTOPPED)
        {
            isWentThrough = false;
            isStartNexttoWall = true;

        }
        
    }



    private void OnTriggerExit(Collider other)
    {
        if (power == POWER.GOTHOUGH && other.gameObject.tag == "Wall")
        {
            BoxCollider[] col = other.gameObject.GetComponentsInChildren<BoxCollider>();
            col[1].enabled = true;

            if (isStartNexttoWall == false)
            {
                isWentThrough = true;
            }   
        }
    }


}