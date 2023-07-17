using System.Linq;
using Goal_Spots_System;
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
                GameManager.Instance.NextLevel();
                foreach (var goal in goalSpotsArray)
                {
                    goal.RestartGoalSpot();
                }
            }
        }
    }
}