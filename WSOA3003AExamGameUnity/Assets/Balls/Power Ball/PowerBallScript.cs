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
    // Start is called before the first frame update
    void Start()
    {
        power = POWER.NORMAL;
        GM = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //if(GM.state == STATE.CANSHOOTPOWERBALL || GM.state == STATE.PLACEPOWERBALL)
        //{
        //}
    }

    public void SelectPower(string PowerType)
    {
        if (PowerType == "NormalBall")
        {   
            NormalBall();
        }
        if (PowerType == "ThroughBall")
        {   
            ThroughBall();
        }
        if (PowerType == "StickyBall")
        {
            StickyBall();
        }
    }


    void NormalBall()
    {
        Debug.Log("Normal Ball Selected");
        power = POWER.NORMAL;

    }


    void StickyBall()
    {
        Debug.Log("Sticky Ball Selected");
        power = POWER.STICKY;

    }


    void ThroughBall()
    {
        Debug.Log("Through Ball Selected");
        power = POWER.GOTHOUGH;

    }

}