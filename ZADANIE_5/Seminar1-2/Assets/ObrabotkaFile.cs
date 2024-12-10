using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.EventSystems;
using UnityEngine.AI;
using Unity.VisualScripting;

public class ObrabotkaFile : MonoBehaviour
{
    public Text first;
  
    public string jsonURL;
    public Jsonclass Data;
    public Renderer Sphere_object;


    void Start()
    {
        
        StartCoroutine(GetData());
    }

    IEnumerator GetData()
    {
        Debug.Log("Происходит скачивание...");
        var uwr = new UnityWebRequest(jsonURL);
        uwr.method = UnityWebRequest.kHttpVerbGET;
        var resultFile = Path.Combine(Application.persistentDataPath, "result.json");
        var dh = new DownloadHandlerFile(resultFile);
        dh.removeFileOnAbort = true;
        uwr.downloadHandler = dh;
        yield return uwr.SendWebRequest();
        if (uwr.result!=UnityWebRequest.Result.Success)
        {
            first.text = "Упс! Ошибочка...";
        }
        else
        {
            Debug.Log("Файл сохранён по пути: " + resultFile);
            Data = JsonUtility.FromJson<Jsonclass>(File.ReadAllText(Application.persistentDataPath + "/result.json"));
            first.text = Data.Privetstvie;

            Sphere_object.GetComponent<Renderer>().material.color = new Color32((byte)Data.Collor_1, (byte)Data.Collor_2, (byte)Data.Collor_3,0);
            
            yield return StartCoroutine(GetData());
        }
    }
    [System.Serializable]

    public class Jsonclass
    {
        public int Collor_1;
        public int Collor_2;
        public int Collor_3;
        public string Privetstvie;
    }


}
