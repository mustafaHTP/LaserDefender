using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPause : MonoBehaviour
{
    public void ContinueGame()
    {
        Time.timeScale = 1f;
        MenuController.Instance.DisableAllMenus();
    }
}
