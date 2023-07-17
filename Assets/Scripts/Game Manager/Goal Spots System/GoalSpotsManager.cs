using System.Linq;
using Game_Manager;
using UnityEngine;
using UnityEngine.Serialization;

namespace Goal_Spots_System
{
    public class GoalSpotsManager : MonoBehaviour
    {
        [SerializeField] private GoalSpot[] goalSpotsArray;
        
        public static GoalSpotsManager Instance { get; private set; }
        
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

        public void CheckAllSpots()
        {
            int totalTrue = goalSpotsArray.Count(c => c.IsAvailable() == true);

            if (totalTrue <= 0)
            {
                GameManager.Instance.NextLevel();
                foreach (var goal in goalSpotsArray)
                {
                    goal.RestartGoalSpot();
                }
            }
        }
    }
}