using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bulletPrefab;
    private CapsuleCollider _playerBodyRef;

    private bool _canAttack = true;
    
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
        // Gun
        if (Input.GetButtonDown("GunAttack") && _canAttack)
        {
            GunAttack();
            _canAttack = false;
            Invoke(nameof(ResetAttack), 5f);
        }
    }

    private void ResetAttack()
    {
        _canAttack = true;
    }    
    
    private void GunAttack()
    {
        Instantiate(bulletPrefab, transform.position + transform.forward * 0.25f, transform.rotation);
    }
}
