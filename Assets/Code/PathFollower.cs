using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PathFollower : MonoBehaviour
{
    [Header("Takip Ayarlarý")]
    public Transform playerTransform;
    public float monsterSpeed = 3.5f;
    public float recordInterval = 0.2f;

    [Header("Yakalama Ayarlarý")]
    public float killDistance = 1.2f;
    public string gameOverSceneName = "ResultScreen";

    [Header("Ses Ayarlarý")]
    public AudioClip walkSound;
    public AudioClip attackSound;

    private List<Vector3> playerHistory = new List<Vector3>();
    private float recordTimer;
    private Animator enemyAnimator;
    private bool isPlayerDead = false;

    private GameManager gameManager;
    private AudioSource audioSource;

    private Vector3 playerStartPosition;
    private bool hasTeleportedToStart = false; // Iþýnlanmanýn sadece 1 kere çalýþmasý için kilit

    void Start()
    {
        enemyAnimator = GetComponent<Animator>();
        gameManager = FindObjectOfType<GameManager>();
        audioSource = GetComponent<AudioSource>();

        if (audioSource != null && walkSound != null)
        {
            audioSource.clip = walkSound;
            audioSource.loop = true;
            audioSource.playOnAwake = false;
        }

        if (playerTransform == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null) playerTransform = player.transform;
        }

        // Oyun baþladýðý ilk salise oyuncunun bulunduðu konumu hafýzaya alýyoruz
        if (playerTransform != null)
        {
            playerStartPosition = playerTransform.position;
        }

        recordTimer = recordInterval;
    }

    void Update()
    {
        if (playerTransform == null || isPlayerDead) return;

        // Eðer GameManager henüz canavarýn yürümesine izin vermediyse dur!
        if (gameManager != null && !gameManager.shouldMonsterFollow)
        {
            if (enemyAnimator != null)
            {
                enemyAnimator.SetFloat("speed", 0f);
                enemyAnimator.SetBool("isPlayerDetected", false);
            }
            if (audioSource != null && audioSource.isPlaying) audioSource.Stop();
            return;
        }

        // --- BURADAN AÞAÐISI CANAVAR UYANDIÐI ANDA ÇALIÞIR ---

        // Canavar uyandý ve henüz baþlangýç noktasýna ýþýnlanmadýysa onu oraya ýþýnla!
        if (!hasTeleportedToStart)
        {
            transform.position = playerStartPosition; // Canavarý oyuncunun ilk doðduðu yere taþý
            hasTeleportedToStart = true;             // Kilidi kapat ki sürekli ýþýnlanmasýn
            Debug.Log("Gorgon oyuncunun baþlangýç noktasýnda belirdi!");
        }

        // Karakterin güzergahýný kaydetme
        recordTimer -= Time.deltaTime;
        if (recordTimer <= 0)
        {
            if (playerHistory.Count == 0 || Vector3.Distance(playerTransform.position, playerHistory[playerHistory.Count - 1]) > 0.1f)
            {
                playerHistory.Add(playerTransform.position);
            }
            recordTimer = recordInterval;
        }

        MoveAlongPath();
        CheckPlayerCaught();
    }

    void MoveAlongPath()
    {
        if (playerHistory.Count > 0)
        {
            Vector3 targetPosition = playerHistory[0];
            targetPosition.y = transform.position.y;

            transform.position = Vector3.MoveTowards(transform.position, targetPosition, monsterSpeed * Time.deltaTime);

            if (enemyAnimator != null)
            {
                enemyAnimator.SetFloat("speed", monsterSpeed);
                enemyAnimator.SetBool("isPlayerDetected", true);
            }

            if (audioSource != null && !audioSource.isPlaying && walkSound != null)
            {
                audioSource.Play();
            }

            Vector3 direction = targetPosition - transform.position;
            if (direction != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * 10f);
            }

            if (Vector3.Distance(transform.position, targetPosition) < 0.2f)
            {
                playerHistory.RemoveAt(0);
            }
        }
        else
        {
            if (enemyAnimator != null) enemyAnimator.SetFloat("speed", 0f);
            if (audioSource != null && audioSource.isPlaying) audioSource.Stop();
        }
    }

    void CheckPlayerCaught()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
        if (distanceToPlayer <= killDistance)
        {
            ExecuteDeath();
        }
    }

    // YENÝ ENTEGRASYONLU FONKSÝYON
    void ExecuteDeath()
    {
        isPlayerDead = true;
        monsterSpeed = 0f;

        if (audioSource != null) audioSource.Stop();

        if (attackSound != null)
        {
            AudioSource.PlayClipAtPoint(attackSound, Camera.main.transform.position, 1.0f);
        }

        // Canavarýn kendi saldýrma animasyonunu tetikle
        if (enemyAnimator != null)
        {
            enemyAnimator.SetFloat("speed", 0f);
            enemyAnimator.SetBool("isAttacking", true);
        }

        // YENÝ: Oyuncunun PlayerController scriptine ulaþýp ölümü tetikliyoruz
        if (playerTransform != null)
        {
            PlayerController playerCtrl = playerTransform.GetComponent<PlayerController>();
            if (playerCtrl != null)
            {
                playerCtrl.TriggerDeath(); // Oyuncunun girdilerini kapatýr ve "isDead" animasyonunu açar
            }
        }

        Invoke("LoadGameOverScene", 1.5f);
    }

    void LoadGameOverScene()
    {
        SceneManager.LoadScene(gameOverSceneName);
    }
}