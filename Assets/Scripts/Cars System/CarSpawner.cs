using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    [SerializeField] private GameObject objectToSpawn;
    [SerializeField] private float timeBetweenSpawn;
    [SerializeField] private float lifeSpan = 10;

    private float _timer = 0;
    private readonly Quaternion _rotationDirection = Quaternion.Euler(0,90,0);

    private void Start()
    {
        var newObj = Instantiate(objectToSpawn, transform.position, _rotationDirection);
        Destroy(newObj, lifeSpan);
        _timer = 0;
    }

    private void Update()
    {
        if (CrashController.Instance._isGameOver) return;
        
        if (_timer >= timeBetweenSpawn)
        {
            var newObj = Instantiate(objectToSpawn, transform.position, _rotationDirection);
            Destroy(newObj, lifeSpan);
            _timer = 0;
        }

        _timer += Time.deltaTime;
    }
}
