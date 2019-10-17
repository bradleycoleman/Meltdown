﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public abstract class ScoreController : MonoBehaviour
    {

    public Slider slider;
    protected float currentValue = 1f;
    public Image barImage;

    protected static readonly float DEFAULT_RATE = 0.003f;
    protected static readonly float ONE_STAR_RATE = 0.001f;
    protected static readonly float TWO_STAR_RATE = 0.003f;
    protected static readonly float THREE_STAR_RATE = 0.005f;


    protected float increaseRate = DEFAULT_RATE;
    public TextMeshProUGUI scoreText;

    public TextMeshProUGUI temp;
    private readonly string degreesText = "°";
    private readonly float hotTemp = 20f;
    private readonly float coldTemp = 10f;

    private void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        currentValue += increaseRate * Time.deltaTime;
        //Fix value between 0 and 1
        currentValue = Mathf.Clamp(currentValue, 0f, 1f);

        slider.value = currentValue;
        barImage.fillAmount = currentValue;

        calculateRate();
        Score.GetInstance().SetPoints(currentValue);
        scoreText.text = Score.GetInstance().GetPoints().ToString();
        
        //Cheat to decrease temperature
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.T))
        {
            currentValue -= 0.2f;
        }

        updateScenery();

        UpdateTempText();
    }


    public void taskScored(float points)
    {
        currentValue -= points;
    }

    public void taskFailed(float points)
    {
        currentValue += points;
    }

    protected virtual void calculateRate()
    {
        float newRate = DEFAULT_RATE;
        if (1 - currentValue > ScoreDisplay.THREESTARTHRESHOLD / 10000f)
        {
            newRate += THREE_STAR_RATE;
        }
        else if (1 - currentValue > ScoreDisplay.TWOSTARTHRESHOLD / 10000f)
        {
            newRate += TWO_STAR_RATE;
        }
        else if (1 - currentValue > ScoreDisplay.ONESTARTHRESHOLD / 10000f)
        {
            newRate += ONE_STAR_RATE;
        }
        increaseRate = newRate;
    }

    protected abstract void updateScenery();

    private void UpdateTempText()
    {
        float currentTemp = (currentValue) * (hotTemp) + (1 - currentValue) * (coldTemp);

        //Round temp to 2dp
        currentTemp = (float) System.Math.Round(currentTemp, 2);

        temp.text = currentTemp.ToString() + degreesText;
    }

}
