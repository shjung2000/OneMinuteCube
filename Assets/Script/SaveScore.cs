using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveScore
{
    public static void savePlayerHighScore(int score , bool isHighScore)  //static to allow function to be called without having to instantiate
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/PlayerHighScore.save";
        FileStream stream = new FileStream(path, FileMode.Create);

        RecordData data = new RecordData(score, isHighScore);

        formatter.Serialize(stream, data);  //Create the file and stream. Save the score (Int) into the file and turn it into binary file
        stream.Close();  //Close the file stream
    }

    public static RecordData loadHighScore()
    {
        string path = Application.persistentDataPath + "/PlayerHighScore.save";
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            
            FileStream stream = new FileStream(path, FileMode.Open); //Path exists means the file has already been created and saved. Open the file

            RecordData data = formatter.Deserialize(stream) as RecordData;  //Convert the binary file back to normal file

            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }

    public static void savePlayerScore(int score)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/PlayerScore.save";
        FileStream stream = new FileStream(path , FileMode.Create);

        formatter.Serialize(stream , score);

        stream.Close();
    }

    public static int loadScore()
    {
        string path = Application.persistentDataPath + "/PlayerScore.save";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();

            FileStream stream = new FileStream(path, FileMode.Open);

            int data = (int)formatter.Deserialize(stream);

            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return 0;
        }
    }

}
