using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] GameObject slotsHolder;
    [SerializeField] ItemClass itemToAdd;
    [SerializeField] ItemClass itemToRemove;

    [SerializeField] SlotClass[] startingItems;

    SlotClass[] items;

    GameObject[] slots;

    void Start()
    {
        slots = new GameObject[slotsHolder.transform.childCount];
        items = new SlotClass[slots.Length];

        for (int i = 0; i < items.Length; i++)
        {
            items[i] = new SlotClass();
        }

        for (int i = 0; i < startingItems.Length; i++)
        {
            items[i] = startingItems[i];
        }


        Debug.Log(items.Length);
        //set all the slots
        for (int i = 0; i < slotsHolder.transform.childCount; i++)
        {
            slots[i] = slotsHolder.transform.GetChild(i).gameObject;
        }

        RefreshUI();
        Add(itemToAdd);
        Remove(itemToRemove);
    }

    public void RefreshUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            try 
            {
                slots[i].transform.GetChild(0).GetComponent<Image>().enabled = true;
                slots[i].transform.GetChild(0).GetComponent<Image>().sprite = items[i].GetItem().itemIcon;
                if (items[i].GetItem().isStackable)
                    slots[i].transform.GetChild(1).GetComponent<Text>().text = items[i].GetQuantity() + "";
                else
                {
                    slots[i].transform.GetChild(1).GetComponent<Text>().text = "";
                }
            }
            catch 
            { 
                slots[i].transform.GetChild(0).GetComponent<Image>().sprite = null; 
                slots[i].transform.GetChild(0).GetComponent<Image>().enabled = false; 
                slots[i].transform.GetChild(1).GetComponent<Text>().text = "";
            }
        }
    }
    public bool Add(ItemClass item)
    {
         //items.Add(item);
         //check if inventory contains item

         SlotClass slot = Contains(item);
         if (slot != null && slot.GetItem().isStackable)
         {
             slot.AddQuantity(1);
         }
         else
         {
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i].GetItem() == null)//this is an empty slot
                {
                    items[i].AddItem(item, 1);
                    break;
                }
            }
             //if(items.Count < slots.Length)
             //items.Add(new SlotClass(item, 1));
             //else
             //{
             //    return false;
             //}
         }
         RefreshUI();
         return true;
     }
    public bool Remove(ItemClass item)
    {
        //items.Remove(item);
        SlotClass temp = Contains(item);
        if (temp != null)
        {
            if (temp.GetQuantity() > 1)
                temp.SubQuantity(1);
            else
            {
                int slotToRemoveIndex = 0;
                for (int i = 0; i < items.Length; i++)
                {
                    if (items[i].GetItem() == item)
                    {
                        slotToRemoveIndex = i;
                        break;
                    }
                }
                items[slotToRemoveIndex].Clear();
            }
        }
        else
        {
            return false;
        }
        RefreshUI();
        return true;
    }

    public SlotClass Contains(ItemClass item)
     {
        for(int i = 0; i < items.Length; i++)
        {
            if(items[i].GetItem() == item)
            {
                return items[i];
            }
        }
        return null;
     }
}
