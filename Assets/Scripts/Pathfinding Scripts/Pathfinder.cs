using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    [SerializeField] private Vector2Int _startCoordinates;
    public Vector2Int StartCoordinates { get => _startCoordinates; }
    [SerializeField] private Vector2Int _endCoordinates;
    public Vector2Int EndCoordinates { get => _endCoordinates; }

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
            _startNode = _grid[_startCoordinates];
            _endNode = _grid[_endCoordinates];
            _startNode.isWalkable = true;
            _endNode.isWalkable = true;
        }
    }

    private void Start()
    {

        GetNewPath();
    }

    public List<Node> GetNewPath()
    {
        return GetNewPath(_startCoordinates);
    }

    public List<Node> GetNewPath(Vector2Int coordinates)
    {
        _gridManager.ResetNodes();
        BreadthFirstSearch(coordinates);
        return BuildPath();
    }

    private void BreadthFirstSearch(Vector2Int coordinates)
    {
        _frontier.Clear();
        _reached.Clear();

        bool isRunning = true;

        _frontier.Enqueue(_grid[coordinates]);
        _reached.Add(_grid[coordinates].Coordinates, _grid[coordinates]);

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

    public bool WillBlockPath(Vector2Int coordinates)
    {
        if (_grid.ContainsKey(coordinates))
        {
            bool previousState = _grid[coordinates].isWalkable;
            _grid[coordinates].isWalkable = false;

            List<Node> newPath = GetNewPath();
            _grid[coordinates].isWalkable = previousState;

            if (newPath.Count <= 1)
            {
                GetNewPath();
                return true;
            }
        }

        return false;
    }

    public void NotifyReceivers()
    {
        BroadcastMessage("RecalculatePath", false, SendMessageOptions.DontRequireReceiver);
    }
}