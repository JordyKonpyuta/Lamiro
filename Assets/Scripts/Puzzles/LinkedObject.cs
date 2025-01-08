using Unity.VisualScripting;
using UnityEngine;

public class LinkedObject : MonoBehaviour
{
    public Enum_LinkedObjectsType.LinkedObjectType objectType;
    public Enum_MushroomColors.Colors objectColor;

    private bool _isHidden = false;

    private GameObject _linkedObject;
    private MeshFilter _objectMesh;

    private Vector3 _targetPositionUp;
    private Vector3 _targetPositionDown;

    private void Awake()
    {
        _targetPositionUp = transform.position;
        _targetPositionDown = new Vector3(transform.position.x, transform.position.y - 50, transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ennemy"))
        {
            print("Bam!");
        }
    }
    

    public void Interaction()
    {
        if (_isHidden)
        {
            _linkedObject.GetComponent<BoxCollider>().enabled = true;
            transform.position = Vector3.MoveTowards(transform.position, _targetPositionUp, 0.5f);
            _isHidden = false;
        }
        
        else
        {
            _linkedObject.GetComponent<BoxCollider>().enabled = false;
            transform.position = Vector3.MoveTowards(transform.position, _targetPositionDown, 0.5f);
            _isHidden = true;
        }
        
    }
}
