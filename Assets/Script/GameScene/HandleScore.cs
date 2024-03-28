using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HandleScore : MonoBehaviour
{
    public int PlayerScore;
    public TextMeshProUGUI scoreText;
    // Start is called before the first frame update


    private void Start()
    {
        PlayerScore = 0;
    }

  
    public void AddScore()
    {
        PlayerScore++;
        scoreText.text = PlayerScore.ToString();
    }

    public void MinusScore()
    {
        PlayerScore--;
        if(PlayerScore < 0)
        {
            PlayerScore = 0;
        }
        scoreText.text = PlayerScore.ToString();
    }
}
