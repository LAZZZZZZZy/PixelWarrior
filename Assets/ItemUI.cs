using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemUI : MonoBehaviour, IDragHandler, IEndDragHandler
{

    public Item Item { get; set; } //UI上的物品
    private Image itemImage;//获取item的Image组件
    private Vector3 dragPos;
    private bool successSet;
    public Transform parent;
    public Vector3 slotPos;
    public RectTransform slots;
    public int inSlot = 0;
    void Start() //可用 属性get方式替代
    {
        successSet = false;
        dragPos = transform.position;
        itemImage = this.GetComponent<Image>();
        itemImage.sprite = Resources.Load<Sprite>(Item.Sprite);
        transform.localScale = Item.Size;
    }

    public void OnDrag(PointerEventData eventData)
    {
        var rt = gameObject.GetComponent<RectTransform>();
        Vector3 globalMousePos;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(rt, eventData.position, eventData.pressEventCamera, out globalMousePos))
        {
            rt.position = globalMousePos;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("drop");
        if (inSlot == Item.Size.x * Item.Size.y)
        {
            transform.position = slotPos / (Item.Size.x * Item.Size.y);
            dragPos = transform.position;
            successSet = true;
            Debug.Log("success"+slotPos);
        }
        else
        {
            transform.position = dragPos;
            Debug.Log("fail" + slotPos);
        }
    }

    ///// <summary>
    /////更新item的UI显示，默认数量为1个
    ///// </summary>
    ///// <param name="item"></param>
    //public void SetItem(Item item, int amount = 1)
    //{
    //    this.transform.localScale = this.animationScale;//物品更新时放大UI，用于动画
    //    this.Item = item;
    //    this.Amount = amount;
    //    this.itemImage.sprite = Resources.Load<Sprite>(item.Sprite);        //更新UI
    //    if (this.Amount > 1)
    //    {
    //        this.amountText.text = Amount.ToString();
    //    }
    //    else
    //    {
    //        this.amountText.text = "";
    //    }
    //}
    ///// <summary>
    ///// 添加item数量
    ///// </summary>
    ///// <param name="num"></param>
    //public void AddItemAmount(int num = 1)
    //{
    //    this.transform.localScale = this.animationScale;//物品更新时放大UI，用于动画
    //    this.Amount += num;
    //    if (this.Amount > 1)//更新UI
    //    {
    //        this.amountText.text = Amount.ToString();
    //    }
    //    else
    //    {
    //        this.amountText.text = "";
    //    }
    //}
    ////设置item的个数
    //public void SetAmount(int amount)
    //{
    //    this.transform.localScale = this.animationScale;//物品更新时放大UI，用于动画
    //    this.Amount = amount;
    //    if (this.Amount > 1)//更新UI
    //    {
    //        this.amountText.text = Amount.ToString();
    //    }
    //    else
    //    {
    //        this.amountText.text = "";
    //    }
    //}

    ////减少物品数量
    //public void RemoveItemAmount(int amount = 1)
    //{
    //    this.transform.localScale = this.animationScale;//物品更新时放大UI，用于动画
    //    this.Amount -= amount;
    //    if (this.Amount > 1)//更新UI
    //    {
    //        this.amountText.text = Amount.ToString();
    //    }
    //    else
    //    {
    //        this.amountText.text = "";
    //    }
    //}

    ////显示方法
    //public void Show()
    //{
    //    gameObject.SetActive(true);
    //}

    ////隐藏方法
    //public void Hide()
    //{
    //    gameObject.SetActive(false);
    //}

    ////设置位置方法
    //public void SetLocalPosition(Vector3 position)
    //{
    //    this.transform.localPosition = position;
    //}

    ////当前物品（UI）和 出入物品（UI）交换显示
    //public void Exchange(ItemUI itemUI)
    //{
    //    Item itemTemp = itemUI.Item;
    //    int amountTemp = itemUI.Amount;
    //    itemUI.SetItem(this.Item, this.Amount);
    //    this.SetItem(itemTemp, amountTemp);
    //}
}
