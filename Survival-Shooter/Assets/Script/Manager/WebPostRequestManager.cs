using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class WebPostRequestManager : MonoBehaviour
{
    public InputField usernameInputField;
    public Button activeButton;

    private readonly string m_Url = "http://134.209.97.218:5051/scoreboards/13517012";

    public void OnButtonSendScore()
    {
        if (usernameInputField.text != String.Empty)
        {
            string username = usernameInputField.text;
            usernameInputField.text = "Sending Score...";
            StartCoroutine(PostRequest(username));
        }
    }

    private IEnumerator PostRequest(string username)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("score", ScoreManager.score);

        UnityWebRequest www = UnityWebRequest.Post(m_Url, form);

        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.LogError(www.error);
        }
        else
        {
            usernameInputField.text = "Score sent!";
            usernameInputField.enabled = false;
            activeButton.enabled = false;
        }
    }
}
