using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DrawStatistics : MonoBehaviour
{
    CustomSettings customSettings;

    private void Awake() {
        customSettings = FindObjectOfType<CustomSettings>();
    }

    public void GetStats(int givenInt)
    {
        TextMeshProUGUI UIText = gameObject.GetComponent<TextMeshProUGUI>();
        // Load custom settings for Data, returns a list
        List<float> loadData = customSettings.LoadAccuracyNum(givenInt-1);
        if (loadData != null)
        {
            Debug.Log("data: "+loadData[0]);
            // get times scene has run and average from float values.
            int runTimes = loadData.Count;
            float totalNum = 0f;
            foreach (float accuracy in loadData)
            {
                totalNum += accuracy;
                Debug.Log("num: "+accuracy);
            }
            float averageAcc = totalNum/runTimes;
            
            // print values to screen
            UIText.text = "Scene run: "+runTimes+"\nAverage stopping Accuracy: "+averageAcc;
        }else
        {
            // 
            UIText.text = "No Data yet.";
        }
    }
}
