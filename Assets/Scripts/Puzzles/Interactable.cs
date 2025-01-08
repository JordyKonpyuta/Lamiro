using System;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public Enum_InteractableTypes.InteractableType objectType;
    public LinkedObject[] linkedObjects;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sword"))
        {
            OnInteract();
        }
    }

    void OnInteract()
    {
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
