using UnityEngine;
using UnityEngine.SceneManagement;


public class HandleCloseOne : MonoBehaviour
{

    public void closeInstruction()
    {   
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 3);
    }
}
