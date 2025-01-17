using System.Collections;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] private float _patrolSpeed = 2f;
    [SerializeField] private float _patrolRadius = 5f;
    [SerializeField] private float _delayBetweenMoves = 1f;

    private Vector2 _patrolCenter;
    private Vector2 _nextPatrolPoint;
    private bool _isMoving;
    private bool _isPatrolling = true; // Flag to control patrol state

    private void Start()
    {
        _patrolCenter = transform.position;
        SetNextPatrolPoint();
    }

    private void Update()
    {
        if (_isPatrolling && !_isMoving)
        {
            StartCoroutine(MoveToNextPoint());
        }
    }

    private void SetNextPatrolPoint()
    {
        _nextPatrolPoint = _patrolCenter + (Vector2)Random.insideUnitCircle * _patrolRadius;
    }

    private IEnumerator MoveToNextPoint()
    {
        _isMoving = true;

        while (Vector2.Distance(transform.position, _nextPatrolPoint) > 0.1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, _nextPatrolPoint, _patrolSpeed * Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(_delayBetweenMoves);

        SetNextPatrolPoint();
        _isMoving = false;
    }

    // Public methods to control patrol state
    public void StartPatrolling()
    {
        _isPatrolling = true;
    }

    public void StopPatrolling()
    {
        _isPatrolling = false;
        StopAllCoroutines(); // Stop any ongoing movement
        _isMoving = false;
    }
}
