using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defender : MonoBehaviour
{
    [SerializeField] private int _cost = 100;
    [SerializeField] private float _buildDelay = 1f;

    private void Start()
    {
        StartCoroutine(BuildDefense());
    }

    IEnumerator BuildDefense()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
            foreach (Transform grandChild in child)
            {
                grandChild.gameObject.SetActive(false);
            }
        }

        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
            yield return new WaitForSeconds(_buildDelay);

            foreach (Transform grandChild in child)
            {
                grandChild.gameObject.SetActive(true);
            }

        }
    }

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
