using Il2CppRUMBLE.Managers;
using Il2CppTMPro;
using MelonLoader;
using RumbleModdingAPI;
using System;
using System.Collections;
using UnityEngine;

namespace DamageNumbers
{
    public class main : MelonMod
    {
        string currentScene = "Loader";
        int sceneCount = 0;
        int[] healths;
        int playerCount = 0;
        bool waitForMatchStart = false;

        public override void OnLateInitializeMelon()
        {
            Calls.onMapInitialized += SceneInit;
        }

        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            currentScene = sceneName;
            sceneCount++;
        }

        private void SceneInit()
        {
            waitForMatchStart = false;
            try
            {
                if ((currentScene == "Loader")) { return; }
                MelonCoroutines.Start(HealthWatcher(sceneCount));
            } catch (Exception e) { MelonLogger.Error(e); }
        }

        private IEnumerator HealthWatcher(int sceneNumber)
        {
            yield return new WaitForSeconds(3f);
            playerCount = PlayerManager.instance.AllPlayers.Count;
            healths = new int[playerCount];
            for (int i = 0; i < playerCount; i++)
            {
                healths[i] = PlayerManager.instance.AllPlayers[i].Data.HealthPoints;
            }
            while (sceneNumber == sceneCount)
            {
                if (!waitForMatchStart)
                {
                    try
                    {
                        if (playerCount != PlayerManager.instance.AllPlayers.Count)
                        {
                            healths = new int[playerCount];
                            for (int i = 0; i < playerCount; i++)
                            {
                                healths[i] = PlayerManager.instance.AllPlayers[i].Data.HealthPoints;
                            }
                        }
                        for (int i = 0; i < playerCount; i++)
                        {
                            if (healths[i] > PlayerManager.instance.AllPlayers[i].Data.HealthPoints)
                            {
                                string damage = "";
                                Color color = Color.black;
                                switch (healths[i] - PlayerManager.instance.AllPlayers[i].Data.HealthPoints)
                                {
                                    case 1:
                                        damage = "1";
                                        color = new Color(249f / 255f, 250f / 255f, 165f / 255f, 1);
                                        break;
                                    case 2:
                                        damage = "2";
                                        color = new Color(254f / 255f, 228f / 255f, 89f / 255f, 1);
                                        break;
                                    case 3:
                                        damage = "3";
                                        color = new Color(254f / 255f, 168f / 255f, 4f / 255f, 1);
                                        break;
                                    case 4:
                                        damage = "4";
                                        color = new Color(251f / 255f, 107f / 255f, 43f / 255f, 1);
                                        break;
                                    case 5:
                                        damage = "5";
                                        color = new Color(250f / 255f, 63f / 255f, 76f / 255f, 1);
                                        break;
                                    case 6:
                                        damage = "6";
                                        color = new Color(247f / 255f, 0f / 255f, 145f / 255f, 1);
                                        break;
                                    case 7:
                                        damage = "7";
                                        color = new Color(90f / 255f, 111f / 255f, 255f / 255f, 1);
                                        break;
                                    case 8:
                                        damage = "8";
                                        color = new Color(57f / 255f, 133f / 255f, 87f / 255f, 1);
                                        break;
                                    case 9:
                                        damage = "9";
                                        color = new Color(1f, 0f, 1f, 1);
                                        break;
                                    case 10:
                                        damage = "10";
                                        color = new Color(1f, 0f, 1f, 1);
                                        break;
                                    case 11:
                                        damage = "11";
                                        color = new Color(1f, 0f, 1f, 1);
                                        break;
                                    case 12:
                                        damage = "12";
                                        color = new Color(1f, 0f, 1f, 1);
                                        break;
                                    case 13:
                                        damage = "13";
                                        color = new Color(1f, 0f, 1f, 1);
                                        break;
                                    case 14:
                                        damage = "14";
                                        color = new Color(1f, 0f, 1f, 1);
                                        break;
                                    case 15:
                                        damage = "15";
                                        color = new Color(1f, 0f, 1f, 1);
                                        break;
                                    case 16:
                                        damage = "16";
                                        color = new Color(1f, 0f, 1f, 1);
                                        break;
                                    case 17:
                                        damage = "17";
                                        color = new Color(1f, 0f, 1f, 1);
                                        break;
                                    case 18:
                                        damage = "18";
                                        color = new Color(1f, 0f, 1f, 1);
                                        break;
                                    case 19:
                                        damage = "19";
                                        color = new Color(1f, 0f, 1f, 1);
                                        break;
                                    case 20:
                                        damage = "20";
                                        color = new Color(1f, 0f, 1f, 1);
                                        break;
                                    default:
                                        break;
                                }
                                if (damage != "")
                                {
                                    int x = 5;
                                    if (i > 0) { x++; }
                                    Transform playerTransform = PlayerManager.instance.AllPlayers[i].Controller.gameObject.transform.GetChild(2).GetChild(0).GetChild(0).gameObject.transform;
                                    GameObject damageNumber = Calls.Create.NewText();
                                    damageNumber.name = damage + " Damage";
                                    damageNumber.transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y + 1f, playerTransform.position.z);
                                    damageNumber.transform.rotation = PlayerManager.instance.localPlayer.Controller.gameObject.transform.GetChild(2).GetChild(0).GetChild(0).gameObject.transform.localRotation;
                                    if (x == 1)
                                    {
                                        damageNumber.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
                                    }
                                    TextMeshPro damageNumberText = damageNumber.GetComponent<TextMeshPro>();
                                    damageNumberText.fontSize = 12f;
                                    damageNumberText.text = damage;
                                    damageNumberText.color = color;
                                    damageNumberText.SetOutlineColor(new Color32(0, 0, 0, 255));
                                    damageNumberText.outlineWidth = 0.5f;
                                    MelonCoroutines.Start(MoveNumber(damageNumber, sceneNumber, i));
                                    if (healths[i] - PlayerManager.instance.AllPlayers[i].Data.HealthPoints > 8)
                                    {
                                        MelonCoroutines.Start(RainbowColor(damageNumberText, sceneNumber));
                                    }
                                    GameObject.Destroy(damageNumber, 4f);
                                }
                            }
                            if (healths[i] != PlayerManager.instance.AllPlayers[i].Data.HealthPoints)
                            {
                                healths[i] = PlayerManager.instance.AllPlayers[i].Data.HealthPoints;
                                if (((currentScene == "Map0") || (currentScene == "Map1")) && (healths[i] <= 0) && !waitForMatchStart)
                                {
                                    MelonCoroutines.Start(WaitForMatchStart(i, sceneNumber));
                                }
                            }
                        }
                    }
                    catch (Exception e){ /*MelonLogger.Error(e);*/ }
                }
                yield return new WaitForFixedUpdate();
            }
            yield break;
        }

        private IEnumerator RainbowColor(TextMeshPro damageNumberText, int sceneNumber)
        {
            float r = 255;
            float g = 0;
            float b = 255;
            float a = 255f;
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

        private IEnumerator WaitForMatchStart(int playerNumber, int sceneNumber)
        {
            yield return new WaitForSeconds(0.5f);
            waitForMatchStart = true;
            while (waitForMatchStart && (sceneCount == sceneNumber))
            {
                try
                {
                    if (PlayerManager.instance.AllPlayers[playerNumber].Data.HealthPoints == 20)
                    {
                        for (int i = 0; i < playerCount; i++)
                        {
                            healths[i] = PlayerManager.instance.AllPlayers[i].Data.HealthPoints;
                        }
                        waitForMatchStart = false;
                    }
                }
                catch { }
                yield return new WaitForFixedUpdate();
            }
            yield break;
        }

        private IEnumerator MoveNumber(GameObject damageNumber, int sceneNumber, int playerNumber)
        {
            DateTime stopMovingTime = DateTime.Now.AddSeconds(3.9f);
            DateTime startFadeTime = DateTime.Now.AddSeconds(2);
            float moveAmount = 0.16f;
            TextMeshPro damageNumberText = damageNumber.GetComponent<TextMeshPro>();
            while ((sceneNumber == sceneCount) && (DateTime.Now < stopMovingTime))
            {
                try
                {
                    if (playerNumber != 0)
                    {
                        damageNumber.transform.rotation = GetAngleToFaceMe(damageNumber.transform.position, PlayerManager.instance.localPlayer.Controller.gameObject.transform.GetChild(2).GetChild(0).GetChild(0).gameObject.transform.position);
                    }
                    else
                    {
                        damageNumber.transform.rotation = PlayerManager.instance.localPlayer.Controller.gameObject.transform.GetChild(2).GetChild(0).GetChild(0).gameObject.transform.rotation;
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

        Quaternion GetAngleToFaceMe(Vector3 objectPosition, Vector3 lookAtPosition)
        {
            Vector3 targetDir = objectPosition - lookAtPosition;
            Quaternion lookDir = Quaternion.LookRotation(targetDir);
            return lookDir;
        }
    }
}
