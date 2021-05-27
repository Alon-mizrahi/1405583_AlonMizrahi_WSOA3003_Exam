﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    /*
     * NB!!!!!
     Get the current mouse position with Input.mousePosition, then convert that position to world space using Camera.ScreenToWorldPoint. 
    */
    Vector3 startPos;
    Vector3 endPos;
    Vector3 direction;

    float power;
    float maxPower;

    float dircX;
    float dircY;
    float dircZ;

    bool CanAim = false;

    public GameObject TrajectoryLine;

    Rigidbody myRigidbody;

    DataDesingHandler DataHandler;

    private void Start()
    {
        //get variables from data handler script
        DataHandler = GameObject.FindGameObjectWithTag("GM").GetComponent<DataDesingHandler>();
        power = DataHandler.ShootPower;
        maxPower = DataHandler.MaxPower;
        myRigidbody = gameObject.GetComponent<Rigidbody>();

        TrajectoryLine.SetActive(false);
    }

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startPos.x = Input.mousePosition.x;
            startPos.y = 0;
            startPos.z = Input.mousePosition.y;
            //AimLine();
        }
    }

/*
private void AimLine()
    {

        TrajectoryLine.transform.position = this.transform.position; //startpos
        TrajectoryLine.SetActive(true);
        CanAim = true;
    }
*/






    private void OnMouseUp()
    {

        if (Input.GetMouseButtonUp(0))
        {
            endPos.x = Input.mousePosition.x;
            endPos.y = 0;
            endPos.z = Input.mousePosition.y;

            direction = startPos - endPos;

            myRigidbody.isKinematic = false;

            dircX = direction.x * power;
            dircY = direction.y * power;
            dircZ = direction.z * power;

            if (direction.x * power > maxPower)
            {
                dircX = maxPower;
            }
            if (direction.y * power > maxPower)
            {
                dircY = maxPower;
            }
            if (direction.z * power > maxPower)
            {
                dircZ = maxPower;
            }
            if (direction.x * power < -maxPower)
            {
                dircX = -maxPower;
            }
            if (direction.y * power < -maxPower)
            {
                dircY = -maxPower;
            }
            if (direction.z * power < -maxPower)
            {
                dircZ = -maxPower;
            }
            Debug.Log("force: " + dircX+", "+dircY + ", " +dircZ);

        
            myRigidbody.AddForce(dircX, dircY, dircZ);

            TrajectoryLine.SetActive(false);
            CanAim = false;
        }
        
    }


    private void Update()
    {
        if (CanAim == true)
        {
            
            //TrajectoryLine.transform.position.x = this.transform.position.x - Input.mousePosition.x;
            //TrajectoryLine.transform.position.z = gameObject.transform.position.z - Input.mousePosition.y;
            //TrajectoryLine.transform.position.y = 0;
        }
    }



}
