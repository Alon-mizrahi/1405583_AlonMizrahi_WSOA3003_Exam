using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//State Machine states
public enum STATE { START, CANSHOOT, BALLROLLING, WIN, LOST }
//START--> start of game, during setup phase
//CANSHOOT--> when the player is allowed to take a shot. ie ball has stopped andhas not won or lost yet
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


    // Start is called before the first frame update
    void Start()
    {
        state = STATE.START;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
