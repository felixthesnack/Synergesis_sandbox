using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public static bool autoPlay = false;

    public GameObject PauseMenuUI;
    public GameObject AutoPlayOn;
    public GameObject AutoPlayOff;
    public GameObject DebugMenuOn;
    public GameObject DebugMenuOff;
    public GameObject DebugMenu;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Resume();
            } else
            {
                Pause();
            }
        }
        
    }

    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;   
    }

    public void Pause()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void ToggleAutoPlay()
    {
        if (!autoPlay)
        {
            AutoPlayOn.SetActive(true);
            AutoPlayOff.SetActive(false);
            autoPlay = true;
            Debug.Log("AutoPlay is on.");
        }
        else
        {
            AutoPlayOn.SetActive(false);
            AutoPlayOff.SetActive(true);
            autoPlay = false;
            Debug.Log("AutoPlay is off.");
        }
    }

    public void ToggleDebugMenu()
    {
        if (!DebugMenu.activeInHierarchy)
        {
            DebugMenu.SetActive(true);
            DebugMenuOn.SetActive(true);
            DebugMenuOff.SetActive(false);
            Debug.Log("Debug Menu is on.");
        }
        else
        {
            DebugMenu.SetActive(false);
            DebugMenuOn.SetActive(false);
            DebugMenuOff.SetActive(true);
            Debug.Log("Debug Menu is on.");
        }
    }
}