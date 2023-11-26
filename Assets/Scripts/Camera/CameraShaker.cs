using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class CameraShaker : MonoBehaviour
{
    [SerializeField] private float shakeMagnitude;
    [SerializeField] private float shakeDuration;

    private Vector3 initialPosition;

    private float shakeTimer = 0f;

    private void Awake()
    {
        initialPosition = transform.position;
    }

    public void Play()
    {
        StartCoroutine(Shake());
    }

    private IEnumerator Shake()
    {
        while (shakeTimer <= shakeDuration)
        {
            shakeTimer += Time.deltaTime;
            transform.position = initialPosition + (Vector3)Random.insideUnitCircle * shakeMagnitude;
            yield return new WaitForEndOfFrame();
        }

        shakeTimer = 0f;
        transform.position = initialPosition;
    }
}
