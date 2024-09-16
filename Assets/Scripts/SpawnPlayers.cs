using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayers : MonoBehaviour
{
    
    public GameObject []playerPrefab;

    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    public static int i = 0;
	private void Start()
	{
		Vector2 randomPos = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        PhotonNetwork.Instantiate(playerPrefab[i].name, randomPos, Quaternion.identity);
	}

}
