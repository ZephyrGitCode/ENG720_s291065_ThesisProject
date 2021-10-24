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
    public class CustomDataStopAcc
    {
        public List<float> accuracy;

        public CustomDataStopAcc()
        {
            // accuracy list
            accuracy = new List<float>() {};
        }
    }

    [System.Serializable]
    public class CustomDataCollisions
    {
        public List<float> collisions;

        public CustomDataCollisions()
        {
            // accuracy list
            collisions = new List<float>() {};
        }
    }

    public CustomDataStopAcc customDataStopAcc;
    public CustomDataCollisions customDataCollisions;
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
        SaveGame.Save<CustomDataStopAcc>(identifier, customDataStopAcc);
    }

    public void SaveCollision(int objectHit)
    {
        // Called from GamePlayLogic: collides with object, gives int, adds collision
        // 1 = pedestrian, 2 = cars

        // Load previous save data
        List<float> saveCollision = LoadCollisions();
        
        // Append to previous save data
        saveCollision.Add(objectHit);

        // Save custom Data
        SaveGame.Save<CustomDataCollisions>("collisions", customDataCollisions);
    }

    /* Loading Code --------------------------------------------------------- */
    public List<float> LoadAccuracy(string sceneName)
    {
        customDataStopAcc = SaveGame.Load<CustomDataStopAcc>(
            sceneName,
            new CustomDataStopAcc());
        List<float> saveAccuracy = customDataStopAcc.accuracy;
        return saveAccuracy;
    }

    public List<float> LoadCollisions()
    {
        // load collision data
        customDataCollisions = SaveGame.Load<CustomDataCollisions>("collisions", new CustomDataCollisions());
        List<float> saveCollisions = customDataCollisions.collisions;
        return saveCollisions;
    }
    
    List<string> sceneList = new List<string>() {"1_ZebraCrossingSimple", "2_ZebraCrossingAdvanced", "3_StopSignSimple", "4_StopSignAdvanced", "5_RightofWaySimple", "6_RightofWaySimple"};
    
    public List<float> LoadAccuracyNum(int StatNum)
    {
        // Load Custom Data
        customDataStopAcc = SaveGame.Load<CustomDataStopAcc>(
                sceneList[StatNum],
                new CustomDataStopAcc());

        // Load data into list
        List<float> stoppingStats = customDataStopAcc.accuracy;

        // Return list to statistics
        return stoppingStats;
    }

    public List<List<float>> LoadAccuracyAll()
    {
        List<List<float>> stoppingStats = new List<List<float>>();
        foreach (string sceneName in sceneList)
        {
            // foreach scenename in list, get accuracy data, store into list of lists.
            customDataStopAcc = SaveGame.Load<CustomDataStopAcc>(
                sceneName,
                new CustomDataStopAcc());
            stoppingStats.Add(customDataStopAcc.accuracy);
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
