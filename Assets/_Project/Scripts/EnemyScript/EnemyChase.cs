using System.Collections;
using UnityEngine.Events;
using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    [SerializeField] private float _fovRadius = 7f;
    [SerializeField] private float _chaseSpeed = 4f;
    [SerializeField] private float _lostPlayerTimeout = 2f;
    [SerializeField] private float _frozenDelay = 1f;

    [SerializeField] private UnityEvent _gunClickTriggerAudio;
    [SerializeField] private UnityEvent _shootTrigger;
    [SerializeField] private UnityEvent _startPatrol;
    [SerializeField] private UnityEvent _stopPatrol;

    private bool _isChasing;
    private float _timeSinceLostPlayer;
    private Transform _player;
    private Animator _animator;
    private Rigidbody2D _rb;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player")?.transform;
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        if (_animator == null)
        {
            Debug.LogError("Animator component not found on the enemy chase");
        }

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

        _shootTrigger.Invoke();

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
        _animator.SetBool("isChasing", true);
        if (!_isChasing) _gunClickTriggerAudio.Invoke();
        _isChasing = true;

        _stopPatrol.Invoke();
    }

    private void StopChasing()
    {
        _animator.SetBool("isChasing", false);
        _animator.SetBool("isPatrolling", false);
        _isChasing = false;
        _timeSinceLostPlayer = 0f;

        StartCoroutine(Freeze());
    }


    public IEnumerator Freeze()
    {
        yield return new WaitForSeconds(_frozenDelay);
        _startPatrol.Invoke();
    }
}
