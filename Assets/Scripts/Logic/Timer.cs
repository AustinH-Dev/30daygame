using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Timer : MonoBehaviour
{
    [SerializeField] GameObject textBox;
    TextMeshProUGUI textBoxText;
    public float timer = 0.0f;
    string output = "999:99:999";

    bool gameIsOver = false;
    // Start is called before the first frame update
    void Start()
    {
        //Get a level manager componenet, subscribe to the recieve victory event
        LevelManager levelManager = GetComponent<LevelManager>();
        levelManager.OnVictory += RecieveVictory;

        textBoxText = textBox.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {

        if (!gameIsOver)
        {
            //add to the timer
            timer += Time.deltaTime;

            //Setup our strings so that they have nice padding
            string secondString = ((int)(timer % 60)).ToString("D2");
            int milliSeconds = (int)((timer * 1000) % 1000);
            string milliString = milliSeconds.ToString("D3");

            //update the textbox
            output = ((int)(timer / 60) + ":" + secondString + ":" + milliString);
            textBoxText.text = output;
        }
    }

    private void RecieveVictory(object Sender, EventArgs e)
    {
        gameIsOver = true;
        int level = SceneManager.GetActiveScene().buildIndex;

        switch (level)
        {
            case 1:
                if (PlayerPrefs.GetFloat("Level1Best", 99999999999) > timer)
                {
                    PlayerPrefs.SetFloat("Level1Best", timer);
                }
                break;
            case 2:
                if (PlayerPrefs.GetFloat("Level2Best", 99999999999) > timer)
                    PlayerPrefs.SetFloat("Level2Best", timer);
                break;
            case 3:
                if (PlayerPrefs.GetFloat("Level3Best", 99999999999) > timer)
                    PlayerPrefs.SetFloat("Level3Best", timer);
                break;
            case 4:
                if (PlayerPrefs.GetFloat("Level4Best", 99999999999) > timer)
                    PlayerPrefs.SetFloat("Level4Best", timer);
                break;
            case 5:
                if (PlayerPrefs.GetFloat("Level5Best", 99999999999) > timer)
                    PlayerPrefs.SetFloat("Level5Best", timer);
                break;
            case 6:
                if (PlayerPrefs.GetFloat("Level6Best", 99999999999) > timer)
                    PlayerPrefs.SetFloat("Level6Best", timer);
                break;
        }
    }
}
