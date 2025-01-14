using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class Collectibles : MonoBehaviour
{

    public Enum_Collectibles.CollectibleType type;

    public Transform[] meshes;
    public AudioResource[] audios;

    public int screwsGains = 1;
    
    
    // Singleton to be accessible anywhere
    
    private static Collectibles _instance;

    public static Collectibles Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("Player is null!");
            }

            return _instance;
        }
    }

    void Awake()
    {
        _instance = this;

        switch (type)
        {
            case Enum_Collectibles.CollectibleType.Sword :
                ActivateMesh(0);
                gameObject.GetComponent<AudioSource>().resource = audios[2];
                break;
            case Enum_Collectibles.CollectibleType.Screws :
                if (screwsGains >= 25)
                {
                    meshes[2].gameObject.GetComponent<MeshRenderer>().material.color = new Color(0.8773585f, 0.8257492f, 0f, 1f);
                    meshes[1].gameObject.GetComponent<MeshRenderer>().material.color = new Color(0.8773585f, 0.8257492f, 0f, 1f);
                    meshes[0].gameObject.GetComponent<MeshRenderer>().material.color = new Color(0.8773585f, 0.8257492f, 0f, 1f);
                    meshes[0].localScale += new Vector3(5f, 5f, 5f);
                    meshes[1].localScale += new Vector3(5f, 5f, 5f);
                    meshes[2].localScale += new Vector3(5f, 5f, 5f);
                    ActivateMesh(2);
                    ActivateMesh(1);
                    ActivateMesh(0);
                    gameObject.GetComponent<AudioSource>().resource = audios[1];
                }
                else if (screwsGains >= 10)
                {
                    meshes[1].gameObject.GetComponent<MeshRenderer>().material.color = new Color(0.8773585f, 0.8257492f, 0f, 1f);
                    meshes[0].gameObject.GetComponent<MeshRenderer>().material.color = new Color(0.8773585f, 0.8257492f, 0f, 1f);
                    meshes[0].localScale += new Vector3(5f, 5f, 5f);
                    meshes[1].localScale += new Vector3(5f, 5f, 5f);
                    ActivateMesh(1);
                    ActivateMesh(0);
                    gameObject.GetComponent<AudioSource>().resource = audios[1];
                }
                else if (screwsGains >= 5)
                {
                    meshes[0].gameObject.GetComponent<MeshRenderer>().material.color = new Color(0.8773585f, 0.8257492f, 0f, 1f);
                    meshes[0].localScale += new Vector3(5f, 5f, 5f);
                    ActivateMesh(0);
                    gameObject.GetComponent<AudioSource>().resource = audios[1];
                }
                else if (screwsGains == 3)
                {
                    ActivateMesh(2);
                    ActivateMesh(1);
                    ActivateMesh(0);
                    gameObject.GetComponent<AudioSource>().resource = audios[0];
                }
                else if (screwsGains == 2)
                {
                    ActivateMesh(1);
                    ActivateMesh(0);
                    gameObject.GetComponent<AudioSource>().resource = audios[0];
                }
                else if (screwsGains == 1)
                {
                    ActivateMesh(0);
                    gameObject.GetComponent<AudioSource>().resource = audios[0];
                }
                break;
            case Enum_Collectibles.CollectibleType.Gun :
                ActivateMesh(1);
                gameObject.GetComponent<AudioSource>().resource = audios[3];
                break;
            case Enum_Collectibles.CollectibleType.Jetpack :
                ActivateMesh(2);
                gameObject.GetComponent<AudioSource>().resource = audios[4];
                break;
            case Enum_Collectibles.CollectibleType.SpaceshipPieces :
                break;
        }
    }

    private void Update()
    {
        transform.Rotate(new Vector3(0f, 45f * Time.deltaTime, 0f));
    }


    // When player Collide with the collectible
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            switch (type)
            {
                case Enum_Collectibles.CollectibleType.Screws :
                    other.gameObject.GetComponent<Inventory>().AddScrews(screwsGains);
                    HUD.Instance.UpdateScrewsText(Inventory.Instance.GetScrews());
                    gameObject.GetComponent<AudioSource>().Play();
                    Destroy(this);
                    DesactivateMesh(0);
                    Destroy(gameObject, 1.5f);
                    break;
                case Enum_Collectibles.CollectibleType.Gun :
                    other.gameObject.GetComponent<Gun>().enabled = true;
                    HUD.Instance.gunIcon.enabled = true;
                    gameObject.GetComponent<AudioSource>().Play();
                    Destroy(this);
                    DesactivateMesh(0);
                    Destroy(gameObject, 1.5f);
                    break;
                case Enum_Collectibles.CollectibleType.Sword :
                    other.gameObject.GetComponentInChildren<Sword>().enabled = true;
                    HUD.Instance.swordIcon.enabled = true;
                    gameObject.GetComponent<AudioSource>().Play();
                    Destroy(this);
                    DesactivateMesh(0);
                    Destroy(gameObject, 1.5f);
                    break;
                case Enum_Collectibles.CollectibleType.Jetpack :
                    other.gameObject.GetComponentInChildren<Jump>().enabled = true;
                    HUD.Instance.jetpackIcon.enabled = true;
                    gameObject.GetComponent<AudioSource>().Play();
                    Destroy(this);
                    DesactivateMesh(0);
                    Destroy(gameObject, 1.5f);
                    break;
                case Enum_Collectibles.CollectibleType.SpaceshipPieces :
                    other.gameObject.GetComponent<Inventory>().spaceshipPieces++;
                    gameObject.GetComponent<AudioSource>().Play();
                    Destroy(this);
                    DesactivateMesh(0);
                    Destroy(gameObject, 1.5f);
                    break;
            }
        }
    }

    private void ActivateMesh(int index)
    {
        meshes[index].gameObject.SetActive(true);
    }

    private void DesactivateMesh(int index)
    {
        meshes[index].gameObject.SetActive(false);
    }
}
