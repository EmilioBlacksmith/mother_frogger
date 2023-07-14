using HP_System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Game_Manager
{
    public class GameManager : MonoBehaviour
    {
        private int _score = 0;
        private int _highScore = 0;

        public static GameManager Instance { get; private set; }

        [Header("UI")] 
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private TextMeshProUGUI hiScoreText;
        [SerializeField] private Slider timerSlider;

        [Header("Timer")] 
        [SerializeField] private float gameTime;
        [HideInInspector]public bool timerDone;
        private float _timer;

        [Header("Difficulty")] 
        [SerializeField] private int difficultyLever = 1;

        [Header("Player Parenting")] 
        [SerializeField] private Transform playerParent;
        [SerializeField] private Transform playerHip;

        public int DifficultyLevel() => difficultyLever;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
        }
    
        private void Start() => RestartTimer();

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

        public void RestartTimer()
        {
            timerSlider.maxValue = gameTime;
            timerSlider.value = gameTime;
            _timer = gameTime;
            playerHip.SetParent(playerParent, true);
        }
        
        private void Update()
        {
            if (!(_timer > 0) || HealthSystem.Instance.IsGameOver) return;
            
            _timer -= Time.deltaTime;
            timerSlider.value = _timer;

            if (_timer <= 0)
            {
                HealthSystem.Instance.SubstractHealthPoint();
                RestartTimer();
            }
        }

        public void NextLevel()
        {
            HealthSystem.Instance.NextLevel();
            difficultyLever++;
            difficultyLever = Mathf.Clamp(difficultyLever, 1, 5);
            playerHip.SetParent(playerParent, true);
        }
    }
}
