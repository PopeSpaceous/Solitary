// Company: The Puzzlers
// Copyright (c) 2018 All Rights Reserved
// Author: Nathan Misener
// Co-Author: Leonel Jara
// Date: 04/13/2018
/* Summary: 
 * This class will load or save a file SavedGames.gd or IdentificationData.gd to a persistent data path
*/

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
    //Save method. 
    public static void SaveIdentification()
    {
        //sets binary formatter
        BinaryFormatter bf = new BinaryFormatter();
        //opens the filestream and saves file to local appdata
        FileStream file = File.Create(Application.persistentDataPath + "/IdentificationData.gd");
        //serilalizes the gamedata
        bf.Serialize(file, IdentifyData.current);
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

    //Load method. Returns false if the file does not exists
    public static bool LoadIdentification()
    {
        //check if file exists
        if (File.Exists(Application.persistentDataPath + "/IdentificationData.gd"))
        {
            //set the binary formatter
            BinaryFormatter bf = new BinaryFormatter();
            //open the filestream
            FileStream file = File.Open(Application.persistentDataPath + "/IdentificationData.gd", FileMode.Open);
            //assign the current game data
            IdentifyData.current = (IdentifyData)bf.Deserialize(file);
            //close the file
            file.Close();
        }
        else {
            return false;
        }
        return true;
    }
}
