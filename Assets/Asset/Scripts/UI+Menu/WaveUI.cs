using TMPro;
using UnityEngine;

public class WaveUI : MonoBehaviour
{
    public TextMeshProUGUI waveText;
    public WaveSpawner waveSpawner;
    void Update()
    {
        waveText.text = "Wave: " + PlayerStat.Rounds.ToString() + "/" + waveSpawner.waves.Length;
    }
}
