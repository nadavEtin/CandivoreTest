using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "AssetReference", menuName = "ScriptableObjects/AssetReference")]
public class AssetReferences : ScriptableObject
{
    [Header("Sprites")]
    [Space(2)]
    public List<Sprite> bombs = new List<Sprite>();
    public List<Sprite> hearts = new List<Sprite>();
    public List<Sprite> crystals = new List<Sprite>();
    public List<Sprite> lightning = new List<Sprite>();
    public List<Sprite> stickers = new List<Sprite>();
    public List<Sprite> boosters = new List<Sprite>();

    [Space(10)]
    [Header("Prefabs")]
    [Space(2)]
    public GameObject PrizeShelf;
    public GameObject Pinata;
}
