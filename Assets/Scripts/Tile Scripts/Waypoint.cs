using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] private Defender _defensePrefab;
    [Tooltip("Can a defender be placed here?")]
    [SerializeField] private bool _isPlaceable;
    public bool IsPlaceable { get => _isPlaceable; }
    private void OnMouseDown()
    {
        if (_isPlaceable)
        {
            bool isPlaced = _defensePrefab.Create(_defensePrefab, transform.position);
            _isPlaceable = !isPlaced;
        }
    }
}
