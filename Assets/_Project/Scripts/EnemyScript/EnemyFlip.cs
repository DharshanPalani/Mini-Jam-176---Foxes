using UnityEngine;

public class EnemyFlip : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb2D; // Reference to Rigidbody2D
    private float _lastXPosition;

    private void Start()
    {
        if (_rb2D == null)
        {
            _rb2D = GetComponent<Rigidbody2D>();
        }
        _lastXPosition = transform.position.x; // Initialize the last known position
    }

    private void Update()
    {
        float currentXPosition = transform.position.x;

        // Check velocity if Rigidbody2D is present
        if (_rb2D != null)
        {
            Flip(_rb2D.velocity.x);
        }
        else
        {
            // Flip based on position changes if Rigidbody2D is not used
            float movementDirection = currentXPosition - _lastXPosition;
            Flip(movementDirection);
        }

        _lastXPosition = currentXPosition; // Update last position
    }

    private void Flip(float movementDirection)
    {
        if (movementDirection != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(movementDirection), 1, 1);
        }
    }
}
