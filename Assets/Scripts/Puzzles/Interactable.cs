using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public Enum_InteractableTypes.InteractableType objectType;
    private LinkedObject[] _allObjects;
    private List<LinkedObject> _linkedObjects = new();
    private Interactable[] _allInteractables;
    private List<Interactable> _linkedInteractables = new();
    private Enum_MushroomColors.Colors _objectColor;

    public Transform[] meshes;

    private GameObject _interactable;
    
    private Color _redColor = new Color(1f, 0.3470062f, 0.3254717f);
    private Color _greenColor = new Color(0.1254717f, 1f, 0.3470062f);


    private void Start()
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
                _allInteractables = Resources.FindObjectsOfTypeAll<Interactable>();
                foreach (Interactable interObject in _allInteractables)
                {
                    if (interObject.gameObject)
                    {
                        _linkedInteractables.Add(interObject);
                    }
                }

                foreach (Interactable interObject in _linkedInteractables.ToList())
                {
                    if (interObject.gameObject.transform.position.x < 2 &&
                        interObject.gameObject.transform.position.x > -2 &&
                        interObject.gameObject.transform.position.z > -2 &&
                        interObject.gameObject.transform.position.z < 2)
                    {
                        _linkedInteractables.Remove(interObject);
                    }
                }
                
                _allObjects = Resources.FindObjectsOfTypeAll<LinkedObject>();
                foreach (LinkedObject linkObject in _allObjects)
                {
                    if (linkObject.gameObject)
                    {
                        _linkedObjects.Add(linkObject);
                    }
                }

                foreach (LinkedObject linkObject in _linkedObjects.ToList())
                {
                    if (linkObject.gameObject.transform.position.x < 2 &&
                        linkObject.gameObject.transform.position.x > -2 &&
                        linkObject.gameObject.transform.position.z > -2 &&
                        linkObject.gameObject.transform.position.z < 2)
                    {
                        _linkedObjects.Remove(linkObject);
                    }
                }
                
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
                
                List<LinkedObject> curLinkObj = _linkedObjects;
                if (_linkedObjects.Count != 0)
                {
                    foreach (LinkedObject linkedObject in curLinkObj)
                    {
                    print(linkedObject);
                        if (linkedObject)
                            linkedObject.Interaction();
                        else
                            _linkedObjects.Remove(linkedObject);
                    }
                }
                print("Switched up Color!!!");

                
                List<Interactable> curInter = _linkedInteractables;
                if (_linkedInteractables.Count != 0)
                {
                    foreach (Interactable interObject in curInter)
                    {
                        if (interObject)
                            interObject.switchUpColors();
                        else
                            _linkedInteractables.Remove(interObject);
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
            meshes[2].gameObject.GetComponent<MeshRenderer>().material.color = _redColor;
        else
            meshes[2].gameObject.GetComponent<MeshRenderer>().material.color = _greenColor;
        
        _objectColor = _objectColor == Enum_MushroomColors.Colors.Green ? Enum_MushroomColors.Colors.Red : Enum_MushroomColors.Colors.Green;
    }
}
