using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public ItemLogic[] ItemSprites;
    public Dictionary<Item, GameObject> Inventory=new Dictionary<Item, GameObject>();
    public GameObject ItemUI,Bag;

    private void Awake()
    {
        if (Instance == null|| Instance!=this)
            Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        foreach (ItemLogic i in ItemSprites)
        {
            if (i.alreadyimage)
            {
                GameObject newitem = Instantiate<GameObject>(ItemUI, Bag.transform);
                newitem.GetComponentsInChildren<Image>()[2].sprite = ItemSprites[(int)i.item].sprite;
                newitem.GetComponentInChildren<Text>().text = "0";

                Inventory.Add(i.item, newitem);
            }
        }
    }

    // Update is called once per frame
    public int HowMany(Item item)
    {
        if (Inventory.ContainsKey(item))
        {
            return int.Parse(Inventory[item].GetComponentInChildren<Text>().text);
        }
        return 0;
    }
    public bool CheckItem(Item item)
    {
        if (Inventory.ContainsKey(item))
        {
            return int.Parse(Inventory[item].GetComponentInChildren<Text>().text)>0;
        }
        else
        {
            return false;
        } 
    }
    public void ModifyInventory(Item item, bool adding, int valor)
    {
        if (adding)
        {
            if (Inventory.ContainsKey(item))
            {
                int value = int.Parse(Inventory[item].GetComponentInChildren<Text>().text);
                value += valor;
                Inventory[item].GetComponentInChildren<Text>().text = "" + value;
            }
            else
            {
                if (ItemSprites.Length < (int)item+1)
                    return;
                GameObject newitem = Instantiate<GameObject>(ItemUI, Bag.transform);
                newitem.GetComponentsInChildren<Image>()[2].sprite = ItemSprites[(int)item].sprite;
                newitem.GetComponentInChildren<Text>().text = "" + valor;

                Inventory.Add(item, newitem);
            }
        }
        else
        {
            if (Inventory.ContainsKey(item))
            {
                int value = int.Parse(Inventory[item].GetComponentInChildren<Text>().text);
                if (value == 0)
                    return;
                value -= valor;
                Inventory[item].GetComponentInChildren<Text>().text = "" + value;
                //  if (value == 0)
                //  {
                //  //   Destroy(Inventory[item]);
                //  //   Inventory.Remove(item);
                //  }
                //  else
                //  {
                //  }
            }
        }
    }
   
    public void HardSetInventory(Item item,int valor)
    {
        if (Inventory.ContainsKey(item))
        {
            Inventory[item].GetComponentInChildren<Text>().text = "" + valor;
        }
    } 
}
public enum Item
{
    Gem=0,Ammo=1, kill = 2,health =3,battery=4,time=5
}
[System.Serializable]
public class ItemLogic
{
    public Sprite sprite;
    public Item item;
    public bool alreadyimage=false;
}