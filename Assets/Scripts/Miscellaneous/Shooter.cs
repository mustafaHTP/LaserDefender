using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Shooter : MonoBehaviour
{
    [Header("AI")]
    [SerializeField] private bool useAI;

    [Header("Projectile Config")]
    [SerializeField] private GameObject projectile;
    [SerializeField] private float projectileSpeed = 10f;
    [SerializeField] private float projectileLifeTime = 5f;
    [SerializeField] private float firingRate = 0.1f;
    [SerializeField] private float firingRateTimeVarience = 0.05f;
    [SerializeField] private float minFiringRate = 0f;

    public float FiringRate
    {
        get => firingRate;
        set => firingRate = value;
    }

    [HideInInspector] public bool IsFiring;
    private bool _isProcessingFire;
    private AudioPlayer _audioPlayer;

    private void Awake()
    {
        _audioPlayer = FindObjectOfType<AudioPlayer>();

        if (useAI)
        {
            IsFiring = true;
        }
    }

    private void Update()
    {
        Fire();
    }

    private void Fire()
    {
        if (IsFiring && !_isProcessingFire)
        {
            StartCoroutine(FireContinously());
        }
    }

    private IEnumerator FireContinously()
    {
        _isProcessingFire = true;

        GameObject clone = Instantiate(projectile, transform.position, Quaternion.identity);

        if (clone.TryGetComponent(out Rigidbody2D rigidbody))
        {
            rigidbody.velocity = transform.up * projectileSpeed;
        }

        Destroy(clone, projectileLifeTime);

        _audioPlayer?.PlayFireSFX();

        float firingRate = GetRandomFiringRate();

        yield return new WaitForSeconds(firingRate);

        _isProcessingFire = false;
    }

    private float GetRandomFiringRate()
    {
        float randomFiringRate = Random.Range(
                    firingRate - firingRateTimeVarience,
                    firingRate + firingRateTimeVarience
                    );

        float clampedFiringRate = Mathf.Clamp(randomFiringRate, minFiringRate, float.MaxValue);
        return clampedFiringRate;
    }
}
