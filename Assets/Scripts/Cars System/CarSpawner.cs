using Game_Manager;
using HP_System;
using UnityEngine;

namespace Cars_System
{
    public class CarSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject objectToSpawn;
        [SerializeField] private float lifeSpan = 10;
        [SerializeField] private float startingTimeBetweenSpawn = 7.5f;

        private float _timeBetweenSpawn;
        private float _timer = 0;
        private readonly Quaternion _rotationDirection = Quaternion.Euler(0,90,0);

        private void Start()
        {
            var newObj = Instantiate(objectToSpawn, transform.position, _rotationDirection);
            Destroy(newObj, lifeSpan);
            _timer = 0;
            _timeBetweenSpawn = startingTimeBetweenSpawn;
        }

        private void Update()
        {
            if(HealthSystem.Instance.IsGameOver) return;
        
            _timeBetweenSpawn = startingTimeBetweenSpawn - (GameManager.Instance.DifficultyLevel() * 1.5f);
            _timeBetweenSpawn = Mathf.Clamp(_timeBetweenSpawn, 1f, 10f);
        
            if (_timer >= _timeBetweenSpawn)
            {
                var newObj = Instantiate(objectToSpawn, transform.position, _rotationDirection);
                Destroy(newObj, lifeSpan);
                _timer = 0;
            }

            _timer += Time.deltaTime;
        }
    }
}
