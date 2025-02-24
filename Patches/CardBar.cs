using HarmonyLib;
using System;
using System.Collections.Generic;
using UnboundLib;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.ProceduralImage;
using TMPro;
using ModdingUtils.Utils;

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

    [HarmonyPatch(typeof(Cards), "SilentAddToCardBar", typeof(int), typeof(CardInfo), typeof(string), typeof(float), typeof(float))]
    class CardBarUtilsPatch
    {
        private static void Postfix(int playerID)

        {
            CardBar __instance = CardBarUtils.instance.PlayersCardBar(playerID);
            GameObject cardButton = __instance.transform.GetChild(__instance.transform.childCount - 1).gameObject;
            if (((CardInfo)cardButton.GetComponent<CardBarButton>().GetFieldValue("card")).GetComponent<FancyIcon>() is FancyIcon icon)
            {
                var cardBarIcon = GameObject.Instantiate(icon.fancyIcon, cardButton.transform);
                cardBarIcon.transform.localScale = Vector3.one * (cardButton.GetComponent<RectTransform>().sizeDelta.x / 128f);

                cardButton.GetComponentInChildren<TextMeshProUGUI>().enabled = false;
            }
        }
    }

    [HarmonyPatch(typeof(CardBarUtils), "SilentAddToCardBar", typeof(int), typeof(CardInfo), typeof(string))]
    class CardBarUtilsPatch2
    {
        private static void Postfix(int playerID)
        {
            CardBar __instance = CardBarUtils.instance.PlayersCardBar(playerID);
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