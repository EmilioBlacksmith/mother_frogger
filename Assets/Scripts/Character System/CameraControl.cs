using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.Mathematics;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float rotationSpeed = 1f;
    private float mouseX;
    
    
    [SerializeField] private Transform hipTransform;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        var position = hipTransform.position;
        transform.position = new Vector3(position.x, 0, position.z);
        CamControl();
    }

    void CamControl()
    {
        mouseX += Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
        
        Quaternion rootRotation = quaternion.Euler(0f, mouseX, 0f);

        transform.rotation = rootRotation;
    }
}
