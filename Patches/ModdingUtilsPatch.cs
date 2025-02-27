using HarmonyLib;
using ModdingUtils.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UI;
using UnityEngine;

namespace FancyCardBar.Patches
{
    [Serializable]
    [HarmonyPatch(typeof(CardBarUtils))]
    class ModdingUtilsPatch
    {

        [HarmonyPrefix]
        [HarmonyPatch(nameof(CardBarUtils.ResetPlayersLineColor), typeof(int))]
        public static bool FixResetPlayersLineColor(int playerID)
        {
            Transform cardBar = CardBarUtils.instance.PlayersCardBar(playerID).transform;
            for (int i = 1; i < cardBar.childCount; i++)
            {
                Transform child = cardBar.GetChild(i);
                child.GetComponent<Graphic>().color = new Color(0.462f, 0.462f, 0.462f, 1f);
                child.GetChild(1).GetComponent<Graphic>().color = new Color(0.6509f, 0.6509f, 0.6509f, 1f);
            }
            return false;
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(CardBarUtils.ChangePlayersLineColor), typeof(int), typeof(Color))]
        public static bool FixChangePlayersLineColor(int playerID, Color color)
        {
            Transform cardBar = CardBarUtils.instance.PlayersCardBar(playerID).transform;
            for (int i = 1; i < cardBar.childCount; i++)
            {
                Transform child = cardBar.GetChild(i);
                child.GetComponent<Graphic>().color = color;
                child.GetChild(1).GetComponent<Graphic>().color = color;
            }
            return false;
        }
    }
}
