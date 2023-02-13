using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollision : MonoBehaviour
{
    private bool rightFlag = false;

    private bool leftFlag = false;

    [HideInInspector]
    public bool contactFlag = false;
    void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision.gameObject.name + " entered.");
        if (collision.gameObject.name == "right_fingertip")
        {
            rightFlag = true;
        }

        if (collision.gameObject.name == "left_fingertip")
        {
            leftFlag = true;
        }

        if (rightFlag && leftFlag)
        {
            contactFlag = true;
            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<Rigidbody>().freezeRotation = true;
            this.transform.position =
                GameObject.Find("Nest").transform.position;
            this.transform.parent = GameObject.Find("Nest").transform;
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (contactFlag)
        {
            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<Rigidbody>().freezeRotation = true;
            this.transform.position =
                GameObject.Find("Nest").transform.position;
            this.transform.parent = GameObject.Find("Nest").transform;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "right_fingertip")
        {
            rightFlag = false;
            contactFlag = false;
        }

        if (collision.gameObject.name == "left_fingertip")
        {
            leftFlag = false;
            contactFlag = false;

        }

        //Debug.Log(collision.gameObject.name + " extited.");
        if (!contactFlag)
        {
            this.transform.parent = null;
            GetComponent<Rigidbody>().useGravity = true;
            GetComponent<Rigidbody>().freezeRotation = false; 
        }
    }

}
