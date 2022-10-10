using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] private GameObject _defensePrefab;
    [SerializeField] private bool _isPlaceable;
    public bool IsPlaceable { get => _isPlaceable; }
    private void OnMouseDown()
    {
        if (_isPlaceable)
        {
            Instantiate(_defensePrefab, transform.position, Quaternion.identity);
            _isPlaceable = false;
        }
    }
}
