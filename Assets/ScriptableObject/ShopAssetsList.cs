using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Shop Assets List", menuName = "Shop Asset Data")]

public class ShopAssetsList : ScriptableObject
{
    [SerializeField] List<ShopElement> body = new List<ShopElement>();
    [SerializeField] List<ShopElement> legs = new List<ShopElement>();
    [SerializeField] List<ShopElement> shoes = new List<ShopElement>();
    [SerializeField] List<ShopElement> hair = new List<ShopElement>();
    [SerializeField] List<ShopElement> arms = new List<ShopElement>();

    public List<ShopElement> Body
    {
        get
        {
            return body;
        }
    }
    public List<ShopElement> Arms
    {
        get
        {
            return arms;
        }
    }

    public List<ShopElement> Legs
    {
        get
        {
            return legs;
        }
    }

    public List<ShopElement> Shoes
    {
        get
        {
            return shoes;
        }
    }

    public List<ShopElement> Hair
    {
        get
        {
            return hair;
        }
    }
}
