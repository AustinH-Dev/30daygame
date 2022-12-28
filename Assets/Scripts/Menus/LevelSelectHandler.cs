using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class LevelSelectHandler : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI[] highscores;
    string output = "Not Yet Beaten!";
    // Start is called before the first frame update
    void Start()
    {
        PopulateHighScores();
    }

    public void LoadLevel(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
    }
    private void PopulateHighScores()
    {
        float timer = 99999999999;
        for (int i = 0; i < 6; i++)
        {
            switch (i + 1)
            {
                case 1:
                    timer = PlayerPrefs.GetFloat("Level1Best", 99999999999);
                    break;
                case 2:
                    timer = PlayerPrefs.GetFloat("Level2Best", 99999999999);
                    break;
                case 3:
                    timer = PlayerPrefs.GetFloat("Level3Best", 99999999999);
                    break;
                case 4:
                    timer = PlayerPrefs.GetFloat("Level4Best", 99999999999);
                    break;
                case 5:
                    timer = PlayerPrefs.GetFloat("Level5Best", 99999999999);
                    break;
                case 6:
                    timer = PlayerPrefs.GetFloat("Level6Best", 99999999999);
                    break;
            }
            if (timer != 99999999999) highscores[i].text = "Best Time: " + GetStringOfTimer(timer);
            else highscores[i].text = "Not Yet Beaten!";
        }
    }

    private string GetStringOfTimer(float timer)
    {
        //Setup our strings so that they have nice padding
        string minuteString = ((int)(timer / 60)).ToString("D2");
        string secondString = ((int)(timer % 60)).ToString("D2");
        int milliSeconds = (int)((timer * 1000) % 1000);
        string milliString = milliSeconds.ToString("D3");
        return (minuteString + ":" + secondString + ":" + milliString);
    }

    public void ClearTimes()
    {
        PlayerPrefs.DeleteKey("Level1Best");
        PlayerPrefs.DeleteKey("Level2Best");
        PlayerPrefs.DeleteKey("Level3Best");
        PlayerPrefs.DeleteKey("Level4Best");
        PlayerPrefs.DeleteKey("Level5Best");
        PlayerPrefs.DeleteKey("Level6Best");

        foreach (TextMeshProUGUI score in highscores)
        {
            score.text = "Not Yet Beaten!";
        }
    }
}
