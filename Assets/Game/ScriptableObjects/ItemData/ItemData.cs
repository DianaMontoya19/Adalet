using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Adalet/Inventory/Item Data")]
public class ItemData : ScriptableObject
{
    public string Name;
    public string Description;
    public Sprite Sprite;
    public bool CanUse;
    public bool CanExamine;
}
