using System;
using UnityEngine;

public class CustomPlayerActions : MonoBehaviour
{
    public float movementSpeed = 5.0f;

    private Rigidbody _body;
    private Vector3 _directionMove = Vector3.zero;
    
    // Rotation
    private float newRot = 0;
    private float xToCheck = 0;
    private float xChecker = 0;
    private float yToCheck = 0;
    private float yChecker = 0;

    private bool DONTMOVE = true;

    // -------------------- //
    //       FUNCTIONS      //
    // -------------------- //
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _body = GetComponent<Rigidbody>();
        Invoke(nameof(StartMoving), 1f);
    }

    private void StartMoving()
    {
        DONTMOVE = false;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (DONTMOVE) return;
        // Movement Priorities Calculations
        if (gameObject.GetComponent<Jump>().bIsGrounded)
        {
            if ((Input.GetAxis("Horizontal") > 0.01 || Input.GetAxis("Horizontal") < -0.01) &&
                Math.Abs(Input.GetAxis("Horizontal")) >= Math.Abs(xToCheck) && Input.GetAxis("Horizontal") != 0)
            {
                xToCheck = Input.GetAxis("Horizontal");
                yToCheck = 0;
                _directionMove = new Vector3(Input.GetAxis("Horizontal") > 0 ? 1 : -1, 0, 0);
            }
            else if ((Input.GetAxis("Vertical") > 0.01 || Input.GetAxis("Vertical") < -0.01) &&
                     Math.Abs(Input.GetAxis("Vertical")) >= Math.Abs(yToCheck) && Input.GetAxis("Vertical") != 0)
            {
                xToCheck = 0;
                yToCheck = Input.GetAxis("Vertical");
                _directionMove = new Vector3(0, 0, Input.GetAxis("Vertical") > 0 ? 1 : -1);
            }
            else
            {
                xToCheck = 0;
                yToCheck = 0;
                _directionMove = new Vector3(0, 0, 0);
            }

            // Move
            _directionMove *= movementSpeed * Time.deltaTime;
        }

        _body.MovePosition(transform.position + _directionMove);
        
        // Rotation
        int xToCheck2 = Input.GetAxis("Horizontal") > 0 ? 1 : -1;
        int yToCheck2 = Input.GetAxis("Vertical") > 0 ? 1 : -1;
        if (Input.GetButton("Horizontal") && Math.Abs(xToCheck - xChecker) > 0.01 )
        {
            RotationHorizontal(Input.GetAxis("Horizontal"));
            xChecker = xToCheck2;
            yChecker = 0;
        }
        else if (Input.GetButton("Vertical") && Math.Abs(yToCheck - yChecker) > 0.01)
        {
            RotationVertical(Input.GetAxis("Vertical"));
            xChecker = 0;
            yChecker = yToCheck2;
        }

        if (Input.GetButton("Pause"))
        {
            Pause.Instance.Animation();
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
