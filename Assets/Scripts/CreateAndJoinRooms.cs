using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
	public InputField createInput;
	public InputField joinInput;

	public void CreateRoom()
	{
		SpawnPlayers.i = 0;
		PhotonNetwork.CreateRoom(createInput.text);
	}

	public void JoinRoom()
	{
		SpawnPlayers.i = 1;
		PhotonNetwork.JoinRoom(joinInput.text);
	}

	public override void OnJoinedRoom()
	{
		PhotonNetwork.LoadLevel("Game");
	}
}
