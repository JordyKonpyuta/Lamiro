using System;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public Enum_InteractableTypes.InteractableType objectType;
    public LinkedObject[] linkedObjects;
    public Enum_MushroomColors.Colors objectColor;

    private GameObject _interactable;
    private MeshFilter _interactableMesh;


    private void Awake()
    {
        _interactableMesh = _interactable.GetComponent<MeshFilter>();
        
        switch (objectType)
        {
            case Enum_InteractableTypes.InteractableType.Crate :
                break;
            case Enum_InteractableTypes.InteractableType.Grass :
                break;
            case Enum_InteractableTypes.InteractableType.Mushroom :
                _interactableMesh.sharedMesh = (Mesh)Resources.Load("Assets/Addons/Polytope Studio/Lowpoly_Environments/Sources/Meshes/Mushrooms/PT_Caesars_Mushroom_01.fbx");
                switch (objectColor)
                {
                    case Enum_MushroomColors.Colors.Blue :
                        _interactableMesh.GetComponent<Renderer>().material.color = Color.blue;
                        break;
                    case Enum_MushroomColors.Colors.Green :
                        _interactableMesh.GetComponent<Renderer>().material.color = Color.green;
                        break;
                    case Enum_MushroomColors.Colors.Red :
                        _interactableMesh.GetComponent<Renderer>().material.color = Color.red;
                        break;
                }
                break;
            case Enum_InteractableTypes.InteractableType.Pinecone :
                break;
        }
    }

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
                if (objects.objectColor == objectColor)
                {
                    objects.Interaction();
                }
            }
        }
    }
}
