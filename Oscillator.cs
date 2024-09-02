using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startingPosition;
    Quaternion startingRotation;
    [SerializeField] Vector3 movementVector;
    [SerializeField] Vector3 rotationVector;
    float movementFactor; // note: [Range(0,1)] gives us a slider! Cool!
    [SerializeField] float period = 2f;

    void Start()
    {
        startingPosition = transform.position;
        startingRotation = transform.rotation;
    }

    void Update()
    {
        if (period <= Mathf.Epsilon) { return; } // Mathf.Epsilon is pretty much equivelant to 0
        float cycles = Time.time / period; // continually growing over time

        const float tau = Mathf.PI * 2; // constant value of 6.283
        float rawSinWave = Mathf.Sin(cycles * tau); // going from -1 to 1

        movementFactor = (rawSinWave + 1f) / 2f; // recalculated to go from 0 to 1 so it's cleaner

        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;

        Quaternion rotationOffset = Quaternion.Euler(rotationVector * movementFactor);
        transform.rotation = startingRotation * rotationOffset;
    }

    public void SetStartingPosition(Vector3 newPosition) // Referenced from the UINavigation.cs | arrow can oscillate and move up and down
    {
        startingPosition = newPosition;
    }
}
