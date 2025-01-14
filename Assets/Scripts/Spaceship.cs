using UnityEngine;

public class Spaceship : MonoBehaviour, IInteractable
{
    public GameObject menu;
    
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
            menu.SetActive(true);
        }
       
    }
}
