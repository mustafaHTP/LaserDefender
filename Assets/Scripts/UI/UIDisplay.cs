using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIDisplay : MonoBehaviour
{
    [Header("Health UI")]
    [Space(1)]
    [SerializeField] private Health health;
    [SerializeField] private Slider healthSlider;

    [Header("Score UI")]
    [Space(1)]
    [SerializeField] private TextMeshProUGUI scoreTMP;
    [SerializeField] private ScoreKeeper scoreKeeper;

    private void Update()
    {
        DisplayHealth();
        DisplayScore();
    }

    private void DisplayHealth()
    {
        healthSlider.value = (float)health.CurrentHealth / health.HealthAmount;
    }

    private void DisplayScore()
    {
        scoreTMP.text = scoreKeeper.Score.ToString();
    }
}
