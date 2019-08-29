using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : Health
{
    public int CurrentLives = 5;
    public GameObject Heart;
    public Slider Slider;
    private readonly List<GameObject> _hearts = new List<GameObject>();

    private void AddLife()
    {
        var heart = Instantiate(Heart, Slider.transform.parent);
        heart.GetComponent<RectTransform>().localPosition = new Vector3((_hearts.Count - 1) * 30, 0, 0);
        _hearts.Add(heart);
    }

    private void RemoveLife()
    {
        Destroy(_hearts[_hearts.Count-1].gameObject);
        _hearts.Remove(_hearts[_hearts.Count - 1]);
        if (_hearts.Count < 0)
        {
            GameOver();
        }
    }

    void Update()
    {
        //Updates the hearts on the screen
        if (_hearts.Count < CurrentLives)
        {
            AddLife();
        }
        else if (_hearts.Count > CurrentLives && _hearts.Count > 0)
        {
            RemoveLife();
        }
        //Matches the slider to the player's health
        Slider.value = CurrentHealth / MaxHealth;
        if (CurrentHealth <= 0)
        {
            IsDead();
        }
    }

    public void GameOver()
    {
        //What happens when player loses all lives
    }

    public override void IsDead()
    {
        CurrentLives -= 1;
        CurrentHealth = MaxHealth;
        //Restart Game
    }
}
