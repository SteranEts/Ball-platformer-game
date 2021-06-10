using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject homeMenu;
    public GameObject uiHome;
    public GameObject uiOptions;
    public GameObject soundFxObject;
    public GameObject musicObject;

    public GameObject sliderFx;
    public GameObject sliderMusic;
    // Update is called once per frame
    void Update()
    {
        soundFxObject.GetComponent<PlayerController>().soundFxVolume = sliderFx.GetComponent<Slider>().value;
        musicObject.GetComponent<AudioSource>().volume = sliderMusic.GetComponent<Slider>().value;
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
        
    }

    public void Resume() 
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        //musicObject
    }
    public void Pause() 
    {
        if (!pauseMenuUI.activeInHierarchy)
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void toggleModeMenu() 
    {
        bool stateBefore = uiHome.activeSelf;
        uiHome.SetActive(!stateBefore);
        uiOptions.SetActive(stateBefore);
    }
}
