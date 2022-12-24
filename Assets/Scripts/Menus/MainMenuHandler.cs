using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MainMenuHandler : MonoBehaviour
{
    [SerializeField] private Button wakeupSelectedButton;
    void OnEnable()
    {
        wakeupSelectedButton.Select();
    }
    public void QuitGame()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }
}
