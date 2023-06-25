using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyMotion : MonoBehaviour
{
    [SerializeField] private Transform targetLimb;
    private ConfigurableJoint m_ConfigurableJoint;
    
    private Quaternion targetInitialRotation;

    private void Start()
    {
        this.m_ConfigurableJoint = this.GetComponent<ConfigurableJoint>();
        this.targetInitialRotation = this.targetLimb.transform.localRotation;
    }

    private void FixedUpdate()
    {
        if (!CrashController.Instance._hasCrash)
        {
            this.m_ConfigurableJoint.targetRotation = CopyRotation();
        }
    }

    private Quaternion CopyRotation()
    {
        return Quaternion.Inverse(this.targetLimb.localRotation) * this.targetInitialRotation;
    }
}
