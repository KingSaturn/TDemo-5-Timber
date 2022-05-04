using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;
    private static Canvas menu;
    private void Awake()
    {
        menu = this.GetComponentInChildren<Canvas>();
        menu.enabled = false;
    }

    public static void PauseGame()
    {
        if (!isPaused)
        {
            isPaused = true;
            Time.timeScale = 0;
            menu.enabled = true;
        }
    }

    public static void UnpauseGame()
    {
        if (isPaused)
        {
            isPaused = false;
            Time.timeScale = 1;
            menu.enabled = false;
        }
    }

    public void SaveAndQuit()
    {
        if (File.Exists(Path.Combine(Application.dataPath + "/SaveData.txt")))
        {
            string input = File.ReadAllText(Path.Combine(Application.dataPath + "/SaveData.txt"));
            SaveData data = JsonUtility.FromJson<SaveData>(input);
            SaveSerial.SaveGame(data.scene, data.loadId);
            SceneManager.LoadScene(0);
            Time.timeScale = 1;
            isPaused = false;
        }
        else
        {
            SceneManager.LoadScene(0);
            Time.timeScale = 1;
            isPaused = false;
        }
    }
}
