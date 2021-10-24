﻿using System.Collections;
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
        if(givenInt == 3 || givenInt == 4 || givenInt == 6)
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
                // No data found
                UIText.text = "No Data yet.";
            }
        }

        // 7 = Collisions
        if(givenInt == 7)
        {
            TextMeshProUGUI UIText = gameObject.GetComponent<TextMeshProUGUI>();
            // Load custom settings for Data, returns a list
            List<float> loadData = customSettings.LoadCollisions();
            if (loadData != null)
            {
                Debug.Log("data: "+loadData[0]);
                // Count collisions Total, pedestrian or Vehicle
                int collisionTotal = loadData.Count;
                int countPed=0;
                int countVehicle=0;

                // For each object hit in loaddata
                foreach (int objectHit in loadData)
                {
                    if(objectHit == 1)
                    {
                        countPed++;
                    }else if(objectHit == 2)
                    {
                        countVehicle++;
                    }
                }
                // print values to screen
                UIText.text = "Total collisions: "+collisionTotal+"\nPedistrian Related: "+countPed+"\nOther Vehicle Related: "+countVehicle;
                
            }else
            {
                // No data found
                UIText.text = "No Data yet.";
            }
        }

        // 8 = Objectives
        if(givenInt == 8)
        {
            TextMeshProUGUI UIText = gameObject.GetComponent<TextMeshProUGUI>();
            // Load custom settings for Data, returns a list
            List<float> loadData = customSettings.LoadCollisions();
            if (loadData != null)
            {
                Debug.Log("data: "+loadData[0]);
                // get times scene has run and average from float values.
                /*
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
                */
            }else
            {
                // No data found
                UIText.text = "No Data yet.";
            }
            
        }

    }
}
