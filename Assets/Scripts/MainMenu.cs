using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject aboutPanel;
    
    public void StartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("SampleScene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ToggleAboutDeveloper()
    {
        aboutPanel.SetActive(!aboutPanel.activeSelf);
    }

    public void ExitToMenu()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null) Destroy(player);
        SceneManager.LoadScene("MainMenu");
    }
}
