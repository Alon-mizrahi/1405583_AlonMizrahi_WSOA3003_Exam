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
public enum POWER { NORMAL, STICKY, GOTHOUGH, MAGNET }

public class PowerBallScript : MonoBehaviour
{

    GameManager GM;

    public POWER power;

    GameObject Normal_UI;
    GameObject Sticky_UI;
    GameObject Through_UI;
    GameObject Mag_UI;

    public Material Mat_Normal;
    public Material Mat_Sticky;
    public Material Mat_Through;
    public Material Mat_Mag;
    
    Transform Target;

    public Color Selected = new Color(225,225,0,225);
    public Color UnSelected = new Color(176,176,176,128);


    // Start is called before the first frame update
    void Start()
    {

        Debug.Log("We made it here");
        power = POWER.NORMAL;
        Debug.Log("1");
        GM = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
        Debug.Log("2");
        Normal_UI = GameObject.FindGameObjectWithTag("UI_Normal");
        Sticky_UI = GameObject.FindGameObjectWithTag("UI_Sticky");
        Through_UI = GameObject.FindGameObjectWithTag("UI_Through");
        Mag_UI = GameObject.FindGameObjectWithTag("UI_Mag");
        Debug.Log("3");
        Normal_UI.GetComponent<Outline>().effectColor = Selected;
        
        Debug.Log("4");
        if (Normal_UI.GetComponent<Image>().IsActive() == false) { Normal_UI = null; }
        if (Sticky_UI.GetComponent<Image>().IsActive() == false) { Sticky_UI = null; }
        if (Through_UI.GetComponent<Image>().IsActive() == false) { Through_UI = null; }
        if (Mag_UI.GetComponent<Image>().IsActive() == false) { Mag_UI = null; }
        Debug.Log("5");


        Debug.Log("Power UI:");
        Debug.Log("Normal UI: "+ Normal_UI);
        Debug.Log("Sticky UI: " + Sticky_UI);

        Debug.Log("---------------");
    }


    public void SelectPower(string PowerType)
    {
        Debug.Log("2) power: " + PowerType);


        if (PowerType == "NormalBall")
        {
            if (Normal_UI != null) { Normal_UI.GetComponent<Outline>().effectColor = Selected;}
            if (Sticky_UI != null) { Sticky_UI.GetComponent<Outline>().effectColor = UnSelected; }
            if (Through_UI != null) { Through_UI.GetComponent<Outline>().effectColor = UnSelected; }
            if (Mag_UI != null) { Mag_UI.GetComponent<Outline>().effectColor = UnSelected; }

            NormalBall();
        }
        if (PowerType == "ThroughBall")
        {
            if (Normal_UI != null) { Normal_UI.GetComponent<Outline>().effectColor = UnSelected; }
            if (Sticky_UI != null) { Sticky_UI.GetComponent<Outline>().effectColor = UnSelected; }
            if (Through_UI != null) { Through_UI.GetComponent<Outline>().effectColor = Selected; }
            if (Mag_UI != null) { Mag_UI.GetComponent<Outline>().effectColor = UnSelected; }

            ThroughBall();
        }
        if (PowerType == "StickyBall")
        {
            //Normal_UI.GetComponent<Outline>().effectColor = UnSelected;
            //Sticky_UI.GetComponent<Outline>().effectColor = Selected;
            //Through_UI.GetComponent<Outline>().effectColor = UnSelected;
            //Mag_UI.GetComponent<Outline>().effectColor = UnSelected;

            if (Normal_UI != null) { Normal_UI.GetComponent<Outline>().effectColor = UnSelected; }
            if (Sticky_UI != null) { Sticky_UI.GetComponent<Outline>().effectColor = Selected; }
            if (Through_UI != null) { Through_UI.GetComponent<Outline>().effectColor = UnSelected; }
            if (Mag_UI != null) { Mag_UI.GetComponent<Outline>().effectColor = UnSelected; }


            StickyBall();
        }
        if (PowerType == "MagBall")
        {
            if (Normal_UI != null) { Normal_UI.GetComponent<Outline>().effectColor = UnSelected; }
            if (Sticky_UI != null) { Sticky_UI.GetComponent<Outline>().effectColor = UnSelected; }
            if (Through_UI != null) { Through_UI.GetComponent<Outline>().effectColor = UnSelected; }
            if (Mag_UI != null) { Mag_UI.GetComponent<Outline>().effectColor = Selected; }

            MagBall();
        }
    }

    void MagBall()
    {
        Target = GameObject.FindGameObjectWithTag("TargetBall").GetComponent<Transform>();
        Debug.Log("Mag Ball Selected");
        power = POWER.MAGNET;
        gameObject.GetComponent<MeshRenderer>().material = Mat_Mag;
        ToggleThroughWalls(true);
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


    private void Update()
    {
        if (power == POWER.MAGNET)
        {
            Debug.Log(Vector3.Distance(gameObject.transform.position, Target.position));
            if (Vector3.Distance(gameObject.transform.position, Target.position) <= 6.5f)
            {
                Vector3 dir = transform.position - Target.position;
                dir = dir.normalized;
                Target.GetComponent<Rigidbody>().AddForce(dir * 0.5f);
            }
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