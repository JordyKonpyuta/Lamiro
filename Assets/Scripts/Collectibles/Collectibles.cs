using UnityEngine;
using UnityEngine.Audio;

public class Collectibles : MonoBehaviour
{

    public Enum_Collectibles.CollectibleType type;

    public int spaceshipIndex;

    public GameObject vfx;

    public Transform[] spaceshipMeshes;
    public Transform[] meshes;

    private AudioSource _audioSource;
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

        _audioSource = GetComponent<AudioSource>();

        switch (type)
        {
            case Enum_Collectibles.CollectibleType.Sword :
                ActivateMesh(0);
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
                }
                else if (screwsGains >= 10)
                {
                    meshes[1].gameObject.GetComponent<MeshRenderer>().material.color = new Color(0.8773585f, 0.8257492f, 0f, 1f);
                    meshes[0].gameObject.GetComponent<MeshRenderer>().material.color = new Color(0.8773585f, 0.8257492f, 0f, 1f);
                    meshes[0].localScale += new Vector3(5f, 5f, 5f);
                    meshes[1].localScale += new Vector3(5f, 5f, 5f);
                    ActivateMesh(1);
                    ActivateMesh(0);
                }
                else if (screwsGains >= 5)
                {
                    meshes[0].gameObject.GetComponent<MeshRenderer>().material.color = new Color(0.8773585f, 0.8257492f, 0f, 1f);
                    meshes[0].localScale += new Vector3(5f, 5f, 5f);
                    ActivateMesh(0);
                }
                else if (screwsGains == 3)
                {
                    ActivateMesh(2);
                    ActivateMesh(1);
                    ActivateMesh(0);
                }
                else if (screwsGains == 2)
                {
                    ActivateMesh(1);
                    ActivateMesh(0);
                }
                else if (screwsGains == 1)
                {
                    ActivateMesh(0);
                }
                break;
            case Enum_Collectibles.CollectibleType.Gun :
                ActivateMesh(1);
                break;
            case Enum_Collectibles.CollectibleType.Jetpack :
                ActivateMesh(2);
                break;
            case Enum_Collectibles.CollectibleType.SpaceshipPieces :
                ActivateMesh(spaceshipIndex);
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
                    other.gameObject.GetComponent<AllPlayerReferences>().HUDref.UpdateScrewsText(other.gameObject.GetComponent<AllPlayerReferences>().invRef.GetScrews());
                    PlaySound(screwsGains < 2 ? 0 : 1);
                    DesactivateMesh(0);
                    DesactivateMesh(1);
                    DesactivateMesh(2);
                    foreach(Collider thisCol in gameObject.GetComponents<Collider>())
                        Destroy(thisCol);
                    Destroy(gameObject.GetComponent<Rigidbody>());
                    Destroy(vfx);
                    Destroy(gameObject, 1.5f);
                    break;
                case Enum_Collectibles.CollectibleType.Sword :
                    other.gameObject.GetComponentInChildren<Sword>().enabled = true;
                    other.gameObject.GetComponent<AllPlayerReferences>().HUDref.swordIcon.enabled = true;
                    PlaySound(2);
                    DesactivateMesh(0);
                    foreach(Collider thisCol in gameObject.GetComponents<Collider>())
                        Destroy(thisCol);
                    Destroy(vfx);
                    Destroy(gameObject, 1.5f);
                    break;
                case Enum_Collectibles.CollectibleType.Gun :
                    other.gameObject.GetComponent<Gun>().enabled = true;
                    other.gameObject.GetComponent<AllPlayerReferences>().HUDref.gunIcon.enabled = true;
                    PlaySound(3);
                    DesactivateMesh(1);
                    foreach(Collider thisCol in gameObject.GetComponents<Collider>())
                        Destroy(thisCol);
                    Destroy(gameObject.GetComponent<Rigidbody>());
                    Destroy(vfx);
                    Destroy(gameObject, 1.5f);
                    break;
                case Enum_Collectibles.CollectibleType.Jetpack :
                    other.gameObject.GetComponentInChildren<Jump>().enabled = true;
                    other.gameObject.GetComponent<AllPlayerReferences>().HUDref.jetpackIcon.enabled = true;
                    PlaySound(4);
                    DesactivateMesh(2);
                    foreach(Collider thisCol in gameObject.GetComponents<Collider>())
                        Destroy(thisCol);
                    Destroy(gameObject.GetComponent<Rigidbody>());
                    Destroy(vfx);
                    Destroy(gameObject, 1.5f);
                    break;
                case Enum_Collectibles.CollectibleType.SpaceshipPieces :
                    other.gameObject.GetComponent<Inventory>().spaceshipPieces++;
                    Pause.Instance.SetText();
                    PlaySound(5);
                    DesactivateMesh(spaceshipIndex);
                    foreach(Collider thisCol in gameObject.GetComponents<Collider>())
                        Destroy(thisCol);
                    Destroy(gameObject.GetComponent<Rigidbody>());
                    Destroy(vfx);
                    Destroy(gameObject, 1.5f);
                    break;
            }
        }
    }

    private void ActivateMesh(int index)
    {
        if (type != Enum_Collectibles.CollectibleType.SpaceshipPieces)
            meshes[index].gameObject.SetActive(true);
        else
            spaceshipMeshes[index].gameObject.SetActive(true);
    }

    private void DesactivateMesh(int index)
    {
        if (type != Enum_Collectibles.CollectibleType.SpaceshipPieces)
            meshes[index].gameObject.SetActive(false);
        else
            spaceshipMeshes[index].gameObject.SetActive(false);
    }

    private void PlaySound(int index)
    {
        _audioSource.resource = audios[index];
        _audioSource.Play();
    }
}
