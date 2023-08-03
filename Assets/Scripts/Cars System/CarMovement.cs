using System.Collections;
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

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _speed = startingSpeed;
        }

        private void FixedUpdate()
        {
            if (!hasCrashed)
            {
                //transform.Translate(Vector3.forward * (speed * Time.deltaTime));
                _rigidbody.velocity = transform.forward * (_speed + (GameManager.Instance.DifficultyLevel() * 10));
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.layer == collisionLayerInt)
            {
                ParticleSpawningSystem.Instance.SpawnCrashParticle(other.transform);
                _rigidbody.AddExplosionForce(300f, transform.position, 200f, 350f, ForceMode.Impulse);
                Destroy(this.gameObject, 1.5f);
                hasCrashed = true;
                //_rigidbody.velocity = Vector3.zero;
            }
        }

        public IEnumerator CrashPlayer()
        {
            yield return _waitForDead;
            _rigidbody.AddExplosionForce(150f, transform.position, 100f, 175f, ForceMode.Impulse);
            hasCrashed = true;
            Destroy(this.gameObject, .75f);
        }
    }
}
