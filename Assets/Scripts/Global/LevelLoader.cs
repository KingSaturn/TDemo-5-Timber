using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator animator;
    public SaveSerial saver;
    public float transitiontime;

    private void Update()
    {
      
    }

    public void LoadLevel(int index)    
    {
        StartCoroutine(IELoadLevel(index));
    }

    IEnumerator IELoadLevel(int index)
    {
        saver.SaveGame();

        animator.SetTrigger("Load");

        yield return new WaitForSeconds(transitiontime);

        SceneManager.LoadScene(index);
    }
}
