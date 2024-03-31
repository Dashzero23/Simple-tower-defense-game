using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyUI : MonoBehaviour
{
    public TextMeshProUGUI moneyText;
    void Update()
    {
        moneyText.text = "Money: $" + PlayerStat.Money.ToString();
    }
}
