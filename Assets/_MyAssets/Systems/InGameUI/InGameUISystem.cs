using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InGameUISystem : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _scoreText;


    public void UpdateScoreText(float val)
    {
        _scoreText.text = val.ToString();
    }
}
