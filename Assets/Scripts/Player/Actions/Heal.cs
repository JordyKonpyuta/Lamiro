using UnityEngine;

public class Heal : MonoBehaviour
{
    private CapsuleCollider _playerBodyRef;
    
    // -------------------- //
    //       FUNCTIONS      //
    // -------------------- //
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _playerBodyRef = transform.GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        // Heal
        if (Input.GetButtonDown("Heal"))
        {
            Healing();
        }
    }

    private void Healing()
    {
        if (transform.GetComponent<Inventory>().GetScrews() >= 10 && transform.GetComponent<PlayerHealth>().GetHealth() < transform.GetComponent<PlayerHealth>().GetMaxHealth())
        {
            transform.GetComponent<PlayerHealth>().SetHealth(transform.GetComponent<PlayerHealth>().GetHealth() + 1);
            transform.GetComponent<Inventory>().AddScrews(-10);
            HUD.Instance.SetVisualHealth();
            HUD.Instance.UpdateScrewsText(transform.GetComponent<Inventory>().GetScrews());
        }
    }
}
