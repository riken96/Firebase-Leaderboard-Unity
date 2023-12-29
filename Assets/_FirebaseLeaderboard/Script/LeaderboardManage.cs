using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Firebase.Leaderboard
{
    public class LeaderboardManage : MonoBehaviour
    {
        public void LeaderboardTabButton()
        {
            LeaderboardTabManage();
        }
        public void LeaderboardTabManage()
        {
            FirebaseManager.Instance.LeaderboardBTN();
        }
    }

}
