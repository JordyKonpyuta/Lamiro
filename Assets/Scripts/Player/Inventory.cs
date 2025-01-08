using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    [SerializeField] private int screws;
    
    // Singleton to be accessible anywhere
    
    private static Player _instance;

    public static Player Instance
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
    }
    
    private bool[] _spaceshipPieces = { false, false, false, false, false };
    
    // Getters
    public int GetScrews()
    {
        return screws;
    }
    
    
    // Setters
    public void AddScrews()
    {
        screws++;
    }

    public void ObtainSpaceshipPiece(int index)
    {
        _spaceshipPieces[index] = true;
        VerifySpaceshipPiecesObtained();
    }
    
    // Verify if all Spaceship Pieces have been obtained
    public void VerifySpaceshipPiecesObtained()
    {
        for (int i = 0; i < _spaceshipPieces.Length; i++)
        {
            if (_spaceshipPieces[i] == true)
            {
                if (i == _spaceshipPieces.Length)
                {
                    print("Congratulations !");
                }
            }

            else break;
        }
    }
}
