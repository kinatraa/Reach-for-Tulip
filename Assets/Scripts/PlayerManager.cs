using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    public static int money = 50;
    public TextMeshProUGUI moneyDisplay;

    void Update() {
        moneyDisplay.text = money.ToString() + "$";
    }
}
