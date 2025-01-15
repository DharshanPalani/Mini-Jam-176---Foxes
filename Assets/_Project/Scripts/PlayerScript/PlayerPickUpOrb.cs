using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class PlayerPickUpOrb : MonoBehaviour
{
    public UnityEvent pickUpCount;

    private Collider2D _orbCollider; 

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && _orbCollider != null) 
        {
            Destroy(_orbCollider.gameObject); 
            pickUpCount?.Invoke(); 
            _orbCollider = null; 
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Orb"))
        {
            _orbCollider = collision; 
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Orb"))
        {
            _orbCollider = null; 
        }
    }
}
