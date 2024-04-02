using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Scaler : MonoBehaviour
{
    [Header("Axes to Scale")]
    [SerializeField] bool scaleX;
    [SerializeField] bool scaleY;
    [SerializeField] bool scaleZ;

    [Header("Scale Strengths")]
    [Range(-10.0f, 10.0f)]
    [SerializeField] float strengthX = 1;
    [Range(-10.0f, 10.0f)]
    [SerializeField] float strengthY = 1;
    [Range(-10.0f, 10.0f)]
    [SerializeField] float strengthZ = 1;

    // Update is called once per frame
    void Update()
    {
        float scaX = strengthX * (scaleX ? 1 : 0);
        float scaY = strengthY * (scaleY ? 1 : 0);
        float scaZ = strengthZ * (scaleZ ? 1 : 0);

        transform.localScale = new Vector3 ( scaX, scaY, scaZ );
    }
}
