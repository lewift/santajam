using UnityEngine;

public class YolYapici : MonoBehaviour
{
    [Header("Sýnýr Ayarlarý")]
    public int maxEvSayisi = 15;
    private int uretilenEvSayisi = 0;
    private bool casinoUretildiMi = false;

    [Header("Ev ve Casino Ayarlarý")]
    public GameObject[] evPrefablari;
    public GameObject casinoPrefab;

    // --- YENÝ EKLENEN KISIM (DEKORASYON) ---
    [Header("Dekorasyon Ayarlarý")]
    public GameObject copKovasiPrefab; // Buraya çöp kovasý prefabýný sürükle
    public float copKovasiYOffset = -1.9f; // Çöp kovasýnýn yükseklik ayarý (zemine otursun diye)
    // ----------------------------------------

    public float evAraligi = 25f;
    public float evYukseklik = 0f;
    private float sonEvKonumu = 0f;
    private int sradakiEvIndex = 0;

    [Header("Zemin Ayarlarý")]
    public GameObject zeminPrefab;
    public float zeminGenisligi = 20f;
    private float sonZeminKonumu = -20f;

    [Header("Genel")]
    public Transform karakter;

    void Start()
    {
        // Baþlangýçta sadece normal evler üretilsin
        for (int i = 0; i < 5; i++)
        {
            if (uretilenEvSayisi < maxEvSayisi)
            {
                EvUret();
            }
        }

        for (int i = 0; i < 5; i++)
        {
            ZeminUret();
        }
    }

    void Update()
    {
        // --- EV VE CASINO KONTROLÜ ---
        if (karakter.position.x > sonEvKonumu - 20f)
        {
            // 1. DURUM: Henüz 15 ev olmadýysa, normal ev üretmeye devam et
            if (uretilenEvSayisi < maxEvSayisi)
            {
                EvUret();
            }
            // 2. DURUM: 15 ev bittiyse VE henüz Casino üretilmediyse
            else if (!casinoUretildiMi)
            {
                CasinoUret();
            }
        }

        // --- ZEMÝN KONTROLÜ ---
        if (karakter.position.x > sonZeminKonumu - 30f)
        {
            ZeminUret();
        }
    }

    void EvUret()
    {
        // 1. Evi oluþtur
        Vector3 yeniPozisyon = new Vector3(sonEvKonumu + evAraligi, evYukseklik, 0);
        Instantiate(evPrefablari[sradakiEvIndex], yeniPozisyon, Quaternion.identity);

        // Deðiþkenleri güncelle
        sonEvKonumu += evAraligi;
        uretilenEvSayisi++;
        sradakiEvIndex = (sradakiEvIndex + 1) % evPrefablari.Length;

        // --- YENÝ EKLENEN KISIM: ÇÖP KOVASI KONTROLÜ ---
        // Eðer üretilen ev sayýsý 4'ün katýysa (4, 8, 12...) ve son ev deðilse
        if (uretilenEvSayisi % 3 == 0 && uretilenEvSayisi < maxEvSayisi)
        {
            // Kovayý þu anki ev ile bir sonraki evin tam ortasýna koyalým
            // Formül: Þu anki evin konumu + (Aralýk / 2) -> Ama þu an 'sonEvKonumu' zaten arttýrýldýðý için
            // Kovayý 'sonEvKonumu'nun yarým aralýk gerisine deðil, þu anki 'sonEvKonumu' ile bir sonraki yerin ortasýna
            // HESAP: Evler arasý boþluða koymak için:
            // sonEvKonumu þu an en son koyduðumuz evden 'evAraligi' kadar ilerideki boþ noktayý deðil, en son koyulan evin konumunu + ev aralýðýný tutuyor.

            // Basit mantýk: Az önce koyduðumuz evin pozisyonu (yeniPozisyon) ile bir sonraki gelecek evin ortasý.
            float kovaX = yeniPozisyon.x + (evAraligi / 2f);

            Vector3 kovaPozisyon = new Vector3(kovaX, copKovasiYOffset, 0);
            Instantiate(copKovasiPrefab, kovaPozisyon, Quaternion.identity);
        }
        // ------------------------------------------------
    }

    void CasinoUret()
    {
        Vector3 casinoPozisyon = new Vector3(sonEvKonumu + evAraligi, evYukseklik, 0);
        Instantiate(casinoPrefab, casinoPozisyon, Quaternion.identity);
        sonEvKonumu += evAraligi;
        casinoUretildiMi = true;
        Debug.Log("Oyun Sonu! Casino Geldi.");
    }

    void ZeminUret()
    {
        Vector3 zeminPozisyon = new Vector3(sonZeminKonumu + zeminGenisligi, -3f, 0);
        Instantiate(zeminPrefab, zeminPozisyon, Quaternion.identity);
        sonZeminKonumu += zeminGenisligi;
    }
}