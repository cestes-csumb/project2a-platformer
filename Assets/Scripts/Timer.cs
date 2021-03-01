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
          //if we run out of time update the timer to display game over
          if (timerValue <= 0)
          {
               timer.text = "Game Over!";
          }
          else {
               //truncate float value to just the whole number (if necessary)
               if (timerValue.ToString().Length > 3 || timerValue < 100)
               {
                    truncatedTime = timerValue.ToString().Remove(3);
               }
               //there is now additional truncation code to account for 2 digit values and 1 digit values
               if (timerValue < 100)
               {
                    if (timerValue < 10)
                    {
                         truncatedTime = timerValue.ToString().Remove(1);
                    }
                    else {
                         truncatedTime = timerValue.ToString().Remove(2);
                    }

               }
               else
               {
                    truncatedTime = timerValue.ToString();
               }
               //update display
               timer.text = truncatedTime;
          }
    }

}
