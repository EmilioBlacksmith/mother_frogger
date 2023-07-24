using System.Collections;
using Character_System.HP_System;
using UnityEngine;

namespace Character_System.Physics
{
    public class CrashController : MonoBehaviour
    {
        
        public static CrashController Instance { get; private set; }
        
        [SerializeField] private ConfigurableJoint[] bodyJoints;
        [SerializeField] private ConfigurableJoint hipJoint;
        [SerializeField] private float normalSpringForceBody = 4000;
        [SerializeField] private float normalSpringForceHips = 100000;
        [SerializeField] private float crashForceSpring = 50f;
        [SerializeField] private int crashPoints = 3;
        [SerializeField] private int startingCrashPoints = 4;
        [SerializeField] private float timeAfterHit = 3;
    
    
        public bool hasCrash;
        public bool recovering;
        
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
        }

        private void Start()
        {
            Instance = this;
            hasCrash = false;
            recovering = false;
            HUDSystem.Instance?.AllocateCrashPoints(crashPoints);
        }

        private void Update()
        {
            if (crashPoints <= 0 && !HealthSystem.Instance.IsGameOver)
            {
                Restart();
            }

            if (hipJoint.transform.position.y <= -4f && !HealthSystem.Instance.IsGameOver)
            {
                Restart();
            }
        
        }

        private void Restart()
        {
            HealthSystem.Instance.SubtractHealthPoint();
        }

        public void RestartCrashPoints()
        {
            crashPoints = startingCrashPoints;
            HUDSystem.Instance?.AllocateCrashPoints(crashPoints);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Car") || hasCrash || HealthSystem.Instance.IsGameOver || crashPoints <= 0 || recovering) return;
        
            hasCrash = true;
            recovering = true;
            
            CrashedJoints();

            crashPoints--;
            HUDSystem.Instance.UpdateCrashPoints(crashPoints);
        
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

        public void CrashedJoints()
        {
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
        }
    }
}
