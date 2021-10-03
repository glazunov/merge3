using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    bool useScriptableObjects;


    public List<ItemSettingsOld> itemDatas;
    public static ItemManager instance;

    private void Awake()
    {
        instance = this;
    }
    [Button("Init Default")]
    void Init()
    {
        itemDatas = new List<ItemSettingsOld>();

        itemDatas.Add(new ItemSettingsOld("cakes_01", "cakes_02", "cakes/cakes_01", "Chocolate Donut"));
        itemDatas.Add(new ItemSettingsOld("cakes_02", "cakes_04", "cakes/cakes_02", "Pink Donut"));
        itemDatas.Add(new ItemSettingsOld("cakes_04", "cakes_05", "cakes/cakes_04", "Honey Donut"));
        itemDatas.Add(new ItemSettingsOld("cakes_05", "cakes_06", "cakes/cakes_05", "Blue Cake"));
        itemDatas.Add(new ItemSettingsOld("cakes_06", "cakes_08", "cakes/cakes_06", "Chocolate Cake"));
        itemDatas.Add(new ItemSettingsOld("cakes_08", "cakes_09", "cakes/cakes_08", "Rose Cake"));
        itemDatas.Add(new ItemSettingsOld("cakes_09", "cakes_10", "cakes/cakes_09", "Pink Cake"));
        itemDatas.Add(new ItemSettingsOld("cakes_10", "cakes_11", "cakes/cakes_10", "White Cake"));
        itemDatas.Add(new ItemSettingsOld("cakes_11", "cakes_12", "cakes/cakes_11", "Cake with berry"));
        itemDatas.Add(new ItemSettingsOld("cakes_12", "cakes_12", "cakes/cakes_12", "Cherry Cake"));
    }

    [Button("Init Scriptable Objects")]

    void InitScriptableObjects()
    {
        itemDatas = new List<ItemSettingsOld>();
        Resources.LoadAll<ItemSettings>("ItemsSettings/")
            .ToList()
            .ForEach(x =>
            {
                itemDatas.Add(new ItemSettingsOld(x.Id, x.RewardId, x.SpritePath, x.title));

            });

    }

    private void OnValidate()
    {
        if (useScriptableObjects)
        {
            InitScriptableObjects();
        }

    }


}
