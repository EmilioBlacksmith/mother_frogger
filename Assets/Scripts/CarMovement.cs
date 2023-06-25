using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private bool hasCrashed = false;
    [SerializeField] private int collisionLayerInt;
    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (!hasCrashed)
        {
            _rigidbody.velocity = transform.forward * speed;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == collisionLayerInt)
        {
            hasCrashed = true;
            //_rigidbody.velocity = Vector3.zero;
        }
    }
}
