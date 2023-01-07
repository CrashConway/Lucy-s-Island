using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTestController : MonoBehaviour
{
    public InventoryManager inventory;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            //use item
            if(inventory.selectedItem != null)
                inventory.selectedItem.Use(this);
        }
    }
}
