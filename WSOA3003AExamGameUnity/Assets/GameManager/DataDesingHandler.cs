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
    PhysicMaterial BallPhysicsMat;
    public float BallDynamicFriction;
    public float BallStaticFriction;
    public float BallBounciness;

    // Start is called before the first frame update
    void Start()
    {
        //TargetBall
        GameObject.FindGameObjectWithTag("TargetBall").GetComponent<Rigidbody>().mass = BallMass;
        BallPhysicsMat = GameObject.FindGameObjectWithTag("TargetBall").GetComponent<SphereCollider>().material;
        BallPhysicsMat.dynamicFriction = BallDynamicFriction;
        BallPhysicsMat.bounciness = BallBounciness;
        BallPhysicsMat.staticFriction = BallStaticFriction;
        //PowerBall


        //BasicLevelElements


    }

    // Update is called once per frame
    void Update()
    {

    }
}
