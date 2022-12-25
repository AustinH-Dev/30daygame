using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsHandler : MonoBehaviour
{
    [SerializeField] private Button wakeupSelectedButton;
    [SerializeField] private Button defaultTab;
    [SerializeField] private GameObject startupTab;
    [SerializeField] private List<GameObject> disableOnStartupTabs;
    void OnEnable()
    {
        wakeupSelectedButton.Select();

        //Open the options menu, disable all other tabs, make button non interactable
        //Mark GFX button as non interact

        //get every button/tab
        var buttonsToEnable = transform.parent.GetComponentsInChildren<Button>();
        foreach (Button button in buttonsToEnable)
        {
            //enable them
            button.interactable = true;
        }
        //redisable the tab we dont want active
        defaultTab.interactable = false;

        //Activate our startup tab
        startupTab.SetActive(true);

        //deactivate all other tabs
        foreach (GameObject tab in disableOnStartupTabs)
        {
            tab.SetActive(false);
        }
    }
}
