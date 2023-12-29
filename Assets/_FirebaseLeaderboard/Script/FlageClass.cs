using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Flage", menuName = "FlageObject")]
public class FlageClass : ScriptableObject
{
    public static FlageClass instance;
    public List<CountryFlage> all_country_Flage = new List<CountryFlage>();

    private void Awake()
    {
        instance = this;
    }
}

[System.Serializable]   
public class CountryFlage
{
    [Space]
    public string country_Name;
    public Sprite country_Flage;
}
