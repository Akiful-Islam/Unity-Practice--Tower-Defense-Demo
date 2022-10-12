using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Defender _defensePrefab;
    [Tooltip("Can a defender be placed here?")]
    [SerializeField] private bool _isPlaceable;
    public bool IsPlaceable { get => _isPlaceable; }

    private GridManager _gridManager;
    private Pathfinder _pathFinder;
    private Vector2Int _coordinates = new Vector2Int();
    private void Awake()
    {
        _gridManager = FindObjectOfType<GridManager>();
        _pathFinder = FindObjectOfType<Pathfinder>();
    }

    private void Start()
    {
        if (_gridManager != null)
        {
            _coordinates = _gridManager.GetCoordinatesFromPosition(transform.position);
            if (!_isPlaceable)
            {
                _gridManager.BlockNode(_coordinates);
            }
        }
    }
    private void OnMouseDown()
    {
        if (_gridManager.GetNode(_coordinates).isWalkable && !_pathFinder.WillBlockPath(_coordinates))
        {
            bool isPlaced = _defensePrefab.Create(_defensePrefab, transform.position);
            _isPlaceable = !isPlaced;
            _gridManager.BlockNode(_coordinates);
        }
    }
}
