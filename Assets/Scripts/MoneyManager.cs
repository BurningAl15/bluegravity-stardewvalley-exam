using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager _instance;

    [SerializeField] int moneyAmount;
    [SerializeField] TextMeshProUGUI moneyAmountText;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        moneyAmount = SaveManager._instance.GetMoneyAmount();
        moneyAmountText.text = "My money: "+moneyAmount+"$";
    }

    public int GetMoneyAmount()
    {
        moneyAmount = SaveManager._instance.GetMoneyAmount();
        return moneyAmount;
    }

    public bool HasEnoughMoney(int amount)
    {
        moneyAmount = SaveManager._instance.GetMoneyAmount();
        return amount <= moneyAmount;
    }

    public void ShowMoneyAmount()
    {
        moneyAmount = SaveManager._instance.GetMoneyAmount();
        moneyAmountText.text = "My money: "+moneyAmount+"$";
    }
}
