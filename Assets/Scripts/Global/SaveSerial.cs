using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Timber.InventorySystem;

public class SaveSerial : MonoBehaviour
{
	public static void SaveGame(int scene, int id)
	{
		SaveData data = new SaveData();
		GameObject newPlayer = GameObject.FindGameObjectWithTag("Player");
		Inventory newInventory = newPlayer.GetComponent<Inventory>();
		PlayerInfo newStats = newPlayer.GetComponent<PlayerInfo>();
		data.player = newPlayer;
		Debug.Log(newStats.maxHp.GetValue());
		data.info.Add(newStats.maxHp.GetValue());
		data.info.Add(newStats.attack.GetValue());
		data.info.Add(newStats.inventorySize.GetValue());
		data.info.Add(newStats.speed.GetValue());
		data.ethics = newStats.ethics;
		data.items = newInventory.items;
		data.scene = scene;
		data.loadId = id;
		string output = JsonUtility.ToJson(data);
		File.WriteAllText(Path.Combine(Application.persistentDataPath,"/SaveData.txt"), output);
	}
}

[Serializable]
class SaveData 
{
    public GameObject player;
	public List<int> info = new List<int>();
	public float ethics;
	public List<InventoryItem> items;
	public int scene;
	public int loadId;
}