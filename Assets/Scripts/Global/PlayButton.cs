using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    private SaveData data;

    private void Awake()
    {
        if (File.Exists(Path.Combine(Application.persistentDataPath, "/SaveData.txt")))
        {
            string input = File.ReadAllText(Path.Combine(Application.persistentDataPath, "/SaveData.txt"));
            data = JsonUtility.FromJson<SaveData>(input);
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
