using Character_System.HP_System;
using Goal_Spots_System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game_Manager
{
    [RequireComponent(typeof(TimerManager), typeof(GoalSpotsManager))]
    public class GameManager : MonoBehaviour
    {
        private int _score = 0;
        private int _highScore = 0;

        public static GameManager Instance { get; private set; }

        [Header("UI")] 
        [SerializeField] private TextMeshProUGUI scoreText;
        private float _timer;

        [Header("Difficulty")] 
        [SerializeField] private int difficultyLever = 1;

        [Header("Player Parenting")] 
        [SerializeField] private Transform playerParent;
        [SerializeField] private Transform playerHip;

        public int DifficultyLevel() => difficultyLever;
        public void RestartParenting() => playerHip.SetParent(playerParent, true);

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

        public void GoalSpotCrossed()
        {
            HealthSystem.Instance.NextLevel();
            RestartParenting();
        }

        public void NextLevel()
        {
            HealthSystem.Instance.NextLevel();
            difficultyLever++;
            difficultyLever = Mathf.Clamp(difficultyLever, 1, 5);
            RestartParenting();
        }
    }
}
