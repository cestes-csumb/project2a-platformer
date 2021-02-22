using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
     public float timerValue = 300f;
     public TMPro.TextMeshProUGUI timer;
     private string truncatedTime;
     // Start is called before the first frame update
    void Start()
    {
          //set initial time
          timer.text = timerValue.ToString();
    }

     // Update is called once per frame
     void Update()
     {
          //tick time down
          timerValue -= Time.deltaTime;
          //truncate float value to just the whole number (if necessary)
          if (timerValue.ToString().Length > 3)
          {
               truncatedTime = timerValue.ToString().Remove(3);
          }
          else {
               truncatedTime = timerValue.ToString();
          }
          //update display
          timer.text = truncatedTime;
    }

}
