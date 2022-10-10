using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _moneyReward = 10;
    [SerializeField] private int _moneyPenalty = 10;

    Bank _bank;

    private void Awake()
    {
        _bank = FindObjectOfType<Bank>();
    }

    public void RewardMoney()
    {
        if (_bank == null) return;
        _bank.Deposit(_moneyReward);
    }

    public void TakeMoney()
    {
        if (_bank == null) return;
        _bank.Withdraw(_moneyPenalty);
    }
}
