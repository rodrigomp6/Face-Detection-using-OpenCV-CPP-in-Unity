using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScare : MonoBehaviour
{
    public InteractionController scrController;
    public PositionAtFaceScreenSpace scrPosCam;

    public Transform PosJump;

    public bool Active;

    public AudioSource JumpScare_Audio;

    public float Speed = 2;

    public GameObject Luz;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Active)
        {
            if (Vector3.Distance(transform.position, PosJump.position) > 2)
            {
                transform.position = Vector3.Lerp(transform.position, PosJump.position, Speed);
            }
            else
            {
                scrPosCam.enabled = false;
                Active = false;
                JumpScare_Audio.Play();
                Luz.SetActive(true);
                Invoke("PanelWin", 2);
            }
        }
    }

    public void PanelWin()
    {
        scrController.PanelWin.SetActive(true);
        scrController.TextWin.text = "Morreu";
    }
}
