#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

namespace GameBuffs.FreeStylizedTextures
{
    [InitializeOnLoad]
    public static class GameBuffsInitializer
    {
        private const string PACKAGE_VERSION = "1.2.0";
        private const string GAMEBUFFS_MEGAPACK_URL = "https://prf.hn/l/gABLkvq";

        private readonly static string GameBuffsInitializedPackagePrefKey = $"GameBuffs.Initialized.FreeStylizedTextures.u{Application.unityVersion}.p{PACKAGE_VERSION}";

        /// <summary>
        /// Initialize Game Buffs asset on first install
        /// </summary>
        static GameBuffsInitializer()
        {
            var initializedFreeTextures = EditorPrefs.GetBool(GameBuffsInitializedPackagePrefKey, false);
            if (!initializedFreeTextures)
            {
                EditorPrefs.SetBool(GameBuffsInitializedPackagePrefKey, true);

                OpenMegapackUrl();
            }
        }

        /// <summary>
        /// Open the Megapack URL the first time the asset is installed
        /// </summary>
        private static void OpenMegapackUrl()
        {
            Application.OpenURL(GAMEBUFFS_MEGAPACK_URL);
        }
    }
}
#endif