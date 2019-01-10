using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Sample02 : MonoBehaviour　{

    [SerializeField]
    private Text label;
    [SerializeField]
    private InputField inputField;

    public void OnClick()
    {
        label.text = "読み込み中…";
        StartCoroutine("Method");
    }

    private IEnumerator Method()
    {
        Uri uri = new Uri("https://wusyllabusapi.herokuapp.com/api/search?name=" + inputField.text + "&page=1");
        UnityWebRequest request = UnityWebRequest.Get(uri.AbsoluteUri);
        yield return request.SendWebRequest();

        if (request.isHttpError || request.isNetworkError)
        {
            Debug.Log(request.error);
        }
        else
        {
            SyllabusResponse response = JsonUtility.FromJson<SyllabusResponse>(request.downloadHandler.text);
            label.text = response.result[0].editors[0];
        }
    }
}

[Serializable]
public class Result
{
    public string[] editors;
    public string credit;
}

[Serializable]
public class SyllabusResponse
{
    public Result[] result;
}
