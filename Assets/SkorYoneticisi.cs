using TMPro;
using UnityEngine;

public class SkorYoneticisi : MonoBehaviour
{

    public TextMeshProUGUI skorText; // Ekrandaki yazý objesi
    private int toplamHediye = 0;    // Kaç hediye topladýk?

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        {
            // Oyun baþlayýnca skoru sýfýrla ve ekrana yaz
            SkoruGuncelle();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HediyeToplandi()
    {
        toplamHediye++; // Sayýyý 1 artýr
        SkoruGuncelle(); // Ekrana yeni sayýyý yaz
    }

    void SkoruGuncelle()
    {
        skorText.text = "Hediye: " + toplamHediye.ToString();
    }
}
