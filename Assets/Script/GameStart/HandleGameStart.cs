using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HandleCount : MonoBehaviour
{

  
    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {   
            FindObjectOfType<AudioManager>().Play("clock");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
