using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBar : MonoBehaviour
{
    [SerializeField] private Image _barBackground;
    private Image _bar;

    private void Awake()
    {
        _bar = GetComponent<Image>();
        //  _barBackground = gameObject.GetComponentInParent<Image>();
    }

    public void SetValue(int val, int maxVal)
    {
        _bar.fillAmount = (float)val / maxVal;
        _barBackground.rectTransform.sizeDelta = new Vector2(20 * maxVal, 20);
        _bar.rectTransform.sizeDelta = new Vector2(20 * maxVal, 20);
    }
}