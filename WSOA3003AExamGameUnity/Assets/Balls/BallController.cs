using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    /// <summary>
    /// Notes:
    /// still need to do drag and shoot not point and shoot. ie invert axes of trline and direction to shoot. 
    /// also need to do scaling of trline
    /// </summary>


    Rigidbody Rb;
    LineRenderer lr;

    Vector3 StartPos;
    Vector3 EndPos;
    Vector3 Direction;
    Vector3 ApplyVect;
    Vector3 LrEndpoint;

    public float LrLength;
    public float LrMaxLength;
    public float LrScale;

    public DataDesingHandler DataHandler;
    GameManager GM;
    STATE PreviousSTATE;
    public float power;
    public float maxPower;

    bool ClickedToShoot=false;

    Vector3 NotMoving = new Vector3(0, 0, 0);

    private void Start()
    {
        Rb = gameObject.GetComponent<Rigidbody>();
        lr = gameObject.GetComponent<LineRenderer>();
        lr.enabled = false;
        GM = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
        DataHandler = GameObject.FindGameObjectWithTag("GM").GetComponent<DataDesingHandler>();

        PreviousSTATE = GM.state;

        if(gameObject.tag == "TargetBall") {DataHandler.UpdateTargetData(); }
        if(gameObject.tag == "PowerBall") {DataHandler.UpdatePowerData(); }
    }


    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0) && GM.state == STATE.SHOOTTARGETBALL && gameObject.tag =="TargetBall" && Rb.velocity == NotMoving)
        {
            StartPos.x = gameObject.transform.position.x;
            StartPos.y = gameObject.transform.position.y;
            StartPos.z = gameObject.transform.position.z;
            lr.SetPosition(0, StartPos);
            lr.SetPosition(1, StartPos);
            lr.enabled = true;
            ClickedToShoot = true;
        }

        if (Input.GetMouseButtonDown(0) && GM.state == STATE.CANSHOOTPOWERBALL && gameObject.tag == "PowerBall" && Rb.velocity == NotMoving)
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

    private void OnMouseUp()
    {
        if (Input.GetMouseButtonUp(0) && GM.state == STATE.SHOOTTARGETBALL && gameObject.tag == "TargetBall" && Rb.velocity == NotMoving)
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

        if (Input.GetMouseButtonUp(0) && GM.state == STATE.CANSHOOTPOWERBALL && gameObject.tag == "PowerBall" && Rb.velocity == NotMoving)
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
            if (Input.GetMouseButton(0) && GM.state == STATE.SHOOTTARGETBALL && gameObject.tag == "TargetBall" && Rb.velocity == NotMoving)
            {
                Vector3 mousePos = new Vector3(Input.mousePosition.x, gameObject.transform.position.y, Input.mousePosition.y);
                //End Point of Line renderer
                EndPos.x = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
                EndPos.y = gameObject.transform.position.y;
                EndPos.z = Camera.main.ScreenToWorldPoint(Input.mousePosition).z;

                //Get direction for force to be applied
                Direction.x = (EndPos.x - StartPos.x);
                Direction.z = (EndPos.z - StartPos.z);
                Direction.y = 0;

                //to scale line renderer multiply endpos by power or scaling factor

                LrEndpoint = new Vector3(EndPos.x, EndPos.y, EndPos.z);//do i make x,z -ve?

                //LrEndpoint. = LrScale;

                lr.SetPosition(1, LrEndpoint);//*LrScale
            }

            if (Input.GetMouseButton(0) && GM.state == STATE.CANSHOOTPOWERBALL && gameObject.tag == "PowerBall" && Rb.velocity == NotMoving)
            {
                Vector3 mousePos = new Vector3(Input.mousePosition.x, gameObject.transform.position.y, Input.mousePosition.y);
                //End Point of Line renderer
                EndPos.x = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
                EndPos.y = gameObject.transform.position.y;
                EndPos.z = Camera.main.ScreenToWorldPoint(Input.mousePosition).z;

                //Get direction for force to be applied
                Direction.x = (EndPos.x - StartPos.x);
                Direction.z = (EndPos.z - StartPos.z);
                Direction.y = 0;

                //to scale line renderer multiply endpos by power or scaling factor

                LrEndpoint = new Vector3(EndPos.x, EndPos.y, EndPos.z);//do i make x,z -ve?

                //LrEndpoint. = LrScale;

                lr.SetPosition(1, LrEndpoint);//*LrScale
            }

        }


        //Ball rolling state. so that dont make any other moves while a shot is happening
        //need to emend to include state after a set shot
        //can this be used for power ball??
        //if (PreviousSTATE != STATE.START && Rb.velocity != NotMoving)
        //{
        //    GM.state = STATE.BALLROLLING;
        //}

    }





}

