using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;


[System.Serializable]
public class Score
{
    public string _id;
    public string nim;
    public string username;
    public string score;
}

[System.Serializable]
public class ScoreCollection
{
    public Score[] scores;
}

public class WebGetRequestManager : MonoBehaviour
{
    public Text usernameText;
    public Text scoreText;
    public Button activeButton;
    public int scoreNumber = 5;
    
    
    private readonly string m_Url = "http://134.209.97.218:5051/scoreboards/13517012";
    private Text m_ButtonText;

    private void Start()
    {
        m_ButtonText = activeButton.GetComponentInChildren<Text>();
    }

    public void OnButtonGetScore()
    {
        m_ButtonText.text = "WAIT";
        StartCoroutine(GetRequest());
    }

    private IEnumerator GetRequest()
    {
        UnityWebRequest www = UnityWebRequest.Get(m_Url);

        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.LogError(www.error);
        }
        else
        {
            showHighscore(www.downloadHandler.text);
        }
    }

    void showHighscore(string highscoreText)
    {
        usernameText.text = "";
        scoreText.text = "";
        ScoreCollection highscoreList = JsonUtility.FromJson<ScoreCollection>("{\"scores\":" + highscoreText + "}");
        for (int i = 0; i < Math.Min(scoreNumber, highscoreList.scores.Length); i++)
        {
            Score score = highscoreList.scores[i];
            usernameText.text += score.username + "\n";
            scoreText.text += score.score + "\n";
        }
        m_ButtonText.text = "REFRESH";

    }
}
