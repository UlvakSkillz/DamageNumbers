using HarmonyLib;
using Il2CppRUMBLE.Managers;
using Il2CppRUMBLE.Players;
using Il2CppRUMBLE.Players.Subsystems;
using Il2CppTMPro;
using MelonLoader;
using RumbleModdingAPI.RMAPI;
using System;
using System.Collections;
using UnityEngine;

namespace DamageNumbers
{
    public class main : MelonMod
    {
        private static int sceneCount = 0;

        [HarmonyPatch(typeof(PlayerHealth), nameof(PlayerHealth.SetHealth), new Type[] { typeof(short), typeof(short), typeof(bool) })]
        public static class SetHealth
        {
            private static void Prefix(ref PlayerHealth __instance, short newHealth, short previousHealth, bool useEffects)
            {
                //triggers when players spawn in and something is null so I check this
                try
                {
                    if (!useEffects //prevents 0s when initially setting player healths.
                        || (previousHealth == 0) //prevents spawn in 0s when hitting kill areas
                        || (previousHealth < newHealth) //prevents healing numbers
                        || (__instance.parentController.PlayerCamera.gameObject.transform == null) //makes sure player camera gameobject is spawned (error otherwise< in fact probably triggers the try/catch conveniently when I want to block it)
                        || (PlayerManager.instance.localPlayer.Controller.PlayerCamera.gameObject == null)) { return; } //makes sure local player camera game object exists (error otherwise< in fact probably triggers the try/catch conveniently when I want to block it)
                    int amount = previousHealth - newHealth;
                    Color color = Color.black;
                    switch (amount)
                    {
                        case 1:
                            color = new Color(249f / 255f, 250f / 255f, 165f / 255f, 1);
                            break;
                        case 2:
                            color = new Color(254f / 255f, 228f / 255f, 89f / 255f, 1);
                            break;
                        case 3:
                            color = new Color(254f / 255f, 168f / 255f, 4f / 255f, 1);
                            break;
                        case 4:
                            color = new Color(251f / 255f, 107f / 255f, 43f / 255f, 1);
                            break;
                        case 5:
                            color = new Color(250f / 255f, 63f / 255f, 76f / 255f, 1);
                            break;
                        case 6:
                            color = new Color(247f / 255f, 0f / 255f, 145f / 255f, 1);
                            break;
                        case 7:
                            color = new Color(90f / 255f, 111f / 255f, 255f / 255f, 1);
                            break;
                        case 8:
                            color = new Color(57f / 255f, 133f / 255f, 87f / 255f, 1);
                            break;
                        case 9:
                            color = new Color(1f, 0f, 1f, 1);
                            break;
                        case 10:
                            color = new Color(1f, 0f, 1f, 1);
                            break;
                        case 11:
                            color = new Color(1f, 0f, 1f, 1);
                            break;
                        case 12:
                            color = new Color(1f, 0f, 1f, 1);
                            break;
                        case 13:
                            color = new Color(1f, 0f, 1f, 1);
                            break;
                        case 14:
                            color = new Color(1f, 0f, 1f, 1);
                            break;
                        case 15:
                            color = new Color(1f, 0f, 1f, 1);
                            break;
                        case 16:
                            color = new Color(1f, 0f, 1f, 1);
                            break;
                        case 17:
                            color = new Color(1f, 0f, 1f, 1);
                            break;
                        case 18:
                            color = new Color(1f, 0f, 1f, 1);
                            break;
                        case 19:
                            color = new Color(1f, 0f, 1f, 1);
                            break;
                        case 20:
                            color = new Color(1f, 0f, 1f, 1);
                            break;
                        default:
                            color = new Color(1f, 0f, 1f, 1);
                            break;
                    }
                    Transform playerTransform = __instance.parentController.PlayerCamera.gameObject.transform;
                    GameObject damageNumber = Create.NewText();
                    damageNumber.name = amount + " Damage";
                    damageNumber.transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y + 1f, playerTransform.position.z);
                    damageNumber.transform.rotation = PlayerManager.instance.localPlayer.Controller.PlayerCamera.gameObject.transform.localRotation;
                    if (__instance.parentController.controllerType == ControllerType.Local)
                    {
                        damageNumber.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
                    }
                    TextMeshPro damageNumberText = damageNumber.GetComponent<TextMeshPro>();
                    damageNumberText.fontSize = 12f;
                    damageNumberText.text = amount.ToString();
                    damageNumberText.color = color;
                    damageNumberText.SetOutlineColor(new Color32(0, 0, 0, 255));
                    damageNumberText.outlineWidth = 0.5f;
                    MelonCoroutines.Start(MoveNumber(damageNumber, sceneCount, __instance.parentController));
                    if (amount > 8)
                    {
                        MelonCoroutines.Start(RainbowColor(damageNumberText, sceneCount));
                    }
                    GameObject.Destroy(damageNumber, 4f);
                }
                catch { return; }
            }
        }

        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            sceneCount++;
        }

        private static IEnumerator RainbowColor(TextMeshPro damageNumberText, int sceneNumber)
        {
            float r = 255;
            float g = 0;
            float b = 255;
            int colorToChange = 1;
            DateTime timeToPlay = DateTime.Now.AddSeconds(3.9f);
            yield return new WaitForFixedUpdate();
            while ((DateTime.Now < timeToPlay) && (sceneNumber == sceneCount))
            {
                try
                {
                    switch (colorToChange)
                    {
                        case 1:
                            g += 5f;
                            r -= 5f;
                            break;
                        case 2:
                            r += 5f;
                            b -= 5f;
                            break;
                        case 3:
                            b += 5f;
                            g -= 5f;
                            break;
                    }
                    if ((r == 0) || (g == 0) || (b == 0))
                    {
                        colorToChange++;
                        if (colorToChange == 4)
                        {
                            colorToChange = 1;
                        }
                    }
                    damageNumberText.color = new Color(r / 255f, g / 255f, b / 255f, damageNumberText.color.a);
                }
                catch { }
                yield return new WaitForFixedUpdate();
            }
            yield break;
        }

        private static IEnumerator MoveNumber(GameObject damageNumber, int sceneNumber, PlayerController player)
        {
            DateTime stopMovingTime = DateTime.Now.AddSeconds(3.9f);
            DateTime startFadeTime = DateTime.Now.AddSeconds(2);
            float moveAmount = 0.16f;
            TextMeshPro damageNumberText = damageNumber.GetComponent<TextMeshPro>();
            while ((sceneNumber == sceneCount) && (DateTime.Now < stopMovingTime))
            {
                try
                {
                    if (player.controllerType == ControllerType.Remote)
                    {
                        damageNumber.transform.rotation = GetAngleToFaceMe(damageNumber.transform.position, PlayerManager.instance.localPlayer.Controller.PlayerCamera.gameObject.transform.position);
                    }
                    else
                    {
                        damageNumber.transform.rotation = PlayerManager.instance.localPlayer.Controller.PlayerCamera.gameObject.transform.rotation;
                    }
                    if (moveAmount > 0)
                    {
                        damageNumber.transform.position = new Vector3(damageNumber.transform.position.x, damageNumber.transform.position.y + moveAmount, damageNumber.transform.position.z);
                        moveAmount -= 0.0064f;
                    }
                    if (startFadeTime < DateTime.Now)
                    {
                        damageNumberText.color = new Color(damageNumberText.color.r, damageNumberText.color.g, damageNumberText.color.b, damageNumberText.color.a - 0.05f);
                    }
                }
                catch { }
                yield return new WaitForFixedUpdate();
            }
            yield break;
        }

        private static Quaternion GetAngleToFaceMe(Vector3 objectPosition, Vector3 lookAtPosition)
        {
            Vector3 targetDir = objectPosition - lookAtPosition;
            Quaternion lookDir = Quaternion.LookRotation(targetDir);
            return lookDir;
        }
    }
}
