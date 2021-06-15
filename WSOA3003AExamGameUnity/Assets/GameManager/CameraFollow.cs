using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //for camera follow
    Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 Offset;

    GameManager GM;
    Transform StartArea;
    Transform TargetBall;
    Transform PowerBall;

    //for panning
    public float speed;
    public float boundary;

    private void Start()
    {
        GM = GameObject.FindWithTag("GM").GetComponent<GameManager>();
        StartArea = GameObject.FindWithTag("Respawn").GetComponent<Transform>();
    }

    //when placing and shooting target ball, set as target, else power ball is target
    void SetTarget()
    {
        if (GM.state == STATE.PLACETARGETBALL) { target = StartArea; }/*target is start area*/
        if (GM.state == STATE.SHOOTTARGETBALL) { GetTarget(); target = TargetBall; }/*target is target ball*/
        if (GM.state == STATE.PLACEPOWERBALL) { target = StartArea; }/*target is start area*/
        if (GM.state == STATE.CANSHOOTPOWERBALL) { GetPower(); target = PowerBall; }/*target is PowerBall*/
        //if ball rolling can either change fov to fit both balls or follow power ball??

    }

    public void GetTarget() { TargetBall = GameObject.FindGameObjectWithTag("TargetBall").GetComponent<Transform>(); }
    public void GetPower() { PowerBall = GameObject.FindGameObjectWithTag("PowerBall").GetComponent<Transform>(); }




    private void LateUpdate()
    {

        if (Input.mousePosition.x > Screen.width - boundary || Input.mousePosition.x < 0 + boundary || Input.mousePosition.y > Screen.height - boundary || Input.mousePosition.y < 0 + boundary)
        {
            if (Input.mousePosition.x > Screen.width - boundary)
            {
                transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
            }

            if (Input.mousePosition.x < 0 + boundary)
            {
                transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);
            }

            if (Input.mousePosition.y > Screen.height - boundary)
            {
                transform.position += new Vector3(0, 0, speed * Time.deltaTime);
            }

            if (Input.mousePosition.y < 0 + boundary)
            {
                transform.position -= new Vector3(0, 0, speed * Time.deltaTime);
            }
        }else{
            CamFollow();
        }
    }


    void CamFollow()
    {
        SetTarget();
        transform.position = target.position + Offset;
    }



}
