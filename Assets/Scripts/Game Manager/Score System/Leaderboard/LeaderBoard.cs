using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using Dan.Main;
using Unity.VisualScripting;

namespace Game_Manager.Score_System.Leaderboard
{
    public class LeaderBoard : MonoBehaviour
    {
        [SerializeField] private List<TextMeshProUGUI> names;
        [SerializeField] private List<TextMeshProUGUI> scores;
        [SerializeField] private List<GameObject> leaderboardList;

        private readonly string publicLeaderboardKey = 
            "77856b344686ba177464e866b70890f6ab590aa3f37a3d7e40684030ad3936f1";

        public void GetLeaderboard()
        {
            
            LeaderboardCreator.GetLeaderboard(publicLeaderboardKey, ((msg) =>
            {
                int loopLength = (msg.Length < names.Count) ? msg.Length : names.Count;
                
                for (int i = 0; i < loopLength; ++i)
                {
                    names[i].text = msg[i].Username;
                    scores[i].text = msg[i].Score.ToString();
                    leaderboardList[i].SetActive(true);
                }

                if (msg.Length < names.Count)
                {
                    for (int i = loopLength; i < names.Count; ++i)
                    {
                        leaderboardList[i].SetActive(false);
                    }
                }
                else
                {
                    for (int i = 0 - 1; i < names.Count; ++i)
                    {
                        leaderboardList[i].SetActive(true);
                    }
                }
                
            }));
        }

        public void SetLeaderboardEntry(string username, int score)
        {
            LeaderboardCreator.UploadNewEntry(publicLeaderboardKey, username, score, (msg) =>
            {
                GetLeaderboard();
            });
        }
    }
}
