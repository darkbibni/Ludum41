using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPreviewUI : MonoBehaviour {

    [SerializeField] private Image itemImg;
    [SerializeField] private Text itemCost;

    public void SetupPreview(Item item)
    {
        itemImg.sprite = item.Data.sprite;
        itemCost.text = item.Data.rewardPrice + " $";

        gameObject.SetActive(true);
    }
}
