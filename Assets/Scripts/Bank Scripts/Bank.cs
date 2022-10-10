using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bank : MonoBehaviour
{
    [SerializeField] private int _startingBalance = 150;
    private int _currentBalance;
    public int CurrentBalance { get => _currentBalance; }

    private void Awake()
    {
        _currentBalance = _startingBalance;
    }

    public void Deposit(int amount)
    {
        _currentBalance += Mathf.Abs(amount);
    }

    public void Withdraw(int amount)
    {
        _currentBalance -= Mathf.Abs(amount);

        if (_currentBalance < 0)
        {
            _currentBalance = 0;
        }
    }
}
