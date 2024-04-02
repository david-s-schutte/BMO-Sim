using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [Header("Axes to Rotate Around")]
    [SerializeField] bool rotateX;
    [SerializeField] bool rotateY;
    [SerializeField] bool rotateZ;

    [Header("Rotation Speeds")]
    [Range(-100.0f, 100.0f)]
    [SerializeField] float speedX = 10;
    [Range(-100.0f, 100.0f)]
    [SerializeField] float speedY = 10;
    [Range(-100.0f, 100.0f)]
    [SerializeField] float speedZ = 10;

    // Update is called once per frame
    void Update()
    {
        float rotX = speedX * (rotateX ? 1: 0) * Time.deltaTime;
        float rotY = speedY * (rotateY ? 1 : 0) * Time.deltaTime;
        float rotZ = speedZ * (rotateZ ? 1 : 0) * Time.deltaTime;

        transform.Rotate(rotX, rotY, rotZ);

    }
}
