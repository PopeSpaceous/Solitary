using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveLoad{

	//Save method. 
	public static void Save() {
		//sets binary formatter
		BinaryFormatter bf = new BinaryFormatter();
		//opens the filestream and saves file to local appdata
		FileStream file = File.Create (Application.persistentDataPath + "/savedGames.gd");
		//serilalizes the gamedata
		bf.Serialize(file, GameData.current);
		//closes the file
		file.Close();
	} 

	//Load method
	public static void Load() {
		//check if file exists
		if(File.Exists(Application.persistentDataPath + "/savedGames.gd")) {
			//set the binary formatter
			BinaryFormatter bf = new BinaryFormatter();
			//open the filestream
			FileStream file = File.Open(Application.persistentDataPath + "/savedGames.gd", FileMode.Open);
			//assign the current game data
			GameData.current = (GameData)bf.Deserialize(file);
			//close the file
			file.Close();
		}
	}
}
