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
	public void SaveGame()
	{
		SaveData data = new SaveData();
		GameObject newPlayer = GameObject.FindGameObjectWithTag("Player");
		Inventory newInventory = newPlayer.GetComponent<Inventory>();
		data.player = newPlayer;
		data.items = newInventory.items;
		data.scene = SceneManager.GetActiveScene().buildIndex;
		string output = JsonUtility.ToJson(data);
		File.WriteAllText(Path.Combine(Application.persistentDataPath,"/SaveData.txt"), output);
		Debug.Log("Game data saved!");
	}
}

[Serializable]
class SaveData 
{
    public GameObject player;
	public List<InventoryItem> items;
	public InventoryItem item;
	public int scene;

}