using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnStart : MonoBehaviour
{

    public bool OnStartBlock = false;
    public GameObject TargetBall;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TargetBall = GameObject.FindGameObjectWithTag("TargetBall");
    }


    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "TargetBall")
        {
            OnStartBlock = true;
            TargetBall.GetComponent<BallController>().OnStartBlock(OnStartBlock);
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "TargetBall")
        {
            OnStartBlock = false;
            TargetBall.GetComponent<BallController>().OnStartBlock(OnStartBlock);
        }
    }










}
