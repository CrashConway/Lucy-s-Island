using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Tool Class", menuName = "Item/Tool/Tool")]
public class ToolClass : ItemClass
{
    [Header("Tool")]
    public ToolType toolType;
    public enum ToolType
    {
        gun,
        grenade,
        knife,
        sword,
    }


    public override void Use(PlayerTestController caller)
    {
        base.Use(caller);
        Debug.Log("Swing Tool");
    }
    public override ToolClass GetTool() { return this; }

}
