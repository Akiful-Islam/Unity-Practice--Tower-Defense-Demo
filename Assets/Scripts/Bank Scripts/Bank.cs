using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Bank : MonoBehaviour
{
    [SerializeField] private int _startingBalance = 150;

    [SerializeField] private TextMeshProUGUI _balanceText;
    private int _currentBalance;
    public int CurrentBalance { get => _currentBalance; }

    private void Awake()
    {
        _currentBalance = _startingBalance;
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        _balanceText.text = "Money: " + _currentBalance.ToString();
    }

    public void Deposit(int amount)
    {
        _currentBalance += Mathf.Abs(amount);
        UpdateDisplay();
    }

    public void Withdraw(int amount)
    {
        _currentBalance -= Mathf.Abs(amount);

        if (_currentBalance < 0)
        {
            _currentBalance = 0;
        }
        UpdateDisplay();
    }
}
