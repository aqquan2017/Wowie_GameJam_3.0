using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject mainUI;
    public GameObject mapSelectUI;

    // Start is called before the first frame update
    void Start()
    {
        mainUI.SetActive(true);
        mapSelectUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
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
