using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    private Shooter _shooter;
    private bool _attackInput;

    private void Awake()
    {
        _shooter = GetComponent<Shooter>();
    }

    private void OnFire(InputValue inputValue)
    {
        _shooter.IsFiring = inputValue.isPressed;
    }
}
