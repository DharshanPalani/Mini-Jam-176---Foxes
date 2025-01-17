using System.Collections;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    [SerializeField][Range(0f, 1f)] private float _shootChance = 0.5f;
    [SerializeField] private float _shootCooldown = 2f;

    private bool _canShoot = true;

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
}
