using UnityEngine;

public class PolisAI : MonoBehaviour
{
    [Header("Ayarlar")]
    public Transform oyuncuTarget; // Bizim ana karakterimiz
    public float polisHizi = 5f;   // Polisin normal hýzý
    public float yakalamaMesafesi = 0.5f; // Temas mesafesi

    [Header("Zaman Ayarlarý")]
    public float kovalamaSuresi = 7f; // Kaç saniye kovalasýn (7 sn)
    public float terkEtmeSuresi = 7f; // Kaç saniye sola koþup gitsin (7 sn)

    // Private (Gizli) Deðiþkenler
    private float varsayilanHiz;
    private float guncelHiz;
    private float yavaslamaSayaci = 0f; // Yön deðiþtirince sayacak sayaç
    private float toplamGecenSure = 0f; // Polisin hayatta olduðu süre
    private int sonYon = 0; // 1: Sað, -1: Sol, 0: Baþlangýç

    void Start()
    {
        // Baþlangýç hýzýný hafýzaya alalým
        varsayilanHiz = polisHizi;
        guncelHiz = polisHizi;
    }

    void Update()
    {
        // 1. OYUNCU KONTROLÜ
        if (oyuncuTarget == null) return;

        // Süreyi her saniye artýr
        toplamGecenSure += Time.deltaTime;

        // --- DURUM 1: KOVALAMA MODU (Ýlk 7 Saniye) ---
        if (toplamGecenSure < kovalamaSuresi)
        {
            KovalamaModu();
        }
        // --- DURUM 2: TERK ETME MODU (Sonraki 7 Saniye) ---
        else if (toplamGecenSure < (kovalamaSuresi + terkEtmeSuresi))
        {
            TerkEtmeModu();
        }
        // --- DURUM 3: YOK OLMA ---
        else
        {
            Destroy(gameObject); // Polisi sahneden sil
        }
    }

    void KovalamaModu()
    {
        // Oyuncunun nerede olduðunu hesapla (Saðýmýzda mý solumuzda mý?)
        float yonFarki = oyuncuTarget.position.x - transform.position.x;
        int gitmesiGerekenYon = (yonFarki > 0) ? 1 : -1;

        // --- YÖN DEÐÝÞTÝRME CEZASI ---
        // Eðer ilk kez deðilse (0) VE gitmesi gereken yön, son yönden farklýysa:
        if (sonYon != 0 && gitmesiGerekenYon != sonYon)
        {
            yavaslamaSayaci = 3f; // 3 saniye ceza süresi baþlat
        }
        sonYon = gitmesiGerekenYon; // Son yönü güncelle

        // Eðer ceza süresi bitmediyse hýzý yarýya düþür
        if (yavaslamaSayaci > 0)
        {
            yavaslamaSayaci -= Time.deltaTime; // Sayacý düþür
            guncelHiz = varsayilanHiz * 0.5f;  // Hýz %50 olsun
        }
        else
        {
            guncelHiz = varsayilanHiz; // Süre bittiyse normal hýza dön
        }

        // --- HAREKET ---
        float mesafe = Vector2.Distance(transform.position, oyuncuTarget.position);
        if (mesafe > yakalamaMesafesi)
        {
            // Sadece X ekseninde takip etsin (Y sabit kalsýn)
            Vector2 hedefPozisyon = new Vector2(oyuncuTarget.position.x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, hedefPozisyon, guncelHiz * Time.deltaTime);
        }

        // --- GÖRSEL DÖNDÜRME ---
        YuzunuDon(gitmesiGerekenYon);
    }

    void TerkEtmeModu()
    {
        // Artýk oyuncuya bakmýyoruz, sürekli SOLA (-1) gidiyoruz
        // Sola doðru sabit hýzla git (MoveTowards yerine Translate kullanabiliriz veya vektör hesabý)
        transform.Translate(Vector2.left * varsayilanHiz * Time.deltaTime);

        // Yüzünü sola dön
        YuzunuDon(-1);
    }

    void YuzunuDon(int yon)
    {
        // 1 ise Saða (Scale X pozitif), -1 ise Sola (Scale X negatif)
        if (yon == 1)
            transform.localScale = new Vector3(1, 1, 1);
        else
            transform.localScale = new Vector3(-1, 1, 1);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("YAKALADIM SENÝ!");
            // Oyun Bitti kodlarý buraya
        }
    }
}