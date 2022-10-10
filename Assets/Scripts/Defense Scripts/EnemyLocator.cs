using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLocator : MonoBehaviour
{
    [SerializeField] private Transform _weapon;
    private Transform _enemy;


    private void Start()
    {
        _enemy = FindObjectOfType<EnemyMover>().transform;

    }
    private void Update()
    {
        AimWeapon();
    }

    private void AimWeapon()
    {
        _weapon.LookAt(_enemy);
    }
}
