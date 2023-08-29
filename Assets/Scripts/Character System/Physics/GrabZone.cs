using Audio;
using Particles;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Character_System.Physics
{
    public class GrabZone : MonoBehaviour
    {
        [SerializeField] private Animator targetAnimator;
        [SerializeField] private GameObject grabbedObj;
        [SerializeField] private Rigidbody rigidBody;
        [SerializeField] private Transform placingPosition;
        private bool alreadyGrabbing = false;
        private bool grabbing = false;
        private static readonly int Grabbing = Animator.StringToHash("Grabbing");

        private void Start()
        {
            rigidBody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            if (CrashController.Instance.hasCrash || PauseMenuSystem.Instance.isPaused) return;
        
            if ((Mouse.current.leftButton.wasPressedThisFrame || Gamepad.current.rightTrigger.wasPressedThisFrame) && !grabbing)
            {
                targetAnimator.SetBool(Grabbing, true);
                AudioSystem.Instance.PlaySoundEffect(AudioSystem.SoundEffect.GrabObj);
                grabbing = true;

                if (grabbedObj != null && !alreadyGrabbing)
                {
                    grabbedObj.transform.position = transform.position;
                
                    FixedJoint fj = grabbedObj.AddComponent<FixedJoint>();
                    fj.connectedBody = rigidBody;
                    fj.breakForce = 100000;

                    Collider component = grabbedObj.GetComponent<Collider>();
                    component.isTrigger = true;

                    alreadyGrabbing = true;
                }

            }
           
            if((Mouse.current.leftButton.wasReleasedThisFrame|| Gamepad.current.rightTrigger.wasReleasedThisFrame) && grabbing)
            {
                targetAnimator.SetBool(Grabbing, false);
                AudioSystem.Instance.PlaySoundEffect(AudioSystem.SoundEffect.PlaceObj);

                if (grabbedObj != null && alreadyGrabbing)
                {
                    Destroy(grabbedObj.GetComponent<FixedJoint>());

                    var component = grabbedObj.GetComponent<Collider>();
                    var rotationYObject = grabbedObj.transform.eulerAngles.y;

                    grabbedObj.transform.position = placingPosition.position;
                    grabbedObj.transform.rotation = Quaternion.Euler(0, rotationYObject, 0);
                    ParticleSpawningSystem.Instance.SpawnPlacingObjectParticle(grabbedObj.transform);
                    component.isTrigger = false;

                    alreadyGrabbing = false;
                }

                grabbedObj = null;
                grabbing = false;
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
}
