using UnityEngine;

public class KameraTakip : MonoBehaviour
{
    public Transform hedef;      // Takip edilecek obje (Noel Baba)
    public float mesafe = 3f;    // Oyuncu kameranýn merkezinden ne kadar saðda/solda dursun?
    public float otomatikHiz = 2f; // Kameranýn hiç durmadan saða gitme hýzý

    void LateUpdate()
    {
        if (hedef != null)
        {
            // 1. ADIM: Kamerayý mevcut pozisyonundan otomatik hýz kadar ileri taþýyoruz.
            // Bu, oyuncu dursa bile kameranýn saða gitmesini saðlar.
            float otomatikX = transform.position.x + (otomatikHiz * Time.deltaTime);

            // 2. ADIM: Oyuncunun olmasý gereken kamera pozisyonunu hesaplýyoruz.
            float oyuncuOdakX = hedef.position.x + mesafe;

            // 3. ADIM: KARAR ANI (En Önemli Kýsým)
            // Mathf.Max fonksiyonu içine girilen iki sayýdan BÜYÜK olaný seçer.
            // Eðer 'otomatikX' büyükse kamera kendi hýzýyla gider.
            // Eðer 'oyuncuOdakX' büyükse (oyuncu öne fýrladýysa), kamera oyuncuya yetiþir.
            // Bu sayede kamera asla geriye (küçük deðere) gitmez.
            float sonX = Mathf.Max(otomatikX, oyuncuOdakX);

            // Kamerayý yeni pozisyona taþý
            transform.position = new Vector3(sonX, transform.position.y, -10);
        }
    }
}