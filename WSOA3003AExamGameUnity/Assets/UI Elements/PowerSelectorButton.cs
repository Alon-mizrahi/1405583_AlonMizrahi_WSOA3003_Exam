using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerSelectorButton : MonoBehaviour
{
    public PowerBallScript PBScript;
    public GameManager GM;

    string power;


    // Start is called before the first frame update
    void Start()
    {
        power = gameObject.name;
        GM = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
    }

    public void PowerSelector()
    {
        if (GM.state == STATE.CANSHOOTPOWERBALL)
        {
            if (PBScript == null)
            { 
                PBScript = GameObject.FindGameObjectWithTag("PowerBall").GetComponent<PowerBallScript>();
            }
            Debug.Log("Clicked power selector: " + power);

            PBScript.SelectPower(power);
        }
    }



/*
    private void OnMouseDown()
    {

        if(Input.GetMouseButtonDown(0) && GM.state == STATE.CANSHOOTPOWERBALL)// || GM.state == STATE.PLACEPOWERBALL)
        { 
            PBScript = GameObject.FindGameObjectWithTag("PowerBall").GetComponent<PowerBallScript>();

            Debug.Log("Clicked power selector: " + power);
            PBScript.SelectPower(power);
        }
    }
    */


}
