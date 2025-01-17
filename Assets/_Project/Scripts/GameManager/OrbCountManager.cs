using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class OrbCountManager : MonoBehaviour
{

    public int orbCount;

    public UnityEvent OnOrbCollected;

    public TextMeshProUGUI orbCountText;

    public void AddOrb()
    {
        orbCount++;
        OnOrbCollected.Invoke();

        orbCountText.text = orbCount.ToString();
    }
}
