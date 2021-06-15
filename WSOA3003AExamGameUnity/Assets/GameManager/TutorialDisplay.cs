using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialDisplay : MonoBehaviour
{

    GameManager GM;
    public Text TutorialText;
    public GameObject TutDisplay;

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
        //highlight startblock
        TutDisplay.SetActive(true);
        TutorialText.text = "Click on the Highlighted Starting Area to place the target ball";
    }

    void ShootTargetTut()
    {
        //highlight goal
        //Un-highlight startblock
        TutDisplay.SetActive(true);
        TutorialText.text = "Click and Drag on the Target ball to shoot it. The aim is to get this target ball into the Goal. You can only shoot this target ball once!";
    }

    void PlacePowerTut()
    {
        //highlight startblock
        TutDisplay.SetActive(true);
        TutorialText.text = "Click on the Starting Area to place the Power ball";
    }

    void ShootPowerTut()
    {
        TutDisplay.SetActive(true);
        TutorialText.text = "Click and Drag on the Power ball to shoot it. Use this power ball to help hit the target ball into the goal.";
    }

    void RollingWait()
    {
        TutorialText.text = "";
        TutDisplay.SetActive(false);
    }

}
