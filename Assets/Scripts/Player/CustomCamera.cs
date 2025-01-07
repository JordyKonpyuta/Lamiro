using System;
using Unity.VisualScripting;
using UnityEngine;

public class Camera : MonoBehaviour
{
    // Initialisation Variables
    [Tooltip("Camera Component")] private Camera _camera;
    
    [Header("Camera target position")] [Tooltip("Camera Position to Reach")] 
    public Vector3 trueCameraPosition = new Vector3(0,0,0);
        // X = room middle ; Y = constant 17 ; Z = room middle - 4
        
    private Vector3 _camVelocity = Vector3.zero;

    // Big Room Camera Handling
    [Header("Camera Behaviour")] [Tooltip("Is a Big Room")] 
    public bool bIsBigRoom;
    [Tooltip("Big Room Bottom Left Position")] private Vector2 _bigRoomBottomLeft = new Vector2(0,0);
    [Tooltip("Big Room Top Right Position")] private Vector2 _bigRoomTopRight = new Vector2(0,0);
    
    // Cinematic Handling
    [Header("Camera Behaviour")] [Tooltip("Is in a cinematic")] 
    public bool bIsCinematic;
    

    // -------------------- //
    //       FUNCTIONS      //
    // -------------------- //
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _camera = this.GameObject().GetComponent<Camera>();
        _camera.transform.parent = null;
        trueCameraPosition = new Vector3(20.0f, 20.0f, 7.0f);
        _camera.transform.rotation = Quaternion.Euler(75.0f, 0.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (bIsCinematic || !_camera)
        {
            return;
        }

        if (bIsBigRoom)
        {
            trueCameraPosition = new Vector3(
                Mathf.Clamp(this.GameObject().transform.position.x, _bigRoomBottomLeft.x, _bigRoomTopRight.x), 
                17.0f,
                Mathf.Clamp(this.GameObject().transform.position.y, _bigRoomBottomLeft.y, _bigRoomTopRight.y)
            );
        }
        
        _camera.transform.position = Vector3.SmoothDamp(_camera.transform.position, trueCameraPosition, ref _camVelocity, 0.1f);
    }

    public void SetRoomDataBig(Vector2 entrance)
    {
        if (bIsBigRoom)
        {
            if (entrance.x == 0)
            {
                _bigRoomBottomLeft.x = trueCameraPosition.x;
                _bigRoomTopRight.x = trueCameraPosition.x + 40;
            }
            else
            {
                _bigRoomBottomLeft.x = trueCameraPosition.x - 40;
                _bigRoomTopRight.x = trueCameraPosition.x;
            }

            if (entrance.y == 0)
            {
                _bigRoomBottomLeft.y = trueCameraPosition.y;
                _bigRoomTopRight.y = trueCameraPosition.y + 20;
            }
            else
            {
                _bigRoomBottomLeft.y = trueCameraPosition.y - 20;
                _bigRoomTopRight.y = trueCameraPosition.y;
            }
        }
    }

    public void GoRoomLeft(bool bBigRoom, Vector2 entrance)
    {
        trueCameraPosition.x -= 40;
        if (bBigRoom)
        {
            bIsBigRoom = true;
            SetRoomDataBig(entrance);
            if (Math.Abs(entrance.x) < 0.1f)
                print("Entrance X is impossible!");
        }
    }

    public void GoRoomRight(bool bBigRoom, Vector2 entrance)
    {
        trueCameraPosition.x += 40;
        if (bBigRoom)
        {
            bIsBigRoom = true;
            SetRoomDataBig(entrance);
            if (Math.Abs(entrance.x - 1) < 0.1)
                print("Entrance X is impossible!");
        }
    }

    public void GoRoomTop(bool bBigRoom, Vector2 entrance)
    {
        trueCameraPosition.y += 20;
        if (bBigRoom)
        {
            bIsBigRoom = true;
            SetRoomDataBig(entrance);
            if (Math.Abs(entrance.y - 1) < 0.1)
                print("Entrance Y is impossible!");
        }
    }

    public void GoRoomBottom(bool bBigRoom, Vector2 entrance)
    {
        trueCameraPosition.y -= 20;
        if (bBigRoom)
        {
            bIsBigRoom = true;
            SetRoomDataBig(entrance);
            if (Math.Abs(entrance.y) < 0.1)
                print("Entrance Y is impossible!");
        }
    }
}
