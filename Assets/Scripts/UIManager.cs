using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _scoreText, _gameOverText;
    [SerializeField]
    private Sprite[] _liveSprites;
    [SerializeField]
    private Image _livesDisplay;

    void Start()
    {
        _gameOverText.gameObject.SetActive(false);
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
        if (newLives < 1)
        {
            _gameOverText.text = "GAME OVER";
            _gameOverText.gameObject.SetActive(true);
        }
    }
}
