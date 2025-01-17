using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OrbCountManager : MonoBehaviour
{

    public int orbCount;

    public TextMeshProUGUI orbCountText;

    public void AddOrb()
    {
        orbCount++;

        orbCountText.text = orbCount.ToString();
    }
}
