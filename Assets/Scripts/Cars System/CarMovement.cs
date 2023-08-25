using System.Collections;
using Audio;
using Game_Manager;
using Particles;
using UnityEngine;

namespace Cars_System
{
    public class CarMovement : MonoBehaviour
    {
        [SerializeField] private float startingSpeed = 50;
        [SerializeField] private bool hasCrashed = false;
        [SerializeField] private int collisionLayerInt;
    
        private Rigidbody _rigidbody;
        private float _speed;
        
        private readonly WaitForSeconds _waitForDead = new WaitForSeconds(.25f);
        private readonly WaitForSeconds _waitForCrash = new WaitForSeconds(1f);


        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _speed = startingSpeed;
        }

        private void OnEnable()
        {
            hasCrashed = false;
        }

        private void FixedUpdate()
        {
            if (!hasCrashed)
            {
                //transform.Translate(Vector3.forward * (speed * Time.deltaTime));
                _rigidbody.velocity = transform.forward * (_speed + (GameManager.Instance.DifficultyLevel() * 15));
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.layer == collisionLayerInt && !hasCrashed)
            {
                ParticleSpawningSystem.Instance.SpawnCrashParticle(other.transform);
                //_rigidbody.AddExplosionForce(150f, transform.position, 100f, 175f, ForceMode.Impulse);
                //Destroy(this.gameObject, 1.5f);
                hasCrashed = true;
                AudioSystem.Instance.PlayCrash(other.transform);
                StartCoroutine(CrashObject());
                //_rigidbody.velocity = Vector3.zero;
            }
        }

        public IEnumerator CrashObject()
        {
            yield return _waitForCrash;
            ObjectPoolManager.ReturnObjectToPool(gameObject);
        }

        public IEnumerator CrashPlayer()
        {
            AudioSystem.Instance.PlayCrash(transform);
            //_rigidbody.AddExplosionForce(150f, transform.position, 100f, 175f, ForceMode.Impulse);
            yield return _waitForDead;
            
            hasCrashed = true;
            yield return _waitForCrash;

            ObjectPoolManager.ReturnObjectToPool(gameObject);
        }
    }
}
