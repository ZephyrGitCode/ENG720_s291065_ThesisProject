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

    private void Start() {
        //GetStats(7);
    }

    public void GetStats(int givenInt)
    {
        if(givenInt == 3 || givenInt == 4 || givenInt == 6)
        {
            TextMeshProUGUI UIText = gameObject.GetComponent<TextMeshProUGUI>();
            // Load custom settings for Data, returns a list
            List<float> loadData = customSettings.LoadAccuracyNum(givenInt-1);
            UIText.text = "";
            if (loadData != null)
            {
                Debug.Log("data: "+loadData[0]);
                // get times scene has run and average from float values.
                int runTimes = loadData.Count;
                float totalNum = 0f;
                foreach (float accuracy in loadData)
                {
                    totalNum += Mathf.Abs(accuracy);
                    Debug.Log("num: "+Mathf.Abs(accuracy));
                    UIText.text+=Mathf.Abs(accuracy)+",";
                }
                float averageAcc = totalNum/runTimes;
                
                // print values to screen
                UIText.text = UIText.text+"\nScene run: "+runTimes+"\nAverage stopping Accuracy: "+averageAcc;
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
            UIText.text = "";
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
                        UIText.text+="Ped,";
                    }else if(objectHit == 2)
                    {
                        countVehicle++;
                        UIText.text+="Car,";
                    }
                }
                
                // print values to screen
                UIText.text = UIText.text+"\nTotal collisions: "+collisionTotal+"\nPedistrian Related: "+countPed+"\nOther Vehicle Related: "+countVehicle;
                
            }else
            {
                // No data found
                UIText.text = "No Data yet.";
            }
        }

        // 8 = Objectives
        if(givenInt == 13|| givenInt == 14 || givenInt == 16)
        {
            // Objectives, 13 = 3, 14 = 4, 16 = 6
            int levelInt = 0;
            switch(givenInt)
            {
                case 13:
                    levelInt = 3;
                    break;
                case 14:
                    levelInt = 4;
                    break;
                case 16:
                    levelInt = 6;
                    break;
            }

            TextMeshProUGUI UIText = gameObject.GetComponent<TextMeshProUGUI>();
            // Load custom settings for Data, returns a list
            List<float> loadData = customSettings.LoadObjectiveNum(levelInt-1);
            UIText.text = "";
            if (loadData != null)
            {
                Debug.Log("data: "+loadData[0]);
                // get times scene has run and average from float values.
                float runTimes = loadData.Count;
                float totalCount = 0;
                foreach (float objective in loadData)
                {
                    // Count objectives
                    totalCount += objective;
                    
                    Debug.Log("num: "+objective);
                    UIText.text+=objective+",";
                }

                // Missed objectives = 2*runTimes - objectives
                float missedObjectives = (runTimes*2) - totalCount;
                
                // print values to screen
                UIText.text = "Scene run: "+runTimes+"\nObjectives Indentified: "+totalCount+"\nObjectives Missed: "+missedObjectives;
            }else
            {
                // No data found
                UIText.text = "No Data yet.";
            }
            
        }

    }
}
