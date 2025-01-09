using UnityEngine;

public class Sword : MonoBehaviour
{
    private CapsuleCollider _playerBodyRef;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _playerBodyRef = transform.parent.GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        // Sword
        if (Input.GetButtonDown("SwordAttack"))
        {
            SwordAttack();
        }
    }

    private void SwordAttack()
    {
        
    }
}
