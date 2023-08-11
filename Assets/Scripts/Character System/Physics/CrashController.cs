using System.Collections;
using Audio;
using Cars_System;
using Character_System.HP_System;
using Particles;
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

        [SerializeField] private LayerMask collisionLayerMask;
        [SerializeField] private LayerMask invisibleLayerMask;

        [Header("Material")]
        [SerializeField] private Renderer frogRenderer;
        [SerializeField] private Material normalMaterial;
        [SerializeField] private Material invisibleMaterial;
    
    
        public bool hasCrash;
        public bool recovering;

        [SerializeField] private ConfigurableJoint[] allJoints;
        
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

            allJoints = new ConfigurableJoint[bodyJoints.Length + 1];
            for(int i = 0; i < bodyJoints.Length; i++)
            {
                allJoints[i] = bodyJoints[i];
            }
            allJoints[allJoints.Length - 1] = hipJoint;
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
            AudioSystem.Instance.PlaySoundEffect(AudioSystem.SoundEffect.Drown);
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
            
            ParticleSpawningSystem.Instance.SpawnCrashParticle(other.transform);
            CarMovement car = other.GetComponent<CarMovement>();

            if (car != null)
            {
                car.StartCoroutine(nameof(CarMovement.CrashPlayer));
            }
            
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
            BecomeInvisible();

            StartCoroutine(Recovering());
        }

        private IEnumerator Recovering()
        {
            yield return new WaitForSeconds(timeAfterHit);
        
            if(HealthSystem.Instance.IsGameOver) yield break;

            recovering = false;
            BecomeVisible();
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

        private void BecomeInvisible()
        {
            foreach (var joint in allJoints)
            {
                joint.gameObject.layer = LayerMask.NameToLayer("Character Invisible");
                frogRenderer.material = invisibleMaterial;
            }
        }

        private void BecomeVisible()
        {
            foreach (var joint in allJoints)
            {
                joint.gameObject.layer = LayerMask.NameToLayer("Character");
                frogRenderer.material = normalMaterial;
            }
        }
    }
}
