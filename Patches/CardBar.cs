using HarmonyLib;
using UnboundLib;
using UnityEngine;
using TMPro;
using ModdingUtils.Utils;

namespace FancyCardBar.Patches
{
    class FancyIconAdder
    {
        internal static void addIcon(CardBar __instance)
        {
            if (!FancyCardBar.modActive) return;
            GameObject cardButton = __instance.transform.GetChild(__instance.transform.childCount - 1).gameObject;
            if (((CardInfo)cardButton.GetComponent<CardBarButton>().GetFieldValue("card")).GetComponent<FancyIcon>() is FancyIcon icon)
            {
                var cardBarIcon = Object.Instantiate(icon.fancyIcon, cardButton.transform);
                cardBarIcon.transform.localScale = Vector3.one * (cardButton.GetComponent<RectTransform>().sizeDelta.x / 128f);

                cardButton.GetComponentInChildren<TextMeshProUGUI>().enabled = false;
            }
            else if (FancyCardBar.genIcons && ((GameObject)cardButton.GetComponent<CardBarButton>().GetFieldValue("card").GetFieldValue("cardArt") is GameObject cardArt))  
            {

                var cardBarIcon = Object.Instantiate(FancyCardBar.blankIcon, cardButton.transform);

                GameObject artObject = Object.Instantiate(cardArt, cardBarIcon.transform);
                artObject.transform.SetAsFirstSibling();
                artObject.transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);

                var animators = artObject.GetComponents<Animator>();
                foreach (Animator animator in animators)
                {
                    Object.Destroy(animator);
                }
                animators = artObject.GetComponentsInChildren<Animator>();
                foreach (Animator animator in animators)
                {
                    Object.Destroy(animator);
                }

                cardBarIcon.transform.localScale = Vector3.one * (cardButton.GetComponent<RectTransform>().sizeDelta.x / 128f);

                cardButton.GetComponentInChildren<TextMeshProUGUI>().enabled = false;
            }
        }
    }

    [HarmonyPatch(typeof(CardBar), "AddCard")]
    class CardBarPatch
    {
        private static void Postfix(CardBar __instance)
        {
            FancyIconAdder.addIcon(__instance);
        }
    }

    [HarmonyPatch(typeof(Cards), "SilentAddToCardBar", typeof(int), typeof(CardInfo), typeof(string), typeof(float), typeof(float))]
    class CardBarUtilsPatch
    {
        private static void Postfix(int playerID)
        {
            CardBar __instance = CardBarUtils.instance.PlayersCardBar(playerID);
            FancyIconAdder.addIcon(__instance);
        }
    }

    [HarmonyPatch(typeof(CardBarUtils), "SilentAddToCardBar", typeof(int), typeof(CardInfo), typeof(string))]
    class CardBarUtilsPatch2
    {
        private static void Postfix(int playerID)
        {
            CardBar __instance = CardBarUtils.instance.PlayersCardBar(playerID);
            FancyIconAdder.addIcon(__instance);
        }
    }
}