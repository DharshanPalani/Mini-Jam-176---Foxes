using System.Collections;
using UnityEngine;

public class EnemyFreeze : MonoBehaviour
{
    [SerializeField] private float _frozenDelay = 1f;

    private bool _isFrozen;

    public bool IsFrozen => _isFrozen;

    public IEnumerator Freeze()
    {
        _isFrozen = true;
        yield return new WaitForSeconds(_frozenDelay);
        _isFrozen = false;
    }
}
