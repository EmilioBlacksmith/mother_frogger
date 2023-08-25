using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckWalls : MonoBehaviour
{
    [SerializeField] Transform normalParent;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 10 && transform.parent != normalParent)
        {
            transform.SetParent(normalParent);
        }
    }
}
