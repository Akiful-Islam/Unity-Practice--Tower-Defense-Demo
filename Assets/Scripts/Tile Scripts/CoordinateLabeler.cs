using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[ExecuteAlways]
[RequireComponent(typeof(TextMeshPro))]
public class CoordinateLabeler : MonoBehaviour
{
    [SerializeField] private Color _defaultColor = Color.white;
    [SerializeField] private Color _blockedColor = Color.gray;
    [SerializeField] private Color _exploredColor = Color.yellow;
    [SerializeField] private Color _pathColor = new Color(1f, 0.5f, 0f);
    private TextMeshPro _label;
    private Vector2Int _coordinates = new Vector2Int();
    private GridManager _gridManager;


    private void Awake()
    {
        _gridManager = FindObjectOfType<GridManager>();
        _label = GetComponent<TextMeshPro>();
        DisplayCoordinates();
    }

    private void Update()
    {
        if (!Application.isPlaying)
        {
            DisplayCoordinates();
            UpdateTileName();
        }

        SetLabelColor();
        ToggleLabels();
    }

    private void DisplayCoordinates()
    {
        if (_gridManager != null)
        {
            _coordinates.x = Mathf.RoundToInt(transform.parent.position.x / _gridManager.UnityGridSize);
            _coordinates.y = Mathf.RoundToInt(transform.parent.position.z / _gridManager.UnityGridSize);
            _label.text = $"({_coordinates.x} , {_coordinates.y})";
        }

    }

    private void UpdateTileName()
    {
        transform.parent.name = _coordinates.ToString();
    }

    private void SetLabelColor()
    {
        if (_gridManager == null) { return; }

        Node node = _gridManager.GetNode(_coordinates);
        if (node == null) { return; }

        if (!node.isWalkable)
        {
            _label.color = _blockedColor;
        }
        else if (node.isPath)
        {
            _label.color = _pathColor;
        }
        else if (node.isExplored)
        {
            _label.color = _exploredColor;
        }
        else
        {
            _label.color = _defaultColor;
        }

    }

    private void ToggleLabels()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            _label.enabled = !_label.IsActive();
        }
    }
}
