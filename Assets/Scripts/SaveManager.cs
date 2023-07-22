using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Cloth
{
    public List<AnimationIndexGroup> animationIndex = new List<AnimationIndexGroup>();

    public int shopId;
    public int sectionId;
    public bool isCurrentlyUsed;

    public Cloth(List<AnimationIndexGroup> _animationIndex, int _shopId, int _sectionId)
    {
        animationIndex = _animationIndex;
        shopId = _shopId;
        sectionId = _sectionId;
        isCurrentlyUsed = false;
    }

    public void EnableCloth()
    {
        isCurrentlyUsed = true;
    }

    public void DisableCloth()
    {
        isCurrentlyUsed = false;
    }
}

[Serializable]
public class SaveData
{
    public int moneyAmount;
    public List<Cloth> clothes = new List<Cloth>();
    List<AnimationIndexGroup> animationGroup = new List<AnimationIndexGroup>();

    public List<Cloth> GetClothFiltered(int _sectionId)
    {
        List<Cloth> tempClothes = new List<Cloth>();
        for (int i = 0; i < clothes.Count; i++)
        {
            if (clothes[i].sectionId == _sectionId)
            {
                tempClothes.Add(clothes[i]);
            }
        }
        return tempClothes;
    }

    public void SetCurrentlyUsedCloth(){
        
    }

    void CurrentlyUsedAnimationIndex()
    {
        List<AnimationIndexGroup> tempAnimationGroup = new List<AnimationIndexGroup>();
        List<Cloth> tempClothes = new List<Cloth>();

        for (int i = 0; i < clothes.Count; i++)
        {
            if (clothes[i].isCurrentlyUsed)
            {
                tempClothes.Add(clothes[i]);
            }
        }

        for (int i = 0; i < tempClothes.Count; i++)
        {
            List<AnimationIndexGroup> animIndex = tempClothes[i].animationIndex;
            for (int j = 0; j < animIndex.Count; j++)
            {
                tempAnimationGroup.Add(animIndex[j]);
            }
        }
        animationGroup = tempAnimationGroup;
    }

    public List<AnimationIndexGroup> CurrentlyUsed_Filter(int _partIndex)
    {
        CurrentlyUsedAnimationIndex();
        List<AnimationIndexGroup> tempAnimationGroup = new List<AnimationIndexGroup>();

        for (int i = 0; i < animationGroup.Count; i++)
        {
            if (animationGroup[i].partIndex == _partIndex)
            {
                AnimationIndexGroup temp = new AnimationIndexGroup(animationGroup[i].groupIndex, _partIndex, animationGroup[i].bodyPartIndex);
                tempAnimationGroup.Add(temp);
            }
        }
        // Create an instance of the custom comparer
        AnimationIndexGroupComparer customComparer = new AnimationIndexGroupComparer();

        // Sort the list using the custom comparer
        tempAnimationGroup.Sort(customComparer);
        return tempAnimationGroup;
    }

    public List<Cloth> CurrentlyUsed_ClothFilter(){
        List<Cloth> tempClothes = new List<Cloth>();
        for (int i = 0; i < clothes.Count; i++)
        {
            if (clothes[i].isCurrentlyUsed)
            {
                tempClothes.Add(clothes[i]);
            }
        }
        return tempClothes;
    }
}


public class SaveManager : MonoBehaviour
{
    public static SaveManager _instance;
    [SerializeField] SaveData data;

    private void Awake()
    {
        _instance = this;
        InitData();
        LoadSavedData();
    }

    public void LoadSavedData()
    {
        if (PlayerPrefs.HasKey("Save"))
        {
            string saveJson = PlayerPrefs.GetString("Save");
            data = JsonUtility.FromJson<SaveData>(saveJson);
        }
        else
        {
            InitData();
        }
    }

    public void SaveData()
    {
        string saveJson = JsonUtility.ToJson(data);
        PlayerPrefs.SetString("Save", saveJson);
        PlayerPrefs.Save();
    }

    public void InitData()
    {
        if (!PlayerPrefs.HasKey("Save"))
        {
            //Set Initial Money
            data.moneyAmount = 100;

            //Initial Sprites to save
            List<Cloth> clothes = new List<Cloth>();
            Cloth tempCloth = null;

            for (int j = 0; j < 4; j++)
            {
                List<AnimationIndexGroup> animationIndexGroup = new List<AnimationIndexGroup>();
                switch (j)
                {
                    //Body / Arms
                    default:
                    case 0:
                        for (int i = 0; i < 3; i++)
                        {
                            AnimationIndexGroup tempAnimGroup = new AnimationIndexGroup(0, i, 2);
                            animationIndexGroup.Add(tempAnimGroup);
                            tempAnimGroup = new AnimationIndexGroup(0, i, 3);
                            animationIndexGroup.Add(tempAnimGroup);
                        }
                        break;
                    //Pants
                    case 1:
                        for (int i = 0; i < 3; i++)
                        {
                            AnimationIndexGroup tempAnimGroup = new AnimationIndexGroup(0, i, 4);
                            animationIndexGroup.Add(tempAnimGroup);
                        }
                        break;
                    //Shoes
                    case 2:
                        for (int i = 0; i < 3; i++)
                        {
                            AnimationIndexGroup tempAnimGroup = new AnimationIndexGroup(0, i, 5);
                            animationIndexGroup.Add(tempAnimGroup);
                        }
                        break;
                    //Hair
                    case 3:
                        for (int i = 0; i < 3; i++)
                        {
                            AnimationIndexGroup tempAnimGroup = new AnimationIndexGroup(0, i, 0);
                            animationIndexGroup.Add(tempAnimGroup);
                            tempAnimGroup = new AnimationIndexGroup(0, i, 1);
                            animationIndexGroup.Add(tempAnimGroup);
                        }
                        break;
                }
                tempCloth = new Cloth(animationIndexGroup, 0, j);
                tempCloth.EnableCloth();
                clothes.Add(tempCloth);
            }

            data.clothes = clothes;
            SaveData();
        }
    }

