using System;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public Enum_InteractableTypes.InteractableType objectType;
    public LinkedObject[] linkedObjects;
    public Enum_MushroomColors.Colors objectColor;

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
                switch (objectColor)
                {
                    case Enum_MushroomColors.Colors.Blue :
                        meshes[2].gameObject.GetComponent<Material>().SetColor(1, Color.blue);
                        break;
                    case Enum_MushroomColors.Colors.Red :
                        meshes[2].gameObject.GetComponent<Material>().SetColor(1, Color.red);
                        break;
                }
                break;
            case Enum_InteractableTypes.InteractableType.Pinecone :
                ActivateMesh(3);
                break;
        }
    }

    // Event Interaction
    void OnInteract()
    {
        switch (objectType)
        {
            case Enum_InteractableTypes.InteractableType.Crate :
                break;
            case Enum_InteractableTypes.InteractableType.Mushroom :
                if (linkedObjects.Length != 0)
                {
                    foreach (LinkedObject objects in linkedObjects)
                    {
                        if (objects.objectColor == objectColor)
                        {
                            objects.Interaction();
                        }
                    }
                }
                objectColor = objectColor == Enum_MushroomColors.Colors.Blue ? Enum_MushroomColors.Colors.Red : Enum_MushroomColors.Colors.Blue;
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
}
