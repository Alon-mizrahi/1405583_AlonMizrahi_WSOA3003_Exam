using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialDisplay : MonoBehaviour
{

    GameManager GM;
    public Text TutorialText1;
    public GameObject TutDisplay1;

    public GameObject TutDisplay2;
    public Text TutorialText2;

    public GameObject TutDisplay3;
    public Text TutorialText3;

    public GameObject TutDisplay4;

    public GameObject startBlocker;

    bool Tutclicked = false;
    public Transform camPos;

    PowerBallScript PB;

    GameObject Cam;
    //Objects to highlight when referring to them
    //public GameObject PowerBall;
    //public GameObject TargetBall;
    //public GameObject Goal;
    //public GameObject StartBlock;

    //public GameObject ThroughUI;
    //public GameObject NormalUI;


    // Start is called before the first frame update
    void Start()
    {
        GM = GameObject.FindWithTag("GM").GetComponent<GameManager>();
        Cam = GameObject.FindWithTag("MainCamera");
        TutDisplay2.SetActive(false);
        TutDisplay3.SetActive(false);
        Cam.GetComponent<CameraFollow>().enabled = false;
        //highlight startblock


        TutDisplay1.SetActive(true);
        TutorialText1.text = "Hi, welcome to Golf? " +
            "its like golf, kinda... ";

    }

    // Update is called once per frame
    void Update()
    {
        if (GM.state == STATE.PLACETARGETBALL) { PlaceTargetTut(); }
        if (GM.state == STATE.SHOOTTARGETBALL) { ShootTargetTut(); }
        if (GM.state == STATE.PLACEPOWERBALL) { PlacePowerTut(); }
        if (GM.state == STATE.CANSHOOTPOWERBALL){ ShootPowerTut(); }
        if (GM.state == STATE.BALLROLLING) { RollingWait(); }
    }

    void PlaceTargetTut()
    {
        //TutDisplay2.SetActive(false);
        //TutDisplay3.SetActive(false);

        //highlight startblock
        //TutDisplay1.SetActive(true);
        //TutorialText1.text = "Hi, welcome to Golf? " +
            //"its like golf, kinda... ";

        if (Input.GetMouseButtonDown(0) == true)
        {
            TutDisplay1.SetActive(false);
            TutDisplay3.SetActive(false);
            TutDisplay4.SetActive(false);
            TutDisplay2.SetActive(true);
            TutorialText2.text = "You can get a lay of the course by moving your mouse to the edges of the screen";
            Cam.GetComponent<CameraFollow>().enabled = true;
        }

        Vector3 camStart = new Vector3(-2, 15, -7.98f);

        if (camPos.position.x != camStart.x || camPos.position.z != camStart.z)
        {
            Tutclicked = true;
        }
        
        if(Tutclicked == true)
        {
            TutDisplay3.SetActive(true);
            TutorialText3.text = "To begin, Click on the Pink start block to place the Target ball. ";
            startBlocker.SetActive(false);
        }

        

    }

    void ShootTargetTut()
    {
        //highlight goal
        //Un-highlight startblock
        TutDisplay1.SetActive(true);
        TutDisplay1.transform.position = new Vector3(360, 840,0);
        TutorialText1.text = "Click and Drag on the Target ball to shoot it. The aim is to get this target ball into the Goal. You can only shoot this target ball once!";
        TutDisplay2.SetActive(false);
        TutDisplay3.SetActive(false);
    }

    void PlacePowerTut()
    {
        //highlight startblock
        TutDisplay1.SetActive(true);
        TutorialText1.text = "Click on the Starting Area to place the Power ball";
    }

    void ShootPowerTut()
    {
        PB = GameObject.FindWithTag("PowerBall").GetComponent<PowerBallScript>();
        TutDisplay1.SetActive(false);
        TutDisplay2.SetActive(true);
        TutDisplay2.transform.position = new Vector3(230 ,198 , 0);
        TutDisplay2.transform.localScale = new Vector3(1.5f, 1.5f, 1);
        TutorialText2.text = "The Powerball has some abilities, click Through to select the through ball";


        if(PB.power == POWER.GOTHOUGH)
        {
            TutDisplay2.SetActive(false);
            TutDisplay1.SetActive(true);
            TutorialText1.text = "Click and Drag on the Power ball to shoot it. Use this power ball to help hit the target ball into the goal. You can shoot this ball multiple times";

            TutDisplay3.SetActive(true);
            TutDisplay3.transform.position = new Vector3(960, 180, 0);
            TutorialText3.text = "The through ball can go through the transparent walls";
        }
    }

    void RollingWait()
    {
        TutorialText1.text = "";
        TutDisplay1.SetActive(false);
    }

}
