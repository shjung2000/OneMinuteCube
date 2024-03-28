using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandleRecordDisplay : MonoBehaviour
{
    public Text NewBestText;
    public Text recordObtained;

    public int score;
    public bool isHighScore;
    RecordData data;

    private void Start()
    {
        data = SaveScore.loadHighScore();

        score = data.score;
        isHighScore = data.isHighScore;

        if(isHighScore)
        {
            NewBestText.text = "NEW";
        }
        else
        {
            NewBestText.text = "";
        }

        recordObtained.text = score.ToString();
    }


}
