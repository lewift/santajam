using UnityEngine;

public class YolYapici : MonoBehaviour
{

    [Header("Ev Ayarlarý")]
    public GameObject evPrefab;
    public float evAraligi = 25f; // Evler arasý mesafe
    public float evYukseklik = 0f;
    private float sonEvKonumu = 0f;

    [Header("Zemin Ayarlarý")]
    public GameObject zeminPrefab;
    public float zeminGenisligi = 20f; // Zeminin Scale X deðeriyle ayný olmalý!
    private float sonZeminKonumu = -20f; // Baþlangýçta geriden baþlasýn

    [Header("Genel")]
    public Transform karakter;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        for (int i = 0; i < 5; i++)
        {
            EvUret();
        }

        for (int i = 0; i < 5; i++)
        {
            ZeminUret();
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (karakter.position.x > sonEvKonumu - 20f)
        {
            EvUret();
        }

        // --- ZEMÝN KONTROLÜ ---
        // Karakter son zemine yaklaþtýysa yeni zemin ekle
        if (karakter.position.x > sonZeminKonumu - 30f)
        {
            ZeminUret();
        }


    }
    void EvUret()
    {
        // Yeni evin pozisyonunu belirle (X ekseninde ileriye koyuyoruz)
        Vector3 yeniPozisyon = new Vector3(sonEvKonumu + evAraligi, evYukseklik, 0);

        // Evi oluþtur (Instantiate = Kopyala ve Sahneye Koy)
        Instantiate(evPrefab, yeniPozisyon, Quaternion.identity);

        // Son ev konumunu güncelle
        sonEvKonumu += evAraligi;

    }

    void ZeminUret()
    {
        // Zemini tam bir öncekinin ucuna ekliyoruz (Boþluk olmasýn diye)
        // Zemin Y konumu genelde -3 civarýdýr, senin projene göre ayarla:
        Vector3 zeminPozisyon = new Vector3(sonZeminKonumu + zeminGenisligi, -3f, 0);

        Instantiate(zeminPrefab, zeminPozisyon, Quaternion.identity);

        // Bir sonraki zemin için konumu güncelle
        sonZeminKonumu += zeminGenisligi;
    }
}
