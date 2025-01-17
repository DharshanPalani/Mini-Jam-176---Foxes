using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TrapTrigger : MonoBehaviour
{
    public UnityEvent OnTrapTriggered;

    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _animator.SetBool("isTrapTriggered", true);
            OnTrapTriggered.Invoke();
            StartCoroutine(WaitForAnimationToEnd());
        }
    }

    private IEnumerator WaitForAnimationToEnd()
    {
        yield return new WaitForSeconds(.5f);

        Destroy(gameObject);
    }
}
