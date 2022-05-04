using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class LevelLoader : MonoBehaviour
{
    public Animator animator;
    private LevelInfo info;
    public float transitiontime;
    public int spawnId = 0;

    private void Awake()
    {
        info = this.GetComponent<LevelInfo>();
        if (File.Exists(Path.Combine(Application.persistentDataPath, "/SaveData.txt")))
        {
            string input = File.ReadAllText(Path.Combine(Application.persistentDataPath, "/SaveData.txt"));
            SaveData data = JsonUtility.FromJson<SaveData>(input);
            Instantiate(Resources.Load("Prefabs/Player") as GameObject, info.idCoords[data.loadId], Quaternion.Euler(0, 0, 0));
        }
        else
        {
            Instantiate(Resources.Load("Prefabs/Player") as GameObject, new Vector3(-173.361115f, 0.2240448f, -32.868961f), Quaternion.Euler(0, 0, 0));
        }   
    }

    private void Update()
    {
      
    }

    public void LoadLevel(int index,int id)    
    {
        spawnId = id;
        StartCoroutine(IELoadLevel(index,id));
    }

    IEnumerator IELoadLevel(int index, int id)
    {
        SaveSerial.SaveGame(index, id);

        animator.SetTrigger("Load");

        yield return new WaitForSeconds(transitiontime);

        SceneManager.LoadScene(index);
        
    }
}
