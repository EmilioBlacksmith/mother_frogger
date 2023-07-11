using HP_System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Water.Spawner_and_DeSpawner
{
    public class WaterSpawnerTurtlePlatform : MonoBehaviour
    {
        private enum PlatformDirection
        {
            Normal,
            Inverse
        };
    
        [SerializeField] private PlatformDirection thisDirection;
        [SerializeField] private GameObject doublePlatform;
        [SerializeField] private GameObject triplePlatform;
        [SerializeField] private float timeBetweenSpawn;
        //[SerializeField] private float lifeSpan = 10;
    
        private int _randomNum;
        private float _timer = 0;
        private readonly Quaternion _inverseRotationDirection = Quaternion.Euler(0,-90,0);
        private readonly Quaternion _normalRotationDirection = Quaternion.Euler(0,90,0);
        private Quaternion _thisDirectionRotation;

        private void Start()
        {
            _randomNum = (Random.Range(0, 5000))%2;
        
            _thisDirectionRotation = thisDirection switch
            {
                PlatformDirection.Normal => _normalRotationDirection,
                PlatformDirection.Inverse => _inverseRotationDirection,
                _ => _thisDirectionRotation
            };
        
            switch (_randomNum)
            {
                case 0:
                    Instantiate(triplePlatform, transform.position, _thisDirectionRotation);
                    break;
                case 1:
                    Instantiate(doublePlatform, transform.position, _thisDirectionRotation);
                    break;
            }
            _timer = 0;
        }
    
        private void Update()
        {
            if(HealthSystem.Instance.IsGameOver) return;
        
            if (_timer >= timeBetweenSpawn)
            {
                _randomNum = (Random.Range(0, 5000))%2;
                switch (_randomNum)
                {
                    case 0:
                        Instantiate(triplePlatform, transform.position, _thisDirectionRotation);
                        break;
                    case 1:
                        Instantiate(doublePlatform, transform.position, _thisDirectionRotation);
                        break;
                }
                //Destroy(newObj, lifeSpan);
                _timer = 0;
            }

            _timer += Time.deltaTime;
        }
    }
}
