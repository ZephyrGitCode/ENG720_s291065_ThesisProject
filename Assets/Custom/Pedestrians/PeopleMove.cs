using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleMove : MonoBehaviour
{
    [SerializeField] List<Waypoint> path = new List<Waypoint>();
    [SerializeField] [Range(0f,5f)] float speed = 1f;
    Pedestrian pedestrian;

    private void Start() {
        pedestrian = GetComponent<Pedestrian>();
    }

    void OnEnable()
    {
        FindPath();
        ReturnToStart();
        StartCoroutine(FollowPath());
    }

    void FindPath()
    {
        path.Clear();
        GameObject parent = GameObject.FindGameObjectWithTag("Path");

        foreach(Transform child in parent.transform)
        {
            Waypoint waypoint = child.GetComponent<Waypoint>();
            if(waypoint != null)
            {
                path.Add(waypoint);
            }
        }
    }

    void ReturnToStart()
    {
        // spawn them on waypoint parent.
        transform.position = path[0].transform.position;
    }

    void FinishPath(){
        gameObject.SetActive(false);
    }

    IEnumerator FollowPath()
    {
        
        foreach(Waypoint waypoint in path)
        {
            //Debug.Log("Following Path: " + waypoint);
            Vector3 startPosition = transform.position;
            Vector3 endPosition = waypoint.transform.position;
            float travelPercent = 0f;

            transform.LookAt(endPosition);

            while(travelPercent < 1f)
            {
                travelPercent += Time.deltaTime * speed;
                transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
                yield return new WaitForEndOfFrame();
            }
        }
        FinishPath();
    }
}
