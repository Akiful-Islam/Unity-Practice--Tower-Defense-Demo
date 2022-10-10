using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defender : MonoBehaviour
{
    [SerializeField] private int _cost = 100;


    public bool Create(Defender defender, Vector3 position)
    {
        Bank bank = FindObjectOfType<Bank>();
        if (bank == null)
        {
            return false;
        }

        if (bank.CurrentBalance >= _cost)
        {
            Instantiate(defender, position, Quaternion.identity);
            bank.Withdraw(_cost);
            return true;
        }

        return false;
    }
}
