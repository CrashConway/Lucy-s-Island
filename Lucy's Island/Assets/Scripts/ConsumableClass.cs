using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Tool Class", menuName = "Item/Consumable")]
public class ConsumableClass : ItemClass
{
    [Header("Consumable")]
    public float heathAdded;

    public override void Use(PlayerTestController caller)
    {
        base.Use(caller);
        Debug.Log("Eats Consumable");
        caller.inventory.UseSelected();
    }
    public override ConsumableClass GetConsumable() { return this; }
}
