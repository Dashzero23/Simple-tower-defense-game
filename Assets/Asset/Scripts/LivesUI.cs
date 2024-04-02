using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LiveUI : MonoBehaviour
{
    public TextMeshProUGUI liveText;
    void Update()
    {
        liveText.text = "Lives: " + PlayerStat.Lives.ToString();
    }
}
