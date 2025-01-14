using UnityEngine;

public class Jump : MonoBehaviour
{
    public float jumpForce = 10.0f;
    
    private bool _bIsGrounded = true;
    private LayerMask _groundLayer;

    private Rigidbody _playerBodyRef;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _groundLayer = LayerMask.NameToLayer("Ground");
        _playerBodyRef = transform.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Detect Ground
        RaycastHit hitCast;
        _bIsGrounded = Physics.Raycast(transform.position, Vector3.down, out hitCast, 1.5f, _groundLayer);
        
        // Jump
        if (_bIsGrounded && Input.GetButtonDown("Jump"))
        {
            _playerBodyRef.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}
