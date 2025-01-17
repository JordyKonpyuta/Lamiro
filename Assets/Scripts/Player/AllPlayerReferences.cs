using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AllPlayerReferences : MonoBehaviour
{
    public Camera cameraRef = null;
    public List<Ennemy> allEnemies = new();
    public Vector3 Entrypoint = Vector3.zero;
    public HUD HUDref = null;
    public Inventory invRef = null;
}