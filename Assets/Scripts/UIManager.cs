using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _scoreText, _gameOverText, _restartText;
    [SerializeField]
    private Sprite[] _liveSprites;
    [SerializeField]
    private Image _livesDisplay;
    private GameManager _gameManager;

    void Start()
    {
        _gameOverText.gameObject.SetActive(false);
        _restartText.gameObject.SetActive(false);
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
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
        newLives = newLives < 0 ? 0 : newLives >= _liveSprites.Length ? _liveSprites.Length - 1 : newLives;
        _livesDisplay.sprite = _liveSprites[newLives];
        if (newLives < 1)
        {
            GameOverSequence();
        }
    }

    private void GameOverSequence()
    {
        if (_gameManager != null)
        {
            _gameManager.SetGameOver();
        }
        StartCoroutine(GameOverFlicker());
        _restartText.gameObject.SetActive(true);
    }

    private IEnumerator GameOverFlicker()
    {
        while(true)
        {
            yield return new WaitForSeconds(Random.Range(0.0f, 0.1f));
            _gameOverText.gameObject.SetActive(true);
            yield return new WaitForSeconds(Random.Range(0.0f, 0.3f));
            _gameOverText.gameObject.SetActive(false);
        }
    }
}
