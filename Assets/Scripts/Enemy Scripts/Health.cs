using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHP = 5f;
    private float _currentHP;

    private void Awake()
    {
        _currentHP = maxHP;
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
        }
    }
}
