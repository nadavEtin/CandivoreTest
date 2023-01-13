using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "AssetReference", menuName = "ScriptableObjects/AssetRefrence")]
public class AssetRefrences : ScriptableObject
{
    //sprites
    public List<Sprite> bombs = new List<Sprite>();
    public List<Sprite> hearts = new List<Sprite>();
    public List<Sprite> crystals = new List<Sprite>();
    public List<Sprite> lightning = new List<Sprite>();
    public List<Sprite> stickers = new List<Sprite>();
    public List<Sprite> boosters = new List<Sprite>();
}
