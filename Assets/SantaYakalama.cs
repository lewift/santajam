using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SantaYakalama : MonoBehaviour
{
    // DEÐÝÞÝKLÝK 1: Buraya direkt scriptin gerçek adýný yazdýk.
    // "MonoBehaviour" yerine "SantaMovement" yazdýk ki Unity ne aradýðýný tam bilsin.
    public SantaMovement hareketScripti;

    // DEÐÝÞÝKLÝK 2: Start fonksiyonu eklendi.
    void Start()
    {
        // Bu komut, oyun baþlar baþlamaz Noel Baba'nýn üzerindeki
        // "SantaMovement" scriptini otomatik bulur ve yukarýdaki kutuya baðlar.
        hareketScripti = GetComponent<SantaMovement>();
    }

    // Çarpýþma olduðunda çalýþýr
    void OnCollisionEnter2D(Collision2D carpanSey)
    {
        // Eðer çarpan þeyin etiketi "Polis" ise
        if (carpanSey.gameObject.CompareTag("Polis"))
        {
            // Zamanlayýcýyý baþlat
            StartCoroutine(Dondur());
        }
    }

    // 5 Saniye bekletme iþlemi
    IEnumerator Dondur()
    {
        Debug.Log("YAKALANDIN! - Hareket Kesiliyor...");

        // 1. Hareketi Kapat
        if (hareketScripti != null)
        {
            hareketScripti.enabled = false;
        }

        // 2. 5 Saniye Bekle
        yield return new WaitForSeconds(5f);

        // 3. Hareketi Geri Aç
        if (hareketScripti != null)
        {
            hareketScripti.enabled = true;
        }

        Debug.Log("KURTULDUN! - Hareket Baþlýyor...");
    }
}