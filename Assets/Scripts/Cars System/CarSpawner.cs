using Character_System.HP_System;
using Game_Manager;
using UnityEngine;

namespace Cars_System
{
    public class CarSpawner : MonoBehaviour
    {
        private enum CarDirection
        {
            Normal,
            Inverse
        };

        [SerializeField] private CarDirection thisDirection;
        [SerializeField] private GameObject objectToSpawn;
        [SerializeField] private float lifeSpan = 10;
        [SerializeField] private float startingTimeBetweenSpawn = 7.5f;

        private float _timeBetweenSpawn;
        private float _timer = 0;
        
        private readonly Quaternion _inverseRotationDirection = Quaternion.Euler(0,-90,0);
        private readonly Quaternion _normalRotationDirection = Quaternion.Euler(0,90,0);
        private Quaternion _thisDirectionRotation = Quaternion.Euler(0,90,0);

        private void Start()
        {
            _thisDirectionRotation = thisDirection switch
            {
                CarDirection.Normal => _normalRotationDirection,
                CarDirection.Inverse => _inverseRotationDirection,
                _ => _thisDirectionRotation
            };
            
            var newObj = Instantiate(objectToSpawn, transform.position, _thisDirectionRotation);
            Destroy(newObj, lifeSpan * (10 - GameManager.Instance.DifficultyLevel()));
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
                var newObj = Instantiate(objectToSpawn, transform.position, _thisDirectionRotation);
                Destroy(newObj, lifeSpan);
                _timer = 0;
            }

            _timer += Time.deltaTime;
        }
    }
}
