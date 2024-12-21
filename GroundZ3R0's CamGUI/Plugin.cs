using System;
using System.Threading.Tasks;
using BepInEx;
using Cinemachine;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UIElements;

namespace GroundZ3R0s_CamGUI
{
	[BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
	public class Plugin : BaseUnityPlugin
	{
		public static float mousePosY = 1038f;
        public static float mousePosX = 256f;
        public static float mousePosY1 = 1038f;
        public static float mousePosX1 = 641f;
        public static bool on = false;
        public static bool on1 = false;
        public static bool spectating = false;
		public static VRRig otherRig = null;

        void OnEnable()
		{
			HarmonyPatches.ApplyHarmonyPatches();
		}

		void OnDisable()
		{
			HarmonyPatches.RemoveHarmonyPatches();
		}
		void moveWindow()
		{
            mousePosY = UnityInput.Current.mousePosition.y;
            mousePosX = UnityInput.Current.mousePosition.x;
			UnityEngine.Debug.Log($"Y: {mousePosY}, X: {mousePosX}");
        }
        void moveWindow1()
        {
            mousePosY1 = UnityInput.Current.mousePosition.y;
            mousePosX1 = UnityInput.Current.mousePosition.x;
            UnityEngine.Debug.Log($"Y1: {mousePosY1}, X1: {mousePosX1}");
        }
        void Update()
		{
			if (UnityInput.Current.GetKeyDown(KeyCode.BackQuote))
			{
				on = !on;
			}
            if (UnityInput.Current.GetKeyDown(KeyCode.Tab))
            {
                on1 = !on1;
            }
            if (UnityInput.Current.GetMouseButton(0) && (UnityInput.Current.GetKey(KeyCode.LeftShift) || UnityInput.Current.GetKey(KeyCode.RightShift)))
			{
				moveWindow();
            }
            if (UnityInput.Current.GetMouseButton(1) && (UnityInput.Current.GetKey(KeyCode.LeftShift) || UnityInput.Current.GetKey(KeyCode.RightShift)))
			{
				moveWindow1();
			}
            if (spectating)
			{
				spectate();
			}
        }

		void spectate()
		{
            GameObject.Find("Player Objects/Third Person Camera/Shoulder Camera/CM vcam1").GetComponent<CinemachineVirtualCamera>().Follow.transform.position = otherRig.transform.position;
            GameObject.Find("Player Objects/Third Person Camera/Shoulder Camera/CM vcam1").GetComponent<CinemachineVirtualCamera>().Follow.transform.LookAt(otherRig.transform);
            GameObject.Find("Player Objects/Third Person Camera/Shoulder Camera/CM vcam1").GetComponent<CinemachineVirtualCamera>().Follow.transform.rotation = otherRig.transform.rotation;
        }

        void OnGUI()
		{
			if (on1)
			{
                GUI.Box(new Rect(mousePosX1 - 125f, -mousePosY1 + 1040f, 250f, 400f), "Spectator\n--------------------------------------------------------------------");
                var i = 0f;
                foreach (Photon.Realtime.Player player in PhotonNetwork.PlayerListOthers)
                {
                    VRRig vrrig = GorillaGameManager.instance.FindPlayerVRRig(player);
                    if (!vrrig.isOfflineVRRig)
                    {
                        if (GUI.Button(new Rect(mousePosX1 - 100f, -mousePosY1 + (1080f + i), 200f, 25f), "Spectate " + player.NickName))
                        {
                            spectating = true;
                            otherRig = vrrig;
                        }
                        i += 30;
                    }
                }
                if (GUI.Button(new Rect(mousePosX1 - 100f, -mousePosY1 + (1080f + i), 200f, 25f), "Stop Spectating"))
                {
                    spectating = false;
                    GameObject.Find("Player Objects/Third Person Camera/Shoulder Camera/CM vcam1").GetComponent<CinemachineVirtualCamera>().Follow.transform.localPosition = new Vector3(0f, 0f, 0f);
                    GameObject.Find("Player Objects/Third Person Camera/Shoulder Camera/CM vcam1").GetComponent<CinemachineVirtualCamera>().Follow.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
                }
            } else {
                GUI.Box(new Rect(mousePosX1 - 125f, -mousePosY1 + 1040f, 250f, 30f), "Spectator");
            }
			if (on)
			{
				GUI.Box(new Rect(mousePosX - 250f, -mousePosY + 1040f, 500f, 500f), "CamGUI\n----------------------------------------------------------------------------------------------------------------------------------------");
				if (GUI.Button(new Rect(mousePosX - 225f, -mousePosY + 1100f, 450f, 25f), "Center Cam"))
				{
					GameObject.Find("Player Objects/Third Person Camera/Shoulder Camera/CM vcam1").GetComponent<CinemachineVirtualCamera>().Follow.transform.localPosition = new Vector3(-0.4f, 0.5f, -0.5f);
				}
				if (GUI.Button(new Rect(mousePosX - 225f, -mousePosY + 1130f, 450f, 25f), "Left Hand Cam"))
				{
					GameObject.Find("Player Objects/Third Person Camera/Shoulder Camera/CM vcam1").GetComponent<CinemachineVirtualCamera>().Follow.transform.localPosition = new Vector3(-1f, 0f, 0f);
				}
				if (GUI.Button(new Rect(mousePosX - 225f, -mousePosY + 1160f, 450f, 25f), "Right Hand Cam"))
				{
					GameObject.Find("Player Objects/Third Person Camera/Shoulder Camera/CM vcam1").GetComponent<CinemachineVirtualCamera>().Follow.transform.localPosition = new Vector3(0.5f, 0f, 0f);
				}
				if (GUI.Button(new Rect(mousePosX - 225f, -mousePosY + 1190f, 450f, 25f), "Front Cam"))
				{
					GameObject.Find("Player Objects/Third Person Camera/Shoulder Camera/CM vcam1").GetComponent<CinemachineVirtualCamera>().Follow.transform.localRotation = new Quaternion(0f, 1f, 0f, 0f);
				}
				if (GUI.Button(new Rect(mousePosX - 225f, -mousePosY + 1220f, 450f, 25f), "Top Cam"))
				{
					GameObject.Find("Player Objects/Third Person Camera/Shoulder Camera/CM vcam1").GetComponent<CinemachineVirtualCamera>().Follow.transform.localRotation = new Quaternion(1f, 0f, 0f, 1f);
				}
				if (GUI.Button(new Rect(mousePosX - 225f, -mousePosY + 1250f, 450f, 25f), "Shoulder Cam"))
				{
					GameObject.Find("Player Objects/Third Person Camera/Shoulder Camera/CM vcam1").GetComponent<CinemachineVirtualCamera>().Follow.transform.localRotation = new Quaternion(0f, 1f, 0f, 1f);
				}
				if (GUI.Button(new Rect(mousePosX - 225f, -mousePosY + 1280f, 450f, 25f), "Default Cam"))
				{
					GameObject.Find("Player Objects/Third Person Camera/Shoulder Camera/CM vcam1").GetComponent<CinemachineVirtualCamera>().Follow.transform.localPosition = new Vector3(0f, 0f, 0f);
					GameObject.Find("Player Objects/Third Person Camera/Shoulder Camera/CM vcam1").GetComponent<CinemachineVirtualCamera>().Follow.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
				}
			} else {
                GUI.Box(new Rect(mousePosX - 250f, -mousePosY + 1040f, 500f, 30f), "CamGUI");
            }
        }
	}
}
