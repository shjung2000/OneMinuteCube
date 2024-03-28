using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManage : MonoBehaviour
{
    public float time;
    public Text TimerText;
    public Image Fill;
    public float Max;

    // Start is called before the first frame update
    void Start()
    {
        TimerText.text = "60";
        time = 60;
        Max = 60;
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        TimerText.text = "" + (int)time;
        Fill.fillAmount = time / Max; //Fill Amount is range from 0 to 1
            
        if(time < 0)
        {
            time = 0;
        }


    }
}
