using System.Collections;
using Character_System.HP_System;
using Game_Manager;
using Particles;
using UnityEngine;

namespace Water.Platforms
{
    public class MovingCrocodile : MonoBehaviour
    {
        [SerializeField] private float startingSpeed = 10;
        private float _speed;
        private Rigidbody _rigidbody;
        private bool _eatingTheFrog = false;

        private readonly WaitForSeconds _waitForDead = new WaitForSeconds(.25f); 

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _speed = startingSpeed;
        }

        private void FixedUpdate()
        {
            transform.Translate(Vector3.forward * (_speed + (GameManager.Instance.DifficultyLevel() / 2f) * Time.deltaTime));
        }

        private IEnumerator OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player") && !_eatingTheFrog)
            {
                _eatingTheFrog = true;
                ParticleSpawningSystem.Instance.SpawnBloodParticle(other.transform);
                yield return _waitForDead;
                HealthSystem.Instance.SubtractHealthPoint();
                _eatingTheFrog = false;
            }
            yield break;
        }
    }
}
