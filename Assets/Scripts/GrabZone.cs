using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabZone : MonoBehaviour
{
    public Animator targetAnimator;
    public GameObject grabbedObj;
    public Rigidbody rigidBody;
    public bool alreadyGrabbing = false;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (CrashController.Instance._hasCrash) return;
        
        if (Input.GetMouseButton(0))
        {
            targetAnimator.SetBool("Grabbing", true);

            if (grabbedObj != null && !alreadyGrabbing)
            {
                grabbedObj.transform.position = transform.position;
                
                FixedJoint fj = grabbedObj.AddComponent<FixedJoint>();
                fj.connectedBody = rigidBody;
                fj.breakForce = 100000;

                Collider collider = grabbedObj.GetComponent<Collider>();
                collider.isTrigger = true;

                alreadyGrabbing = true;
            }

        }

        if (Input.GetMouseButtonUp(0))
        {
            targetAnimator.SetBool("Grabbing", false);

            if (grabbedObj != null && alreadyGrabbing)
            {
                Destroy(grabbedObj.GetComponent<FixedJoint>());
                
                var collider = grabbedObj.GetComponent<Collider>();
                var rotationYObject = grabbedObj.transform.eulerAngles.y;
                
                grabbedObj.transform.rotation = Quaternion.Euler(0,rotationYObject, 0);
                collider.isTrigger = false;
                
                alreadyGrabbing = false;
            }

            grabbedObj = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Grabbable"))
        {
            grabbedObj = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Grabbable"))
        {
            grabbedObj = null;
        }
    }
}
