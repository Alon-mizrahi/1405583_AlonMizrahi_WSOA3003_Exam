using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataDesingHandler : MonoBehaviour
{
    //This script is used to handle the data design of the game objects
    //It is used to centralise all data
    // should be attached to GameManager object, singal instance
    //

    //TARGET/POWER BALL DATA
    public float ShootPower;
    public float MaxPower;
    public float BallMass;
    public float BallDrag;
    public float BallAngularDrag;
    PhysicMaterial BallPhysicsMat;
    public float BallDynamicFriction;
    public float BallStaticFriction;
    public float BallBounciness;

    //public float LrLength;
    public float LrMaxLength;
    public float LrScale;

    public Vector3 NotMoving;

    GameObject TargetBall;
    GameObject PowerBall;
    

    // Start is called before the first frame update
    void Start()
    {
        //BasicLevelElements
    }





//TargetBall Data Update
    public void UpdateTargetData()
    {
        Debug.Log("Start function called when instantiated");
        
        TargetBall = GameObject.FindGameObjectWithTag("TargetBall");

        TargetBall.GetComponent<Rigidbody>().mass = BallMass;
        TargetBall.GetComponent<SphereCollider>().material.dynamicFriction = BallDynamicFriction;
        TargetBall.GetComponent<SphereCollider>().material.staticFriction = BallStaticFriction;
        TargetBall.GetComponent<SphereCollider>().material.bounciness = BallBounciness;

        TargetBall.GetComponent<Rigidbody>().drag = BallDrag;
        TargetBall.GetComponent<Rigidbody>().angularDrag = BallAngularDrag;

        //Power
        TargetBall.GetComponent<BallController>().power = ShootPower;
        TargetBall.GetComponent<BallController>().maxPower = MaxPower;

        //Line renderer
        TargetBall.GetComponent<BallController>().LrMaxLength = LrMaxLength;
        TargetBall.GetComponent<BallController>().LrScale = LrScale;
    }

    //PowerBall Data Update
    public void UpdatePowerData()
    {
        Debug.Log("Start function called when instantiated");

        PowerBall = GameObject.FindGameObjectWithTag("PowerBall");

        PowerBall.GetComponent<Rigidbody>().mass = BallMass;
        PowerBall.GetComponent<SphereCollider>().material.dynamicFriction = BallDynamicFriction;
        PowerBall.GetComponent<SphereCollider>().material.staticFriction = BallStaticFriction;
        PowerBall.GetComponent<SphereCollider>().material.bounciness = BallBounciness;

        PowerBall.GetComponent<Rigidbody>().drag = BallDrag;
        PowerBall.GetComponent<Rigidbody>().angularDrag = BallAngularDrag;

        //Power
        PowerBall.GetComponent<BallController>().power = ShootPower;
        PowerBall.GetComponent<BallController>().maxPower = MaxPower;

        //Line renderer
        PowerBall.GetComponent<BallController>().LrMaxLength = LrMaxLength;
        PowerBall.GetComponent<BallController>().LrScale = LrScale;

    }



}
