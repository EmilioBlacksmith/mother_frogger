using Character_System.HP_System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuSystem : MonoBehaviour
{
    [SerializeField] private GameObject PauseMenu;
    [SerializeField] private Button buttonSelected;

    public bool isPaused { get; private set; }

    public static PauseMenuSystem Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;

        isPaused = false;
    }

    private void Update()
    {
        bool pressPause = Keyboard.current.escapeKey.wasPressedThisFrame 
            || Gamepad.current.startButton.wasPressedThisFrame 
            || Gamepad.current.selectButton.wasPressedThisFrame
            || Keyboard.current.pKey.wasPressedThisFrame;

        if (pressPause && !isPaused)
        {
            PauseGame();            
        }else if (pressPause && isPaused)
        {
            ResumeGame();
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
        PauseMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(buttonSelected.gameObject);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        isPaused = true;
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    public void ExitGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        PauseMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        isPaused = false;
    }
}
