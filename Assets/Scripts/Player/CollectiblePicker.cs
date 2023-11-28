using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectiblePicker : MonoBehaviour
{
    [Header("Collectible Configs")]
    [Space(5)]
    [Header("Health")]
    [SerializeField] private int collectHealtAmount;

    [Header("Fast Shoot")]
    [SerializeField] private float fastShootFiringRate;

    private Health _health;
    private Shooter _shooter;

    private float _initialFiringRate;

    private float _fastShootTimer = 0f;
    private bool _isFastShootActive;

    public bool IsFastShootActive { get => _isFastShootActive; }

    private void Awake()
    {
        _shooter = GetComponent<Shooter>();
        _health = GetComponent<Health>();

        _initialFiringRate = _shooter.FiringRate;
    }

    private void Update()
    {
        if (_isFastShootActive)
        {
            _fastShootTimer -= Time.deltaTime;
            if (_fastShootTimer < 0f)
            {
                _isFastShootActive = false;
                DeactivateFastShoot();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Collectible collectible))
        {
            switch (collectible.collectibleType)
            {
                case CollectibleType.Health:
                    ActivateHealth();
                    break;
                case CollectibleType.FastShoot:
                    _fastShootTimer = collectible.effectTime;
                    _isFastShootActive = true;
                    ActivateFastShoot();
                    break;
            }

            Destroy(collectible.gameObject);
        }
    }

    private void ActivateHealth()
    {
        _health.IncreaseHealth(collectHealtAmount);
    }

    private void ActivateFastShoot()
    {
        _shooter.FiringRate = fastShootFiringRate;
    }

    private void DeactivateFastShoot()
    {
        _shooter.FiringRate = _initialFiringRate;
    }
}
