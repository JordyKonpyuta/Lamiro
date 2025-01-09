using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bulletPrefab;
    private CapsuleCollider _playerBodyRef;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _playerBodyRef = transform.parent.GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        // Gun
        if (Input.GetButtonDown("GunAttack"))
        {
            GunAttack();
        }
    }

    private void GunAttack()
    {
        Instantiate(bulletPrefab, transform.position + transform.forward * 0.25f, transform.rotation);
    }
}
