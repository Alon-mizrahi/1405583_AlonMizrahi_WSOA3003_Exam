using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//State Machine states
public enum STATE { PLACETARGETBALL, SHOOTTARGETBALL, PLACEPOWERBALL, CANSHOOTPOWERBALL, BALLROLLING, WIN, LOST }


//PLACETARGETBALL--> can place the target ball
//SHOOTTARGETBALL--> can shoot target ball
//PLACEPOWERBALL--> can place powerball
//CANSHOOTPOWERBALL --> can shoot powerball
//BALLROLLING--> ball is moving and therfore player can not shoot. must wait for ball to stop
//WIN--> player has hit the goal and won, display won message and give options
//LOST--> player has lost. allow reset and other things


public class GameManager : MonoBehaviour
{
    public STATE state;
    //coruitine setup
    /*
     * void func1()
     *  {
     *      StartCoroutine(func2);
     *  }
     * 
     *IEnumerator func2()
    {
        //some code
        yield return new WaitForSeconds(1.0f);
        // some more code
    }
    */

    public float shootCounter;
    public Text shoottxt;

    // Start is called before the first frame update
    void Start()
    {
        //state = STATE.START; if there is menu

        //for testing
        state = STATE.PLACETARGETBALL;


        shootCounter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        shoottxt.text = ""+shootCounter;
    }
}
