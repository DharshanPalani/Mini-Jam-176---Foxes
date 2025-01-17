using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Patrolling")]
    [SerializeField] private float _patrolSpeed = 2f;
    [SerializeField] private float _patrolRadius = 5f;
    [SerializeField] private float _delayBetweenMoves = 1f;

    [Header("Chasing")]
    [SerializeField] private float _fovRadius = 7f;
    [SerializeField] private float _chaseSpeed = 4f;
    [SerializeField] private float _lostPlayerTimeout = 2f;

    [Header("Freezing")]
    [SerializeField] private float _frozenDelay = 1f;

    [Header("Shooting")]
    [Range(0f, 1f)]
    [SerializeField] private float _shootChance = 0.5f;
    [SerializeField] private float _shootCooldown = 2f;

    private Vector2 _patrolCenter;
    private Vector2 _nextPatrolPoint;
    private bool _isMoving = false;
    private bool _isChasing = false;
    private bool _isFrozen = false;
    private bool _canShoot = true;
    private CircleCollider2D _fovArea;
    private float _timeSinceLostPlayer = 0f;

    private void Start()
    {
        _patrolCenter = transform.position;
        SetNextPatrolPoint();

        // Create FOV area using CircleCollider2D
        _fovArea = gameObject.AddComponent<CircleCollider2D>();
        _fovArea.radius = _fovRadius;
        _fovArea.isTrigger = true;
    }

    private void Update()
    {
        if (_isFrozen) return;

        if (_isChasing)
        {
            ChasePlayer();
            ShootAtPlayer();
        }
        else
        {
            Patrol();
        }
    }

    private void Patrol()
    {
        if (!_isMoving)
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

    private void ChasePlayer()
    {
        Transform playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, _chaseSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, playerTransform.position) > _fovRadius)
        {
            _timeSinceLostPlayer += Time.deltaTime;
            if (_timeSinceLostPlayer >= _lostPlayerTimeout)
            {
                StopChasing();
            }
        }
        else
        {
            _timeSinceLostPlayer = 0f;
        }
    }

    private void StopChasing()
    {
        _isChasing = false;
        _timeSinceLostPlayer = 0f;
        StartCoroutine(FreezeAndReturnToPatrol());
    }

    private IEnumerator FreezeAndReturnToPatrol()
    {
        _isFrozen = true;
        yield return new WaitForSeconds(_frozenDelay);
        _isFrozen = false;
        SetNextPatrolPoint();
    }

    private void ShootAtPlayer()
    {
        if (!_canShoot) return;

        float randomChance = Random.Range(0f, 1f);
        if (randomChance <= _shootChance)
        {
            Debug.Log("Shot hit the player!");
        }
        else
        {
            Debug.Log("Shot missed!");
        }

        _canShoot = false;
        StartCoroutine(ShootCooldown());
    }

    private IEnumerator ShootCooldown()
    {
        yield return new WaitForSeconds(_shootCooldown);
        _canShoot = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered FOV at position: " + other.transform.position);
            _isChasing = true;
            _timeSinceLostPlayer = 0f;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player exited FOV at position: " + other.transform.position);
            _timeSinceLostPlayer = 0f;
        }
    }
}
