using System;
using System.Diagnostics;
using UnityEngine;

namespace LudumDare57.Inventory
{
    [Conditional("UNITY_EDITOR")]
    [AttributeUsage(AttributeTargets.Field)]
    public class InventoryItemAttribute : PropertyAttribute { }
}