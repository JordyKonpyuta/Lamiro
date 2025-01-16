using UnityEngine;

public class CreateHUD : MonoBehaviour
{
    public GameObject HUDref;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject trueHUDRef = Instantiate(HUDref, transform.position, Quaternion.identity);
        gameObject.GetComponent<AllPlayerReferences>().HUDref = trueHUDRef.GetComponent<HUD>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
