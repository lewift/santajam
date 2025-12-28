using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SantaYakalama : MonoBehaviour
{
    // Noel Baba'nýn hareket kodunu buraya sürükleyeceðiz
    // "MonoBehaviour" genel bir tanýmdýr, her türlü scripti kabul eder.
    public MonoBehaviour hareketScripti;

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
        hareketScripti.enabled = false;

        // 2. 5 Saniye Bekle
        yield return new WaitForSeconds(5f);

        // 3. Hareketi Geri Aç
        hareketScripti.enabled = true;
        Debug.Log("KURTULDUN! - Hareket Baþlýyor...");
    }
}