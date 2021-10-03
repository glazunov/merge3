using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/ItemSettings", order = 1)]
public class ItemSettings : ScriptableObject
{
    [ShowNonSerializedField]
    string id = "";

    [ShowNonSerializedField]
    string rewardId = "";

    [ShowNonSerializedField]
    string spritePath = "";


    public string title;

    [ShowAssetPreview(128, 128)]
    public Sprite sprite;

    [ShowAssetPreview(64, 64)]
    public Sprite reward;

    public string SpritePath { get => spritePath; }
    public string Id { get => id; }
    public string RewardId { get => rewardId; }

    private void OnValidate()
    {
        id = sprite.name;
        rewardId = reward.name;
        spritePath = "cakes/" + sprite.name;
    }
}
