using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode()]
public class ProgressBar : MonoBehaviour
{
    #if UNITY_EDITOR
    [MenuItem("GameObject/UI/Progress Bar")]
    public static void AddProgressBar()
    {
        GameObject obj = Instantiate(Resources.Load<GameObject>("Prefabs/Progress Bar"));
        obj.transform.SetParent(Selection.activeGameObject.transform, false);
    }
    #endif
    
    public int minimum;
    public int maximum;
    public int current;
    public Image mask;
    public Image fill;
    public Color color;
    

    // Update is called once per frame
    void Update()
    {
        SetBarPercentage();
    }

    void SetBarPercentage()
    {
        float currentOffset = current - minimum;
        float maximumOffset = maximum - minimum;
        float fillAmount = currentOffset / maximumOffset;
        mask.fillAmount = fillAmount;

        fill.color = color;
    }
}
