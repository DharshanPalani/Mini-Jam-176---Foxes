using UnityEngine;
using UnityEngine.Events;

public class EnemyFOV : MonoBehaviour
{
    [SerializeField] private float _fovRadius = 7f;
    [SerializeField] private UnityEvent _startChase;
    //private EnemyChase _enemyChase;

    private void Start()
    {
        CircleCollider2D fovArea = gameObject.AddComponent<CircleCollider2D>();
        fovArea.radius = _fovRadius;
        fovArea.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered FOV.");
            _startChase.Invoke();
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
