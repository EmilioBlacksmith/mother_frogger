using Game_Manager;
using HP_System;
using UnityEngine;

namespace Water.Platforms
{
    public class MovingCrocodile : MonoBehaviour
    {

        [SerializeField] private float startingSpeed = 10;
        private float _speed;
        private Rigidbody _rigidbody;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _speed = startingSpeed;
        }

        private void FixedUpdate()
        {
            transform.Translate(Vector3.forward * (_speed + (GameManager.Instance.DifficultyLevel() / 2f) * Time.deltaTime));
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                HealthSystem.Instance.SubstractHealthPoint();
            }
        }
    }
}
