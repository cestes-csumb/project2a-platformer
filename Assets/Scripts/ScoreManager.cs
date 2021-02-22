using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
     public TMPro.TextMeshProUGUI currentScore;
     public TMPro.TextMeshProUGUI currentCoins;
     private int score = 0;
     private int coins = 0;
     // Start is called before the first frame update
     void Start()
    {
          UpdateScore(0);
    }

    // Update is called once per frame
    void Update()
    {
          currentScore.SetText(score.ToString());
          currentCoins.SetText("x" + coins.ToString());
    }

     public void UpdateScore(int points) {
          score += points;
     }

     public void UpdateCoins() {
          coins++;
     }
}
