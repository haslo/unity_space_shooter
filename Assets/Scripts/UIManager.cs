using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _scoreText;
    [SerializeField]
    private Sprite[] _liveSprites;
    [SerializeField]
    private Image _livesDisplay;

    void Start()
    {

    }

    void Update()
    {
        
    }

    public void UpdateScore(int newScore)
    {
        _scoreText.text = "Score:   " + newScore.ToString("D6");
    }

    public void UpdateLives(int newLives)
    {
        _livesDisplay.sprite = _liveSprites[newLives];
    }
}
