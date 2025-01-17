using UnityEngine;

public class EnemyFOV : MonoBehaviour
{
    [SerializeField] private float _fovRadius = 7f;

    private EnemyChase _enemyChase;

    private void Start()
    {
        _enemyChase = GetComponent<EnemyChase>();

        CircleCollider2D fovArea = gameObject.AddComponent<CircleCollider2D>();
        fovArea.radius = _fovRadius;
        fovArea.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered FOV.");
            _enemyChase.StartChasing();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player exited FOV.");
        }
    }
}
