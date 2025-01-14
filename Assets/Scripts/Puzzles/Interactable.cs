using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public Enum_InteractableTypes.InteractableType objectType;
    private LinkedObject[] linkedObjects;
    private Enum_MushroomColors.Colors objectColor;
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
                objectColor = Enum_MushroomColors.Colors.Green;
                linkedInteractables = Resources.FindObjectsOfTypeAll<Interactable>();
                linkedObjects = Resources.FindObjectsOfTypeAll<LinkedObject>();
                meshes[2].gameObject.GetComponent<MeshRenderer>().material.color = new Color(0.1254717f, 1f, 0.3470062f);
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
                print(linkedInteractables.Length);
                if (linkedObjects.Length != 0)
                {
                    for (int i = 0; i < linkedObjects.Length - 1; i++)
                    {
                        linkedObjects[i].Interaction();
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
                break;
        }
        print("Bim !");
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
        if (objectColor == Enum_MushroomColors.Colors.Green)
            meshes[2].gameObject.GetComponent<MeshRenderer>().material.color = new Color(1f, 0.3470062f, 0.3254717f);
        else
            meshes[2].gameObject.GetComponent<MeshRenderer>().material.color = new Color(0.1254717f, 1f, 0.3470062f);
        
        objectColor = objectColor == Enum_MushroomColors.Colors.Green ? Enum_MushroomColors.Colors.Red : Enum_MushroomColors.Colors.Green;
            
    }
}
