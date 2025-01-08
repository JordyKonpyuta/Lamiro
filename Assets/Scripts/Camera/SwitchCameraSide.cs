using Unity.VisualScripting;
using UnityEngine;

public class SwitchCameraSide : MonoBehaviour
{
    public enum_Sides.Direction direction;
    public bool bigRoom;
    
    [Header("Box1")] public BoxCollider col;

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
    
    void OnTriggerExit(Collider other)
    {
        if (other.GameObject().CompareTag("Player"))
        {
            switch (direction)
            {
                case enum_Sides.Direction.Vertical:
                    if (other.GameObject().GetComponent<AllPlayerReferences>().cameraRef.transform.position.z <
                        this.GameObject().transform.position.z)
                    {
                        other.GameObject().GetComponent<AllPlayerReferences>().cameraRef.GetComponent<CustomCamera>().GoRoomTop(false, Vector2.zero);
                    }
                    else
                    {
                        other.GameObject().GetComponent<AllPlayerReferences>().cameraRef.GetComponent<CustomCamera>().GoRoomBottom(false, Vector2.zero);
                    }
                    break;
                case enum_Sides.Direction.Horizontal:
                    if (other.GameObject().GetComponent<AllPlayerReferences>().cameraRef.transform.position.x <
                        this.GameObject().transform.position.x)
                    {
                        other.GameObject().GetComponent<AllPlayerReferences>().cameraRef.GetComponent<CustomCamera>().GoRoomRight(false, Vector2.zero);
                    }
                    else
                    {
                        other.GameObject().GetComponent<AllPlayerReferences>().cameraRef.GetComponent<CustomCamera>().GoRoomLeft(false, Vector2.zero);
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
