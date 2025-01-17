using UnityEngine;
using UnityEngine.Events;

public class CoinFlipManager : MonoBehaviour
{
    public PlayerTrapManager playerTrapManager;

    private void Start()
    {
        FlipCoin();
    }

    void FlipCoin()
    {
        byte coinFlip = (byte)Random.Range(1, 4);

        playerTrapManager.GrantImmunity(coinFlip);
    }
}
