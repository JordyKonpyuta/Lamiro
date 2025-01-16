using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class LinkedObject : MonoBehaviour
{
    public Enum_LinkedObjectsType.LinkedObjectType objectType;
    public Enum_MushroomColors.Colors objectColor;

    private bool _isFlat = false;
    private bool _isFattening = false;

    public Transform[] meshes;

    private Ennemy _ennemyRef;
    
    public List<Material> materials;
    private Material _refMaterial;
    private Color BaseColor;
    
    // Smooth up/down
    private float _targetPos;
    private float _targetScale; 
    
    private NavMeshObstacle _navMeshObstacle;

    private void Awake()
    {
        _navMeshObstacle = GetComponent<NavMeshObstacle>();
        switch (objectColor)
        {
            case Enum_MushroomColors.Colors.Green :
                SwitchColors(0);
                gameObject.transform.position = new Vector3(
                    gameObject.transform.position.x,
                    2f,
                    gameObject.transform.position.z);
                gameObject.transform.localScale = new Vector3(
                    gameObject.transform.localScale.x,
                    1f,
                    gameObject.transform.localScale.z);
                Fatten();
                break;
            case Enum_MushroomColors.Colors.Red :
                SwitchColors(1);
                gameObject.transform.position = new Vector3(
                    gameObject.transform.position.x,
                    0.2f,
                    gameObject.transform.position.z);
                gameObject.transform.localScale = new Vector3(
                    gameObject.transform.localScale.x,
                    0.1f,
                    gameObject.transform.localScale.z);
                _navMeshObstacle.enabled = false;
                Flatten();
                break;
        }
    }

    private void Update()
    {
        int yMultPos =  gameObject.transform.position.y - _targetPos > 0 ? -1 : 1;

        if ((!_isFlat && gameObject.transform.position.y < 2f) || (_isFlat && gameObject.transform.position.y > 0.2f))
            gameObject.transform.position = new Vector3(
                gameObject.transform.position.x,
                gameObject.transform.position.y + (0.02f * yMultPos),
                gameObject.transform.position.z);
        
        
        yMultPos =  gameObject.transform.localScale.y - _targetScale > 0 ? -1 : 1;
        if ((!_isFlat && gameObject.transform.position.y < 1f) || (_isFlat && gameObject.transform.position.y > 0.1f))
            gameObject.transform.localScale = new Vector3(
                gameObject.transform.localScale.x,
                gameObject.transform.localScale.y + (0.01f * yMultPos),
                gameObject.transform.localScale.z);
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
            _targetPos = 2f;
            _targetScale = 1f;
            Fatten();
        }
        
        else
        {
            _targetPos = 0.2f;
            _targetScale = 0.1f;
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
        _targetPos = 0.2f;
        _targetScale = 0.1f;
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
        _navMeshObstacle.enabled = false;
    }

    public void Fatten()
    {
        _targetPos = 2f;
        _targetScale = 1f;
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
        _isFattening = true;
        Invoke(nameof(StoppedFattening), 1.25f);
        _navMeshObstacle.enabled = true;
    }

    private void StoppedFattening()
    {
        _isFattening = false;
    }
    
    // GETTERS

    public bool getIsFattening()
    {
        return _isFattening;
    }
}
