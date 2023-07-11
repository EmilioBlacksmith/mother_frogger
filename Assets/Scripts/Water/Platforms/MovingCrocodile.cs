using System;
using System.Collections;
using System.Collections.Generic;
using HP_System;
using UnityEngine;

public class MovingCrocodile : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector3.forward * (speed * Time.deltaTime));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            HealthSystem.Instance.SubstractHealthPoint();
        }
    }
}
