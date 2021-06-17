using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelleportScript : MonoBehaviour
{
    Transform Partner;
    GameObject[] PartnerObj;
    public Material Mat1;
    public Material Mat2;
    public Material Mat3;

    public int ID;

    public bool Triggered = false;

    // Start is called before the first frame update
    void Start()
    {
        //set colour indicator
        if (ID == 1) { gameObject.GetComponent<MeshRenderer>().material = Mat1; }
        else if (ID == 2) { gameObject.GetComponent<MeshRenderer>().material = Mat2; }
        else if (ID == 3) { gameObject.GetComponent<MeshRenderer>().material = Mat3; }

        //get partner

        PartnerObj = GameObject.FindGameObjectsWithTag("Teleport");

        for (int i = 0; i < PartnerObj.Length; i++)
        {
            if (PartnerObj[i].GetComponent<TelleportScript>().ID == this.ID && PartnerObj[i] != gameObject)
            {
                Partner = PartnerObj[i].GetComponent<Transform>();
                //Debug.Log("This ID: " + this.ID + " Partner ID: " + PartnerObj[i].GetComponent<TelleportScript>().ID);
            }
        }
        //Debug.Log(Partner.GetComponent<TelleportScript>().Triggered = false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Partner.GetComponent<TelleportScript>().Triggered != true)
        {
            if (other.tag == "TargetBall" || other.tag =="PowerBall")
            {
                other.gameObject.transform.position = new Vector3(Partner.position.x, Partner.position.y+0.8f, Partner.position.z);
                Triggered = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "TargetBall" || other.tag == "PowerBall")
        {
            Partner.GetComponent<TelleportScript>().Triggered = false;
        }
    }
}
