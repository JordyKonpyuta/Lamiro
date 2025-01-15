using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LinkedObject : MonoBehaviour
{
    public Enum_LinkedObjectsType.LinkedObjectType objectType;
    public Enum_MushroomColors.Colors objectColor;

    private bool _isFlat = false;

    public Transform[] meshes;

    private Vector3 _targetPositionUp;
    private Vector3 _targetPositionDown;

    private Ennemy _ennemyRef;
    
    public List<Material> materials;
    private Material _refMaterial;
    private Color BaseColor;

    private void Awake()
    {
        _targetPositionUp = transform.position;
        _targetPositionDown = new Vector3(transform.position.x, transform.position.y - 50, transform.position.z);
        switch (objectColor)
        {
            case Enum_MushroomColors.Colors.Green :
                SwitchColors(0);
                Fatten();
                break;
            case Enum_MushroomColors.Colors.Red :
                SwitchColors(1);
                Flatten();
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (other.gameObject.GetComponent<Ennemy>().isAttacking)
            {
                other.gameObject.GetComponent<Ennemy>().GetStunned();
            }
        }
    }
    

    public void Interaction()
    {
        if (_isFlat)
        {
            Fatten();
        }
        
        else
        {
            Flatten();
        }
    }

    public void SwitchColors(int index)
    {
        if (meshes[0])
            meshes[0].gameObject.GetComponent<MeshRenderer>().material = materials[index];
        if (meshes[1])
            meshes[1].gameObject.GetComponent<MeshRenderer>().material = materials[index];
        if (meshes[2])
            meshes[2].gameObject.GetComponent<MeshRenderer>().material = materials[index];
        if (meshes[3])
            meshes[3].gameObject.GetComponent<MeshRenderer>().material = materials[index];
        if (meshes[4])
            meshes[4].gameObject.GetComponent<MeshRenderer>().material = materials[index];
        BaseColor = materials[index].color;
    }

    public void Flatten()
    {
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, 0.2f, gameObject.transform.position.z);
        gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x, 0.1f, gameObject.transform.localScale.z);
        if (meshes[0])
            meshes[0].gameObject.GetComponent<MeshRenderer>().material.color = BaseColor - new Color(0.5f, 0.5f, 0.5f, 0.00f);
        if (meshes[1])
            meshes[1].gameObject.GetComponent<MeshRenderer>().material.color = BaseColor - new Color(0.5f, 0.5f, 0.5f, 0.00f);
        if (meshes[2])
            meshes[2].gameObject.GetComponent<MeshRenderer>().material.color = BaseColor - new Color(0.5f, 0.5f, 0.5f, 0.00f);
        if (meshes[3])
            meshes[3].gameObject.GetComponent<MeshRenderer>().material.color = BaseColor - new Color(0.5f, 0.5f, 0.5f, 0.00f);
        if (meshes[4])
            meshes[4].gameObject.GetComponent<MeshRenderer>().material.color = BaseColor - new Color(0.5f, 0.5f, 0.5f, 0.00f);
        gameObject.GetComponent<BoxCollider>().enabled = false;
        _isFlat = true;
    }

    public void Fatten()
    {
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, 2f, gameObject.transform.position.z);
        gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x, 1f, gameObject.transform.localScale.z);
        if (meshes[0])
            meshes[0].gameObject.GetComponent<MeshRenderer>().material.color = BaseColor;
        if (meshes[1])
            meshes[1].gameObject.GetComponent<MeshRenderer>().material.color = BaseColor;
        if (meshes[2])
            meshes[2].gameObject.GetComponent<MeshRenderer>().material.color = BaseColor;
        if (meshes[3])
            meshes[3].gameObject.GetComponent<MeshRenderer>().material.color = BaseColor;
        if (meshes[4])
            meshes[4].gameObject.GetComponent<MeshRenderer>().material.color = BaseColor;
        gameObject.GetComponent<BoxCollider>().enabled = true;
        _isFlat = false;
    }
}
