using TMPro;
using UnityEngine;

namespace Game_Manager.Score_System
{
    public class ScoreSystem : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreText;
        
        private int _score = 0;
        private int _highScore = 0;
        
        public void AddScore(int addedScore)
        {
            _score += addedScore;
            scoreText.text = "" + _score;
        }

        public void FrogCrossed()
        {
            AddScore(50);
            AddScore((int)(GameManager.Instance.TimerManager.TimeLeft()/2) * 10);
        }
    }
}
