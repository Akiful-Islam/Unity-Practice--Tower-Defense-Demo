using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] private List<Tile> _path = new List<Tile>();
    [SerializeField][Range(0f, 10f)] private float _moveSpeed = 0.25f;

    private Enemy _enemy;

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
    }

    private void OnEnable()
    {
        FindPath();
        ReturnToStart();
        StartCoroutine(FollowPath());
    }

    private void FindPath()
    {
        _path.Clear();

        GameObject parent = GameObject.FindGameObjectWithTag("Path");

        foreach (Transform child in parent.transform)
        {
            Tile waypoint = child.GetComponent<Tile>();
            if (waypoint != null)
            {
                _path.Add(waypoint);
            }
        }
    }

    private void ReturnToStart()
    {
        transform.position = _path[0].transform.position;
    }

    private IEnumerator FollowPath()
    {
        foreach (Tile waypoint in _path)
        {
            Vector3 startPosition = transform.position;
            Vector3 targetPosition = waypoint.transform.position;
            float travelPercent = 0f;

            transform.LookAt(targetPosition);

            while (travelPercent < 1f)
            {
                transform.position = Vector3.Lerp(startPosition, targetPosition, travelPercent);
                travelPercent += _moveSpeed * Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
        }

        gameObject.SetActive(false);
        _enemy.TakeMoney();
    }

}
