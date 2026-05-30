using UnityEngine;

public class HowToPlayUI : MonoBehaviour
{
    public GameObject howToPlayPanel;

    public void OpenPanel()
    {
        howToPlayPanel.SetActive(true);
    }

    public void ClosePanel()
    {
        howToPlayPanel.SetActive(false);
    }
}