using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode()]
public class ProgressBar : MonoBehaviour
{
    
    // Singleton to be accessible everywhere
    private static ProgressBar _instance;

    public static ProgressBar Instance
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
    
    #if UNITY_EDITOR
    [MenuItem("GameObject/UI/Progress Bar")]
    public static void AddProgressBar()
    {
        GameObject obj = Instantiate(Resources.Load<GameObject>("Prefabs/Progress Bar"));
        obj.transform.SetParent(Selection.activeGameObject.transform, false);
    }
    #endif
    
    public int minimum = 0;
    public int maximum = 100;
    public int current = 100;
    public Image mask;
    public Image fill;
    public Color color;
    

    // Bind Fill Bar
    public void SetBarPercentage(int c, int max)
    {
        current = c;
        maximum = max;
        float currentOffset = current - minimum;
        float maximumOffset = maximum - minimum;
        float fillAmount = currentOffset / maximumOffset;
        mask.fillAmount = fillAmount;

        fill.color = color;
    }
}
