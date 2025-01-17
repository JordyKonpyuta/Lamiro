using System;
using UnityEngine;

public class Water : MonoBehaviour
{
    private BoxCollider _boxRef;
    public bool removeLeftSide = false;
    public bool removeUpSide = false;
    public bool removeRightSide = false;
    public bool removeDownSide = false;
    public Transform[] meshes;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _boxRef = gameObject.GetComponent<BoxCollider>();
        if (removeLeftSide)
            meshes[0].gameObject.SetActive(false);
        if (removeUpSide)
            meshes[1].gameObject.SetActive(false);
        if (removeRightSide)
            meshes[2].gameObject.SetActive(false);
        if (removeDownSide)
            meshes[3].gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.position = new Vector3(other.gameObject.GetComponent<AllPlayerReferences>().Entrypoint.x,
                    other.gameObject.GetComponent<AllPlayerReferences>().Entrypoint.y + 3f,
                    other.gameObject.GetComponent<AllPlayerReferences>().Entrypoint.z);
            other.gameObject.GetComponent<PlayerHealth>().TakeDamage(1);
        }
    }
}
