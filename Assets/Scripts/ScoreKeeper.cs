using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    private int _score;

    public int Score { get => _score; }
        
    public void AddScore(int value)
    {
        _score += value;
        _score = (int)Mathf.Max(0f, _score);
    }

    public void ResetScore() => _score = 0;
}
