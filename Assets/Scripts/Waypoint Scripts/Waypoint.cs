using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] private GameObject _defensePrefab;
    public bool isPlaceable;

    private void OnMouseDown()
    {
        if (isPlaceable)
        {
            Instantiate(_defensePrefab, transform.position, Quaternion.identity);
            isPlaceable = false;
        }
    }
}
