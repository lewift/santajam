using UnityEngine;

public class SantaMovement : MonoBehaviour
{
    [Header("Ayarlar")]
    public float yurumeHizi = 5f;
    public float ziplamaGucu = 7f;

    // Zıplama tuşuna bastığımızda bu isteği ne kadar süre hafızada tutalım? (0.2 saniye idealdir)
    public float ziplamaToleransSuresi = 0.2f;

    private Rigidbody2D rb;
    private Animator anim;
    private bool yerdeMi = false;
    private float ziplamaZamanlayicisi; // Tuşa ne zaman bastığımızı tutar

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        HareketEt();
        GirdiKontrol(); // Tuşlara basılmayı dinle
        ZiplamaMantigi(); // Fiziksel zıplamayı gerçekleştir
        YonuCevir();
    }

    void HareketEt()
    {
        float yatayGiris = Input.GetAxis("Horizontal");
        anim.SetFloat("Hiz",Mathf.Abs(yatayGiris));
        rb.linearVelocity = new Vector2(yatayGiris * yurumeHizi, rb.linearVelocity.y);
    }

    void GirdiKontrol()
    {
        // Space veya W tuşuna basıldığında zamanlayıcıyı kur
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
        {
            ziplamaZamanlayicisi = ziplamaToleransSuresi;
        }
        else
        {
            // Basılmadığı sürece süreyi azalt
            ziplamaZamanlayicisi -= Time.deltaTime;
        }
    }

    void ZiplamaMantigi()
    {
        // Eğer zıplama isteği hala taze ise (süresi bitmediyse) VE karakter yerdeyse
        if (ziplamaZamanlayicisi > 0 && yerdeMi)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, ziplamaGucu);

            yerdeMi = false;
            ziplamaZamanlayicisi = 0; // İsteği kullandık, sıfırla
        }
    }

    void YonuCevir()
    {
        float yatayGiris = Input.GetAxis("Horizontal");

        // Karakterin o anki boyutunun mutlak değerini (pozitif halini) alıyoruz.
        // Böylece karakterin boyu 5 ise '5' değerini aklında tutar.
        float mevcutBoyutX = Mathf.Abs(transform.localScale.x);
        float mevcutBoyutY = transform.localScale.y;
        float mevcutBoyutZ = transform.localScale.z;

        if (yatayGiris > 0)
        {
            // Sağa giderken X pozitif (Örn: 5, 5, 1)
            transform.localScale = new Vector3(mevcutBoyutX, mevcutBoyutY, mevcutBoyutZ);
        }
        else if (yatayGiris < 0)
        {
            // Sola giderken X negatif (Örn: -5, 5, 1)
            transform.localScale = new Vector3(-mevcutBoyutX, mevcutBoyutY, mevcutBoyutZ);
        }
    }

    // OnCollisionStay, Enter'a göre daha garantidir. Yerde durduğu sürece 'true' kalır.
    private void OnCollisionStay2D(Collision2D collision)
    {
        yerdeMi = true;
    }

    // Yerden ayrıldığı anda (zıplayınca veya düşünce) 'false' yap
    private void OnCollisionExit2D(Collision2D collision)
    {
        yerdeMi = false;
    }
}