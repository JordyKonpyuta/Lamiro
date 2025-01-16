using System;
using Unity.VisualScripting;
using UnityEditor.Rendering.LookDev;
using UnityEngine;

public class CustomCamera : MonoBehaviour
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
    
    public Vector3 cameraStartPosition = new Vector3(0,0,0);
    
    // References
    private CapsuleCollider _playerBodyRef;
    

    // -------------------- //
    //       FUNCTIONS      //
    // -------------------- //
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _camera = this.GameObject().GetComponent<Camera>();
        _camera.transform.parent.GetComponent<AllPlayerReferences>().cameraRef = _camera;
        _playerBodyRef = _camera.transform.parent.GetComponent<CapsuleCollider>();
        Invoke(nameof(RemoveCamera), 0.4f);
        trueCameraPosition = new Vector3(cameraStartPosition.x, cameraStartPosition.y, cameraStartPosition.z);
        _camera.transform.rotation = Quaternion.Euler(75.0f, 0.0f, 0.0f);
    }

    private void RemoveCamera()
    {
        _camera.transform.parent = null;
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
                Mathf.Clamp(_playerBodyRef.transform.position.x, _bigRoomBottomLeft.x, _bigRoomTopRight.x), 
                25.0f,
                Mathf.Clamp(_playerBodyRef.transform.position.z - 6, _bigRoomBottomLeft.y, _bigRoomTopRight.y)
            );
        }
        
        _camera.transform.position = Vector3.SmoothDamp(_camera.transform.position, trueCameraPosition, ref _camVelocity, 0.1f);
    }

    private void SetRoomDataBig(Vector2 entrance, enum_Sides.Sides entranceSide)
    {
        if (bIsBigRoom)
        {
            switch (entranceSide)
            {
                case enum_Sides.Sides.North:
                    _bigRoomBottomLeft.x = entrance.x - 20;
                    _bigRoomBottomLeft.y = entrance.y;
                    _bigRoomTopRight.x = entrance.x + 20;
                    _bigRoomTopRight.y = entrance.y + 20;
                    break;
                case enum_Sides.Sides.West:
                    _bigRoomBottomLeft.x = entrance.x - 60;
                    _bigRoomBottomLeft.y = entrance.y - 20;
                    _bigRoomTopRight.x = entrance.x - 20;
                    _bigRoomTopRight.y = entrance.y;
                    break;
                case enum_Sides.Sides.East:
                    _bigRoomBottomLeft.x = entrance.x + 20;
                    _bigRoomBottomLeft.y = entrance.y - 20;
                    _bigRoomTopRight.x = entrance.x + 60;
                    _bigRoomTopRight.y = entrance.y ;
                    break;
                case enum_Sides.Sides.South:
                    _bigRoomBottomLeft.x = entrance.x - 20;
                    _bigRoomBottomLeft.y = entrance.y - 40;
                    _bigRoomTopRight.x = entrance.x + 20;
                    _bigRoomTopRight.y = entrance.y - 20;
                    break;
                case enum_Sides.Sides.None:
                    break;
            }
        }
    }

    public void GoRoomLeft(bool bBigRoom, Vector2 entrance)
    {
        trueCameraPosition.x -= 40;
        if (bIsBigRoom)
        {
            trueCameraPosition.z = entrance.y - 10;
        }
        bIsBigRoom = bBigRoom;
        if (bIsBigRoom)
        {
            SetRoomDataBig(entrance, enum_Sides.Sides.West);
        }
        foreach(Ennemy curEnemy in _playerBodyRef.gameObject.GetComponent<AllPlayerReferences>().allEnemies)
        {
            curEnemy.ResetStatus();
        }
        
    }

    public void GoRoomRight(bool bBigRoom, Vector2 entrance)
    {
        trueCameraPosition.x += 40;
        if (bIsBigRoom)
        {
            trueCameraPosition.z = entrance.y - 10;
        }
        bIsBigRoom = bBigRoom;
        if (bIsBigRoom)
        {
            SetRoomDataBig(entrance, enum_Sides.Sides.East);
        }
        foreach(Ennemy curEnemy in _playerBodyRef.gameObject.GetComponent<AllPlayerReferences>().allEnemies)
        {
            curEnemy.ResetStatus();
        }
    }

    public void GoRoomTop(bool bBigRoom, Vector2 entrance)
    {
        trueCameraPosition.z += 20;
        if (bIsBigRoom)
        {
            trueCameraPosition.x = entrance.x;
        }
        bIsBigRoom = bBigRoom;
        if (bIsBigRoom)
        {
            SetRoomDataBig(entrance, enum_Sides.Sides.North);
        }
        foreach(Ennemy curEnemy in _playerBodyRef.gameObject.GetComponent<AllPlayerReferences>().allEnemies)
        {
            curEnemy.ResetStatus();
        }
    }

    public void GoRoomBottom(bool bBigRoom, Vector2 entrance)
    {
        trueCameraPosition.z -= 20;
        if (bIsBigRoom)
        {
            trueCameraPosition.x = entrance.x;
        }
        bIsBigRoom = bBigRoom;
        if (bBigRoom)
        {
            SetRoomDataBig(entrance, enum_Sides.Sides.South);
        }
        foreach(Ennemy curEnemy in _playerBodyRef.gameObject.GetComponent<AllPlayerReferences>().allEnemies)
        {
            curEnemy.ResetStatus();
        }
    }
}
