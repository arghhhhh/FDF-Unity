using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringWheelRotate : MonoBehaviour
{
    public Transform rotateSource;
    private Vector3 prevPos;
    public float intensifier;
    public float intensifier2;
    public Transform car;
    void Start()
    {
        prevPos = Vector3.zero;
    }

    void Update()
    {
        float magnitude = Vector3.Distance(prevPos, rotateSource.position);

        if (prevPos.x > rotateSource.position.x)
            magnitude = -magnitude;
        transform.Rotate(0, magnitude * intensifier, 0);
        car.position = new Vector3(car.position.x + magnitude * intensifier2, 0, 0);

        prevPos = rotateSource.position;
    }
}
