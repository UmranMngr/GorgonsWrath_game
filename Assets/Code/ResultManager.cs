using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ResultManager : MonoBehaviour
{
    [Header("UI Elementleri")]
    public TextMeshProUGUI finalScoreText; // Sahnedeki Toplam Ruh Özü textini buraya baðlayacaðýz

    void Start()
    {
        // UI sahnelerinde farenin kilitli kalmamasý ve görünür olmasý için
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Oyuncunun elenene kadar topladýðý toplam skoru hafýzadan (GameData) çekip ekrana yazdýrýyoruz
        if (finalScoreText != null)
        {
            finalScoreText.text = "Total Soul Essence: " + GameData.TotalScore.ToString();
        }
    }

    // "TEKRAR DENE" Butonuna baðlanacak fonksiyon
    public void RetryButton()
    {
        // Yeni bir oyuna baþlayacaðý için birikmiþ tüm skorlarý sýfýrlýyoruz
        GameData.TotalScore = 0;
        GameData.LatestLevelScore = 0;

        // Oyuncu tekrar sýfýrdan baþlasýn diye sýradaki sahne indeksini Level1'e (Index: 2) kuruyoruz
        GameData.NextSceneIndex = 2;

        // Direkt olarak Level1 sahnesini yeniden yüklüyoruz
        SceneManager.LoadScene("Level1");
    }

    // "ANA MENÜ" Butonuna baðlanacak fonksiyon
    public void BackToMenuButton()
    {
        // Ana menüye dönerken de skorlarý temizliyoruz
        GameData.TotalScore = 0;
        GameData.LatestLevelScore = 0;

        // Ana menü sahnesini yüklüyoruz
        SceneManager.LoadScene("MainMenu");
    }
}