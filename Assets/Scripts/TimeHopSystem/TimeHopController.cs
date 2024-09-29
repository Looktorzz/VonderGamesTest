using System;
using System.Security.Cryptography;
using UnityEngine;

public class TimeHopController : MonoBehaviour
{
    public Action<Times> WhenTimeChanged;
    public Action<Days> WhenDayChanged;
    public Action<int> WhenDayNumberChanged;

    private Times _currentTime;
    private Days _currentDay;
    private int _currentDayNumber;
    private bool _isInit;

    public void Init()
    {
        if (_isInit)
        {
            return;
        }

        _currentTime = Times.Morning;
        _currentDay = Days.Monday;
        _currentDayNumber = 0;

        _isInit = true;
    }

    public void OnTimeChanged()
    {
        _currentTime++;

        if (_currentTime > Times.Evening)
        {
            _currentTime = Times.Morning;
            OnDayChanged();
        }

        WhenTimeChanged?.Invoke(_currentTime);
    }

    private void OnDayChanged()
    {
        _currentDay++;
        _currentDayNumber++;

        if (_currentDay > Days.Sunday)
        {
            _currentDay = Days.Monday;
        }

        WhenDayChanged?.Invoke(_currentDay);
        WhenDayNumberChanged?.Invoke(_currentDayNumber);
    }
}
