using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLocator : MonoBehaviour
{
    [SerializeField] private Transform _weapon;
    [SerializeField] private ParticleSystem _projectileParticle;
    [SerializeField] private float _weaponRange = 15f;
    private Transform _enemy;

    private void Update()
    {
        FindClosestEnemy();
        AimWeapon();
    }

    private void FindClosestEnemy()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();

        Transform closestEnemy = null;
        float maxDistance = Mathf.Infinity;

        foreach (var enemy in enemies)
        {
            float targetDistance = Vector3.Distance(transform.position, enemy.transform.position);
            if (targetDistance < maxDistance)
            {
                closestEnemy = enemy.transform;
                maxDistance = targetDistance;
            }
        }

        _enemy = closestEnemy;
    }


    private void AimWeapon()
    {
        float targetDistance = Vector3.Distance(transform.position, _enemy.position);
        _weapon.LookAt(_enemy);

        if (targetDistance <= _weaponRange)
        {
            Attack(true);
        }
        else
        {
            Attack(false);
        }
    }

    private void Attack(bool isActive)
    {
        var emissionModule = _projectileParticle.emission;
        emissionModule.enabled = isActive;
    }
}
