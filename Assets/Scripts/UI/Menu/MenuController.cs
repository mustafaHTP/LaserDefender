using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public static MenuController Instance;

    private List<Menu> _menus;
    private Menu _currentMenu;

    private void Awake()
    {
        ManageSingleton();

        //Get all menus in children
        _menus = GetComponentsInChildren<Menu>().ToList();

        //Disable all of them
        _menus.ForEach(m => m.gameObject.SetActive(false));

        //Set enable main menu at startup
        SwitchMenu(MenuType.Main);
    }

    private void ManageSingleton()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }

    private void SwitchMenu(MenuType desiredType)
    {
        //Disable the current one
        if (_currentMenu != null)
        {
            _currentMenu.gameObject.SetActive(false);
        }

        //Get desired menu
        Menu desiredMenu = _menus.Find(m => m.Type == desiredType);

        //Enable the desired menu
        if (desiredMenu != null)
        {
            desiredMenu.gameObject.SetActive(true);
            _currentMenu = desiredMenu;
        }
    }

    public void DisableAllMenus()
    {
        _menus.ForEach(m => m.gameObject.SetActive(false));
    }

    public void SwitchMainMenu() => SwitchMenu(MenuType.Main);
    public void SwitchOptionsMenu() => SwitchMenu(MenuType.Options);
    public void SwitchPauseMenu() => SwitchMenu(MenuType.Pause);
}
