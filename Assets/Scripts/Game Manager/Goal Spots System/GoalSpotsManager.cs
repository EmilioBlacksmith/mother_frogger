using Audio;
using System.Linq;
using UnityEngine;

namespace Game_Manager.Goal_Spots_System
{
    public class GoalSpotsManager : MonoBehaviour
    {
        [SerializeField] private GoalSpot[] goalSpotsArray;

        public void CheckAllSpots()
        {
            int totalTrue = goalSpotsArray.Count(c => c.IsAvailable() == true);

            if (totalTrue <= 0)
            {
                GameManager.Instance.NextDifficulty();
                AudioSystem.Instance.NextLevelAudioChange(GameManager.Instance.DifficultyLevel());
                foreach (var goal in goalSpotsArray)
                {
                    goal.RestartGoalSpot();
                }
            }
        }
    }
}