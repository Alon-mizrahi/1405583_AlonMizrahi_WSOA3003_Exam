using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{

    Vector3 startPos;
    Vector3 endPos;
    Vector3 direction;

    float power;
    float maxPower;

    float dircX;
    float dircY;
    float dircZ;

    Rigidbody myRigidbody;

    DataDesingHandler DataHandler;

    private void Start()
    {
        //get variables from data handler script
        DataHandler = GameObject.FindGameObjectWithTag("GM").GetComponent<DataDesingHandler>();
        power = DataHandler.ShootPower;
        maxPower = DataHandler.MaxPower;
        myRigidbody = gameObject.GetComponent<Rigidbody>();
    }

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startPos.x = Input.mousePosition.x;
            startPos.y = 0;
            startPos.z = Input.mousePosition.y;
        }
    }

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

            if (direction.x*power> maxPower)
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
        }
    }
}
