using System;
using UnityEngine;
using System.Collections;

public class Inventory : MonoBehaviour
{
    [SerializeField] private int screws;
    public int spaceshipPieces = 0;
    public float timer = 0;
    void Awake()
    {
        gameObject.GetComponent<AllPlayerReferences>().invRef = this;
    }

    private void Start()
    {
        InvokeRepeating(nameof(SetTimer), 1, 1);
    }


    // Getters
    public int GetScrews()
    {
        return screws;
    }
    
    
    // Setters
    public void AddScrews(int amount)
    {
        screws += amount;
    }

    public void SetTimer()
    {
        timer++;
    }


    public void CancelTimer()
    {
        CancelInvoke(nameof(SetTimer));
    }
}
