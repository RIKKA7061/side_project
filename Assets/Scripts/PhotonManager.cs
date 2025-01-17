using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System.Text.RegularExpressions;

public class PhotonManager : MonoBehaviourPunCallbacks
{
	public Text connectionStatus;
	public Text idText;
	public Button loginBtn;
	public InputField inputField;

	void Start()
	{
		PhotonNetwork.ConnectUsingSettings();
		loginBtn.interactable = false;
		connectionStatus.text = "¸¶½ºÅÍ ¼­¹ö ¿¬°á Áß..";
	}

	public void Connect()
	{
		// ¼ýÀÚ¾ò±â + ¿µ¹®ÀÚ¾ò±â + ÇÑ±Û¾ò±â + Æ¯¼ö¹®ÀÚÁ¦°Å + °ø¹é°ËÃâ
		if (idText.text != Regex.Replace(idText.text, @"[^0-9a-zA-Z°¡-ÆR]", "") || inputField.text.Equals(""))
		{
			return;
		}
		else
		{
			PhotonNetwork.LocalPlayer.NickName = idText.text;
			loginBtn.interactable = false;

			if (PhotonNetwork.IsConnected)
			{
				connectionStatus.text = "¹æ ÀÔÀå Áß..";
				PhotonNetwork.JoinRandomRoom();
			}
			else
			{
				connectionStatus.text = "(¿ÀÇÁ¶óÀÎ) ¿¬°á ½ÇÆÐ\nÀç½Ãµµ Áß..";
				PhotonNetwork.ConnectUsingSettings();
			}
		}
	}

	public override void OnConnectedToMaster()
	{
		loginBtn.interactable = true;
		connectionStatus.text = "(¿Â¶óÀÎ) ¸¶½ºÅÍ ¼­¹ö¿¡ ¿¬°áµÊ";
	}
	public override void OnDisconnected(DisconnectCause cause)
	{
		loginBtn.interactable = false;
		connectionStatus.text = "(¿ÀÇÁ¶óÀÎ) ¿¬°á ½ÇÆÐ\nÀç½Ãµµ Áß..";
		PhotonNetwork.ConnectUsingSettings();
	}
	public override void OnJoinRandomFailed(short returnCode, string message)
	{
		connectionStatus.text = "»õ ¹æ »ý¼º Áß..";
		PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 8 });
		// MaxPlayers¸¦ 0À¸·Î ÇÏ¸é Á¦ÇÑ¾øÀ½
	}

	public override void OnJoinedRoom()
	{
		connectionStatus.text = "Âü°¡ ¼º°ø";
		PhotonNetwork.LoadLevel(1);
	}
}