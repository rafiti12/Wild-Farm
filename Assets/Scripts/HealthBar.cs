using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public int maxHealth;
    public Gradient gradient;
    public Image fill;


    // Start is called before the first frame update
    void Start()
    {
        slider.maxValue = maxHealth;
        slider.value = maxHealth;
        fill.color = gradient.Evaluate(1f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public float damageTaken()
    {
        slider.value--;
        fill.color = gradient.Evaluate(slider.normalizedValue);
        return slider.value;
    }
}
