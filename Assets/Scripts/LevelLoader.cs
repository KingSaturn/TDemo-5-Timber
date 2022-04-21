using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{

    private void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            LoadLevel(1);
        }
        if (Input.GetKeyDown("0"))
        {
            LoadLevel(0);
        }
        if (Input.GetKeyDown("2"))
        {
            LoadLevel(2);
        }
    }

    public Animator animator;

    public float transitiontime;

    public void LoadLevel(int index)    
    {
        StartCoroutine(IELoadLevel(index));
    }
    IEnumerator IELoadLevel(int index)
    {
        animator.SetTrigger("Load");

        yield return new WaitForSeconds(transitiontime);

        SceneManager.LoadScene(index);
    }
}
