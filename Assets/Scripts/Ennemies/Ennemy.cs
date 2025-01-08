using System;
using UnityEngine;

public class Ennemy : MonoBehaviour
{
    public Enum_EnnemyTypes.EnnemyTypes ennemyType;

    private GameObject ennemy;
    private MeshFilter _ennemyMesh;

    private void Awake()
    {
        _ennemyMesh = ennemy.GetComponent<MeshFilter>();
        
        switch (ennemyType)
        {
            case Enum_EnnemyTypes.EnnemyTypes.Cat :
                _ennemyMesh.sharedMesh = (Mesh)Resources.Load("Assets/Addons/Animals_FREE/Meshes/Kitty_001.fbx");
                break;
            case Enum_EnnemyTypes.EnnemyTypes.Mushroom :
                _ennemyMesh.sharedMesh = (Mesh)Resources.Load("Assets/Addons/Monsters - Mushroom + Cactus/Mesh/MushroomMeshPA.fbx");
                break;
            case Enum_EnnemyTypes.EnnemyTypes.Rabbit :
                _ennemyMesh.sharedMesh = (Mesh)Resources.Load("Assets/Addons/Rabbits/Model/Rabbit.fbx");
                break;
            case Enum_EnnemyTypes.EnnemyTypes.Slime :
                _ennemyMesh.sharedMesh = (Mesh)Resources.Load("Assets/Addons/Kawaii Slimes/Models/Slimes/Slime_01.fbx");
                break;
            case Enum_EnnemyTypes.EnnemyTypes.Spider :
                _ennemyMesh.sharedMesh = (Mesh)Resources.Load("Assets/Addons/Spiders/Models/Spider.fbx");
                break;
        }
    }
}
