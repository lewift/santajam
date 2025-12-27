using UnityEngine;

public class Hediye : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 1. Kontrol: Çarpan þeyin üzerinde "SantaMovement" kodu var mý? (Yani Noel Baba mý?)
        if (other.GetComponent<SantaMovement>() != null)
        {
            // 2. Adým: Sahnede "SkorYoneticisi" scriptini ara ve bul
            SkorYoneticisi yonetici = FindAnyObjectByType<SkorYoneticisi>();

            // Eðer yöneticiyi bulduysa puan ekle
            if (yonetici != null)
            {
                yonetici.HediyeToplandi();
                Debug.Log("Hediye toplandý, puan eklendi.");
            }
            else
            {
                // Eðer bulamazsa konsola hata bas (Sorunu anlamamýz için)
                Debug.LogError("HATA: Sahnede SkorYoneticisi bulunamadý! GameManager'a scripti ekledin mi?");
            }

            // 3. Adým: Hediye kutusunu sahneden sil
            Destroy(gameObject);
        }
    }
}