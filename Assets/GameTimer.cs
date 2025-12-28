using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText;

    // Baþlangýç süresini buradan ayarlayabilirsin (60 saniye = 1 dakika)
    public float timeRemaining = 60f;

    // Sayaç çalýþýyor mu kontrolü (Süre bitince durmasý için)
    private bool isTimerRunning = true;

    void Update()
    {
        if (isTimerRunning)
        {
            if (timeRemaining > 0)
            {
                // Süreyi azalt (Geri sayým mantýðý)
                timeRemaining -= Time.deltaTime;
            }
            else
            {
                // Süre bittiðinde tam 0'da kalsýn, eksiye düþmesin
                timeRemaining = 0;
                isTimerRunning = false;

                // ÝLERÝDE BURAYA "OYUN BÝTTÝ" KODLARINI YAZABÝLÝRSÝN
                // Debug.Log("Süre Doldu!"); 
            }

            // Dakika ve Saniye hesaplama
            float minutes = Mathf.FloorToInt(timeRemaining / 60);
            float seconds = Mathf.FloorToInt(timeRemaining % 60);

            // Ekrana Yazdýrma (01:00, 00:59, 00:58 diye gider)
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }
}