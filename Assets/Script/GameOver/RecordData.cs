using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]  //Making sure that this data can be saved into a file
public class RecordData
{
    public int score;
    public bool isHighScore;


    public RecordData(int score , bool isHighScore)
    {
        this.score = score;
        this.isHighScore = isHighScore;
    }
}
