using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandleScoreDisplay : MonoBehaviour
{
    public Text scoreText;
    void Start()
    {
        scoreText.text = SaveScore.loadScore().ToString();
    }

}
