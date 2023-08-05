using Audio;
using Particles;
using UnityEngine;

namespace Character_System.Physics
{
    public class GrabZone : MonoBehaviour
    {
        public Animator targetAnimator;
        public GameObject grabbedObj;
        public Rigidbody rigidBody;
        public bool alreadyGrabbing = false;
        private static readonly int Grabbing = Animator.StringToHash("Grabbing");

        private void Start()
        {
            rigidBody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            if (CrashController.Instance.hasCrash) return;

            if (Input.GetMouseButtonDown(0))
            {
                AudioSystem.Instance.PlaySoundEffect(AudioSystem.SoundEffect.GrabObj);
            }
        
            if (Input.GetMouseButton(0))
            {
                targetAnimator.SetBool(Grabbing, true);

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

            if (Input.GetMouseButtonUp(0))
            {
                targetAnimator.SetBool(Grabbing, false);
                AudioSystem.Instance.PlaySoundEffect(AudioSystem.SoundEffect.PlaceObj);

                if (grabbedObj != null && alreadyGrabbing)
                {
                    Destroy(grabbedObj.GetComponent<FixedJoint>());
                
                    var component = grabbedObj.GetComponent<Collider>();
                    var rotationYObject = grabbedObj.transform.eulerAngles.y;
                
                    grabbedObj.transform.rotation = Quaternion.Euler(0,rotationYObject, 0);
                    ParticleSpawningSystem.Instance.SpawnPlacingObjectParticle(grabbedObj.transform);
                    component.isTrigger = false;
                
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
}
