using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuOptions : MonoBehaviour
{
    [Header("Resolution UI")]
    [SerializeField] private TextMeshProUGUI resolutionTMP;
    [SerializeField] private List<Resolution> resolutions;

    private int _selectedResolution = 0;

    [System.Serializable]
    private struct Resolution
    {
        public int X;
        public int Y;
    }

    private void Update()
    {
        DisplayResolution();
    }

    private void DisplayResolution()
    {
        resolutionTMP.text = 
            resolutions[_selectedResolution].X + 
            " X " + 
            resolutions[_selectedResolution].Y;
    }

    public void ScrollLeftOnResolution()
    {
        if(_selectedResolution > 0)
        {
            --_selectedResolution;
        }
    }

    public void ScrollRightOnResolution()
    {
        if (_selectedResolution < resolutions.Count - 1)
        {
            ++_selectedResolution;
        }
    }

    public void ApplyChanges()
    {
        Screen.SetResolution(
            resolutions[_selectedResolution].X,
            resolutions[_selectedResolution].Y,
            true);
    }
}
