using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderScript : MonoBehaviour
{
    [SerializeField] private Slider _slider;

    [SerializeField] private TextMeshProUGUI _sliderText;

    private const float minValue = 1f;
    private const float maxValue = 10f;

    // Start is called before the first frame update
    void Start()
    {
        _slider.onValueChanged.AddListener((v) =>
        {
            float clampedValue = Mathf.Clamp(v, minValue, maxValue);
            _slider.value = clampedValue; // Réajuste la valeur si elle dépasse les limites
            _sliderText.text = clampedValue.ToString("0");
        });

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
