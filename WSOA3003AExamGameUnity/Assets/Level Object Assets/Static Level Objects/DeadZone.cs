using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    GameManager GM;

    private void Start()
    {
        GM = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
    }

    private void OnTriggerEnter(Collider Ball)
    {
        if (Ball.gameObject.tag == "TargetBall" || Ball.gameObject.tag == "PowerBall")
        {
            Destroy(Ball.gameObject);
            GM.Deadzone();
        }
    }

}
