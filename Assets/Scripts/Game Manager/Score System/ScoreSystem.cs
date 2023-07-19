using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game_Manager.Score_System
{
    public class ScoreSystem : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private GameObject gameOverUI;
        
        private int _score = 0;

        public int Score() => _score;

        private void Start()
        {
            _score = 0;
            scoreText.text = _score.ToString();
        }

        public void AddScore(int addedScore)
        {
            _score += addedScore;
            scoreText.text = _score.ToString();
        }

        public void FrogCrossed()
        {
            AddScore(50);
            AddScore((int)(GameManager.Instance.TimerManager.TimeLeft()/2) * 10);
        }

        public void AllGoalSpotsCrossed() => AddScore(1000);

        public void ShowGameOverUI() => gameOverUI.SetActive(true);
    }
}
