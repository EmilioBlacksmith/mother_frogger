using Game_Manager;
using UnityEngine;

namespace Water.Platforms
{
    public class WaterPlatform : MonoBehaviour
    {
        [SerializeField] private float startingSpeed = 10;
        private float _speed;
        private Rigidbody _rigidbody;
        private Transform _frogParent;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _speed = startingSpeed;
        }

        private void FixedUpdate()
        {
            if (GameManager.Instance != null)
                transform.Translate(Vector3.forward * (_speed + (GameManager.Instance.DifficultyLevel() / 2f) * Time.deltaTime));
            //_rigidbody.velocity = transform.forward * speed;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player") && other.gameObject.name == "Hip")
            {
                _frogParent = other.transform.parent;
                other.transform.SetParent(this.transform, true);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Player") && other.gameObject.name == "Hip")
            {
                other.transform.SetParent(_frogParent, true);
            }
        }
    }
}
