using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class ScreamBehavior : MonoBehaviour
{
    AudioSource audioSource;
    public Camera cameraTeste;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            RaycastHit hit;
            Ray ray = cameraTeste.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit.transform.name);
                if (hit.transform.name == "Sphere") {
                    audioSource.Play();
                };
            }
        }
    }

    void FixedUpdate()
    {
        Ray ray = cameraTeste.ScreenPointToRay(Vector3.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100))
            Debug.DrawLine(ray.origin, hit.point);
    }

}

