using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderScript : MonoBehaviour
{
    [SerializeField] public Slider _slider;

    [SerializeField] private TextMeshProUGUI _sliderText;

    private const int minValue = 1;
    private const int maxValue = 10;

    // Start is called before the first frame update
    void Start()
    {
        _slider.onValueChanged.AddListener((v) =>
        {
            int clampedValue = (int)Mathf.Clamp(v, minValue, maxValue);
            _slider.value = clampedValue; // Réajuste la valeur si elle dépasse les limites
            _sliderText.text = clampedValue.ToString("0");
        });

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
