using System;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public Enum_InteractableTypes.InteractableType objectType;
    public LinkedObject[] linkedObjects;

    void OnInteract()
    {
        switch (objectType)
        {
            case Enum_InteractableTypes.InteractableType.Crate :
                break;
            case Enum_InteractableTypes.InteractableType.Mushroom :
                break;
            case Enum_InteractableTypes.InteractableType.Pinecone :
                break;
            case Enum_InteractableTypes.InteractableType.Grass :
                break;
        }
        print("Bim !");
        if (linkedObjects.Length != 0)
        {
            foreach (LinkedObject objects in linkedObjects)
            {
                objects.Interaction();
            }
        }
    }
}
