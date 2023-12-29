using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class ScoreElement : MonoBehaviour
{
    public TMP_Text rank;
    public TMP_Text usernameText;
    public Image country_Image;
    public TMP_Text ScoreText;
    public string country_Name;


    public void NewScoreElement(int _rank, string _usreName ,Sprite _flageImage, long _userScore,string _country_Names)
    {
        rank.text = "#" + _rank.ToString();
        usernameText.text = _usreName;
        ScoreText.text = _userScore.ToString();
        country_Image.sprite = _flageImage;
        country_Name = _country_Names;
    }
}
