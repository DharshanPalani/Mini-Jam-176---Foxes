using System.Collections;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    [SerializeField][Range(0f, 1f)] private float _shootChance = 0.5f;
    [SerializeField] private float _shootCooldown = 2f;
    [SerializeField] private Transform _player;
    [SerializeField] private float _missOffsetRange = 2f;

    private bool _canShoot = true;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void TryShoot()
    {
        if (!_canShoot) return;

        float randomChance = Random.Range(0f, 1f);
        if (randomChance <= _shootChance)
        {
            Debug.Log("Shot hit the player!");
        }
        else
        {
            Vector3 missPosition = GetRandomMissPosition();
            Debug.Log($"Shot missed! Near miss at position: {missPosition}");
        }

        _canShoot = false;
        StartCoroutine(ShootCooldown());
    }

    private Vector3 GetRandomMissPosition()
    {
        Vector3 randomOffset = new Vector3(
            Random.Range(-_missOffsetRange, _missOffsetRange),
            0f,
            Random.Range(-_missOffsetRange, _missOffsetRange)
        );
        return _player.position + randomOffset;
    }

    private IEnumerator ShootCooldown()
    {
        yield return new WaitForSeconds(_shootCooldown);
        _canShoot = true;
    }
}
