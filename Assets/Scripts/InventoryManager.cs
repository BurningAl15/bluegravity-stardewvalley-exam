using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager _instance;

    [SerializeField] PlayerAnimationController player;
    bool isInventoryActive = false;
    [SerializeField] CanvasGroup panelCanvasGroup;
    [SerializeField] ShopAssetsList shopAssetsList;
    [SerializeField] TextMeshProUGUI sectionName;
    int currentSection = -1;
    [SerializeField] List<ShopOption> shopOptions = new List<ShopOption>();

    [SerializeField] Image hair;
    [SerializeField] Image head;
    [SerializeField] Image body;
    [SerializeField] Image arms;
    [SerializeField] Image legs;
    [SerializeField] Image shoes;


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

    public void ShowInventory()
    {
        isInventoryActive = !isInventoryActive;
        panelCanvasGroup.CanvasGroupInteractable(isInventoryActive);
        panelCanvasGroup.CanvasGroupFade(isInventoryActive ? 1 : 0);
        player.SetClothesFromAssets();
        ResetSection();
        Initialize();
    }

    public void ResetSection()
    {
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
            shopOptions[i].Init(i, _.sprite, _.name);
            shopOptions[i].HideButton();
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
                    shopOptions[i].ShowButton();
                }
            }
        }
    }

    public void SelectShopOption(int shopId)
    {
        switch (currentSection)
        {
            default:
            case 0:
                body.sprite = shopAssetsList.Body[shopId].sprite;
                arms.sprite = shopAssetsList.Arms[shopId].sprite;
                break;
            case 1:
                legs.sprite = shopAssetsList.Legs[shopId].sprite;
                break;
            case 2:
                shoes.sprite = shopAssetsList.Shoes[shopId].sprite;
                break;
            case 3:
                hair.sprite = shopAssetsList.Hair[shopId].sprite;
                break;
        }
        SaveManager._instance.CustomSaveData_ClothChange(shopId, currentSection);
    }

    void Initialize()
    {
        List<Cloth> tempClothes = SaveManager._instance.CurrentlyUsedCloth();

        for (int i = 0; i < tempClothes.Count; i++)
        {
            switch (tempClothes[i].sectionId)
            {
                default:
                case 0:
                    body.sprite = shopAssetsList.Body[tempClothes[i].shopId].sprite;
                    arms.sprite = shopAssetsList.Arms[tempClothes[i].shopId].sprite;
                    break;
                case 1:
                    legs.sprite = shopAssetsList.Legs[tempClothes[i].shopId].sprite;
                    break;
                case 2:
                    shoes.sprite = shopAssetsList.Shoes[tempClothes[i].shopId].sprite;
                    break;
                case 3:
                    hair.sprite = shopAssetsList.Hair[tempClothes[i].shopId].sprite;
                    break;
            }
        }
    }
}
