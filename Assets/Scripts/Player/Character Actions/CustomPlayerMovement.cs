using UnityEngine;

public class CustomPlayerMovement : MonoBehaviour
{
    public float jumpForce = 10.0f;
    public float movementSpeed = 5.0f;

    private Rigidbody _body;
    private Vector3 _directionMove = Vector3.zero;
    private bool _bIsGrounded = true;
    private LayerMask _groundLayer;

    // -------------------- //
    //       FUNCTIONS      //
    // -------------------- //


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _body = GetComponent<Rigidbody>();
        _groundLayer = LayerMask.NameToLayer("Ground");
    }

    // Update is called once per frame
    void Update()
    {
        // Detect Ground
        RaycastHit hitCast;
        _bIsGrounded = Physics.Raycast(transform.position, Vector3.down, out hitCast, 1.5f, _groundLayer);
        
        // Move
        _directionMove = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        _directionMove *= movementSpeed * Time.deltaTime;
        _body.MovePosition(transform.position + _directionMove);
        
        // Jump
        if (_bIsGrounded && Input.GetButtonDown("Jump"))
        {
            _body.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
    
}
