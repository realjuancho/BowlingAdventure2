
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class DataAccess
{
    [DllImport("__Internal")]
    private static extern void SyncFiles();

    [DllImport("__Internal")]
    private static extern void WindowAlert(string message);

	public const string GameName = "BowlingAdventure2.0";

    public static void SaveGame(object gameDetails, int slotNumber)
    {
        string dataPath = string.Format("{0}/" + GameName + "(" + slotNumber + ").dat", Application.persistentDataPath);
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream fileStream;

        try
        {
            if (File.Exists(dataPath))
            {
                File.WriteAllText(dataPath, string.Empty);
                fileStream = File.Open(dataPath, FileMode.Open);
            }
            else
            {
                fileStream = File.Create(dataPath);
            }

            binaryFormatter.Serialize(fileStream, gameDetails);
            fileStream.Close();

            if (Application.platform == RuntimePlatform.WebGLPlayer)
            {
                SyncFiles();
            }
        }
        catch (Exception e)
        {
            PlatformSafeMessage("Failed to Save: " + e.Message);
        }
    }


    public static object LoadGame(int slotNumber)
    {
        object obj = null;
		string dataPath = string.Format("{0}/" + GameName + "("+ slotNumber +").dat", Application.persistentDataPath);
        
        try
        {
            if (File.Exists(dataPath))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                FileStream fileStream = File.Open(dataPath, FileMode.Open);

                obj = binaryFormatter.Deserialize(fileStream);
                fileStream.Close();
            }
        }
        catch (Exception e)
        {
            PlatformSafeMessage("Failed to Load: " + e.Message);
        }

        return obj;
    }



	public static void DeleteGame(int slotNumber)
    {
       
		string dataPath = string.Format("{0}/" + GameName + "("+ slotNumber +").dat", Application.persistentDataPath);
        
        try
        {
            if (File.Exists(dataPath))
            {
                File.Delete(dataPath);

            }
        }
        catch (Exception e)
        {
            PlatformSafeMessage("Failed to Delete: " + e.Message);
        }
    }

	public static void SavePreferences(object gamePreferences)
    {
		string dataPath = string.Format("{0}/" + GameName + "_Preferences.dat", Application.persistentDataPath);
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream fileStream;

        try
        {
            if (File.Exists(dataPath))
            {
                File.WriteAllText(dataPath, string.Empty);
                fileStream = File.Open(dataPath, FileMode.Open);
            }
            else
            {
                fileStream = File.Create(dataPath);
            }

			binaryFormatter.Serialize(fileStream, gamePreferences);
            fileStream.Close();

            if (Application.platform == RuntimePlatform.WebGLPlayer)
            {
                SyncFiles();
            }
        }
        catch (Exception e)
        {
            PlatformSafeMessage("Failed to Save: " + e.Message);
        }
    }


    public static object LoadPreferences(int slotNumber)
    {
        object obj = null;
		string dataPath = string.Format("{0}/" + GameName + "_Preferences.dat", Application.persistentDataPath);
        
        try
        {
            if (File.Exists(dataPath))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                FileStream fileStream = File.Open(dataPath, FileMode.Open);

                obj = binaryFormatter.Deserialize(fileStream);
                fileStream.Close();
            }
        }
        catch (Exception e)
        {
            PlatformSafeMessage("Failed to Load: " + e.Message);
        }

        return obj;
    }


    private static void PlatformSafeMessage(string message)
    {
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            WindowAlert(message);
        }
        else
        {
            Debug.Log(message);
        }
    }
}

