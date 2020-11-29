using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillate : MonoBehaviour
{
    [SerializeField]
    float MaxYAmplitude = 0.0f;

    [SerializeField]
    float MaxXAmplitude = 0.0f;

    float currentAngle = 0.0f;

    [SerializeField]
    float AngleSpeed = 1.0f;

    Vector2 initialPos;

    void Start()
    {
        initialPos = transform.position;
    }

    void FixedUpdate()
    {
        Vector2 currentPos = initialPos;
        currentAngle += AngleSpeed * Time.fixedDeltaTime;

        if (currentAngle > Mathf.PI)
        {
            currentAngle -= Mathf.PI;
        }

        currentPos.x += Mathf.Sin(currentAngle) * MaxXAmplitude;
        currentPos.y += Mathf.Sin(currentAngle) * MaxYAmplitude;

        transform.position = currentPos;
    }
}
