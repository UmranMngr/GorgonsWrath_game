using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour
{
    public TextMeshProUGUI levelScoreText;
    public TextMeshProUGUI totalScoreText;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None; Cursor.visible = true;
        levelScoreText.text = "Bölüm Skoru: " + GameData.LatestLevelScore;
        totalScoreText.text = "Toplam Skor: " + GameData.TotalScore;
    }

    public void NextLevelButton()
    {
        if (GameData.NextSceneIndex <= 4) // Level 3'ün indexi 4'tür
            SceneManager.LoadScene(GameData.NextSceneIndex);
        else
            SceneManager.LoadScene("ResultScreen"); // Bütün oyun bittiyse
    }
}