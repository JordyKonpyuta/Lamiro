using Unity.VisualScripting;
using UnityEngine;

public class LinkedObject : MonoBehaviour
{
    public Enum_LinkedObjectsType.LinkedObjectType objectType;
    public Enum_MushroomColors.Colors objectColor;

    private bool _isHidden = false;

    private GameObject _linkedObject;

    public Transform[] meshes;

    private Vector3 _targetPositionUp;
    private Vector3 _targetPositionDown;

    private Ennemy _ennemyRef;

    private void Awake()
    {
        _targetPositionUp = transform.position;
        _targetPositionDown = new Vector3(transform.position.x, transform.position.y - 50, transform.position.z);
        ActivateMesh(0);
        switch (objectColor)
        {
            case Enum_MushroomColors.Colors.Blue :
                meshes[0].gameObject.GetComponent<Material>().SetColor(1, Color.blue);
                break;
            case Enum_MushroomColors.Colors.Red :
                meshes[0].gameObject.GetComponent<Material>().SetColor(1, Color.red);
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ennemy"))
        {
            other.gameObject.GetComponent<Ennemy>().isStunned = true;
            other.gameObject.GetComponent<Ennemy>().IsStun();
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

    private void ActivateMesh(int index)
    {
        for (int i = 0; i < meshes.Length; i++)
        {
            if (i == index)
                meshes[i].gameObject.SetActive(true);
            else
                meshes[i].gameObject.SetActive(false);
        }
    }
}
