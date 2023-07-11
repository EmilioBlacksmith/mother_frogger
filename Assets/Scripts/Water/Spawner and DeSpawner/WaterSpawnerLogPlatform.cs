using HP_System;
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
        [SerializeField] private float timeBetweenSpawn;
    
        private int _randomNum;
        private float _timer = 0;
        private readonly Quaternion _inverseRotationDirection = Quaternion.Euler(0,-90,0);
        private readonly Quaternion _normalRotationDirection = Quaternion.Euler(0,90,0);
        private Quaternion _thisDirectionRotation;

        private void Start()
        {
            _timer = 0;

            _thisDirectionRotation = thisDirection switch
            {
                PlatformDirection.Normal => _normalRotationDirection,
                PlatformDirection.Inverse => _inverseRotationDirection,
                _ => _thisDirectionRotation
            };
        
            _randomNum = (Random.Range(0, 5000))%4;
            
            switch (_randomNum)
            {
                case 3:
                    Instantiate(crocodileTrap, transform.position, _thisDirectionRotation);
                    break;
                default:
                    Instantiate(woodLogPlatform, transform.position, _thisDirectionRotation);
                    break;
            }
            _timer = 0;
        }
    
        private void Update()
        {
            if(HealthSystem.Instance.IsGameOver) return;
        
            if (_timer >= timeBetweenSpawn)
            {
                _randomNum = (Random.Range(0, 5000))%5;
                //Debug.Log(_randomNum);
            
                switch (_randomNum)
                {
                    case 3:
                        Instantiate(crocodileTrap, transform.position, _thisDirectionRotation);
                        break;
                    default:
                        Instantiate(woodLogPlatform, transform.position, _thisDirectionRotation);
                        break;
                }
                _timer = 0;
            }

            _timer += Time.deltaTime;
        }
    }
}
