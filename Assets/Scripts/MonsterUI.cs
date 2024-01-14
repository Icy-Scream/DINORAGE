using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MonsterUI : MonoBehaviour
{
    [SerializeField] private Slider _hungerSlider;
    [SerializeField] private Slider _happinessSlider;
    [SerializeField] private GameObject _uiIcon;

    private void Start() {
        var monsterUIList = GameObject.FindGameObjectWithTag("MAINUI");
        _uiIcon.transform.SetParent(monsterUIList.transform);
    }
    public Slider GetSlider(int slider = 0) {
        if (slider == 0)
            return _happinessSlider;
        else if (slider == 1)
        return _hungerSlider;

        return null;
    }
}
