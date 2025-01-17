using UnityEngine;

public class EnemyFlip : MonoBehaviour
{
    private float _lastXPosition;

    private void Start()
    {
        _lastXPosition = transform.position.x;
    }

    private void Update()
    {
        float currentXPosition = transform.position.x;
        float movementDirection = currentXPosition - _lastXPosition;
        Flip(movementDirection);
        _lastXPosition = currentXPosition;
    }

    private void Flip(float movementDirection)
    {
        if (movementDirection != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(movementDirection), 1, 1);
        }
    }
}
