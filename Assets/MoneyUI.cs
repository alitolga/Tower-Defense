using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyUI : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<Text>().text = "Money: " + PlayerStats.Money.ToString();
    }
}
