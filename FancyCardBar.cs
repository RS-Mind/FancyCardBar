using BepInEx;
using HarmonyLib;
using UnboundLib.GameModes;
using Jotunn.Utils;
using UnityEngine;
using System.Collections;
using UnboundLib;

namespace FancyCardBar
{
    [BepInDependency("com.willis.rounds.unbound", BepInDependency.DependencyFlags.HardDependency)]
    [BepInPlugin(ModId, ModName, Version)]
    [BepInProcess("Rounds.exe")]
    public class FancyCardBar : BaseUnityPlugin
    {
        private const string ModId = "com.rsmind.rounds.fancycardbar";
        private const string ModName = "Fancy Card Bar";
        public const string Version = "1.0.0";
        public const string ModInitials = "FCB";
        public static FancyCardBar instance { get; private set; }

        void Awake()
        {
            Unbound.RegisterClientSideMod(ModId);
            var harmony = new Harmony(ModId);
            harmony.PatchAll();
        }


        void Start()
        {
            instance = this;
        }

        public static bool Debug = false;
        internal static AssetBundle assets;
    }
}