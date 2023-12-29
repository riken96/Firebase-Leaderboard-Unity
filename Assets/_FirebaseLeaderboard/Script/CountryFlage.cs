using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Networking;

namespace Firebase.Leaderboard
{
    public class CountryFlage : MonoBehaviour
    {
        public string ip;
        private string apiKey = "YOUR_API_KEY";
        private string apiUrl = "https://ipinfo.io/json";

        void Start()
        {
            StartCoroutine(GetLocationData());
        }

        IEnumerator GetLocationData()
        {
            ip = new WebClient().DownloadString("http://icanhazip.com");
            ip = ip.Replace("\n", "");
            // yield return new WaitForSeconds(1);
            string ipads = "https://ipapi.co/" + ip + "/json/";
            // Debug.Log("IP Address: " + ip);
            // Debug.Log("IP Address Length: " + ip.Length);
            //  Debug.Log("IP URL: " + ipads);
            UnityWebRequest www = UnityWebRequest.Get(ipads);
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.LogError("Error: " + www.error);
            }
            else
            {
                // Parse JSON response to get country code
                string jsonResponse = www.downloadHandler.text;
                LocationData locationData = JsonUtility.FromJson<LocationData>(jsonResponse);

                if (locationData != null)
                {
                    string countryCode = locationData.country_calling_code;
                    Debug.Log("Country Code: " + countryCode);
                    FirebaseManager.Instance.userCountry = locationData.country_name;
                    FirebaseManager.Instance.userCountry_Code = countryCode;
                }
                else
                {
                    Debug.LogError("Failed to parse JSON response.");
                }
            }
            FirebaseManager.Instance.SaveData();

        }


    }
}


[System.Serializable]
public class LocationData
{
    public string ip;
    public string network;
    public string version;
    public string city;
    public string region;
    public string region_code;
    public string country;
    public string country_name;
    public string country_code;
    public string country_code_iso3;
    public string country_capital;
    public string country_tld;
    public string continent_code;
    public bool in_eu;
    public string postal;
    public double latitude;
    public double longitude;
    public string timezone;
    public string utc_offset;
    public string country_calling_code;
    public string currency;
    public string currency_name;
    public string languages;
    public double country_area;
    public int country_population;
    public string asn;
    public string org;
}
