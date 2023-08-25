using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSystem : MonoBehaviour
{
    private int objectHealth = 10;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer != 6) return;

        objectHealth--;

        if(objectHealth <= 0)
            Destroy(this.gameObject); 
    }
}
