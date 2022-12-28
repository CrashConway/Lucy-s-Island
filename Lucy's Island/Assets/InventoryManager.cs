using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] GameObject slotsHolder;
    [SerializeField] ItemClass itemToAdd;
    [SerializeField] ItemClass itemToRemove;

    public List<ItemClass> items = new List<ItemClass>();

    GameObject[] slots;

    void Start()
    {
        slots = new GameObject[slotsHolder.transform.childCount];
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
                slots[i].transform.GetChild(0).GetComponent<Image>().sprite = items[i].itemIcon; 
            }
            catch 
            { 
                slots[i].transform.GetChild(0).GetComponent<Image>().sprite = null; 
                slots[i].transform.GetChild(0).GetComponent<Image>().enabled = false; 
            }
        }
    }
    public void Add(ItemClass item)
    {
        items.Add(item);
    }
    public void Remove(ItemClass item)
    {
        items.Remove(item);
    }
}
