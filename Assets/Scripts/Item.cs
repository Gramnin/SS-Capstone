using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType {HEALTH};

public class Item : MonoBehaviour
{
    public ItemType type;

    public Sprite spriteNeutral;
    public Sprite spriteHighlighted;

    public int maxSize;

    public void Use()
    {
        switch (type)
        {
            case ItemType.HEALTH:
                Debug.Log("I just used a first aid kit");
                break;
            default:
                break;
        }
    }
}
