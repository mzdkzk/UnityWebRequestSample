using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Sample : MonoBehaviour {

    [SerializeField]
    private Text label;

	public void OnClick()
    {
        label.text = "読み込み中…";
        StartCoroutine("Method");
    }

    private IEnumerator Method()
    {
        UnityWebRequest request = UnityWebRequest.Get("https://yesno.wtf/api");
        yield return request.SendWebRequest();
        
        if (request.isHttpError || request.isNetworkError)
        {
            Debug.Log(request.error);
        }
        else
        {
            YesNoResponse response = JsonUtility.FromJson<YesNoResponse>(request.downloadHandler.text);
            label.text = response.answer;
        }
    }
}

[Serializable]
public class YesNoResponse
{
    [SerializeField]
    public string answer;
}
