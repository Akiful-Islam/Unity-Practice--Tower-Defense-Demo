using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class Health : MonoBehaviour
{
    public float MaxHP = 5f;
    private float _currentHP;

    private Enemy _enemy;

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
        _currentHP = MaxHP;
    }

    private void OnParticleCollision(GameObject other)
    {
        ProcessHit();
    }

    private void ProcessHit()
    {
        _currentHP--;
        if (_currentHP <= 0)
        {
            gameObject.SetActive(false);
            _enemy.RewardMoney();
        }
    }
}
