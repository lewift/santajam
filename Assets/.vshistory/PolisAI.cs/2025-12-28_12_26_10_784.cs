using UnityEngine;

public class PolisAI : MonoBehaviour
{
    [Header("Ayarlar")]
    public Transform oyuncuTarget; // Bizim ana karakterimiz
    public float polisHizi = 5f;   // Polisin ne kadar hýzlý koþacaðý
    public float yakalamaMesafesi = 0.5f; // Ne kadar yaklaþýnca yakalamýþ saysýn

    void Update()
    {
        // Eðer oyuncu yoksa (öldüyse vs.) hata vermesin diye kontrol
        if (oyuncuTarget == null) return;

        // 1. MESAFE HESABI: Oyuncu ile polis arasýndaki mesafe
        float mesafe = Vector2.Distance(transform.position, oyuncuTarget.position);

        // 2. HAREKET: Eðer çok dibinde deðilse kovalamaya devam et
        if (mesafe > yakalamaMesafesi)
        {
            // MoveTowards: A noktasýndan B noktasýna belirli bir hýzda gitmeyi saðlar
            transform.position = Vector2.MoveTowards(transform.position,
                                                     new Vector2(oyuncuTarget.position.x, transform.position.y),
                                                     polisHizi * Time.deltaTime);
        }

        // 3. YÖN DÜZELTME (Sprite'ýn saða/sola dönmesi için)
        // Eðer oyuncu polisin saðýndaysa polis saða döner, solundaysa sola.
        if (oyuncuTarget.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(1, 1, 1); // Saða bak
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1); // Sola bak
        }
    }

    // Çarpýþma olduðunda ne olacaðýný buraya yazabilirsin
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("YAKALADIM SENÝ!");
            // Buraya "Oyun Bitti" kodlarýný çaðýrabilirsin.
        }
    }
}