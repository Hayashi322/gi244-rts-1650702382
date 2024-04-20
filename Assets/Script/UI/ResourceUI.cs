using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourceUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI resourceText;
    [SerializeField] private Resourcesource resource;

    public void OnQuantityChange()
    {
        resourceText.text = resource.Quantity.ToString();
    }

}
