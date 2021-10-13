using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using BayatGames.SaveGameFree;

public class CustomSettings : MonoBehaviour
{
    LaserPointer laserPointer;
    LineRenderer lineRenderer;

    DrawStatistics drawStatistics;

    public UnityEngine.Material[] MatList;

    // Start is called before the first frame update
    void Awake()
    {
        laserPointer = FindObjectOfType<LaserPointer>();
        LoadPointer();
        drawStatistics = FindObjectOfType<DrawStatistics>();
    }

    private void LoadPointer()
    {
        int matNum = SaveGame.Load<int>("LaserPointerMat");
        if (laserPointer!=null && matNum!=null)
        {
            lineRenderer = laserPointer.GetComponent<LineRenderer>();
            UnityEngine.Material[] mats = lineRenderer.materials;
            mats[0] = MatList[matNum];
            lineRenderer.materials = mats;
        }
    }

    public void SelectMaterial(int matNum)
    {
        if (laserPointer!=null)
        {
            lineRenderer = laserPointer.GetComponent<LineRenderer>();
            UnityEngine.Material[] mats = lineRenderer.materials;
            mats[0] = MatList[matNum];
            lineRenderer.materials = mats;
            SaveGame.Save<int>("LaserPointerMat", matNum);
        }
    }

    /* Custom Data --------------------------------------------------------- */
    [System.Serializable]
    public class CustomData
    {
        public List<float> accuracy;

        public CustomData()
        {
            // accuracy list
            accuracy = new List<float>() {};
        }
    }

    public CustomData customData;
    public string identifier;

    /* Saving code --------------------------------------------------------- */
    public void SaveStop(float accuracy)
    {
        // Called from GamePlayLogic: user stops at sign, takes float as accuracy, saves accuracy against the current scene

        // Get active scene as identifer for save data
        identifier = SceneManager.GetActiveScene().name;

        // Load previous save data
        List<float> saveAccuracy = LoadAccuracy(identifier);

        // Append to previous save data
        saveAccuracy.Add(accuracy);

        // Save custom Data
        SaveGame.Save<CustomData>(identifier, customData);
    }

    /* Loading Code --------------------------------------------------------- */
    public List<float> LoadAccuracy(string sceneName)
    {
        customData = SaveGame.Load<CustomData>(
            sceneName,
            new CustomData());
        List<float> saveAccuracy = customData.accuracy;
        return saveAccuracy;
    }
    
    List<string> sceneList = new List<string>() {"1_ZebraCrossingSimple", "2_ZebraCrossingAdvanced", "3_StopSignSimple", "4_StopSignAdvanced", "5_RightofWaySimple", "6_RightofWaySimple"};
    
    public List<float> LoadAccuracyNum(int StatNum)
    {
        // Load Custom Data
        customData = SaveGame.Load<CustomData>(
                sceneList[StatNum],
                new CustomData());

        // Load data into list
        List<float> stoppingStats = customData.accuracy;

        // Return list to statistics
        return stoppingStats;
    }

    public List<List<float>> LoadAccuracyAll()
    {
        List<List<float>> stoppingStats = new List<List<float>>();
        foreach (string sceneName in sceneList)
        {
            // foreach scenename in list, get accuracy data, store into list of lists.
            customData = SaveGame.Load<CustomData>(
                sceneName,
                new CustomData());
            stoppingStats.Add(customData.accuracy);
        }
        return stoppingStats;
    }

    void MenuStats(float StatNum)
    {
        /*
        // stopping range
        List<List<float>> stoppingStats;
        stoppingStats = LoadAccuracyAll();
        foreach (List<float> numbers in stoppingStats)
        {
            
        }
        drawStatistics.WriteStats();
        //LoadAccuracyAll()
        */

        // Statnum 1 = Stoping range, 2 = Collisions, 3 = Other
        switch(StatNum)
        {
            case 1:
                //load stopping range UI
                break;
            case 2:
                // Collisions
                break;
            case 3:
                // Other
                break;
        
        default:
            Debug.Log("Failure");
            break;
        }
    }

    /* Clearing Data Code --------------------------------------------------------- */
    public void ClearData(int sceneNum) {
        // Clear data for certain scene
    }

    private void Start() {
        //DebugData();
    }

    void DebugData()
    {
        identifier = SceneManager.GetActiveScene().name;
        List<float> saveAccuracy = LoadAccuracy(identifier);
        if (saveAccuracy != null)
        {
            foreach (float number in saveAccuracy)
            {
                Debug.Log("Zep: "+number);
            }
            
        }
    }
}
