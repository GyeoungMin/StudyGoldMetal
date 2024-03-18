using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject boomsGo;
    [SerializeField] private GameObject livesGo;
    [SerializeField] private GameObject scoreGo;

    private Image[] booms;
    private Image[] lives;
    private TMP_Text scoreText;

    int totalScore;

    void Awake()
    {
        booms = boomsGo.GetComponentsInChildren<Image>();
        //lives = livesGo.GetComponentsInChildren<Image>();
        scoreText = scoreGo.GetComponentInChildren<TMP_Text>();
        totalScore = 0;
    }

    void FixedUpdate()
    {
        UIScore();
    }

    public void UIBooms(int count)
    {
        foreach(var boom in  booms)
        {
            boom.gameObject.SetActive(false);
        }
        for(int i = 0; i < count; i++)
        {
            booms[i].gameObject.SetActive(true);
        }
    }

    void UIScore()
    {
        scoreText.text = totalScore.ToString();
    }

    public void AddScore(int score)
    {
        totalScore += score;
    }
}
