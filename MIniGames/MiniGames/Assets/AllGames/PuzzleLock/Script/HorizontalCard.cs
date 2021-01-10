using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HorizontalCard : MonoBehaviour
{
    public List<TextMeshProUGUI> txtCellValue;
    public List<Image> imgCell;

    public void Init(List<InputIfo> previousInputInfo)
    {
        for (int i = 0; i < txtCellValue.Count; i++)
        {
            txtCellValue[i].text = previousInputInfo[i].value.ToString();
            imgCell[i].color = previousInputInfo[i].colorCode;
        }
    }
}
