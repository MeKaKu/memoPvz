using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageLibrary : MonoBehaviour
{
    public ImageData cardImagedata;
    Dictionary<AssetId, Sprite> dictionary = new Dictionary<AssetId, Sprite>();
    private void Awake() {
        foreach(ImageData.SpriteItem spriteItem in cardImagedata.spriteItems){
            dictionary.Add(spriteItem.id, spriteItem.sprite);
        }
    }
    public Sprite GetSprite(AssetId id){
        return dictionary[id];
    }
}
