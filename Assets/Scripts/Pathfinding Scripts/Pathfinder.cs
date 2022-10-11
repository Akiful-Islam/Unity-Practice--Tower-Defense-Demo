using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    [SerializeField] private Vector2Int _startCoordinates;
    [SerializeField] private Vector2Int _endCoordinates;

    private Node _startNode;
    private Node _endNode;
    private Node _currentSearchNode;
    private Queue<Node> _frontier = new Queue<Node>();
    private Vector2Int[] _directions = { Vector2Int.up, Vector2Int.right, Vector2Int.down, Vector2Int.left };
    private GridManager _gridManager;
    private Dictionary<Vector2Int, Node> _grid;
    private Dictionary<Vector2Int, Node> _reached = new Dictionary<Vector2Int, Node>();

    private void Awake()
    {
        _gridManager = FindObjectOfType<GridManager>();
        if (_gridManager != null)
        {
            _grid = _gridManager.Grid;
        }
    }

    private void Start()
    {
        _startNode = _grid[_startCoordinates];
        _endNode = _grid[_endCoordinates];
        BreadthFirstSearch();
        BuildPath();
    }



    private void ExploreNeighbours()
    {
        List<Node> neighbours = new List<Node>();
        foreach (Vector2Int direction in _directions)
        {
            Vector2Int neighbourCoordinates = _currentSearchNode.Coordinates + direction;
            if (_grid.ContainsKey(neighbourCoordinates))
            {
                neighbours.Add(_grid[neighbourCoordinates]);
            }
        }

        foreach (Node neighbour in neighbours)
        {
            if (!_reached.ContainsKey(neighbour.Coordinates) && neighbour.isWalkable)
            {
                neighbour.ConnectedTo = _currentSearchNode;
                _reached.Add(neighbour.Coordinates, neighbour);
                _frontier.Enqueue(neighbour);
            }
        }
    }

    private void BreadthFirstSearch()
    {
        bool isRunning = true;
        _frontier.Enqueue(_startNode);
        _reached.Add(_startNode.Coordinates, _startNode);

        while (_frontier.Count > 0 && isRunning)
        {
            _currentSearchNode = _frontier.Dequeue();
            _currentSearchNode.isExplored = true;

            ExploreNeighbours();

            if (_currentSearchNode.Coordinates == _endCoordinates)
            {
                isRunning = false;
            }
        }
    }

    private List<Node> BuildPath()
    {
        List<Node> path = new List<Node>();
        Node currentNode = _endNode;

        path.Add(currentNode);
        currentNode.isPath = true;

        while (currentNode.ConnectedTo != null)
        {
            currentNode = currentNode.ConnectedTo;
            path.Add(currentNode);
            currentNode.isPath = true;
        }

        path.Reverse();

        return path;
    }
}
