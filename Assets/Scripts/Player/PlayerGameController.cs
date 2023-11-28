using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGameController : MonoBehaviour
{
    private void OnPause(InputValue inputValue)
    {
        if (inputValue.isPressed)
        {
            Time.timeScale = 0f;
            MenuController.Instance.SwitchPauseMenu();
        }
    }
}
