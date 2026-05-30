using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections; // Coroutine (Zamanlayýcý) kullanmak için gerekli

public class GameManager : MonoBehaviour
{
    [Header("Zaman Ayarlarý")]
    public float timeLimit = 60f;

    [Header("Skor ve Çiçek Ayarlarý")]
    public int score = 0;
    private int totalFlowers = 0;
    private int collectedFlowers = 0;

    [Header("UI Elementleri (TextMeshPro)")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI flowerCounterText;

    [Header("Gorgon Uyarý Ayarý")]
    public TextMeshProUGUI gorgonAlertText; // Ekranýn ortasýndaki büyük uyarý yazýsý
    public float alertDisplayTime = 3f;     // Yazýnýn ekranda kaç saniye kalacaðý (Örn: 3 saniye)

    [Header("Çýkýþ Kapýsý Ayarý")]
    public GameObject exitObject;

    [Header("Etkileþim Ayarý")]
    public GameObject interactionPrompt;

    [Header("Bölüm Sonu Ses Ayarý")]
    public AudioClip allFlowersCollectedSound;

    [HideInInspector] public bool shouldMonsterFollow = false;

    private int lastDisplayedTime = -1;

    void Start()
    {
        // KESÝN ÇÖZÜM: Yeni sahne (Level 2 veya 3) açýldýðý an her þeyi tamamen sýfýrlýyoruz
        shouldMonsterFollow = false; 
        StopAllCoroutines(); // Eðer arka planda çalýþan eski bir zamanlayýcý kaldýysa hepsini patlat

        totalFlowers = FindObjectsByType<Collectible>(FindObjectsSortMode.None).Length;

        if (exitObject != null)
        {
            exitObject.SetActive(false);
        }

        // Oyun baþýnda uyarý yazýsýnýn kapalý olduðundan kesin olarak emin oluyoruz
        if (gorgonAlertText != null)
        {
            gorgonAlertText.gameObject.SetActive(false);
        }

        ShowInteraction(false);
        UpdateScoreUI();
        UpdateFlowerUI();
    }

    void Update()
    {
        timeLimit -= Time.deltaTime;

        if (timeLimit <= 0)
        {
            timeLimit = 0;
            UpdateTimerUI();
            SceneManager.LoadScene("ResultScreen");
            return;
        }

        int currentTimeToInt = Mathf.CeilToInt(timeLimit);
        if (currentTimeToInt != lastDisplayedTime)
        {
            lastDisplayedTime = currentTimeToInt;
            UpdateTimerUI();
        }
    }

    public void AddScore(int amount)
    {
        score += amount;
        GameData.TotalScore += amount;
        collectedFlowers++;

        UpdateScoreUI();
        UpdateFlowerUI();

        int requiredFlowersToWakeMonster = Mathf.CeilToInt(totalFlowers / 2f);

        if (collectedFlowers >= requiredFlowersToWakeMonster)
        {
            if (!shouldMonsterFollow)
            {
                shouldMonsterFollow = true;
                Debug.Log("Canavar uyandý! Oyuncunun peþine düþüyor!");

                // Ekrandaki büyük "Gorgon Uyandý" yazýsýný tetikleyen fonksiyonu çaðýrýyoruz
                StartCoroutine(ShowGorgonAlert());
            }
        }

        if (collectedFlowers >= totalFlowers)
        {
            OpenExit();
        }
    }

    // Yazýyý açar, belirlediðin süre kadar bekler ve geri kapatýr
    IEnumerator ShowGorgonAlert()
    {
        if (gorgonAlertText != null)
        {
            gorgonAlertText.text = "Gorgon Uyandý!"; // Yazý içeriði
            gorgonAlertText.gameObject.SetActive(true); // Yazýyý görünür yap

            // Belirttiðin süre kadar (Örn: 3 saniye) kodun burada duraklamasýný saðla
            yield return new WaitForSeconds(alertDisplayTime);

            gorgonAlertText.gameObject.SetActive(false); // Süre bitince yazýyý tekrar gizle
        }
    }

    void OpenExit()
    {
        if (exitObject != null)
        {
            exitObject.SetActive(true);
            if (allFlowersCollectedSound != null)
            {
                AudioSource.PlayClipAtPoint(allFlowersCollectedSound, Camera.main.transform.position, 1.0f);
            }
        }
    }

    public void ShowInteraction(bool state)
    {
        if (interactionPrompt != null)
        {
            interactionPrompt.SetActive(state);
        }
    }

    void UpdateScoreUI() { if (scoreText != null) scoreText.text = "Ruh Özü: " + score.ToString(); }
    void UpdateTimerUI() { if (timerText != null) timerText.text = "Süre: " + lastDisplayedTime.ToString(); }
    void UpdateFlowerUI() { if (flowerCounterText != null) flowerCounterText.text = "Çiçek: " + collectedFlowers + " / " + totalFlowers; }
}