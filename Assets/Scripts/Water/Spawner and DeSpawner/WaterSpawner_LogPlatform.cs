using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class WaterSpawner_LogPlatform : MonoBehaviour
{
    enum platformDirection
    {
        normal,
        inverse
    };

    [SerializeField] private platformDirection thisDirection;
    [SerializeField] private GameObject woodLogPlatform;
    [FormerlySerializedAs("triplePlatform")] [SerializeField] private GameObject crocodileTrap;
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
            platformDirection.normal => _normalRotationDirection,
            platformDirection.inverse => _inverseRotationDirection,
            _ => _thisDirectionRotation
        };
        
        _randomNum = (Random.Range(0, 5000))%5;
            
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
        if (CrashController.Instance._isGameOver) return;
        
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
