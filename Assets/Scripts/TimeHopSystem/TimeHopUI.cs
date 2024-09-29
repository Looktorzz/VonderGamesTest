using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TimeHopUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _timeLabel;

    [SerializeField]
    private TextMeshProUGUI _dayLabel;

    [SerializeField]
    private TextMeshProUGUI _dayNumberLabel;

    [SerializeField]
    private Slider _timePhaseSlider;

    [SerializeField]
    private TimeHopController _timeHopController;

    private const float _timePhaseSliderPoint = 0.01f;
    private const float _timePhaseSliderDelay = 0.05f;
    private const float _timeout = 5f;

    private bool _isInit;
    private Coroutine _currentRoutine;

    public void Setup()
    {
        Init();

        _timeHopController.WhenTimeChanged += OnUpdateTimeUI;
        _timeHopController.WhenDayChanged += OnUpdateDayUI;
        _timeHopController.WhenDayNumberChanged += OnUpdateDayNumberUI;
    }

    public void Deactive()
    {
        _timeHopController.WhenTimeChanged -= OnUpdateTimeUI;
        _timeHopController.WhenDayChanged -= OnUpdateDayUI;
        _timeHopController.WhenDayNumberChanged -= OnUpdateDayNumberUI;
    }

    private void Init()
    {
        if (_isInit)
        {
            return;
        }

        OnUpdateTimeUI(Times.Morning);
        OnUpdateDayUI(Days.Monday);
        OnUpdateDayNumberUI(0);

        _isInit = true;
    }

    private void OnUpdateTimeUI(Times time)
    {
        TimePhaseSliderUI(time);
        _timeLabel.text = time.ToString();
    }

    private void TimePhaseSliderUI(Times time)
    {
        int timesAmount = Enum.GetValues(typeof(Times)).Length;
        float maxTimePhaseSlider = _timePhaseSlider.maxValue;

        float currentTime = (float)time;
        float startPoint = currentTime / timesAmount * maxTimePhaseSlider;

        float nextTime = currentTime + 1;
        float endPoint = nextTime / timesAmount * maxTimePhaseSlider;

        if (_currentRoutine != null)
        {
            StopCoroutine(_currentRoutine);
        }

        _currentRoutine = StartCoroutine(TimePhaseSliderRoutine(startPoint, endPoint));
    }

    private IEnumerator TimePhaseSliderRoutine(float startPoint, float endPoint)
    {
        _timePhaseSlider.value = startPoint;
        float currentTimeOut = 0;

        while (_timePhaseSlider.value < endPoint && currentTimeOut < _timeout)
        {
            if (_timePhaseSlider.value + _timePhaseSliderPoint >= endPoint)
            {
                _timePhaseSlider.value = endPoint;
            }
            else
            {
                _timePhaseSlider.value += _timePhaseSliderPoint;
            }

            currentTimeOut += _timePhaseSliderDelay;

            yield return new WaitForSeconds(_timePhaseSliderDelay);
        }

        _timePhaseSlider.value = endPoint;
    }

    private void OnUpdateDayUI(Days day)
    {
        _dayLabel.text = day.ToString();
    }

    private void OnUpdateDayNumberUI(int dayNumber)
    {
        _dayNumberLabel.text = $"Day {dayNumber}"; 
    }
}
