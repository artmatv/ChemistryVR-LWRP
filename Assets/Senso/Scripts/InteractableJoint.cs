using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    FixedJoint joint;
    Rigidbody rb;
    Rigidbody thisrb;
    Vector3 oldPos;
    public bool Grabbed;
    public bool Pinched;
    [HideInInspector] public Gestures gesture;
    [HideInInspector] public SensoHandExample SensoHandExample;

    [Header("Type of Interaction")]
    public bool Grab;
    public bool Pinch;

    void Update()
    {
        if (gesture != null && ((!gesture.grab && Grabbed) || (!gesture.pinch && Pinched)))
            Clear();
        
            //oldPos = transform.position;
    }

    void FixedUpdate()
    {
        oldPos = transform.position;
    }

    void Start()
    {
        if (thisrb == null)
            thisrb = gameObject.GetComponent<Rigidbody>();
    }


    public void OnCollisionStay(Collision col)
    {
        if (col.gameObject.GetComponentInParent<SensoHandExample>())
        {
            if (!Grabbed && !Pinched)
            {
                SensoHandExample = col.gameObject.GetComponentInParent<SensoHandExample>();
                gesture = SensoHandExample.gameObject.GetComponent<Gestures>();
            }
            if (!gesture.PinchedOrGrabbed)
            {
                if (((SensoHandExample.HandType == Senso.EPositionType.RightHand) || (SensoHandExample.HandType == Senso.EPositionType.LeftHand)) && gesture.grab && Grab && col.gameObject.tag == "InteractableHand")
                {
                    if (Grabbed == false)
                    {
                        if ((rb = col.gameObject.GetComponent<Rigidbody>()) != null)
                        {
                            rb.isKinematic = true;
                            rb.useGravity = false;
                        }

                        else
                        {
                            rb = col.gameObject.AddComponent<Rigidbody>();
                            rb.isKinematic = true;
                            rb.useGravity = false;
                        }
                        CreateJoint(col.gameObject);
                        Grabbed = true;
                        gesture.PinchedOrGrabbed = true;
                    }
                }

                else if (col.gameObject.tag == "InteractableFinger" && gesture.pinch && Pinch)
                {
                    if (Pinched == false)
                    {
                        if ((rb = col.gameObject.GetComponent<Rigidbody>()) != null)
                        {
                            rb.isKinematic = true;
                            rb.useGravity = false;
                        }

                        else
                        {
                            rb = col.gameObject.AddComponent<Rigidbody>();
                            rb.isKinematic = true;
                            rb.useGravity = false;
                        }
                        CreateJoint(col.gameObject);
                        Pinched = true;
                        gesture.PinchedOrGrabbed = true;
                    }
                }

            }


        }
    }

    void CreateJoint(GameObject col)
    {
        if ((rb = gameObject.gameObject.GetComponent<Rigidbody>()) == null)
        {
            thisrb.useGravity = false;
            thisrb.isKinematic = false;
            joint = gameObject.AddComponent<FixedJoint>();
            joint.connectedBody = col.GetComponent<Rigidbody>();
            //joint.transform.position = col.transform.position;
            //transform.position = col.transform.position;
        }
    }

    void Clear()
    {
        Destroy(joint);
        thisrb.useGravity = true;
        Grabbed = false;
        Pinched = false;
        rb = null;
        gesture.PinchedOrGrabbed = false;
        thisrb.AddForce((oldPos - transform.position) * Time.deltaTime * 100);
    }
}
