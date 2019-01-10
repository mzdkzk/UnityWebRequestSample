using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Sample03 : MonoBehaviour {

    public Text label;
    public InputField inputField;

    private readonly string uri = "http://localhost:5000";

    public void Load()
    {
        label.text = "読み込み中…";
        StartCoroutine("LoadData");
    }

    public void Submit()
    {
        StartCoroutine("SubmitData");
    }

    private IEnumerator LoadData()
    {
        UnityWebRequest request = UnityWebRequest.Get(uri);
        yield return request.SendWebRequest();

        if (request.isHttpError || request.isNetworkError)
        {
            Debug.Log(request.error);
        }
        else
        {
            DataResponse response = JsonUtility.FromJson<DataResponse>(request.downloadHandler.text);
            label.text = response.data;
        }
    }

    private IEnumerator SubmitData()
    {
        WWWForm form = new WWWForm();
        form.AddField("data", inputField.text);
        
        UnityWebRequest request = UnityWebRequest.Post(uri, form);
        yield return request.SendWebRequest();

        if (request.isHttpError || request.isNetworkError)
        {
            Debug.Log(request.error);
        }
    }
}

[Serializable]
public class DataResponse
{
    public string data;
}
