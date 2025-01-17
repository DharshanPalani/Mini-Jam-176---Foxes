using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbHolder : MonoBehaviour
{
    public float floatSpeed = 2f;
    public float floatAmplitude = 0.5f;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        transform.position = startPosition + new Vector3(0, Mathf.Sin(Time.time * floatSpeed) * floatAmplitude, 0);
    }
}
