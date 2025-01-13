using UnityEngine;

public class Spaceship : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        if (Inventory.Instance.spaceshipPieces < 5)
        {
            PlayerHealth.Instance.SetHealth(PlayerHealth.Instance.GetMaxHealth());
            HUD.Instance.SetVisualHealth();
        }

        else
        {
            print("Congratulations !");
        }
        
    }
}
