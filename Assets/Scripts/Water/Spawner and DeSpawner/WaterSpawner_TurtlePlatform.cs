using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class WaterSpawner_TurtlePlatform : MonoBehaviour
{
    enum platformDirection
    {
        normal,
        inverse
    };
    
    [SerializeField] private platformDirection thisDirection;
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
            platformDirection.normal => _normalRotationDirection,
            platformDirection.inverse => _inverseRotationDirection,
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
        if (CrashController.Instance._isGameOver) return;
        
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
