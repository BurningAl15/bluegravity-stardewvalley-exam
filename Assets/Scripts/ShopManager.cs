using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


[Serializable]
public class ShopElement
{
    public Sprite sprite;
    public string name;
    public int price;
}

[Serializable]
public class ShopOption
{
    public int shopId;
    public Button button;
    public Image sprite;
    public TextMeshProUGUI name;
    public TextMeshProUGUI price;
    [HideInInspector] public int priceInt;

    public void Init(int _id, Sprite _sprite, string _name, int _price)
    {
        shopId = _id;
        sprite.sprite = _sprite;
        name.text = _name;
        price.text = _price + " $";
        priceInt = _price;
    }
    public void Init(int _id, Sprite _sprite, string _name)
    {
        shopId = _id;
        sprite.sprite = _sprite;
        name.text = _name;
    }

    public void DisableButton()
    {
        button.interactable = false;
    }
    public void RestartButton()
    {
        button.interactable = true;
    }

    public void HideButton(){
        button.gameObject.SetActive(false);
    }
    public void ShowButton(){
        button.gameObject.SetActive(true);
    }
}


public class ShopManager : MonoBehaviour
{
    public static ShopManager _instance;

    [SerializeField] ShopAssetsList shopAssetsList;

    [SerializeField] TextMeshProUGUI sectionName;

    [SerializeField] List<ShopOption> shopOptions = new List<ShopOption>();
    int currentSection = -1;

    [SerializeField] CanvasGroup panelCanvasGroup;
    [SerializeField] UIComponent noFundsUiComponent;
    Coroutine currentCoroutine = null;


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
        SetSection(0);
    }

    public void ResetSection(){
        SetSection(0);
    }

    public void SetSection(int id)
    {
        currentSection = id;
        ShowSectionProducts(id);
    }

    void ShowSectionProducts(int _sectionId)
    {
        List<ShopElement> tempShopElement = new List<ShopElement>();
        switch (_sectionId)
        {
            default:
            case 0:
                sectionName.text = "Body";
                tempShopElement = shopAssetsList.Body;
                break;
            case 1:
                sectionName.text = "Legs";
                tempShopElement = shopAssetsList.Legs;
                break;
            case 2:
                sectionName.text = "Shoes";
                tempShopElement = shopAssetsList.Shoes;
                break;
            case 3:
                sectionName.text = "Hair";
                tempShopElement = shopAssetsList.Hair;
                break;
        }

        for (int i = 0; i < tempShopElement.Count; i++)
        {
            ShopElement _ = tempShopElement[i];
            shopOptions[i].Init(i, _.sprite, _.name, _.price);
            shopOptions[i].RestartButton();
        }

        List<int> shopIds = new List<int>();
        List<Cloth> filteredClothes = SaveManager._instance.GetDataFiltered(_sectionId);
        for (int i = 0; i < filteredClothes.Count; i++)
        {
            shopIds.Add(filteredClothes[i].shopId);
        }
        for (int i = 0; i < shopOptions.Count; i++)
        {
            for (int j = 0; j < shopIds.Count; j++)
            {
                if (shopOptions[i].shopId == shopIds[j])
                {
                    shopOptions[i].DisableButton();
                    break;
                }
            }
        }
    }

    public void BuyShopOptions(int shopId)
    {
        int price = shopOptions[shopId].priceInt;
        if (MoneyManager._instance.HasEnoughMoney(price))
        {
            print("You Bought the product!");
            int currentAmount = MoneyManager._instance.GetMoneyAmount() - price;
            shopOptions[shopId].DisableButton();
            SaveManager._instance.SaveCustomData_Money(currentAmount);
            SaveManager._instance.CustomSaveData_Cloth(shopId, currentSection);
            MoneyManager._instance.ShowMoneyAmount();
        }
        else
        {
            if (currentCoroutine == null)
                currentCoroutine = StartCoroutine(NoFundsCoroutine());
            print("You don't have money enough");
        }
    }

    IEnumerator NoFundsCoroutine()
    {
        panelCanvasGroup.CanvasGroupInteractable(false);

        yield return StartCoroutine(noFundsUiComponent.UI_Start());
        yield return new WaitForSeconds(0.5f);
        yield return StartCoroutine(noFundsUiComponent.UI_End());

        panelCanvasGroup.CanvasGroupInteractable(true);
        currentCoroutine = null;
    }
}
