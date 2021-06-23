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
    public float PauseBoundary;

    public bool Follow = false;
    public float CamSnap=0.4f;
    private void Start()
    {
        GM = GameObject.FindWithTag("GM").GetComponent<GameManager>();
        StartArea = GameObject.FindWithTag("Respawn").GetComponent<Transform>();
        //PauseBoundary = boundary + 5f;
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



//if mouse on boundary move cam
//if player click go back to ball or thing
//if dont click can move around on player demand



    private void LateUpdate()
    {

        //if (Input.mousePosition.x > Screen.width - PauseBoundary && Input.GetMouseButton(0) == false && GM.state !=STATE.BALLROLLING || Input.mousePosition.x < 0 + PauseBoundary && Input.GetMouseButton(0) == false && GM.state != STATE.BALLROLLING || Input.mousePosition.y > Screen.height - PauseBoundary && Input.GetMouseButton(0) == false && GM.state != STATE.BALLROLLING || Input.mousePosition.y < 0 + PauseBoundary && Input.GetMouseButton(0) == false && GM.state != STATE.BALLROLLING)
        //{
        if (Input.GetMouseButton(0) == false && GM.state != STATE.BALLROLLING)
        { 
            if (Input.mousePosition.x > Screen.width - boundary)
            {
                Follow = false;
                transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
            }

            if (Input.mousePosition.x < 0 + boundary)
            {
                Follow = false;
                transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);
            }

            if (Input.mousePosition.y > Screen.height - boundary)
            {
                Follow = false;
                transform.position += new Vector3(0, 0, speed * Time.deltaTime);
            }

            if (Input.mousePosition.y < 0 + boundary)
            {
                Follow = false;
                transform.position -= new Vector3(0, 0, speed * Time.deltaTime);
            }
        }

        if (Input.GetMouseButtonDown(0) == true) { Follow = true; }

        //}else{
        if (Follow == true || GM.state == STATE.BALLROLLING)
        {
            CamFollow();
        }
            
        //}
    }


    void CamFollow()
    {
        SetTarget();
        Vector3 Direction = -gameObject.transform.position+ target.position + Offset;

        //Vector3 dir = transform.position - Target.position;
        //dir = dir.normalized;
        //Target.GetComponent<Rigidbody>().AddForce(dir * 0.5f);

        Direction = Direction.normalized;

        if (gameObject.transform.position != target.position + Offset && GM.state != STATE.BALLROLLING)
        {
            this.transform.position += 0.1f * Direction;

            if (gameObject.transform.position.x > target.position.x + Offset.x - CamSnap && gameObject.transform.position.x < target.position.x + Offset.x + CamSnap && gameObject.transform.position.z > target.position.z + Offset.z - CamSnap && gameObject.transform.position.z < target.position.z + Offset.z + CamSnap)
            {
                Debug.Log("SNAPP");
                transform.position = target.position + Offset;
            }

            
        }

        if (GM.state == STATE.BALLROLLING)
        {
            transform.position = target.position + Offset;
        }
        
    }



}
