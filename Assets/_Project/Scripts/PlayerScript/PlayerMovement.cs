using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float _speed = 5f;
    private Rigidbody2D _rb2D;

    private Vector2 _movement;

    private void Start()
    {
        _rb2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _movement.x = Input.GetAxisRaw("Horizontal");
        _movement.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        _rb2D.MovePosition(_rb2D.position + _movement * _speed * Time.fixedDeltaTime);
    }
}
