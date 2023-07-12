using System;
using System.Collections;
using System.Collections.Generic;
using Game_Manager;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    [SerializeField] private float startingSpeed = 50;
    [SerializeField] private bool hasCrashed = false;
    [SerializeField] private int collisionLayerInt;
    
    private Rigidbody _rigidbody;
    private float _speed;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _speed = startingSpeed;
    }

    private void FixedUpdate()
    {
        if (!hasCrashed)
        {
            //transform.Translate(Vector3.forward * (speed * Time.deltaTime));
            _rigidbody.velocity = transform.forward * (_speed + (GameManager.Instance.DifficultyLevel() * 50));
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == collisionLayerInt)
        {
            _rigidbody.AddExplosionForce(300f, transform.position, 200f, 350f, ForceMode.Impulse);
            Destroy(this.gameObject, 2f);
            hasCrashed = true;
            //_rigidbody.velocity = Vector3.zero;
        }
    }
}
