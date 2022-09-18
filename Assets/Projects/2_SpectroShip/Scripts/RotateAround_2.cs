using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround_2 : MonoBehaviour
{
    [SerializeField]
    private float speed;

    void Update()
    {
        transform.Rotate(0, speed * Time.deltaTime, 0);
        //transform.localRotation = Quaternion.Euler(0, speed * Time.deltaTime, 0);
    }
}
