using System;
using UnityEngine;

public class Spaceship : MonoBehaviour
{
    
    private void Start()
    {
        SpaceshipPopUp.Instance.GetComponent<Canvas>().enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Inventory.Instance.spaceshipPieces < 5)
            {
                SpaceshipPopUp.Instance.PopUp();
                PlayerHealth.Instance.SetHealth(PlayerHealth.Instance.GetMaxHealth());
                HUD.Instance.SetVisualHealth();
                Debug.LogError("Coucou");
            }

            else
            {
                print("Congratulations !");
                EndGame.Instance.OnPopUp();
            }   
        }
    }

    private void DisablePopUp()
    {
        SpaceshipPopUp.Instance.GetComponent<Animator>().SetBool("IsOpen", false);
        SpaceshipPopUp.Instance.GetComponent<Canvas>().enabled = false;
    }
    
}
