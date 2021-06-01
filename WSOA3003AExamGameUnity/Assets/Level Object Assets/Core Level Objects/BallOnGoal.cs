using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallOnGoal : MonoBehaviour
{
    public bool isTargetTouchingGoal = false;
    public bool isPowerTouchingGoal = false;

    private void OnTriggerEnter(Collider Ball)
    {
        if(Ball.tag == "TargetBall")
        {
            isTargetTouchingGoal = true;
        }
        if (Ball.tag == "PowerBall")
        {
            isPowerTouchingGoal = true;
        }
    }

    private void OnTriggerExit(Collider Ball)
    {
        if (Ball.tag == "TargetBall")
        {
            isTargetTouchingGoal = false;
        }
        if (Ball.tag == "PowerBall")
        {
            isPowerTouchingGoal = false;
        }
    }
}
