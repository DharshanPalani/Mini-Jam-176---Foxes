using UnityEngine;

public class PlayerTrapManager : MonoBehaviour
{
    public bool isFoxTrapImmunity = false;
    public bool isHunterTrapImmunity = false;

    public void OnTrapTrigger(int trapID)
    {
        if (trapID == 1) FoxTrapTrigger();
        else if (trapID == 2) HunterTrapTrigger();
    }

    private void FoxTrapTrigger()
    {
        if (isFoxTrapImmunity) return;
        Debug.Log("Fox trap triggered!");
    }

    private void HunterTrapTrigger()
    {
        if (isHunterTrapImmunity) return;
        Debug.Log("Hunter trap triggered!");
    }

    public void GrantImmunity(byte immunityType)
    {
        if(immunityType == 1) isFoxTrapImmunity = true;
        else if (immunityType == 2) isHunterTrapImmunity = true;
        else Debug.Log("No immunity granted!");
    }
}
