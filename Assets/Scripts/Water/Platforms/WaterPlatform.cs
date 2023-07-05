using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPlatform : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody _rigidbody;
    private Transform frogParent;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector3.forward * (speed * Time.deltaTime));
        //_rigidbody.velocity = transform.forward * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && other.gameObject.name == "Hip")
        {
            frogParent = other.transform.parent;
            other.transform.SetParent(this.transform, true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && other.gameObject.name == "Hip")
        {
            other.transform.SetParent(frogParent, true);
        }
    }
}
