using UnityEngine;

public class HealthReaction : MonoBehaviour
{
    public FloatValue playerHealth;
    public Signal healthSignal;

    public void Use(int amountToIncrease) {
        playerHealth.runtimeValue += amountToIncrease;
        healthSignal.Raise();
    }
}
