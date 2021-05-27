using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBlock : MonoBehaviour
{

    public GameObject TargetBall;
    GameManager GM;

    Vector3 PlacementPos;
    Vector3 MousePos;

    // Start is called before the first frame update
    void Start()
    {
        GM = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
    }


    void PlaceBall()
    {
        Debug.Log("clicked start block");

        MousePos = Input.mousePosition;
        PlacementPos.x = Camera.main.ScreenToWorldPoint(MousePos).x;
        PlacementPos.y = this.transform.position.y + 1f;
        PlacementPos.z = Camera.main.ScreenToWorldPoint(MousePos).z;

        Debug.Log("mousePosX: " + PlacementPos.x + " mousePosY: " + PlacementPos.y + " mousePosZ: " + PlacementPos.z);
        Debug.Log("StartBlockPosX: " + this.transform.position.x + " StartBlockPosY: " + this.transform.position.y + " StartBlockPosZ: " + this.transform.position.z);

        Instantiate(TargetBall, PlacementPos, Quaternion.identity);
    }

    private void OnMouseDown()
    {
        if(Input.GetMouseButtonDown(0) &&  GM.state == STATE.START )
        {
            PlaceBall();
            GM.state = STATE.CANSHOOT;
        }
    }


    // Update is called once per frame
    void Update()
    {

    }




}
