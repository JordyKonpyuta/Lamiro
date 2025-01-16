using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    private static GameObject[] _persistantObjects = new GameObject[3];
    public int objectIndex;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (_persistantObjects[objectIndex] == null)
        {
            _persistantObjects[objectIndex] = gameObject;
            DontDestroyOnLoad(gameObject);
        }
        else if (gameObject != _persistantObjects[objectIndex])
        {
            Invoke(nameof(TeleportPlayer), 0.1f);
        }
    }

    private void TeleportPlayer()
    {
        if (objectIndex == 0)
        {
            _persistantObjects[objectIndex].transform.position = gameObject.transform.position;
        }

        if (objectIndex == 2)
        {
            _persistantObjects[objectIndex].transform.position = gameObject.GetComponent<CustomCamera>().cameraStartPosition;
            print(_persistantObjects[objectIndex].GetComponent<AllPlayerReferences>().cameraRef.transform.position);
            print(gameObject.GetComponent<AllPlayerReferences>().cameraRef.GetComponent<CustomCamera>().cameraStartPosition);
        }
        Destroy(gameObject);
        
    }
}
