using System;
using System.Collections;
using System.Collections.Generic;
using Game_Manager;
using HP_System;
using UnityEngine;

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
        transform.Translate(Vector3.forward * (_speed + (GameManager.Instance.DifficultyLevel() / 2) * Time.deltaTime));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            HealthSystem.Instance.SubstractHealthPoint();
        }
    }
}
