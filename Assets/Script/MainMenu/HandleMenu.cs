using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HandleMenu : MonoBehaviour
{
    private void Start()
    {   
        FindObjectOfType<AudioManager>().Play("mainMenu");   
    }

    public void PlayGame()
    { 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        FindObjectOfType<AudioManager>().Stop("mainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OpenInstruction()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 3);
    }
}
