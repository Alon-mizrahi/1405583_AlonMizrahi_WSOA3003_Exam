using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//State Machine states
public enum STATE { PLACETARGETBALL, SHOOTTARGETBALL, PLACEPOWERBALL, CANSHOOTPOWERBALL, BALLROLLING, BALLSTOPPED, WON, LOST }
public enum PREVIOUSSTATE { PLACETARGETBALL, SHOOTTARGETBALL, PLACEPOWERBALL, CANSHOOTPOWERBALL, BALLROLLING, BALLSTOPPED , WON, LOST }

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
    public PREVIOUSSTATE previousState;
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

    public int SetPar = 0;//not sure how to use this yet??
    
    //UI Elements
    public float shootCounter;
    public Text shoottxt;
    public Text TurnIndicator;
    public Text ParDisplay; // three par displays all equal. 

    //Won and LOST state display
    public GameObject WonDisplay;
    public Text FinalScore;
    public Text FinalShotCount;
    public Text FinalWPar;

    public GameObject LostDisplay;
    public Text FinalLPar;


    public GameObject PowerBall;
    public GameObject TargetBall;
    bool gotPBall = false;
    bool gotTBall = false;

    Vector3 NotMoving;
    DataDesingHandler DataHandler;
    public BallOnGoal Goal;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        //state = STATE.START; if there is menu
        DataHandler = gameObject.GetComponent<DataDesingHandler>();
        state = STATE.PLACETARGETBALL;
        previousState = PREVIOUSSTATE.PLACETARGETBALL;
        NotMoving = DataHandler.NotMoving;
        shootCounter = 0;

        Goal = GameObject.FindGameObjectWithTag("GoalBlock").GetComponent<BallOnGoal>();

        WonDisplay.SetActive(false);
        LostDisplay.SetActive(false);

        //SetPar
        ParDisplay.text = "Par: " + SetPar;
        FinalLPar.text = "Par: " + SetPar;
        FinalWPar.text = "Par: " + SetPar;

    }


    // Update is called once per frame
    void Update()
    {
        UpdateUI();
        isMoving();
        MainLoop();
    }

    //not so efficient. should place where needed
    void UpdateUI()
    {
        shoottxt.text = "" + shootCounter;

        //Turn Indicator text
        if(state == STATE.BALLROLLING) { TurnIndicator.text = ""; }
        if (state == STATE.PLACETARGETBALL) { TurnIndicator.text = "Place the target ball"; }
        if (state == STATE.PLACEPOWERBALL) { TurnIndicator.text = "Place the power ball"; }
        if (state == STATE.SHOOTTARGETBALL) { TurnIndicator.text = "Set target ball"; }
        if (state == STATE.CANSHOOTPOWERBALL) { TurnIndicator.text = "Shoot power ball"; }



    }

    public void getPowerBall()
    {
        PowerBall = GameObject.FindGameObjectWithTag("PowerBall");
        gotPBall = true;
        previousState = PREVIOUSSTATE.CANSHOOTPOWERBALL;
    }
    public void getTargetBall()
    {
        TargetBall = GameObject.FindGameObjectWithTag("TargetBall");
        gotTBall = true;
        previousState = PREVIOUSSTATE.SHOOTTARGETBALL;
    }

    void isMoving()
    {
        if(gotTBall == true)
        {
            //Debug.Log("Velocity: "+TargetBall.GetComponent<Rigidbody>().velocity);
            if(TargetBall.GetComponent<Rigidbody>().velocity.x > NotMoving.x || TargetBall.GetComponent<Rigidbody>().velocity.y > NotMoving.y || TargetBall.GetComponent<Rigidbody>().velocity.z > NotMoving.z)
            {
                state = STATE.BALLROLLING;
            }
            if (TargetBall.GetComponent<Rigidbody>().velocity.x < -NotMoving.x || TargetBall.GetComponent<Rigidbody>().velocity.y < -NotMoving.y || TargetBall.GetComponent<Rigidbody>().velocity.z < -NotMoving.z)
            {
                state = STATE.BALLROLLING;
            }
            if (Goal.isTargetTouchingGoal == false && state == STATE.BALLROLLING && previousState == PREVIOUSSTATE.SHOOTTARGETBALL && TargetBall.GetComponent<Rigidbody>().velocity.x < NotMoving.x && TargetBall.GetComponent<Rigidbody>().velocity.y < NotMoving.y && TargetBall.GetComponent<Rigidbody>().velocity.z < NotMoving.z && TargetBall.GetComponent<Rigidbody>().velocity.x > -NotMoving.x && TargetBall.GetComponent<Rigidbody>().velocity.y > -NotMoving.y && TargetBall.GetComponent<Rigidbody>().velocity.z > -NotMoving.z)
            {
                Debug.Log("ball stopped");
                //TargetBall.GetComponent<Rigidbody>().isKinematic = true;
                state = STATE.BALLSTOPPED;
                //TargetBall.GetComponent<Rigidbody>().isKinematic = false;
            }
        }
        if(gotPBall == true)
        {
            gotTBall = false;
            if (PowerBall.GetComponent<Rigidbody>().velocity.x > NotMoving.x || PowerBall.GetComponent<Rigidbody>().velocity.y > NotMoving.y || PowerBall.GetComponent<Rigidbody>().velocity.z > NotMoving.z || PowerBall.GetComponent<Rigidbody>().velocity.x < -NotMoving.x || PowerBall.GetComponent<Rigidbody>().velocity.y < -NotMoving.y || PowerBall.GetComponent<Rigidbody>().velocity.z < -NotMoving.z)
            {
                state = STATE.BALLROLLING;
            }
            //if (PowerBall.GetComponent<Rigidbody>().velocity.x < -NotMoving.x || PowerBall.GetComponent<Rigidbody>().velocity.y < -NotMoving.y || PowerBall.GetComponent<Rigidbody>().velocity.z < -NotMoving.z)
            //{
            //    state = STATE.BALLROLLING;
            //}
            if (Goal.isPowerTouchingGoal == false && Goal.isTargetTouchingGoal == false && state == STATE.BALLROLLING && previousState == PREVIOUSSTATE.CANSHOOTPOWERBALL && TargetBall.GetComponent<Rigidbody>().velocity.x < NotMoving.x && TargetBall.GetComponent<Rigidbody>().velocity.y < NotMoving.y && TargetBall.GetComponent<Rigidbody>().velocity.z < NotMoving.z && PowerBall.GetComponent<Rigidbody>().velocity.x < NotMoving.x && PowerBall.GetComponent<Rigidbody>().velocity.y < NotMoving.y && PowerBall.GetComponent<Rigidbody>().velocity.z < NotMoving.z && TargetBall.GetComponent<Rigidbody>().velocity.x > -NotMoving.x && TargetBall.GetComponent<Rigidbody>().velocity.y > -NotMoving.y && TargetBall.GetComponent<Rigidbody>().velocity.z > -NotMoving.z && PowerBall.GetComponent<Rigidbody>().velocity.x > -NotMoving.x && PowerBall.GetComponent<Rigidbody>().velocity.y > -NotMoving.y && PowerBall.GetComponent<Rigidbody>().velocity.z > -NotMoving.z)
            {
                Debug.Log("both balls stopped");
                //TargetBall.GetComponent<Rigidbody>().isKinematic = true;
                //PowerBall.GetComponent<Rigidbody>().isKinematic = true;
                state = STATE.BALLSTOPPED;
                //TargetBall.GetComponent<Rigidbody>().isKinematic = false;
               // PowerBall.GetComponent<Rigidbody>().isKinematic = false;
            }
        }

    }



    //main loop:
    /*
     * STATE:                       PREVIOUSSTATE:
     * place target ball            place Target ball           -->     player can place target ball
     * Shoot target ball            place Target ball           -->     player can shoot the target ball
     * ball rolling                 shoot Target ball           -->     player must wait for ball to stop rolling
     * ball stopped                 shoot target ball           -->     must change states to place power ball
     * place power ball             shoot Target ball           -->     player can place power ball
     * ------------------
     * main loop:
     * Shoot power ball             place power ball            -->     player can shoot power ball
     * ball rolling                 shoot power ball            -->     player must wait for balls to stop rolling
     * ball stopped                 shoot target ball           -->     must change states to shoot power ball
     * Shoot power ball             Shoot power ball            -->     player can shoot power ball
     * ------------------ 
    */

    void MainLoop()
    {
        if ( state == STATE.BALLSTOPPED && previousState == PREVIOUSSTATE.SHOOTTARGETBALL)
        {
            state = STATE.PLACEPOWERBALL;
        }
        if(state == STATE.BALLSTOPPED && previousState == PREVIOUSSTATE.CANSHOOTPOWERBALL)
        {
            state = STATE.CANSHOOTPOWERBALL;
        }
    }


    //if power ball should reset powerball? increment shott count
    public void Deadzone()
    {
        Debug.Log("Deadzone Activated");
        Lost();
    }




    public void Lost()
    {
        Time.timeScale = 0f;
        Debug.Log("Lost");
        state = STATE.LOST;
        LostDisplay.SetActive(true);

    }

    public void Won()
    {
        Time.timeScale = 0f;
        gotTBall = false;
        Debug.Log("WON");
        state = STATE.WON;
        FinalShotCount.text = "Shots: " + shootCounter;
        FinalScore.text = "poop";
        WonDisplay.SetActive(true);

    }


    //UI Button functions-----------------

    public void NextLevel()
    {
        Debug.Log("CLICKED NEXT");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ReplayLevel()
    {
        Debug.Log("CLICKED REPLAY");
        Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
    }

    public void BackToMenu()
    {
        Debug.Log("CLICKED MENU");
        //SceneManager.LoadScene(SceneManager.GetSceneByBuildIndex(buildIndex 0));
        SceneManager.LoadScene("StartMenu");
    }



}
