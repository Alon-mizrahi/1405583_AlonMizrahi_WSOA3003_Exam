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

    BallBounce ReflectScript;
    GameObject MagRadius;

    //power limits
    int MagLimit;
    int StickyLimit;
    int ThroughLimit;
    int Stickytxt;
    public bool PBShot = false;


    // Start is called before the first frame update
    void Start()
    {
        MagRadius = GameObject.Find("MagRadius"); 

        ReflectScript = gameObject.GetComponent<BallBounce>();
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
        Sticky_UI.GetComponent<Outline>().effectColor = UnSelected;
        Through_UI.GetComponent<Outline>().effectColor = UnSelected;
        Mag_UI.GetComponent<Outline>().effectColor = UnSelected;


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

        MagLimit = GM.MagLimit;
        StickyLimit = GM.StickyLimit;
        ThroughLimit = GM.ThroughLimit;
        Stickytxt = GM.Stickytxt;
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
        if (PowerType == "ThroughBall" && ThroughLimit!= 0)
        {
            if (Normal_UI != null) { Normal_UI.GetComponent<Outline>().effectColor = UnSelected; }
            if (Sticky_UI != null) { Sticky_UI.GetComponent<Outline>().effectColor = UnSelected; }
            if (Through_UI != null) { Through_UI.GetComponent<Outline>().effectColor = Selected; }
            if (Mag_UI != null) { Mag_UI.GetComponent<Outline>().effectColor = UnSelected; }

            ThroughBall();
        }
        if (PowerType == "StickyBall" && StickyLimit != 0)
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
        if (PowerType == "MagBall" && MagLimit != 0)
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
        ToggleThroughWalls(false);
        ReflectScript.enabled = true;
        MagRadius.GetComponent<MeshRenderer>().enabled = true;
    }

    void NormalBall()
    {
        Debug.Log("Normal Ball Selected");
        power = POWER.NORMAL;
        gameObject.GetComponent<MeshRenderer>().material = Mat_Normal;

        ToggleThroughWalls(false);
        ReflectScript.enabled = true;
        MagRadius.GetComponent<MeshRenderer>().enabled = false;
    }

    void StickyBall()
    {
        Debug.Log("Sticky Ball Selected");
        power = POWER.STICKY;
        gameObject.GetComponent<MeshRenderer>().material = Mat_Sticky;

        ToggleThroughWalls(false);
        ReflectScript.enabled = false;
        MagRadius.GetComponent<MeshRenderer>().enabled = false;
    }

    void ThroughBall()
    {
        Debug.Log("Through Ball Selected");
        power = POWER.GOTHOUGH;
        gameObject.GetComponent<MeshRenderer>().material = Mat_Through;

        ToggleThroughWalls(true);
        ReflectScript.enabled = true;
        MagRadius.GetComponent<MeshRenderer>().enabled = false;
    }

    //THROUGH BALL
    //pass in false to disable | pass in true to enable
    void ToggleThroughWalls(bool ignore) 
    {
        GameObject[] ThroughWalls = GameObject.FindGameObjectsWithTag("ThroughWall");

        for(int i = 0; i < ThroughWalls.Length; i++)
        {
            //ThroughWalls[i].GetComponent<BoxCollider>().enabled = ignore;
            //public static void IgnoreCollision(Collider collider1, Collider collider2, bool ignore = true);
            Physics.IgnoreCollision(ThroughWalls[i].GetComponent<BoxCollider>(), this.GetComponent<SphereCollider>(), ignore);
        }

    }


    private void Update()
    {
        
        if (PBShot == true)
        {
            CheckPowerLimit();
        }
        

        if (power == POWER.MAGNET)
        {
            MagRadius.transform.rotation = Quaternion.identity;

            Debug.Log(Vector3.Distance(gameObject.transform.position, Target.position));
            if (Vector3.Distance(gameObject.transform.position, Target.position) <= 6.5f)
            {
                Vector3 dir = transform.position - Target.position;
                dir = dir.normalized;
                Target.GetComponent<Rigidbody>().AddForce(dir * 0.5f);
            }
        }
    }

    //STICKY BALL / Toggle Magnet Ball
    private void OnCollisionEnter(Collision collision)
    {
        if (power == POWER.STICKY)
        {
            if (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "ThroughWall")
            {
                gameObject.GetComponent<Rigidbody>().velocity = new Vector3 (0f,0f,0f);
                gameObject.transform.rotation = Quaternion.identity;
                gameObject.GetComponent<Rigidbody>().isKinematic = true;
                gameObject.GetComponent<Rigidbody>().isKinematic = false;
            }    
        }

        if (power == POWER.MAGNET)
        {
            if (collision.gameObject.tag == "TargetBall")
            {
                SelectPower("NormalBall");
            }
        }
    }


    private void OnCollisionStay(Collision collision)
    {
        if (power == POWER.MAGNET)
        {
            if (collision.gameObject.tag == "TargetBall")
            {
                SelectPower("NormalBall");
            }
        }
    }


    void CheckPowerLimit()
    {
        if (GM.state == STATE.CANSHOOTPOWERBALL)
        {
            if (power == POWER.GOTHOUGH && ThroughLimit == 0) { SelectPower("NormalBall"); }
            if (power == POWER.STICKY && StickyLimit == 0) { SelectPower("NormalBall"); }
            if (power == POWER.MAGNET && MagLimit == 0) { SelectPower("NormalBall"); }

            PBShot = false;
        }
    }


    public void UpdatePowerLimit()
    {
        if (power == POWER.GOTHOUGH) { ThroughLimit--; GM.ThroughLimitText.text = "" + ThroughLimit; }
        if (power == POWER.STICKY) { StickyLimit--; Stickytxt--; GM.StickyLimitText.text = "" + StickyLimit; }
        if (power == POWER.MAGNET) { MagLimit--; GM.MagLimitText.text = "" + MagLimit; }
        GM.state = STATE.BALLROLLING;
        PBShot = true;
    }

}



