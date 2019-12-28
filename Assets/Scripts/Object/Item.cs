using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    private Vector2 size;
    private int sellPrice;
    private int buyPrice;
    private string sprite;

    public Item(Vector2 size, int sellPrice, int buyPrice, string sprite)
    {
        this.size = size;
        this.sellPrice = sellPrice;
        this.buyPrice = buyPrice;
        this.sprite = sprite;
    }

    public Vector2 Size { get => size; set => size = value; }
    public int SellPrice { get => sellPrice; set => sellPrice = value; }
    public int BuyPrice { get => buyPrice; set => buyPrice = value; }
    public string Sprite { get => sprite; set => sprite = value; }
}
