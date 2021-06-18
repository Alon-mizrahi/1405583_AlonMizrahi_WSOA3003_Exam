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
        Debug.Log("Getting Power?: " + power);
        GM = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
    }

    public void PowerSelector()
    {
        if (GM.state == STATE.CANSHOOTPOWERBALL)
        {
                Debug.Log("1) Clicked power selector: " + power);

                PBScript.SelectPower(power);
           
        }
    }

    private void Update()
    {
        if (GM.state == STATE.CANSHOOTPOWERBALL && PBScript == null)
        {
            PBScript = GameObject.FindGameObjectWithTag("PowerBall").GetComponent<PowerBallScript>();

            Debug.Log("PBScript Found?: " + PBScript);
        }
       
    }


}
