using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelectHandler : MonoBehaviour
{
    [SerializeField] private Button wakeupSelectedButton;
    void OnEnable()
    {
        wakeupSelectedButton.Select();
    }

    public void LoadLevel(int level)
    {
        SceneManager.LoadScene(level);
    }
}