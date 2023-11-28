using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UIElements.Image;
using Slider = UnityEngine.UI.Slider;

public class UIDisplay : MonoBehaviour
{
    [Header("Health UI")]
    [Space(1)]
    [SerializeField] private Health health;
    [SerializeField] private Slider healthSlider;

    [Header("Score UI")]
    [Space(1)]
    [SerializeField] private TextMeshProUGUI scoreTMP;

    [Header("Timer UI")]
    [Space(1)]
    [SerializeField] private TextMeshProUGUI timerTMP;

    [Header("Collectibles UI")]
    [Space(3)]
    [Header("Fast Shooter UI")]
    [Space(1)]
    [SerializeField] private GameObject fastShootImage;
    [SerializeField] private CollectiblePicker collectiblePicker;

    private ScoreKeeper scoreKeeper;
    private float _timer = 0f;

    private void Start()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    private void Update()
    {
        DisplayHealth();
        DisplayScore();
        DisplayTimer();
        DisplayFastShoot();
    }

    private void DisplayHealth()
    {
        healthSlider.value = (float)health.CurrentHealth / health.HealthAmount;
    }

    private void DisplayScore()
    {
        scoreTMP.text = scoreKeeper.Score.ToString();
    }

    private void DisplayTimer()
    {
        _timer += Time.deltaTime;
        int seconds = Mathf.RoundToInt(_timer);
        int minutes = Mathf.RoundToInt(_timer / 60f);

        string secondsInText = seconds < 10 ? "0" + seconds.ToString() : seconds.ToString();
        string minutesInText = minutes < 10 ? "0" + minutes.ToString() : minutes.ToString();

        timerTMP.text = minutesInText + " : " + secondsInText;
    }

    private void DisplayFastShoot()
    {
        if (collectiblePicker.IsFastShootActive)
        {
            fastShootImage.SetActive(true);
        }
        else
        {
            fastShootImage.SetActive(false);
        }
    }
}
