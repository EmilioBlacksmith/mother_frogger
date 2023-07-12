using System;
using Game_Manager;
using UnityEngine;
using UnityEngine.Serialization;

namespace HP_System
{
    public class HealthSystem : MonoBehaviour
    {
        [SerializeField] private int currentHealthPoints;
        [SerializeField] private int startingHealthPoints = 3;
        [SerializeField] private Transform startingPosition;
        [SerializeField] private GameObject player; 

        private bool _gameOver = false;

        public static HealthSystem Instance { get; private set; }
        public bool IsGameOver => _gameOver;

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

        public void Start()
        {
            currentHealthPoints = startingHealthPoints;
            player.transform.position = startingPosition.position;
        } 
        
        public int CurrentHealthPoints => currentHealthPoints;

        public void SubstractHealthPoint()
        {
            currentHealthPoints--;
            if (currentHealthPoints <= 0)
            {
                _gameOver = true;
            }else
            { 
                player.transform.position = startingPosition.position;
                CrashController.Instance.RestartCrashPoints();
                GameManager.Instance.RestartTimer();
                
            }
        }

        public void NextLevel()
        {
            player.transform.position = startingPosition.position;
            CrashController.Instance.RestartCrashPoints();
            GameManager.Instance.RestartTimer();
        }
    }
}
