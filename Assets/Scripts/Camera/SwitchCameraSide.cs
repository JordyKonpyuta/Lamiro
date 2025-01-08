using System;
using Unity.VisualScripting;
using UnityEngine;

public class SwitchCameraSide : MonoBehaviour
{
    public enum_Sides.Direction direction;
    public bool bigRoom;
    
    [Header("Box1")] public BoxCollider col;
    private float _refValue;

    // -------------------- //
    //       FUNCTIONS      //
    // -------------------- //
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        col = GetComponent<BoxCollider>();
        if (col == null || direction == enum_Sides.Direction.None)
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
                                .GetComponent<CustomCamera>().GoRoomTop(false, Vector2.zero);
                        }
                        else
                        {
                            other.GameObject().GetComponent<AllPlayerReferences>().cameraRef
                                .GetComponent<CustomCamera>().GoRoomBottom(false, Vector2.zero);
                        }
                    }
                    print("RefValue = " + _refValue + "; Player Value = " + other.GameObject().transform.position.z);
                    break;
                case enum_Sides.Direction.Horizontal:
                    if (Math.Abs(_refValue - other.GameObject().transform.position.x) > 1.5f)
                    {
                        if (other.GameObject().GetComponent<AllPlayerReferences>().cameraRef
                                .GetComponent<CustomCamera>().trueCameraPosition.x <
                            this.GameObject().transform.position.x)
                        {
                            other.GameObject().GetComponent<AllPlayerReferences>().cameraRef
                                .GetComponent<CustomCamera>().GoRoomRight(false, Vector2.zero);
                        }
                        else
                        {
                            other.GameObject().GetComponent<AllPlayerReferences>().cameraRef
                                .GetComponent<CustomCamera>().GoRoomLeft(false, Vector2.zero);
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
