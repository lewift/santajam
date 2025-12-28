using UnityEngine;

public class SantaYakalama : MonoBehaviour
{
    // Public yaptýk ama sürüklemene gerek kalmayacak
    public MonoBehaviour hareketScripti;

    void Start()
    {
        // OYUN BAÞLAYINCA OTOMATÝK BUL:
        // Bu kodun baðlý olduðu objedeki "SantaMovement" bileþenini bul ve deðiþkene ata.
        hareketScripti = GetComponent<SantaMovement>();
    }

    void OnCollisionEnter2D(Collision2D carpanSey)
    {
        if (carpanSey.gameObject.CompareTag("Polis"))
        {
            StartCoroutine(Dondur());
        }
    }

    // ... (Kodun geri kalaný ayný) ...
}