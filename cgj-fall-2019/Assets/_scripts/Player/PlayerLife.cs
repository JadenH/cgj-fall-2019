using System;
using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics.Eventing.Reader;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerLife : GameBehaviour
{
    public int CurrentLives = 5;
    public TextMeshProUGUI HeartCount;
    public Slider Slider;

    void Update()
    {
        HeartCount.text = "X" + CurrentLives;
        if (CurrentLives <= 0)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
    }
}
