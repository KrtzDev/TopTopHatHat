using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    public TextMeshProUGUI textMeshPrefab;
    public GameObject takePanel;
    public GameObject givePanel;

    public void ContinueGame()
    {
        Time.timeScale = 1;
        GameManager.instance.PauseMenu.gameObject.SetActive(false);
    }

    public void GoToMainMenu()
    {
        SceneLoader.instance.LoadScene("SCENE_Main_Menu");
    }

    public void AddTaskToText_Take(string text)
    {
        TextMeshProUGUI temp = textMeshPrefab;
        temp.text = text;
        Instantiate(temp, takePanel.transform);
    }

    public void AddTaskToText_Give(string text)
    {
        TextMeshProUGUI temp = textMeshPrefab;
        temp.text = text;
        Instantiate(temp, givePanel.transform);
    }
}
