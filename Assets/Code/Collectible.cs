using UnityEngine;

public class Collectible : MonoBehaviour
{
    [Header("Skor Ayarý")]
    public int scoreValue = 10;

    [Header("Ses Ayarý")]
    public AudioClip collectSound; // YENÝ: Freesound'dan indirdiðin toplama sesini buraya atacaksýn

    private bool isPlayerNearby = false; // Oyuncu çiçeðin yanýnda mý?

    private void Update()
    {
        // Sadece oyuncu yanýmýzdaysa VE E tuþuna bastýysa topla
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            Collect();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Alana giren obje Player ise sadece "yanýnda" olduðunu onayla ve [E] yazýsýný aç
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;

            GameManager gm = FindObjectOfType<GameManager>();
            if (gm != null)
            {
                gm.ShowInteraction(true); // E yazýsýný ekranda göster
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Oyuncu alandan uzaklaþýrsa etkileþimi kapat ve [E] yazýsýný gizle
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;

            GameManager gm = FindObjectOfType<GameManager>();
            if (gm != null)
            {
                gm.ShowInteraction(false); // E yazýsýný gizle
            }
        }
    }

    // Toplama iþlemini gerçekleþtiren asýl fonksiyon
    private void Collect()
    {
        // YENÝ: Çiçek yok olmadan hemen önce kameranýn pozisyonunda sesi tek seferlik patlat!
        if (collectSound != null)
        {
            AudioSource.PlayClipAtPoint(collectSound, Camera.main.transform.position, 1.0f);
        }

        GameManager gm = FindObjectOfType<GameManager>();
        if (gm != null)
        {
            gm.AddScore(scoreValue);
            gm.ShowInteraction(false); // Çiçek alýnýnca yazýyý kapat
        }

        Destroy(gameObject); // Çiçeði sahneden sil
    }
}