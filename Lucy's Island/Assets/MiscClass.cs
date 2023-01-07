using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Tool Class", menuName = "Item/Misc")]
public class MiscClass : ItemClass
{
    public override void Use(PlayerTestController caller) { }
    public override MiscClass GetMisc() { return this; }

}
