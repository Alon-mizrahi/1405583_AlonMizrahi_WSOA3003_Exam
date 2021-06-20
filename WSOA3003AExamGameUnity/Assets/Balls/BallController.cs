using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    /// <summary>
    /// Notes:
    /// still need to do drag and shoot not point and shoot. ie invert axes of trline and direction to shoot. 
    /// also need to do scaling of trline
    /// Friction should use drag and angular drag
    /// </summary>


    Rigidbody Rb;
    LineRenderer lr;

    Vector3 StartPos;
    Vector3 EndPos;
    Vector3 Direction;
    Vector3 ApplyVect;
    Vector3 LrEndpoint;

    //public float LrLength;
    public float LrMaxLength;
    public float LrScale;

    public DataDesingHandler DataHandler;
    GameManager GM;

    public float power;
    public float maxPower;


    bool ClickedToShoot=false;
    public bool isOnStartBlock;

    private void Start()
    {
        Rb = gameObject.GetComponent<Rigidbody>();
        lr = gameObject.GetComponent<LineRenderer>();
        lr.enabled = false;
        GM = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
        DataHandler = GameObject.FindGameObjectWithTag("GM").GetComponent<DataDesingHandler>();


        if(gameObject.tag == "TargetBall") {DataHandler.UpdateTargetData(); }
        if(gameObject.tag == "PowerBall")
        {
            DataHandler.UpdatePowerData();
        }

    }

    //clicked ball
    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0) && GM.state == STATE.SHOOTTARGETBALL && gameObject.tag =="TargetBall")
        {
            StartPos.x = gameObject.transform.position.x;
            StartPos.y = gameObject.transform.position.y;
            StartPos.z = gameObject.transform.position.z;
            lr.SetPosition(0, StartPos);
            lr.SetPosition(1, StartPos);
            lr.enabled = true;
            ClickedToShoot = true;
        }

        if (Input.GetMouseButtonDown(0) && GM.state == STATE.CANSHOOTPOWERBALL && gameObject.tag == "PowerBall")
        {
            StartPos.x = gameObject.transform.position.x;
            StartPos.y = gameObject.transform.position.y;
            StartPos.z = gameObject.transform.position.z;
            lr.SetPosition(0, StartPos);
            lr.SetPosition(1, StartPos);
            lr.enabled = true;
            ClickedToShoot = true;
        }

    }

    //mouse up -> apply force based off of mouse up mouse pos
    private void OnMouseUp()
    {
        if (Input.GetMouseButtonUp(0) && GM.state == STATE.SHOOTTARGETBALL && gameObject.tag == "TargetBall")
        {
            ClickedToShoot = false;
            lr.enabled = false;
            GM.shootCounter++;

            ApplyVect = Direction*power;//make this -ve if want oposite to pull

            if (Direction.x * power > maxPower)
            {
                ApplyVect.x = maxPower;
            }
            if (Direction.z * power > maxPower)
            {
                ApplyVect.z = maxPower;
            }
            if (Direction.x * power < -maxPower)
            {
                ApplyVect.x = -maxPower;
            }
            if (Direction.z * power < -maxPower)
            {
                ApplyVect.z = -maxPower;
            }

            //Debug.Log("Mouse to screen world: " + Camera.main.ScreenToWorldPoint(Input.mousePosition));
            Debug.Log("Power: "+ApplyVect);
            Rb.AddForce(ApplyVect);
        }

        if (Input.GetMouseButtonUp(0) && GM.state == STATE.CANSHOOTPOWERBALL && gameObject.tag == "PowerBall")
        {
            ClickedToShoot = false;
            lr.enabled = false;

            GM.shootCounter++;

            ApplyVect = Direction * power;//make this -ve if want oposite to pull

            if (Direction.x * power > maxPower)
            {
                ApplyVect.x = maxPower;
            }
            if (Direction.z * power > maxPower)
            {
                ApplyVect.z = maxPower;
            }
            if (Direction.x * power < -maxPower)
            {
                ApplyVect.x = -maxPower;
            }
            if (Direction.z * power < -maxPower)
            {
                ApplyVect.z = -maxPower;
            }

            //Debug.Log("Mouse to screen world: " + Camera.main.ScreenToWorldPoint(Input.mousePosition));
            Debug.Log("Power: " + ApplyVect);
            Rb.AddForce(ApplyVect);
        }
    }


    private void Update()
    {
        if (ClickedToShoot == true)
        {
            if (Input.GetMouseButton(0) && GM.state == STATE.SHOOTTARGETBALL && gameObject.tag == "TargetBall")
            {
                Vector3 mousePos = new Vector3(Input.mousePosition.x, gameObject.transform.position.y, Input.mousePosition.y);
                //End Point of Line renderer
                EndPos.x = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
                EndPos.y = gameObject.transform.position.y;
                EndPos.z = Camera.main.ScreenToWorldPoint(Input.mousePosition).z;

                //Get direction for force to be applied
                //Invert for drag
                Direction.x = (StartPos.x - EndPos.x);
                Direction.z = (StartPos.z - EndPos.z);
                Direction.y = 0;


                //to scale line renderer multiply endpos by power or scaling factor

                LrEndpoint = new Vector3(2 * StartPos.x - EndPos.x, EndPos.y, 2 * StartPos.z - EndPos.z);

                lr.SetPosition(1, LrEndpoint);
            }

            if (Input.GetMouseButton(0) && GM.state == STATE.CANSHOOTPOWERBALL && gameObject.tag == "PowerBall")
            {
                Vector3 mousePos = new Vector3(Input.mousePosition.x, gameObject.transform.position.y, Input.mousePosition.y);
                //End Point of Line renderer
                EndPos.x = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
                EndPos.y = gameObject.transform.position.y;
                EndPos.z = Camera.main.ScreenToWorldPoint(Input.mousePosition).z;

                //Get direction for force to be applied
                //Invert for drag
                Direction.x = (StartPos.x - EndPos.x);
                Direction.z = (StartPos.z - EndPos.z);
                Direction.y = 0;

                //to scale line renderer multiply endpos by power or scaling factor

                LrEndpoint = new Vector3(2 * StartPos.x - EndPos.x, EndPos.y, 2 * StartPos.z - EndPos.z);

                lr.SetPosition(1, LrEndpoint);

            }

        }

        if (GM.state == STATE.SHOOTTARGETBALL && gameObject.tag == "TargetBall" && isOnStartBlock ==false)
        {
            Debug.Log("TARGET BALL STATE CHANGE SHOULD NOW CHANGE TO CAN PLACE POWERBALL");
            GM.state = STATE.PLACEPOWERBALL;
        }

    }


    public void OnStartBlock(bool OnStartBlock)
    {
        isOnStartBlock = OnStartBlock;
        Debug.Log("is on start block: " + isOnStartBlock);
    }




}

