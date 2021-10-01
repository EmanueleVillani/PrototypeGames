using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
                newitem.GetComponentInChildren<Image>().sprite = ItemSprites[(int)i.item].sprite;
                newitem.GetComponentInChildren<Text>().text = "0";

                Inventory.Add(i.item, newitem);
            }
        }
    }

    void FunzioneProva()
    {
        Debug.Log("Prova");


    }




    // Update is called once per frame
    void Update()
    {

    }
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
    public void ModifyInventory(Item item,bool adding)
    {
        if (adding)
        {
            if (Inventory.ContainsKey(item))
            {
                int value = int.Parse(Inventory[item].GetComponentInChildren<Text>().text);
                value++;
                Inventory[item].GetComponentInChildren<Text>().text = ""+value;
            }
            else
            {
                GameObject newitem = Instantiate<GameObject>(ItemUI,Bag.transform);
                newitem.GetComponentInChildren<Image>().sprite = ItemSprites[(int)item].sprite;
                newitem.GetComponentInChildren<Text>().text = "1";
                
                Inventory.Add(item,newitem);
            }
        }
        else
        {
            if (Inventory.ContainsKey(item))
            {
                int value = int.Parse(Inventory[item].GetComponentInChildren<Text>().text);
                value--;
                if (value == 0)
                {
                    Destroy(Inventory[item]);
                    Inventory.Remove(item);
                }
                else
                {
                    value = int.Parse(Inventory[item].GetComponentInChildren<Text>().text);
                    value--;
                    Inventory[item].GetComponentInChildren<Text>().text = "" + value;
                }
            }
        }
    }
   
    
}
public enum Item
{
    Gem=0,key=1
}
[System.Serializable]
public class ItemLogic
{
    public Sprite sprite;
    public Item item;
    public bool alreadyimage=false;
}