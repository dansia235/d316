using UnityEngine;
using UnityEngine.UI; // Utilisation du type Text

public class Chronometer : MonoBehaviour
{
    public Text chronometerText; // Référence à l'élément Text pour afficher le chronomètre
    private float timeElapsed;
    private bool isRunning; // Indique si le chronomètre est en cours d'exécution

    void Start()
    {
        timeElapsed = 0f;
        isRunning = false;
    }

    void Update()
    {
        if (isRunning)
        {
            timeElapsed += Time.deltaTime;
            UpdateChronometerDisplay();
        }
    }

    public void StartChronometer()
    {
        isRunning = true;
    }

    public void StopChronometer()
    {
        isRunning = false;
    }

    private void UpdateChronometerDisplay()
    {
        // Format du temps : minutes:secondes
        int minutes = (int)timeElapsed / 60;
        int seconds = (int)timeElapsed % 60;
        chronometerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
