using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public Enum_InteractableTypes.InteractableType objectType;
    private LinkedObject[] _linkedObjects;
    private Enum_MushroomColors.Colors _objectColor;
    public Interactable[] linkedInteractables;

    public Transform[] meshes;

    private GameObject _interactable;


    private void Awake()
    {
        
        switch (objectType)
        {
            case Enum_InteractableTypes.InteractableType.Crate :
                ActivateMesh(0);
                break;
            case Enum_InteractableTypes.InteractableType.Grass :
                ActivateMesh(1);
                break;
            case Enum_InteractableTypes.InteractableType.Mushroom :
                ActivateMesh(2);
                _objectColor = Enum_MushroomColors.Colors.Green;
                linkedInteractables = Resources.FindObjectsOfTypeAll<Interactable>();
                _linkedObjects = Resources.FindObjectsOfTypeAll<LinkedObject>();
                meshes[2].gameObject.GetComponent<MeshRenderer>().sharedMaterial.color = new Color(0.1254717f, 1f, 0.3470062f);
                break;
            case Enum_InteractableTypes.InteractableType.Pinecone :
                ActivateMesh(3);
                break;
        }
    }

    // Event Interaction
    public void OnInteract()
    {
        switch (objectType)
        {
            case Enum_InteractableTypes.InteractableType.Crate :
                break;
            case Enum_InteractableTypes.InteractableType.Mushroom :
                if (_linkedObjects.Length != 0)
                {
                    for (int i = 0; i < _linkedObjects.Length - 1; i++)
                    {
                        _linkedObjects[i].Interaction();
                    }
                }

                if (linkedInteractables.Length != 0)
                {
                    for (int i = 0; i < linkedInteractables.Length - 1; i++)
                    {
                        linkedInteractables[i].switchUpColors();
                    }
                }
                break;
            case Enum_InteractableTypes.InteractableType.Pinecone :
                break;
            case Enum_InteractableTypes.InteractableType.Grass :
                Destroy(gameObject,0.1f);
                break;
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

    public void switchUpColors()
    {
        if (_objectColor == Enum_MushroomColors.Colors.Green)
            meshes[2].gameObject.GetComponent<MeshRenderer>().sharedMaterial.color = new Color(1f, 0.3470062f, 0.3254717f);
        else
            meshes[2].gameObject.GetComponent<MeshRenderer>().sharedMaterial.color = new Color(0.1254717f, 1f, 0.3470062f);
        
        _objectColor = _objectColor == Enum_MushroomColors.Colors.Green ? Enum_MushroomColors.Colors.Red : Enum_MushroomColors.Colors.Green;
    }
}
