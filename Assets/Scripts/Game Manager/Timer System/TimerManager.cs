using Character_System.HP_System;
using UnityEngine;
using UnityEngine.UI;

namespace Game_Manager.Timer_System
{
    public class TimerManager : MonoBehaviour
    {
        
        [SerializeField] private Slider timerSlider;
        [SerializeField] private float gameTime;
        private bool _timerDone;
        private float _timer;

        public float TimeLeft() => _timer;

        private void Start() => RestartTimer();
        
        private void Update()
        {
            if (!(_timer > 0) || HealthSystem.Instance.IsGameOver) return;
            
            _timer -= Time.deltaTime;
            timerSlider.value = _timer;

            if (_timer <= 0)
            {
                HealthSystem.Instance.SubtractHealthPoint();
                RestartTimer();
            }
        }
        
        public void RestartTimer()
        {
            timerSlider.maxValue = gameTime;
            timerSlider.value = gameTime;
            _timer = gameTime;
            GameManager.Instance.RestartParenting();
        }
    }
}
