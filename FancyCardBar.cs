using BepInEx;
using HarmonyLib;
using UnboundLib.GameModes;
using Jotunn.Utils;
using UnityEngine;
using System.Collections;
using UnboundLib;
using UnboundLib.Utils.UI;
using TMPro;

namespace FancyCardBar
{
    [BepInDependency("com.willis.rounds.unbound", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("pykess.rounds.plugins.moddingutils", BepInDependency.DependencyFlags.HardDependency)]
    [BepInPlugin(ModId, ModName, Version)]
    [BepInProcess("Rounds.exe")]
    public class FancyCardBar : BaseUnityPlugin
    {
        private const string ModId = "com.rsmind.rounds.fancycardbar";
        private const string ModName = "Fancy Card Bar";
        private const string CompatibilityModName = "FancyCardBar";
        public const string Version = "1.3.0";
        public const string ModInitials = "FCB";
        public static FancyCardBar instance { get; private set; }

        void Awake()
        {
            Unbound.RegisterClientSideMod(ModId);

            assets = AssetUtils.LoadAssetBundleFromResources("fancycardbar", typeof(FancyCardBar).Assembly);

            if (assets == null)
            {
                UnityEngine.Debug.Log("Failed to load Fancy Card Bar asset bundle");
            }

            var harmony = new Harmony(ModId);
            harmony.PatchAll();
        }


        void Start()
        {
            instance = this;
            blankIcon = assets.LoadAsset<GameObject>("I_Template");
            Unbound.RegisterMenu(ModName, () => { }, NewGUI, null, false);
            if(genIcons) instance.ExecuteAfterFrames(20, () => GenerateIcons());
        }

        internal static void GenerateIcons()
        {
            foreach (CardInfo cardInfo in CardChoice.instance.cards)
            {
                if (cardInfo.gameObject.GetComponent<FancyIcon>() is FancyIcon fancyIcon) continue; // Skip auto-generation if there's already an icon
                if (cardInfo.cardArt is GameObject art)
                {
                    FancyIcon icon = cardInfo.gameObject.AddComponent<FancyIcon>();
                    icon.fancyIcon = Instantiate(blankIcon);
                    icon.fancyIcon.transform.SetParent(icon.transform);
                    GameObject artObject = Instantiate(art, icon.fancyIcon.transform);
                    artObject.transform.SetAsFirstSibling();
                    artObject.transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);

                    var animators = artObject.GetComponents<Animator>();
                    foreach(Animator animator in animators)
                    {
                        Destroy(animator);
                    }
                    animators = artObject.GetComponentsInChildren<Animator>();
                    foreach (Animator animator in animators)
                    {
                        Destroy(animator);
                    }
                }
            }
        }

        internal static string GetConfigKey(string name)
        {
            return $"{FancyCardBar.CompatibilityModName}_{name.ToLower()}";
        }

        public static bool modActive
        {
            get
            {
                return PlayerPrefs.GetInt(GetConfigKey("modActive"), 1) == 1;
            }
            internal set
            {
                PlayerPrefs.SetInt(GetConfigKey("modActive"), value ? 1 : 0);
            }
        }

        public static bool genIcons
        {
            get
            {
                return PlayerPrefs.GetInt(GetConfigKey("genIcons"), 0) == 1;
            }
            internal set
            {
                PlayerPrefs.SetInt(GetConfigKey("genIcons"), value ? 1 : 0);
            }
        }

        private static void NewGUI(GameObject menu)
        {
            MenuHandler.CreateText(ModName + " Options", menu, out TextMeshProUGUI _, 60);
            MenuHandler.CreateText(" ", menu, out TextMeshProUGUI _, 30);
            MenuHandler.CreateToggle(modActive, "Use Fancy Icons", menu, (bool val) => { modActive = val; });
            MenuHandler.CreateText(" ", menu, out TextMeshProUGUI _, 30);
            MenuHandler.CreateToggle(genIcons, "Automatically Generate Card Icons (Requires Restart)", menu, (bool val) => { genIcons = val; });
        }

        public static bool Debug = false;
        internal static AssetBundle assets;
        internal static GameObject blankIcon;
    }
}