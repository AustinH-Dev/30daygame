using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] GameObject textBox;
    TextMeshProUGUI textBoxText;
    double timer = 0.0;
    int minutes = 0;
    string output = "999:99:999";
    // Start is called before the first frame update
    void Start()
    {
        textBoxText = textBox.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 60)
        {
            minutes++;
            timer -= 60;
        }
        int milliSeconds = (int)((timer * 1000) % 1000);
        string milliString = milliSeconds.ToString("D3");
        output = (minutes + ":" + (int)timer + ":" + milliString);
        textBoxText.text = "" + output;
    }
}
