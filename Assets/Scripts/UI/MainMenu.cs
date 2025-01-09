using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject settingsPanel;

    public void SettingsButton()
    {
        if (settingsPanel.activeSelf)
            settingsPanel.SetActive(false);
        else 
            settingsPanel.SetActive(true);
    }
}
