using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void OnMouseDown()
    {
        if(GM.state == STATE.CANSHOOTPOWERBALL || GM.state == STATE.PLACEPOWERBALL)
        { 
            PBScript = GameObject.FindGameObjectWithTag("PowerBall").GetComponent<PowerBallScript>();
            PBScript.SelectPower(power);
        }
    }

}
