using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject mainUI;
    public GameObject mapSelectUI; 





    void Start()
    {
        mainUI.SetActive(true);
        mapSelectUI.SetActive(false);
    }

    public void StartGame()
    {
        mainUI.SetActive(false);
        mapSelectUI.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void MapSelect(int i)
    {
        SceneManager.LoadScene(i);
    }

    public void BackToMainUI()
    {
        mainUI.SetActive(true);
        mapSelectUI.SetActive(false);
    }
}
