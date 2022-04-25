using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour
{
    private SaveData data;
    private Text buttonText;

    private void Awake()
    {
        buttonText = this.GetComponentInChildren<Text>();
        if (File.Exists(Path.Combine(Application.persistentDataPath, "/SaveData.txt")))
        {
            string input = File.ReadAllText(Path.Combine(Application.persistentDataPath, "/SaveData.txt"));
            data = JsonUtility.FromJson<SaveData>(input);
            buttonText.text = "Continue";
        }
        else
        {
            buttonText.text = "New Game";
        }
    }

    public void LoadGame()
    {
        if (data != null)
        {
            SceneManager.LoadScene(data.scene);
        }
        else
        {
            SceneManager.LoadScene(2);
        }
    }

}
