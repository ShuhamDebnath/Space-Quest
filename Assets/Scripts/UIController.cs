using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    public static UIController Instance; 


    [SerializeField] private Slider energySlider;
    [SerializeField] private TMP_Text energyText;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private Slider experianceSlider;
    [SerializeField] private TMP_Text experianceText;
    public GameObject pausePannel;



    void Awake()
    {
        if(Instance != null) Destroy(gameObject);
        else Instance = this;
    }

    public void UpdateEnergySlider(float current, float max)
    {
        energySlider.maxValue = max;
        energySlider.value =  Mathf.RoundToInt(current);
        energyText.text = energySlider.value.ToString() +" / "+ energySlider.maxValue.ToString() ;
    }

    public void UpdateHealthSlider(float current, float max)
    {
        healthSlider.maxValue = max;
        healthSlider.value =  Mathf.RoundToInt(current);
        healthText.text = healthSlider.value.ToString() +" / "+ healthSlider.maxValue.ToString() ;
    }
    public void UpdateExperianceSlider(float current, float max)
    {
        experianceSlider.maxValue = max;
        experianceSlider.value =  Mathf.RoundToInt(current);
        experianceText.text = experianceSlider.value.ToString() +" / "+ experianceSlider.maxValue.ToString() ;
    }

}
