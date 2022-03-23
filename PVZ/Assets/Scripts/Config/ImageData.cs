using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/ImageData")]
public class ImageData : ScriptableObject
{
    public SpriteItem[] spriteItems;

    [System.Serializable]
    public class SpriteItem{
        public AssetId id;
        public Sprite sprite;
    }
}
