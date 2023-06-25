using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CrashController : MonoBehaviour
{
    [SerializeField] private ConfigurableJoint[] bodyJoints;
    [SerializeField] private ConfigurableJoint hipJoint;
    [SerializeField] private float normalSpringForceBody = 4000;
    [SerializeField] private float normalSpringForceHips = 100000;
    [SerializeField] private float crashForceSpring = 50f;
    [SerializeField] private int healthPoints= 3;
    [SerializeField] private float timeAfterHit = 3;
    
    
    public bool _hasCrash;
    public bool _isGameOver;
    public bool _recovering;
    public static CrashController Instance { get; private set; }

    private void Start()
    {
        Instance = this;
        _hasCrash = false;
        _isGameOver = false;
        _recovering = false;
    }

    private void Update()
    {
        if (healthPoints <= 0 && !_isGameOver)
        {
            _isGameOver = true;
            GameManager.Instance.GameOver();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Car") || _hasCrash || _isGameOver || _recovering) return;
        
        _hasCrash = true;
        _recovering = true;
            
        foreach (var joint in bodyJoints)
        {
            JointDrive jointDriveX = joint.angularXDrive;
            JointDrive jointDriveYZ = joint.angularYZDrive;
            jointDriveX.positionSpring = crashForceSpring;
            jointDriveYZ.positionSpring = crashForceSpring;
            joint.angularXDrive = jointDriveX;
            joint.angularYZDrive = jointDriveYZ;
        }
            
        JointDrive hipsJointDriveX = hipJoint.angularXDrive;
        JointDrive hipsJointDriveYZ = hipJoint.angularYZDrive;
        hipsJointDriveX.positionSpring = crashForceSpring;
        hipsJointDriveYZ.positionSpring = crashForceSpring;
        hipJoint.angularXDrive = hipsJointDriveX;
        hipJoint.angularYZDrive = hipsJointDriveYZ;

        healthPoints--;

        StartCoroutine(WaitTillFixed());
    }

    private IEnumerator WaitTillFixed()
    {
        yield return new WaitForSeconds(timeAfterHit);

        if (_isGameOver) yield break;
        
        foreach (var joint in bodyJoints)
        {
            JointDrive jointDriveX = joint.angularXDrive;
            JointDrive jointDriveYZ = joint.angularYZDrive;
            jointDriveX.positionSpring = normalSpringForceBody;
            jointDriveYZ.positionSpring = normalSpringForceBody;
            joint.angularXDrive = jointDriveX;
            joint.angularYZDrive = jointDriveYZ;
        }
            
        JointDrive hipsJointDriveX = hipJoint.angularXDrive;
        JointDrive hipsJointDriveYZ = hipJoint.angularYZDrive;
        hipsJointDriveX.positionSpring = normalSpringForceHips;
        hipsJointDriveYZ.positionSpring = normalSpringForceHips;
        hipJoint.angularXDrive = hipsJointDriveX;
        hipJoint.angularYZDrive = hipsJointDriveYZ;

        _hasCrash = false;

        StartCoroutine(Recovering());
    }

    private IEnumerator Recovering()
    {
        yield return new WaitForSeconds(timeAfterHit);
        
        if(_isGameOver) yield break;

        _recovering = false;
    }
}
