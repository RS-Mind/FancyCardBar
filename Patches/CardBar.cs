using HarmonyLib;
using System;
using System.Collections.Generic;
using UnboundLib;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.ProceduralImage;
using TMPro;

namespace FancyCardBar
{
    [HarmonyPatch(typeof(CardBar), "AddCard")]
    class CardBarPatch
    {
        private static void Postfix(CardBar __instance)
        {
            GameObject cardButton = __instance.transform.GetChild(__instance.transform.childCount - 1).gameObject;
            if (((CardInfo)cardButton.GetComponent<CardBarButton>().GetFieldValue("card")).GetComponent<FancyIcon>() is FancyIcon icon)
            {
                var cardBarIcon = GameObject.Instantiate(icon.fancyIcon, cardButton.transform);
                cardBarIcon.transform.localScale = Vector3.one * (cardButton.GetComponent<RectTransform>().sizeDelta.x / 128f);

                cardButton.GetComponentInChildren<TextMeshProUGUI>().enabled = false;
            }
        }
    }
}