using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    [SerializeField] private float _fovRadius = 7f;
    [SerializeField] private float _chaseSpeed = 4f;
    [SerializeField] private float _lostPlayerTimeout = 2f;

    private bool _isChasing;
    private float _timeSinceLostPlayer;
    private Transform _player;
    private EnemyPatrol _enemyPatrol;
    private EnemyFreeze _enemyFreeze;
    private Rigidbody2D _rb;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player")?.transform;
        _rb = GetComponent<Rigidbody2D>();
        _enemyPatrol = GetComponent<EnemyPatrol>();
        _enemyFreeze = GetComponent<EnemyFreeze>();

        if (_player == null)
        {
            Debug.LogError("Player not found! Ensure the player GameObject is tagged 'Player'.");
        }
    }

    private void FixedUpdate()
    {
        if (_isChasing && _player != null)
        {
            ChasePlayer();
        }
    }

    private void ChasePlayer()
    {
        Vector2 direction = (_player.position - transform.position).normalized;
        _rb.MovePosition(_rb.position + direction * _chaseSpeed * Time.fixedDeltaTime);

        if (Vector2.Distance(transform.position, _player.position) > _fovRadius)
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

    public void StartChasing()
    {
        _isChasing = true;

        // Stop patrol when chasing starts
        if (_enemyPatrol != null)
        {
            _enemyPatrol.StopPatrolling();
        }
    }

    private void StopChasing()
    {
        _isChasing = false;
        _timeSinceLostPlayer = 0f;

        // Resume patrol when chasing ends
        if (_enemyPatrol != null)
        {
            StartCoroutine(_enemyFreeze.Freeze());
            _enemyPatrol.StartPatrolling();
        }
    }
}
