using UnityEngine;

public class PolisAI : MonoBehaviour
{
    [Header("Ayarlar")]
    public Transform oyuncuTarget;
    public float polisHizi = 7f;
    public float yakalamaMesafesi = 0.5f;

    [Header("Zaman Ayarlarý")]
    public float kovalamaSuresi = 7f;
    public float terkEtmeSuresi = 7f;

    // Private Deðiþkenler
    private float varsayilanHiz;
    private float guncelHiz;
    private float yavaslamaSayaci = 0f;
    private float toplamGecenSure = 0f;
    private int sonYon = 0;

    // YENÝ: Resmi çevirmek için bu bileþeni kullanacaðýz
    private SpriteRenderer spRenderer;

    void Start()
    {
        varsayilanHiz = polisHizi;
        guncelHiz = polisHizi;

        // Karakterin üzerindeki SpriteRenderer'ý otomatik bul
        spRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (oyuncuTarget == null) return;

        toplamGecenSure += Time.deltaTime;

        // --- DURUM 1: KOVALAMA ---
        if (toplamGecenSure < kovalamaSuresi)
        {
            KovalamaModu();
        }
        // --- DURUM 2: TERK ETME ---
        else if (toplamGecenSure < (kovalamaSuresi + terkEtmeSuresi))
        {
            TerkEtmeModu();
        }
        // --- DURUM 3: YOK OLMA ---
        else
        {
            Destroy(gameObject);
        }
        //transform.localScale = new Vector3(5f, 5f, 1f);
    }

    void KovalamaModu()
    {
        float yonFarki = oyuncuTarget.position.x - transform.position.x;
        int gitmesiGerekenYon = (yonFarki > 0) ? 1 : -1;

        if (sonYon != 0 && gitmesiGerekenYon != sonYon)
        {
            yavaslamaSayaci = 0.5f;
        }
        sonYon = gitmesiGerekenYon;

        if (yavaslamaSayaci > 0)
        {
            yavaslamaSayaci -= Time.deltaTime;
            guncelHiz = varsayilanHiz * 0.5f;
        }
        else
        {
            guncelHiz = varsayilanHiz;
        }

        float mesafe = Vector2.Distance(transform.position, oyuncuTarget.position);
        if (mesafe > yakalamaMesafesi)
        {
            Vector3 hedef = new Vector3(oyuncuTarget.position.x, transform.position.y, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, hedef, guncelHiz * Time.deltaTime);
        }

        YuzunuDon(gitmesiGerekenYon);
    }

    void TerkEtmeModu()
    {
        transform.position += Vector3.left * varsayilanHiz * Time.deltaTime;
        YuzunuDon(-1); // Sola dön
    }

    void YuzunuDon(int yon)
    {
        if (spRenderer != null)
        {
            // Eðer karakterin normal çizimi SAÐA bakýyorsa bu ayar doðrudur.
            // Eðer karakterin SOLA bakýyorsa aþaðýdaki true/false yerlerini deðiþtirmen gerekir.
            if (yon == 1)
            {
                spRenderer.flipX = false; // Saða giderken düz dur
            }
            else
            {
                spRenderer.flipX = true;  // Sola giderken ters çevir
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("YAKALADIM SENÝ!");
        }
    }
}