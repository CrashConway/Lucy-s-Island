using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] GameObject itemCursor;
    [SerializeField] GameObject slotsHolder;
    [SerializeField] ItemClass itemToAdd;
    [SerializeField] ItemClass itemToRemove;
    [SerializeField] SlotClass[] startingItems;


    SlotClass[] items;

    GameObject[] slots;

    SlotClass movingSlot;
    SlotClass tempSlot;
    SlotClass originalSlot;

    bool isMovingItem;

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
        Add(itemToAdd, 1);
        Remove(itemToRemove);
    }


    void Update()
    {
        itemCursor.SetActive(isMovingItem);
        itemCursor.transform.position = Input.mousePosition;
        if (isMovingItem)
            itemCursor.GetComponent<Image>().sprite = movingSlot.GetItem().itemIcon;

        if (Input.GetMouseButtonDown(0))
        {
            //find the closet slot(slot click on)
            if (isMovingItem)
            {
                //end item move
                EndItemMove();
            }
            else
            {
                BeginItemMove();
            }
        }
    }

    #region Inventory Utils
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
    public bool Add(ItemClass item, int quantity)
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
                    items[i].AddItem(item, quantity);
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
    #endregion Inventory Utils

    #region Moving Inventory
    
    bool BeginItemMove()
    {
        originalSlot = (GetClosetSlot());
        if (originalSlot == null || originalSlot.GetItem() == null)
            return false;  //there is not item to be moved

        movingSlot = new SlotClass(originalSlot);
        originalSlot.Clear();
        isMovingItem = true;
        RefreshUI();
        return true;
    }

    bool EndItemMove()
    {
        originalSlot = (GetClosetSlot());
        if (originalSlot == null)
        {
            Add(movingSlot.GetItem(), movingSlot.GetQuantity());
            movingSlot.Clear();
        }
        else
        {
            if (originalSlot.GetItem() != null)
            {
                if (originalSlot.GetItem() == movingSlot.GetItem())//they should stack if they are the same
                {
                    if (originalSlot.GetItem().isStackable)
                    {
                        originalSlot.AddQuantity(movingSlot.GetQuantity());
                        movingSlot.Clear();
                    }
                    else
                        return false;
                }
                else
                {
                    tempSlot = new SlotClass(originalSlot); // a = b
                    originalSlot.AddItem(movingSlot.GetItem(), movingSlot.GetQuantity()); // b = c
                    movingSlot.AddItem(tempSlot.GetItem(), tempSlot.GetQuantity()); // a = c
                    RefreshUI();
                    return true;
                }
            }
            else  // place item as usual
            {
                originalSlot.AddItem(movingSlot.GetItem(), movingSlot.GetQuantity());
                movingSlot.Clear();
            }
        }
        isMovingItem = false;
        RefreshUI();
        return true;
    }
    SlotClass GetClosetSlot()
    {
        Debug.Log(Input.mousePosition);

        for (int i = 0; i < slots.Length; i++)
        {
            if(Vector2.Distance(slots[i].transform.position, Input.mousePosition) < 32)
            {
                return items[i];
            }
        }
        return null;
    }

    #endregion Moving Inventory
}