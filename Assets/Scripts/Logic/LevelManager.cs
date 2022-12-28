using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public event EventHandler OnVictory;
    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void WinTheGame()
    {
        OnVictory?.Invoke(this, EventArgs.Empty);
    }
}
