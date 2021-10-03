using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemAmount : MonoBehaviour
{
    public string id;
    public string title;
    public Image image;
    public TextMeshProUGUI text;


    public void Init(Sprite itemPicture, string id, string title)
    {
        this.image.sprite = itemPicture;
        this.id = id;
        this.title = title;
    }

    public void UpdateAmount(int amount)
    {
        this.text.text = title + " x" + amount.ToString();
        gameObject.SetActive(amount > 0);
    }
}
