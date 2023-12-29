using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Firebase.Leaderboard
{
    public class FirebaseManager : MonoBehaviour
    {
        public static FirebaseManager Instance;
        public FlageClass allFlage;
        public List<Data> allLoginData = new List<Data>();
        public GameObject prefeb;
        public GameObject parent;
        public string userCountry_Code;
        public string userCountry;
        public string scriptebal;
        Sprite flageImage;

        [Space]
        public DataStore dataStore = new DataStore();
        public DatabaseReference reference;
        public string dataBasseURL = "https://soldiers-d6791-default-rtdb.firebaseio.com/";
        public Uri databaseUri;

        private void Awake()
        {
            Instance = this;
            databaseUri = new Uri(dataBasseURL);
            FirebaseApp.DefaultInstance.Options.DatabaseUrl = databaseUri;
            reference = FirebaseDatabase.DefaultInstance.RootReference;
        }

        private void Start()
        {
            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
            {
                var dependencyStatus = task.Result;
                if (dependencyStatus == DependencyStatus.Available)
                {
                    Debug.Log($"Dependency Status: {dependencyStatus}");
                }
                else
                {
                    Debug.LogError("Could not resolve all Firebase dependencies: " + dependencyStatus);
                }
            });
        }

        public void LeaderboardBTN()
        {
            StartCoroutine(Leaderboard());
        }


        public void SaveData()
        {
            Debug.Log("SaveData");
            dataStore = new DataStore(
               SystemInfo.deviceUniqueIdentifier,
               100,
               userCountry_Code, userCountry
               );

            string Data = JsonUtility.ToJson(dataStore);
            reference.Child("Users").Child(SystemInfo.deviceUniqueIdentifier).SetRawJsonValueAsync(Data);
        }

        public IEnumerator Leaderboard()
        {
            Task<DataSnapshot> DBTask = reference.Child("Users").OrderByChild("UserScore").GetValueAsync();

            yield return new WaitUntil(predicate: () => DBTask.IsCompleted);
            if (DBTask.Exception != null)
            {
                Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
            }
            else
            {
                DataSnapshot snapshot = DBTask.Result;

                foreach (Transform child in parent.transform)
                {
                    Destroy(child.gameObject);
                }
                allLoginData.Clear();

                foreach (DataSnapshot childSnapshot in snapshot.Children.Reverse<DataSnapshot>())
                {
                    Data Datas = new Data();
                    Datas.Name = childSnapshot.Child("userName").Value.ToString();
                    Datas.score = int.Parse(childSnapshot.Child("userScore").Value.ToString());
                    Datas.country_Code = childSnapshot.Child("userCountryCode").Value.ToString();
                    Datas.country_Name = childSnapshot.Child("userCountry").Value.ToString();
                    allLoginData.Add(Datas);
                }
                allLoginData = allLoginData.OrderByDescending(o => o.score).ToList();
                for (int i = 0; i < allLoginData.Count; i++)
                {
                    if (i < 100)
                    {
                        GameObject scoreboardElement = Instantiate(prefeb, parent.transform);
                        if (allLoginData[i].country_Name == "Kazakhstan")
                        {
                            allLoginData[i].country_Code = "0.1";
                        }
                        if (allLoginData[i].country_Name == "Canada")
                        {
                            allLoginData[i].country_Code = "0.2";
                        }
                        if (allLoginData[i].country_Name == "Morocco")
                        {
                            allLoginData[i].country_Code = "0.3";
                        }


                        for (int j = 0; j < allFlage.all_country_Flage.Count; j++)
                        {
                            if (allLoginData[i].country_Code == allFlage.all_country_Flage[j].country_Name)
                            {
                                allLoginData[i].flage = allFlage.all_country_Flage[j].country_Flage;
                            }
                        }
                        scoreboardElement.GetComponent<ScoreElement>().NewScoreElement((i + 1), allLoginData[i].Name, allLoginData[i].flage, allLoginData[i].score, allLoginData[i].country_Name);
                    }
                }
            }
        }
    }
}

public class DataStore
{
    public string userName;
    public long userScore;
    public string userCountryCode;
    public string userCountry;

    public DataStore(string _userName, long _userScore, string _country, string _userCountrys)
    {
        userName = _userName;
        userScore = _userScore;
        userCountryCode = _country;
        userCountry = _userCountrys;
    }

    public DataStore()
    {
    }
}


[System.Serializable]
public class Data
{
    public string Name;
    public long score;
    public string country_Code;
    public Sprite flage;
    public string country_Name;
}