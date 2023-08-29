using Character_System.HP_System;
using Unity.Mathematics;
using UnityEngine;
using static UnityEngine.EventSystems.StandaloneInputModule;
using UnityEngine.InputSystem;

namespace Character_System
{
    public class CameraControl : MonoBehaviour
    {
        public float rotationSpeed = 1f;
        private float _mouseX;
        private Vector2 inputLook;
    
    
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
            transform.Rotate(Vector3.up, inputLook.x * rotationSpeed * Time.deltaTime);
        }

        public void OnLook(InputAction.CallbackContext value)
        {
            inputLook = value.ReadValue<Vector2>();
        }
    }
}
