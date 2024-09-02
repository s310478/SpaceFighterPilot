using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Camera's")]
    [SerializeField] GameObject mainCamera;
    [SerializeField] GameObject menuCamera;

    [Header("Music")]
    [SerializeField] GameObject playMusic;
    [SerializeField] GameObject menuMusic;

    [Header("Timeline")]
    [SerializeField] GameObject masterTimeline;

    [Header("UI")]
    [SerializeField] GameObject playUI;
    [SerializeField] GameObject menuUI;
    [SerializeField] GameObject startButton;
    [SerializeField] Slider volumeSlider;

    [Header("Player Ship")]
    [SerializeField] GameObject playerShip;
    [SerializeField] GameObject laserLeft;
    [SerializeField] GameObject laserRight;

    // Start is called before the first frame update
    void Start()
    {
        MenuScreen();
    }

    public void MenuScreen()
    {
        mainCamera.SetActive(false);
        playMusic.SetActive(false);
        masterTimeline.SetActive(false);
        playUI.SetActive(false);
        laserLeft.SetActive(false);
        laserRight.SetActive(false);

        menuCamera.SetActive(true);
        menuMusic.SetActive(true);
        menuUI.SetActive(true);

        PlayerControls playerControls = playerShip.GetComponent<PlayerControls>();
        if (playerControls != null)
        {
            playerControls.enabled = false;
        }

        Oscillator oscillator = playerShip.GetComponent<Oscillator>();
        if (oscillator != null)
        {
            oscillator.enabled = true;
        }

        EventSystem.current.SetSelectedGameObject(startButton);
    }

    public void StartGame()
    {
        menuCamera.SetActive(false);
        menuMusic.SetActive(false);
        menuUI.SetActive(false);

        masterTimeline.SetActive(true);
        mainCamera.SetActive(true);
        playMusic.SetActive(true);
        playUI.SetActive(true);
        laserLeft.SetActive(true);
        laserRight.SetActive(true);

        PlayerControls playerControls = playerShip.GetComponent<PlayerControls>();
        if (playerControls != null)
        {
            playerControls.enabled = true;
        }

        Oscillator oscillator = playerShip.GetComponent<Oscillator>();
        if (oscillator != null)
        {
            oscillator.enabled = false;
        }
    }

    public void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        MenuScreen();
    }
}
