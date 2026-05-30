using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [Header("Ses Ayarlarý")]
    public AudioClip exitSuccessSound; 

    private bool isExiting = false; 

    private void OnTriggerEnter(Collider other)
    {
        if (isExiting) return;

        if (other.CompareTag("Player"))
        {
            int remainingCollectibles = FindObjectsByType<Collectible>(FindObjectsSortMode.None).Length;

            if (remainingCollectibles <= 1)
            {
                GameManager gameManager = FindObjectOfType<GameManager>();

                if (gameManager != null)
                {
                    isExiting = true; 

                    if (exitSuccessSound != null)
                    {
                        AudioSource.PlayClipAtPoint(exitSuccessSound, Camera.main.transform.position, 1.0f);
                    }

                    PlayerController playerCtrl = other.GetComponent<PlayerController>();
                    if (playerCtrl != null)
                    {
                        playerCtrl.TriggerVictory(); 
                    }

                    GameData.LatestLevelScore = gameManager.score;
                    GameData.NextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

                    Invoke("ChangeScene", 2.0f);
                }
            }
            else
            {
                Debug.Log("Ormandaki tüm çiçekleri toplamadan kaçamazsýn! Kalan çiçek sayýsý: " + (remainingCollectibles - 1));
            }
        }
    }

    void ChangeScene()
    {
        SceneManager.LoadScene("TransitionScene");
    }
}