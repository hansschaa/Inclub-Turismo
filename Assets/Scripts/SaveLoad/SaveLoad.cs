using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveLoad {

	public static List<Message> savedMessages = new List<Message>();
			
	//it's static so we can call it from anywhere
	public static void Save(Message ms) 
	{
		SaveLoad.savedMessages.Add(ms);
		//gestionará el trabajo de serialización
		BinaryFormatter bf = new BinaryFormatter();
		
		//Application.persistentDataPath is a string, so if you wanted you can put that into debug.log if you want to know where save games are located
		FileStream file = File.Create (Application.persistentDataPath + "/savedMessages.gd"); //you can call it anything you want
		bf.Serialize(file, SaveLoad.savedMessages);
		file.Close();
	}	
	
	public static void Load() {
		if(File.Exists(Application.persistentDataPath + "/savedMessages.gd")) {
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/savedMessages.gd", FileMode.Open);
			SaveLoad.savedMessages = (List<Message>)bf.Deserialize(file);
			file.Close();
		}
	}
}
	