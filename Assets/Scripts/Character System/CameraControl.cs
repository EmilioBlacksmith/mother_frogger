using Character_System.HP_System;
using Unity.Mathematics;
using UnityEngine;

namespace Character_System
{
    public class CameraControl : MonoBehaviour
    {
        public float rotationSpeed = 1f;
        private float _mouseX;
    
    
        [SerializeField] private Transform hipTransform;

        private void Awake()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void Update()
        {
            if(HealthSystem.Instance.IsGameOver || PauseMenuSystem.Instance.isPaused) return;
        
            var position = hipTransform.position;
            transform.position = new Vector3(position.x, 0, position.z);
            CamControl();
        }

        void CamControl()
        {
            _mouseX += Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
        
            Quaternion rootRotation = quaternion.Euler(0f, _mouseX, 0f);

            transform.rotation = rootRotation;
        }
    }
}
