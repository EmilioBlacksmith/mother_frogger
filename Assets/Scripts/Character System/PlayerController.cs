using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private ConfigurableJoint hipJoint;
    [SerializeField] private Rigidbody hip;

    [SerializeField] private Animator targetAnimator;
    [SerializeField] private Transform cameraTarget;

    private bool walk = false;
    private float _timer = 0f;
    private readonly float _timeBetweenSteps = 1f;

    void Update()
    {
        if (CrashController.Instance._hasCrash) return;
        
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            var eulerAngles = cameraTarget.eulerAngles;
            var localEulerAngles = eulerAngles;
            float targetAngleMovement = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + localEulerAngles.y;
            Vector3 moveDirection = Quaternion.Euler(0f, targetAngleMovement, 0f) * Vector3.forward;

            
            float targetAngle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg - eulerAngles.y;

            this.hipJoint.targetRotation = Quaternion.Euler(0f, targetAngle - 90, 0f);

            this.hip.AddForce(moveDirection.normalized * this.speed, ForceMode.Acceleration);

            this.walk = true;
        }  else {
            this.walk = false;
        }

        this.targetAnimator.SetBool("Walk", this.walk);

        if (walk && _timer <= _timeBetweenSteps)
        {
            _timer += Time.deltaTime;
        }else if (walk && _timer > _timeBetweenSteps)
        {
            _timer = 0;
            GameManager.Instance.AddScore(10);
        }
    }
}
