using UnityEngine;
using UnityEngine.SceneManagement;
public class HandlePause : MonoBehaviour
{
    public GameObject pauseScreen;

    private void Start()
    {
        pauseScreen = GameObject.FindGameObjectWithTag("pauseScreen");
    }
    public void returnToMainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
        Time.timeScale = 1;
    }

    public void resumeGame()
    {
        pauseScreen.SetActive(false);
        Time.timeScale = 1;
        FindObjectOfType<AudioManager>().Play("clock");
    }
}
