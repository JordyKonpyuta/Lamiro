using System;
using UnityEngine;

public class CustomPlayerActions : MonoBehaviour
{
    public float jumpForce = 10.0f;
    public float movementSpeed = 5.0f;

    private Rigidbody _body;
    private Vector3 _directionMove = Vector3.zero;
    private bool _bIsGrounded = true;
    private LayerMask _groundLayer;
    
    // Rotation
    private float newRot = 0;
    private int xChecker = 0;
    private int yChecker = 0;

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
        
        // Rotation
        
        int xToCheck = Input.GetAxis("Horizontal") > 0 ? 1 : -1;
        int yToCheck = Input.GetAxis("Vertical") > 0 ? 1 : -1;
        
        if (Input.GetButton("Horizontal") && xToCheck != xChecker )
        {
            RotationHorizontal(Input.GetAxis("Horizontal"));
            xChecker = xToCheck;
        }
        else if (Input.GetButton("Vertical") && yToCheck != yChecker)
        {
            RotationVertical(Input.GetAxis("Vertical"));
            yChecker = yToCheck;
        }
    }

    void RotationHorizontal(float rotationXValue)
    {
        newRot = rotationXValue > 0 ? 90 : -90;
        if (Math.Abs(newRot - _body.rotation.y) > 0.1)
            _body.rotation = Quaternion.Euler(0, newRot, 0);
    }

    void RotationVertical(float rotationYValue)
    {
        newRot = rotationYValue > 0 ? 0 : 180;
        if (Math.Abs(newRot - _body.rotation.y) > 0.1)
            _body.rotation = Quaternion.Euler(0, newRot, 0);
    }
}
