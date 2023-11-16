using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
[Header("Player")]
        [Tooltip("Move speed of the character in m/s")]
        public float MoveSpeed = 2.0f;

        [Tooltip("How fast the character turns to face movement direction")]
        [Range(0.0f, 0.3f)]
        public float RotationSmoothTime = 0.12f;

        [Tooltip("Acceleration and deceleration")]
        public float SpeedChangeRate = 10.0f;

        [Header("Cinemachine")]
        [Tooltip("The look target set that any camera will follow")]
        public GameObject lookTarget;

        [SerializeField]
        private float minPitchAngle = -30;

        [SerializeField]
        private float maxPitchAngle = 70;

        [SerializeField]
        [Tooltip("Additional degress to override the camera. Useful for fine tuning camera position when locked")]
        private float cameraAngleOverride;

        [SerializeField]
        [Range(1, 8)]
        private float mouseSensitivity = 1;

        private float cameraTargetYaw;
        private CharacterController controller;

        private Vector2 look;
        private float cameraTargetPitch;
        private Vector3 direction;
        private float targetSpeed;
        private float targetRotation;
        private float rotationVelocity;
        private GameObject mainCamera;
        private float speed;
        private float verticalVelocity = 0;
        private const float threshold = 0.01f;

        private void Awake()
        {
            if (mainCamera == null)
            {
                mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
                if (mainCamera == null)
                    throw new ArgumentNullException("Have you set the CinemachineCamera to the MainCamera tag?");
            }
        }

        private void Start()
        {
            cameraTargetYaw = lookTarget.transform.rotation.eulerAngles.y;
            controller = GetComponent<CharacterController>();
        }

        private void Update()
        {
            HandleInput();
            Move();
        }

        private void HandleInput()
        {
            direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

            look.x = Input.GetAxis("Mouse X") * mouseSensitivity;
            look.y = Input.GetAxis("Mouse Y") * mouseSensitivity;

            look.x = Input.GetAxis("Mouse X") * mouseSensitivity;
            look.y = Input.GetAxis("Mouse Y") * mouseSensitivity;
        }

        private void LateUpdate()
        {
            RotateCamera();
        }

        private void Move()
        {
            // set target speed based on move speed, sprint speed and if sprint is pressed
            targetSpeed = MoveSpeed;

            // if there is no input, set the target speed to 0
            if (direction.magnitude == 0) targetSpeed = 0.0f;

            // a reference to the players current horizontal velocity
            float currentHorizontalSpeed = new Vector3(controller.velocity.x, 0.0f, controller.velocity.z).magnitude;

            float speedOffset = 0.1f;
            float inputMagnitude = 1;// _input.analogMovement ? _input.move.magnitude : 1f;

            // accelerate or decelerate to target speed
            if (currentHorizontalSpeed < targetSpeed - speedOffset ||
                currentHorizontalSpeed > targetSpeed + speedOffset)
            {
                // creates curved result rather than a linear one giving a more organic speed change
                // note T in Lerp is clamped, so we don't need to clamp our speed
                speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude, Time.deltaTime * SpeedChangeRate);

                // round speed to 3 decimal places
                speed = Mathf.Round(speed * 1000f) / 1000f;
            }
            else
                speed = targetSpeed;

            // normalise input direction
            Vector3 inputDirection = new Vector3(direction.x, 0.0f, direction.y).normalized;

            // if there is a move input rotate player when the player is moving
            if (direction.magnitude != 0)
            {
                targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + mainCamera.transform.eulerAngles.y;
                float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref rotationVelocity, RotationSmoothTime);

                // rotate to face input direction relative to camera position
                transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
            }

            Vector3 targetDirection = Quaternion.Euler(0.0f, targetRotation, 0.0f) * Vector3.forward;

            // move the player
            controller.Move(targetDirection.normalized * (speed * Time.deltaTime) + new Vector3(0.0f, verticalVelocity, 0.0f) * Time.deltaTime);
        }

        private void RotateCamera()
        {
            // if there is an input and camera position is not fixed
            if (look.sqrMagnitude >= threshold)
            {
                cameraTargetYaw += look.x;
                cameraTargetPitch += look.y;
            }
            

            // camera will follow this target
            lookTarget.transform.rotation = Quaternion.Euler(cameraTargetPitch + cameraAngleOverride, cameraTargetYaw, 0.0f);
        }
}
