using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarDeSpawner : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Car"))
        {
            Destroy(other.gameObject);
        }
    }
}
