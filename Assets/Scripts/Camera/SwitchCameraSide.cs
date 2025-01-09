using System;
using Unity.VisualScripting;
using UnityEngine;

public class SwitchCameraSide : MonoBehaviour
{
    public enum_Sides.Direction direction;
    [Tooltip("Is one of the sides a Beeg Room?")] public bool bigRoom;
    [Tooltip("Set the big room's position")] public enum_Sides.Sides sideEntranceForBigRoom;
    
    private BoxCollider _col;
    private float _refValue;

    // -------------------- //
    //       FUNCTIONS      //
    // -------------------- //
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _col = GetComponent<BoxCollider>();
        if (_col == null || direction == enum_Sides.Direction.None)
            print("ERROR! " + gameObject.name + " HAS NULL REFERENCES AND/OR NO DIRECTION!");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    

    void Enter(Collider other)
    {
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            switch (direction)
            {
                case enum_Sides.Direction.Horizontal:
                    _refValue = other.transform.position.x;
                    break;
                case enum_Sides.Direction.Vertical:
                    _refValue = other.transform.position.z;
                    break;
                case enum_Sides.Direction.None:
                    break;
            }
        }
    }
    
    void OnTriggerExit(Collider other)
    {
        if (other.GameObject().CompareTag("Player"))
        {
            Vector3 entranceRefVector = new Vector2(transform.position.x, transform.position.z);
            switch (direction)
            {
                case enum_Sides.Direction.Vertical:
                    if (Math.Abs(_refValue - other.GameObject().transform.position.z) > 1.5f)
                    {
                        if (other.GameObject().GetComponent<AllPlayerReferences>().cameraRef
                                .GetComponent<CustomCamera>().trueCameraPosition.z <
                            this.GameObject().transform.position.z)
                        {
                            other.GameObject().GetComponent<AllPlayerReferences>().cameraRef
                                .GetComponent<CustomCamera>().GoRoomTop(sideEntranceForBigRoom == enum_Sides.Sides.North, entranceRefVector);
                        }
                        else
                        {
                            other.GameObject().GetComponent<AllPlayerReferences>().cameraRef
                                .GetComponent<CustomCamera>().GoRoomBottom(sideEntranceForBigRoom == enum_Sides.Sides.South, entranceRefVector);
                        }
                    }
                    break;
                case enum_Sides.Direction.Horizontal:
                    if (Math.Abs(_refValue - other.GameObject().transform.position.x) > 1.5f)
                    {
                        if (other.GameObject().GetComponent<AllPlayerReferences>().cameraRef
                                .GetComponent<CustomCamera>().trueCameraPosition.x <
                            this.GameObject().transform.position.x)
                        {
                            other.GameObject().GetComponent<AllPlayerReferences>().cameraRef
                                .GetComponent<CustomCamera>().GoRoomRight(sideEntranceForBigRoom == enum_Sides.Sides.East, entranceRefVector);
                        }
                        else
                        {
                            other.GameObject().GetComponent<AllPlayerReferences>().cameraRef
                                .GetComponent<CustomCamera>().GoRoomLeft(sideEntranceForBigRoom == enum_Sides.Sides.West, entranceRefVector);
                        }
                    }
                    break;
                case enum_Sides.Direction.None:
                    break;
                default:
                    break;
            }
        }
    }
}
