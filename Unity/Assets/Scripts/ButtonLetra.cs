using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonLetra : MonoBehaviour
{
    public InteractionController scrInteracion;

    public Text text;

    public void Action()
    {
        scrInteracion.ButtonAction(text.text);
    }
}
