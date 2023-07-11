using System;
using System.Collections;
using System.Collections.Generic;
using Game_Manager;
using HP_System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class CrashController : MonoBehaviour
{
    [SerializeField] private ConfigurableJoint[] bodyJoints;
    [SerializeField] private ConfigurableJoint hipJoint;
    [SerializeField] private float normalSpringForceBody = 4000;
    [SerializeField] private float normalSpringForceHips = 100000;
    [SerializeField] private float crashForceSpring = 50f;
    [SerializeField] private int crashPoints= 3;
    [SerializeField] private float timeAfterHit = 3;
    
    
    public bool hasCrash;
    public bool recovering;
    public static CrashController Instance { get; private set; }

    private void Start()
    {
        Instance = this;
        hasCrash = false;
        recovering = false;
    }

    private void Update()
    {
        if (crashPoints <= 0 && !HealthSystem.Instance.IsGameOver)
        {
            Restart();
        }
        
    }

    private void Restart()
    {
        HealthSystem.Instance.SubstractHealthPoint();
    }

    public void RestartCrashPoints()
    {
        crashPoints = 3;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Car") || hasCrash || HealthSystem.Instance.IsGameOver || crashPoints <= 0 || recovering) return;
        
        hasCrash = true;
        recovering = true;
            
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

        crashPoints--;
        
        StartCoroutine(WaitTillFixed());
    }

    private IEnumerator WaitTillFixed()
    {
        if(crashPoints > 0) yield return new WaitForSeconds(timeAfterHit);
        if(HealthSystem.Instance.IsGameOver) yield break;

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

        hasCrash = false;

        StartCoroutine(Recovering());
    }

    private IEnumerator Recovering()
    {
        yield return new WaitForSeconds(timeAfterHit);
        
        if(HealthSystem.Instance.IsGameOver) yield break;

        recovering = false;
    }
}