    public void SaveCustomData_Money(int _moneyAmount)
    {
        //Set Initial Money
        data.moneyAmount = _moneyAmount;
        SaveData();
    }

    public void CustomSaveData_Cloth(int _shopId, int _sectionId)
    {
        /*
            Body Indexes:
            Simple T-Shirt
            Shop      - ShopId : 0, SectionId    : 0
            Animation - GroupId: 0, bodyPartIndex: 2, partIndex: 0,1,2

            Detail: For body you should save arms too
            Animation - GroupId: 0, bodyPartIndex: 3, partIndex: 0,1,2

            Pants Indexes:
            Simple Pant
            Shop      - ShopId : 0, SectionId    : 1
            Animation - GroupId: 0, bodyPartIndex: 4, partIndex: 0,1,2

            Shoes Indexes:
            Brown Shoes
            Shop      - ShopId : 0, SectionId    : 2
            Animation - GroupId: 0, bodyPartIndex: 5, partIndex: 0,1,2

            Hair Indexes:
            Carrot Hair
            Shop      - ShopId : 0, SectionId    : 3
            Animation - GroupId: 0, bodyPartIndex: 0, partIndex: 0,1,2
        */
        List<Cloth> clothes = data.clothes;
        Cloth tempCloth = null;

        List<AnimationIndexGroup> animationIndexGroup = new List<AnimationIndexGroup>();
        switch (_sectionId)
        {
            //Body / Arms
            default:
            case 0:
                for (int i = 0; i < 3; i++)
                {
                    AnimationIndexGroup tempAnimGroup = new AnimationIndexGroup(_shopId, i, 2);
                    animationIndexGroup.Add(tempAnimGroup);
                    tempAnimGroup = new AnimationIndexGroup(_shopId, i, 3);
                    animationIndexGroup.Add(tempAnimGroup);
                }
                break;
            //Pants
            case 1:
                for (int i = 0; i < 3; i++)
                {
                    AnimationIndexGroup tempAnimGroup = new AnimationIndexGroup(_shopId, i, 4);
                    animationIndexGroup.Add(tempAnimGroup);
                }
                break;
            //Shoes
            case 2:
                for (int i = 0; i < 3; i++)
                {
                    AnimationIndexGroup tempAnimGroup = new AnimationIndexGroup(_shopId, i, 5);
                    animationIndexGroup.Add(tempAnimGroup);
                }
                break;
            //Hair
            case 3:
                for (int i = 0; i < 3; i++)
                {
                    AnimationIndexGroup tempAnimGroup = new AnimationIndexGroup(_shopId, i, 0);
                    animationIndexGroup.Add(tempAnimGroup);
                    tempAnimGroup = new AnimationIndexGroup(0, i, 1);
                    animationIndexGroup.Add(tempAnimGroup);
                }
                break;
        }

        tempCloth = new Cloth(animationIndexGroup, _shopId, _sectionId);
        clothes.Add(tempCloth);

        for(int i = 0; i < clothes.Count; i++){
            if(clothes[i].sectionId == _sectionId){
                clothes[i].DisableCloth();
                if(clothes[i].shopId == _shopId){
                    clothes[i].EnableCloth();
                }
            }
        }

        data.clothes = clothes;

        SaveData();
    }

    public void CustomSaveData_ClothChange(int _shopId, int _sectionId)
    {
        List<Cloth> clothes = data.clothes;

        for(int i = 0; i < clothes.Count; i++){
            if(clothes[i].sectionId == _sectionId){
                clothes[i].DisableCloth();
                if(clothes[i].shopId == _shopId){
                    clothes[i].EnableCloth();
                }
            }
        }

        data.clothes = clothes;

        SaveData();
    }

    public List<Cloth> GetDataFiltered(int _sectionId)
    {
        return data.GetClothFiltered(_sectionId);
    }

    public List<Cloth> CurrentlyUsedCloth(){
        return data.CurrentlyUsed_ClothFilter();
    }

    public SaveData GetData(){
        return data;
    }

    public int GetMoneyAmount()
    {
        return data.moneyAmount;
    }
}