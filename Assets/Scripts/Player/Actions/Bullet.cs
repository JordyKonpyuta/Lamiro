using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float BulletSpeed = 5.0f;
    
    private Rigidbody _body;
    private Vector3 _moveDirection;
    
    // -------------------- //
    //       FUNCTIONS      //
    // -------------------- //
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _body = GetComponent<Rigidbody>();
        Invoke(nameof(BulletDeath), 2.0f);
    }

    // Update is called once per frame
    void Update()
    {
        // Move Bullet
        _moveDirection = transform.forward * BulletSpeed;
        _moveDirection *= Time.deltaTime;
        
        _body.MovePosition(transform.position + _moveDirection);
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player"))
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                other.gameObject.GetComponent<Ennemy>().TakeDamage(2);
            }
            else if (other.gameObject.CompareTag("Interactable"))
            {
                other.gameObject.GetComponent<Interactable>().OnInteract();
            }
        }
    }

    void BulletDeath()
    {
        Destroy(gameObject);
    }
}
