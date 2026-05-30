using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    void Start()
    {
        // Menü ekranýnda fare imlecinin görünür ve serbest olmasýný saðlýyoruz
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // "START GAME" butonuna baðlanacak fonksiyon
    public void StartGame()
    {
        // Oyunu en baþtaki hikaye ekranýndan (IntroScene) baþlatýr
        SceneManager.LoadScene("IntroScene");
    }

    // "QUIT GAME" butonuna baðlanacak fonksiyon
    public void QuitGame()
    {
        Debug.Log("Exiting game...");
        Application.Quit(); // Oyun build alýnýp bilgisayarda çalýþtýrýldýðýnda oyunu kapatýr
    }
}