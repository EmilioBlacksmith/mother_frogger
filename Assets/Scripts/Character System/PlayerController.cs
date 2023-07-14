using Game_Manager;
using HP_System;
using UnityEngine;

namespace Character_System
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float speed = 5f;
        [SerializeField] private ConfigurableJoint hipJoint;
        [SerializeField] private Rigidbody hip;

        [SerializeField] private Animator targetAnimator;
        [SerializeField] private Transform cameraTarget;

        private bool _walk = false;
        private float _timer = 0f;
        private const float TimeBetweenSteps = 1f;
        private static readonly int Walk = Animator.StringToHash("Walk");

        private void FixedUpdate()
        {
            if (CrashController.Instance.hasCrash || HealthSystem.Instance.IsGameOver) return;
        
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");

            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

            if (direction.magnitude >= 0.1f)
            {
                var eulerAngles = cameraTarget.eulerAngles;
                var targetAngleMovement = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + eulerAngles.y;
                Vector3 moveDirection = Quaternion.Euler(0f, targetAngleMovement, 0f) * Vector3.forward;

            
                var targetAngle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg - eulerAngles.y;

                this.hipJoint.targetRotation = Quaternion.Euler(0f, targetAngle - 90, 0f);

                this.hip.velocity = moveDirection.normalized * this.speed;

                this._walk = true;
            }  else {
                this._walk = false;
            }

            this.targetAnimator.SetBool(Walk, this._walk);

            switch (_walk)
            {
                case true when _timer <= TimeBetweenSteps:
                    _timer += Time.deltaTime;
                    break;
                case true when _timer > TimeBetweenSteps:
                    _timer = 0;
                    GameManager.Instance.AddScore(10);
                    break;
            }
        }
    }
}
