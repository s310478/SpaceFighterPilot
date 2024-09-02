using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    [Header("Inputs")]
    [SerializeField] InputAction movement;
    [SerializeField] InputAction fire;
    
    [Header("General Setup Settings")]
    [Tooltip("How fast ship moves up and down based on player input")] 
    [SerializeField] float controlSpeed = 15f;
    [Tooltip("How far player moves horizontally")]
    [SerializeField] float xRange = 9f;
    [Tooltip("How far player moves vertically")]
    [SerializeField] float yRange = 6f;

    [Header("Laser gun array")]
    [SerializeField] GameObject[] lasers;

    [Header("Screen position based turing")]
    [SerializeField] float positionPitchFactor = -2f;
    [SerializeField] float positionYawFactor = 2.5f;

    [Header("Player input based turing")]
    [Tooltip("How far player pitchs")]
    [SerializeField] float controlPictchFactor = -20f;
    [Tooltip("How far player rolls")]
    [SerializeField] float controlRollFactor = -30f;
    [Tooltip("How fast the ship responds to pitchs & rolls")]
    [SerializeField] float smoothingSpeed = 5f; // player movement smoothing

    [Header("Reticle Settings")]
    [SerializeField] RectTransform reticle; // Reference to the Reticle UI element
    [SerializeField] Camera mainCamera; // Reference to the main camera

    float xThrow, yThrow;
    float targetXThrow, targetYThrow; // Target values for smoothing
    private bool controlsEnabled = true;
    private bool moveToCenter = false;

    private Vector3 centerPosition = new Vector3(0f, 0.21f, 0f);
    private Quaternion centerRotation = Quaternion.Euler(0f, 0f, 0f);

    void OnEnable()
    {
        movement.Enable();
        fire.Enable();
    }

    void OnDisable()
    {
        movement.Disable();
        fire.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        if (controlsEnabled)
        {
            ProcessInput();
            ProcessTranslation();
            ProcessRotation();
            ProcessFiring();
        }
        if (moveToCenter)
        {
            MoveToCenter();
        }
        UpdateReticle();
    }

    void ProcessInput()
    {
        Vector2 inputVector = movement.ReadValue<Vector2>();
        targetXThrow = inputVector.x;
        targetYThrow = inputVector.y;

        // Smoothly interpolate the current throw values towards the target values
        xThrow = Mathf.Lerp(xThrow, targetXThrow, Time.deltaTime * smoothingSpeed);
        yThrow = Mathf.Lerp(yThrow, targetYThrow, Time.deltaTime * smoothingSpeed);
    }

    void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = yThrow * controlPictchFactor;


        float pitch = pitchDueToPosition + pitchDueToControlThrow;
        float yaw = transform.localPosition.x * positionYawFactor;
        float roll = xThrow * controlRollFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    void ProcessTranslation()
    {
        float xOffset = xThrow * Time.deltaTime * controlSpeed;
        float rawXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);

        float yOffset = yThrow * Time.deltaTime * controlSpeed;
        float rawYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }

    void ProcessFiring()
    {
        if (fire.ReadValue<float>() > 0.5) // 1 = button fully pressed, 0 = no button pressed
        {
            SetLasersActive(true);
        }
        else
        {
            SetLasersActive(false);
        }
    }

    public void StopFiring()
    {
        fire.Disable();
        SetLasersActive(false);
    }

    void SetLasersActive(bool isActive)
    {
        foreach (GameObject laser in lasers)
        {
            var emissionModule = laser.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = isActive;

            var laserSound = laser.GetComponent<AudioSource>();
            laserSound.enabled = isActive;
        }
    }

    public void DisableControls()
    {
        controlsEnabled = false;
    }

    public void StartMovingToCenter()
    {
        moveToCenter = true;
    }

    private void MoveToCenter()
    {
        float moveSpeed = 2f;
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, centerPosition, moveSpeed * Time.deltaTime);
        transform.localRotation = Quaternion.RotateTowards(transform.localRotation, centerRotation, moveSpeed * 20f * Time.deltaTime);
    }

    private void UpdateReticle()
    {
        if (reticle == null || mainCamera == null) return;

        // Determine the point in front of the player's ship
        Vector3 laserDirection = transform.forward * 10f; // Adjust the multiplier based on your game's scale

        // Project this point onto the screen
        Vector3 screenPoint = mainCamera.WorldToScreenPoint(transform.position + laserDirection);

        // Update the reticle's position
        reticle.position = screenPoint;
    }
}


//xThrow = movement.ReadValue<Vector2>().x;
//yThrow = movement.ReadValue<Vector2>().y;

//float xOffset = xThrow * Time.deltaTime * controlSpeed;
//float rawXPos = transform.localPosition.x + xOffset;
//float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);

//float yOffset = yThrow * Time.deltaTime * controlSpeed;
//float rawYPos = transform.localPosition.y + yOffset;
//float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);

//transform.localPosition = new Vector3
//    (clampedXPos, clampedYPos, transform.localPosition.z);


// ctrl + k + c
//float xThrow = Input.GetAxis("Horizontal");
//Debug.Log(xThrow);

//float yThrow = Input.GetAxis("Vertical");
//Debug.Log(yThrow);