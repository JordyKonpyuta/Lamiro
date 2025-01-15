using System;
using UnityEditorInternal;
using UnityEngine;

public class SpaceshipPopUp : MonoBehaviour
{
    private static SpaceshipPopUp _instance;

    public static SpaceshipPopUp Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("Spaceship Popup is NULL !");
            }

            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    public void PopUp()
    {
        GetComponent<Canvas>().enabled = true;
        GetComponent<Animator>().SetBool("IsOpen", true);
    }
}
