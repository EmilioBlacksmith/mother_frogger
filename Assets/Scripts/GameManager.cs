using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private int _score = 0;
    private int _highScore = 0;
    private bool _gameOver = false;
    private bool _endLevel = false;
    private int _frogsSaved = 0;

    [HideInInspector]public static GameManager Instance;

    [Header("UI")] 
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI hiscoreText;
    [SerializeField] private Slider timerSlider;

    [Header("Timer")] 
    [SerializeField] private float gameTime;
    private float timer;
    [HideInInspector]public bool timerDone;

    [Header("Frogger Saved System")] 
    [SerializeField] private int frogsToSave;

    private void Start() => RestartTimer();
    public void GameOver() => _gameOver = true;

    public void SavedFrog()
    {
        AddScore(50);
        _frogsSaved++;
    }

    public void AddScore(int addedScore)
    {
        _score += addedScore;
        scoreText.text = "" + _score;
    }

    private void EndLevel()
    {
        if (_highScore <= _score)
        {
            _highScore = _score;
        }
    }

    private void RestartTimer()
    {
        timerSlider.maxValue = gameTime;
        timerSlider.value = gameTime;
        timer = gameTime;
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (_gameOver) return;

        if (timer >= 0 && !_endLevel)
        {
            timer -= Time.deltaTime;
            timerSlider.value = timer;
        }
        
        if (_frogsSaved >= frogsToSave && !_endLevel)
        {
            _endLevel = true;
            EndLevel();
        }
    }
}
