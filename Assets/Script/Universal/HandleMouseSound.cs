using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleMouseSound : MonoBehaviour
{
    public void mouseClick()
    {
        FindObjectOfType<AudioManager>().Play("mouseClick");
    }

    public void mouseHover()
    {
        FindObjectOfType<AudioManager>().Play("mouseHover");
    }
}
