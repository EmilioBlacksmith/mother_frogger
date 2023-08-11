using Character_System.HP_System;
using Game_Manager;
using UnityEngine;

namespace Water.Spawner_and_DeSpawner
{
    public class WaterSpawnerLogPlatform : MonoBehaviour
    {
        private enum PlatformDirection
        {
            Normal,
            Inverse
        };

        [SerializeField] private PlatformDirection thisDirection;
        [SerializeField] private GameObject woodLogPlatform;
        [SerializeField] private GameObject crocodileTrap;
        [SerializeField] private float startingTimeBetweenSpawn = 8;
        //[SerializeField] private float lifeSpan = 10;
        
        private float _timeBetweenSpawn;
        private int _randomNum;
        private float _timer = 0;
        private readonly Quaternion _inverseRotationDirection = Quaternion.Euler(0,-90,0);
        private readonly Quaternion _normalRotationDirection = Quaternion.Euler(0,90,0);
        private Quaternion _thisDirectionRotation;

        private void Start()
        {
            _timer = 0;
            _timeBetweenSpawn = startingTimeBetweenSpawn;

            _thisDirectionRotation = thisDirection switch
            {
                PlatformDirection.Normal => _normalRotationDirection,
                PlatformDirection.Inverse => _inverseRotationDirection,
                _ => _thisDirectionRotation
            };
        
            _randomNum = (Random.Range(0, 5000))%(20 / GameManager.Instance.DifficultyLevel());
            
            switch (_randomNum)
            {
                case 3:
                    //Instantiate(crocodileTrap, transform.position, _thisDirectionRotation);
                    ObjectPoolManager.SpawnObject(crocodileTrap, transform.position, _thisDirectionRotation, ObjectPoolManager.PoolType.WaterObject);
                    break;
                default:
                    //Instantiate(woodLogPlatform, transform.position, _thisDirectionRotation);
                    ObjectPoolManager.SpawnObject(woodLogPlatform, transform.position, _thisDirectionRotation, ObjectPoolManager.PoolType.WaterObject);
                    break;
            }
            _timer = 0;
        }
    
        private void Update()
        {
            if(HealthSystem.Instance.IsGameOver) return;

            _timeBetweenSpawn = startingTimeBetweenSpawn - (GameManager.Instance.DifficultyLevel() * 2f);
            _timeBetweenSpawn = Mathf.Clamp(_timeBetweenSpawn, 2f, 10);
        
            if (_timer >= _timeBetweenSpawn)
            {
                _randomNum = (Random.Range(0, 5000))%(15 / GameManager.Instance.DifficultyLevel());
                var positionSpawn = transform.position;
                var newObj = _randomNum switch
                {
                    3 => ObjectPoolManager.SpawnObject(crocodileTrap, transform.position, _thisDirectionRotation, ObjectPoolManager.PoolType.WaterObject),
                    _ => ObjectPoolManager.SpawnObject(woodLogPlatform, transform.position, _thisDirectionRotation, ObjectPoolManager.PoolType.WaterObject)
                };
                //Destroy(newObj, lifeSpan * (10 - GameManager.Instance.DifficultyLevel()));
                
                _timer = 0;
            }

            _timer += Time.deltaTime;
        }
    }
}
