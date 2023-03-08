using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _scoreText;

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
}
