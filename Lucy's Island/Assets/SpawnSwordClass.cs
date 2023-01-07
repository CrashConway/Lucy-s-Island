using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Tool Class", menuName = "Item/Tool/SpawnSword")]

public class SpawnSwordClass : ToolClass
{
    public GameObject spawnObject;
    public override void Use(PlayerTestController caller)
    {
        base.Use(caller);
        Instantiate(spawnObject, caller.transform.position, Quaternion.identity);
    }
}
