using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Hareket Ayarları")]
    public float walkSpeed = 3.0f;
    public float runSpeed = 6.0f;
    public float jumpHeight = 1.5f;
    public float gravity = -20f;

    [Header("Fare (Mouse) Ayarları")]
    public float mouseSensitivity = 2f;

    private CharacterController controller;
    private Animator animator;
    private Vector3 velocity;

    // KİLİT MEKANİZMALARI: Karakter öldüyse veya kazandıysa hareketleri durdurmak için
    private bool isDead = false;
    private bool isVictory = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        if (animator != null) animator.applyRootMotion = false;

        // Fare imlecini ekranın ortasına kilitle ve gizle
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // KONTROL: Eğer oyuncu öldüyse veya kazandıysa HİÇBİR HAREKET VEYA GİRDİYİ OKUMA!
        if (isDead || isVictory)
        {
            // Sadece yerçekimi etkilemeye devam etsin ki havada kalmasın
            if (!controller.isGrounded)
            {
                velocity.y += gravity * Time.deltaTime;
                controller.Move(new Vector3(0, velocity.y, 0) * Time.deltaTime);
            }
            return; // Update fonksiyonunu burada kes, aşağıya geçme.
        }

        // 1. FARE İLE YÖN DÖNÜŞÜ
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        transform.Rotate(0, mouseX, 0);

        // 2. YATAY HAREKET (Yürüme/Koşma)
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 moveDir = (transform.right * h + transform.forward * v).normalized;

        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float currentSpeed = isRunning ? runSpeed : walkSpeed;

        Vector3 horizontalMove = moveDir * currentSpeed;

        // DÜZELTME: Animator paneline uygun olarak "Speed" yapıldı
        float speedParam = (moveDir.magnitude > 0.1f) ? (isRunning ? 1.0f : 0.5f) : 0f;
        if (animator != null) animator.SetFloat("Speed", speedParam);

        // 3. ZEMİN KONTROLÜ VE ZIPLAMA
        if (controller.isGrounded)
        {
            if (velocity.y < 0)
            {
                velocity.y = -2f;
            }

            if (Input.GetButtonDown("Jump"))
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                if (animator != null) animator.SetTrigger("Jump");
            }
        }

        // 4. YERÇEKİMİ UYGULAMASI
        velocity.y += gravity * Time.deltaTime;

        // 5. KARAKTERİ HAREKET ETTİRME
        Vector3 finalMove = horizontalMove + new Vector3(0, velocity.y, 0);
        controller.Move(finalMove * Time.deltaTime);

        // 6. ANIMATOR İÇİN YERDE Mİ BİLGİSİNİ GÖNDER (Görseldeki isGrounded parametresine göre)
        if (animator != null) animator.SetBool("isGrounded", controller.isGrounded);

        // 7. EŞYA TOPLAMA (Görseldeki Collect parametresine göre)
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (animator != null) animator.SetTrigger("Collect");
        }
    }

    // DISARIDAN ÇAĞRILACAK FONKSİYON: Canavar bizi yakaladığında bunu tetikleyecek
    public void TriggerDeath()
    {
        if (isDead) return;

        isDead = true;

        if (animator != null)
        {
            animator.SetFloat("Speed", 0f); // DÜZELTME: 'Speed' yapıldı
            animator.SetBool("Die", true);   // DÜZELTME: 'isDead' yerine 'Die' yapıldı
        }
    }

    // DISARIDAN ÇAĞRILACAK FONKSİYON: Kapıdan başarıyla geçtiğimizde bunu tetikleyecek
    public void TriggerVictory()
    {
        if (isVictory) return;

        isVictory = true;

        if (animator != null)
        {
            animator.SetFloat("Speed", 0f); // DÜZELTME: 'Speed' yapıldı
            animator.SetBool("Win", true);   // DÜZELTME: 'victory' yerine 'Win' yapıldı
        }
    }
}