using System;
using UnityEngine;

public class PositionAtFaceScreenSpace : MonoBehaviour
{
    private readonly float[] distances = new float[600];
    private int distancesArrayPos = 0;
    private float averageDistance = 0;
    private Vector3 lastHeadPos;


    void Start()
    {
        
        //_camDistance = Vector3.Distance(Camera.main.transform.position, transform.position);
    }

    void Update()
    {
/*        if (OpenCVFaceDetection.NormalizedFacePositions.Count == 0)
            return;
*/        //transform.position = Camera.main.ViewportToWorldPoint(new Vector3(OpenCVFaceDetection.NormalizedFacePositions[0].x, OpenCVFaceDetection.NormalizedFacePositions[0].y, OpenCVFaceDetection.NormalizedFacePositions[0].z));
        Vector3 headpos = new Vector3(OpenCVFaceDetection.NormalizedFacePositions[0].x*-20, OpenCVFaceDetection.NormalizedFacePositions[0].y*-20, OpenCVFaceDetection.NormalizedFacePositions[0].z*-20);
        transform.position = Vector3.Lerp(transform.position, headpos, Time.deltaTime);
        var distance = Vector3.Distance(lastHeadPos, transform.position);
        lastHeadPos = transform.position;

        float variation = Math.Abs(distance * averageDistance);
        if (variation >= 0.5)
            Debug.Log("fright: " + variation);

        distances[distancesArrayPos] = distance;
        if (distancesArrayPos >= 599)
            distancesArrayPos = 0;
        else
            distancesArrayPos++;
        averageDistance = getAverage();

        

        //Debug.Log("Array lenght: " + distances.Length);
        //Debug.Log("Average distance: " + averageDistance);
        
    }

    private float getAverage()
    {
        if (distances.Length <= 0)
            return 0;
        
        float sum = 0;
        for (int i = 0; i < distances.Length; i++)
            sum += distances[i];
        return sum / distances.Length;
    }
}
