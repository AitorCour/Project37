using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO; //input Output. Comprobar si existen carpetas
using System.Xml.Serialization;
using System.Text;
using System.Security.Cryptography;


public static class DataManager //la hacemos estatica para no declararla
{
    /*public static GameData NewGame(string fileName)
    {
        Debug.Log("New Game: " + Time.time);
        GameData data = new GameData();

        Save(data, fileName);

        return data;
    }*/

    //XML
    public static void SaveToXML<T>(object data, string fileName, string path)
    {
        Debug.Log("Saving: " + Time.time);

        //1 - Definir el path
        //string path = Application.persistentDataPath + "/Data";
        //2 - Comprobar si el path existe. Si no, crearlo
        CreateDirectory(path);
        //3.1 - Crear o sobreescribir el archivo data
        //3.2 - Escribir "data" dentro del archivo
        XmlSerializer serializer = new XmlSerializer(typeof(T));  //Serializador de datos tipo GameData. Es el traductor
        using (FileStream stream = new FileStream(path + "/" + fileName, FileMode.Create)) //El using solo se utilizará para leer o escribir datos
        {
            serializer.Serialize(stream, data);
        }

        Debug.Log("Saved: " + Time.time);
        //Copiar la direccion del enlace de la consola que sale en la consola
    }

    public static object LoadToXML<T>(string fileName, string path)
    {
        Debug.Log("Loading: " + Time.time);

        object data;

        //string path = Application.persistentDataPath + "/Data";
        
        XmlSerializer serializer = new XmlSerializer(typeof(T));  //Serializador de datos tipo GameData. Es el traductor
        using (FileStream stream = new FileStream(path + "/" + fileName, FileMode.Open)) //El using se utilizará para abrir los datos
        {
            data = serializer.Deserialize(stream);
        }


        Debug.Log("Loaded: " + Time.time);

        return data;
    }

    //TEXT
    public static void SaveToText<T>(object data, string fileName, string path)
    {
        //Convertir datos a texto
        string textData = SerializerToText<T>(data);
        Debug.Log("[DM] TextData: \n" + textData);
        //Encriptar el texto
        textData = Encrypt(textData);
        //Guardar el texto encriptado en un fichero
        CreateDirectory(path);

        StreamWriter writer = new StreamWriter(path + "/" + fileName); //esdribe en archivos, no en strings
        writer.Write(textData);
        writer.Close();

        //Guardar como player prefs
        //PlayerPrefs.SetString(fileName, textData);
    }
    public static object LoadFromText<T>(string fileName, string path)
    {
        string textData = "";

        StreamReader reader = new StreamReader(path + "/" + fileName);
        textData = reader.ReadToEnd();
        reader.Close();

        /*if(PlayerPrefs.HasKey(fileName))
        {
            textData = PlayerPrefs.GetString(fileName);
        }*/

        //Desencriptar texto
        textData = Decrypt(textData);
        //Convertir el textoa datos
        return DeserializeFromText<T>(textData);
    }
    
    //SERIALIZE
    public static string SerializerToText<T>(object data)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(T));
        using (StringWriter writer = new StringWriter())
        {
            serializer.Serialize(writer, data);
            return writer.ToString();
        }
    }
    public static object DeserializeFromText<T>(string TextData)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(T));

        return serializer.Deserialize(new StringReader(TextData));
    }

    // ENCRYPT
    private static string Encrypt(string text)
    {
        try
        {
            string key = "MyKey12345";

            byte[] keyArray;

            byte[] byteText = UTF8Encoding.UTF8.GetBytes(text);

            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
            keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
            hashmd5.Clear();

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateEncryptor();
            byte[] result = cTransform.TransformFinalBlock(byteText, 0, byteText.Length);
            tdes.Clear();

            text = Convert.ToBase64String(result, 0, result.Length);
        }
        catch (Exception)
        {

        }

        return text;
    }
    private static string Decrypt(string text)
    {
        try
        {
            string key = "MyKey12345";

            byte[] keyArray;

            byte[] byteText = Convert.FromBase64String(text);

            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
            keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
            hashmd5.Clear();

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateDecryptor();
            byte[] result = cTransform.TransformFinalBlock(byteText, 0, byteText.Length);
            tdes.Clear();

            text = UTF8Encoding.UTF8.GetString(result);
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
        return text;
    }

    //UTILS
    public static void DeleteFile(string filePath)
    {
        if(File.Exists(filePath))
        {
            File.Delete(filePath);
        }
    }

    public static bool FileExists(string filePath)
    {
        if (File.Exists(filePath))
        {
            return true;
        }
        else return false;
    }

    public static void CreateDirectory(string path)
    {
        if (!Directory.Exists(path))
        {
            Debug.Log("Create new directory: " + Time.time);
            Directory.CreateDirectory(path);  //esto sale del system.IO
        }
    }
}
