using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    float _currentScore = 0f;

    public void AddScore(float val)
    {
        _currentScore += val;
        FindObjectOfType<InGameUISystem>().UpdateScoreText(_currentScore);
    }
}
