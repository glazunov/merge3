using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemSettingsOld
{
    public string id;
    public string rewardId;
    public string spritePath;
    public string title;


    public ItemSettingsOld(string id, string rewardId, string spritePath, string title)
    {
        this.id = id;
        this.rewardId = rewardId;
        this.spritePath = spritePath;
        this.title = title;
    }
}
