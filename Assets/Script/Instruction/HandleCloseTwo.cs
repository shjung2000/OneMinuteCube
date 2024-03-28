using UnityEngine;
using UnityEngine.SceneManagement;

public class HandleCloseTwo : MonoBehaviour
{

  
    public void CloseInstruction()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
