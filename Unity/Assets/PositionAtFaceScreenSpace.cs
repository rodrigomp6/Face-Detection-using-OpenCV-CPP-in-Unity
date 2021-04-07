using System;
using UnityEngine;

public class PositionAtFaceScreenSpace : MonoBehaviour
{
    private readonly float[] distances = new float[300];
    private int distancesArrayPos = 0;
    private float averageDistance = 0;
    private Vector3 lastHeadPos;
    private bool scareEnabled = false;
    AudioSource audioSource;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (OpenCVFaceDetection.NormalizedFacePositions.Count == 0)
            return;
                Vector3 headpos = new Vector3(OpenCVFaceDetection.NormalizedFacePositions[0].x*-20, OpenCVFaceDetection.NormalizedFacePositions[0].y*-20, OpenCVFaceDetection.NormalizedFacePositions[0].z);
                transform.position = Vector3.Lerp(transform.position, headpos, Time.deltaTime);
                calculateScare(headpos);
    }

    private void calculateScare(Vector3 headPosition)
    {
        var distance = Vector3.Distance(lastHeadPos, headPosition);
        lastHeadPos = transform.position;
        float variation = Math.Abs(distance * averageDistance);
        if (variation >= .8 && (!audioSource.isPlaying) && scareEnabled)
        {
            audioSource.Play();
        }
        distances[distancesArrayPos] = distance;
        if (distancesArrayPos >= 299)
        {
            Debug.Log("Scare enabled");
            scareEnabled = true;
            distancesArrayPos = 0;
        }
        else
            distancesArrayPos++;
        averageDistance = getAverageDistance();
    }

    private float getAverageDistance()
    {
        if (distances.Length <= 0)
            return 0;
        float sum = 0;
        for (int i = 0; i < distances.Length; i++)
            sum += distances[i];
        return sum / distances.Length;
    }
}
