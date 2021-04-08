using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InteractionController : MonoBehaviour
{
    public JumpScare scrJump;

    [Header("Timer")]
    public float TimerGame;
    public float TimerMessage;
    private float f_timeMessage;
    private bool b_TimerMessage;
    private float f_timeGame;
    private bool b_TimerGame = true;

    [Header("Vida")]
    public int Life;

    [Header("UI")]
    public Text TextMessage;
    public Text TextTime;
    public Text TextAnswer;
    public Text TextWin;
    public GameObject PanelWin;
    public GameObject PanelGrid;

    [Header("Prefebs")]
    public GameObject PrefebButton;

    [Header("Listas")]
    public List<GameObject> AnswersObjs;
    public List<GameObject> ButtonsInScene;

    [Header("Respostas")]
    public string AnswerActual;
    public string AnswerComplete;

    [Header("Sons")]
    public AudioSource SoundInteraction;
    public AudioSource SoundCorrect;
    public AudioSource SoundError;
    public AudioSource SoundDamage1;
    public AudioSource SoundDamage2;
    public AudioSource SoundDamage3;
    public AudioSource SoundWin;

    [Header("DanoPersonagem")]
    public Renderer RenderPlayer;
    public Material MatInjured1;
    public Material MatInjured2;
    public Material MatInjured3;

    private int i_AneswerActual = 0;

    private string c_AnswerCorrect;

    private void Start()
    {
        f_timeGame = TimerGame;
    }

    void Update()
    {
        RayHit();
        SetTimerMessage();
        SetTimerGame();
    }

    private void RayHit()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                Debug.DrawLine(ray.origin, hit.point);
                if (hit.collider.gameObject.tag == "Interactive")
                {
                    Debug.Log("Colediu no = " + hit.collider.gameObject.name);
                    InteractiveObj scr = hit.collider.gameObject.GetComponent<InteractiveObj>();
                    SetMessage(scr.Message, true);
                }
            }
        }
    }

    private void SetMessage(string message, bool active)
    {
        if (active)
        {
            SoundInteraction.Play();
            TextMessage.gameObject.SetActive(true);
            TextMessage.text = message;
            b_TimerMessage = true;
        }
        else
        {
            TextMessage.gameObject.SetActive(false);
        }
        f_timeMessage = 0;
    }

    private void SetTimerMessage()
    {
        if (b_TimerMessage)
        {
            f_timeMessage += Time.deltaTime;
            if (f_timeMessage >= TimerMessage)
            {
                b_TimerMessage = false;
                f_timeMessage = 0;
                SetMessage("", false);
            }
        }
    }
    private void SetTimerGame()
    {
        if (b_TimerGame)
        {
            f_timeGame -= Time.deltaTime;
            TextTime.text = f_timeGame.ToString("0");
            if (f_timeGame <= 0)
            {
                b_TimerGame = false;
                f_timeGame = TimerGame;
                InstAnswer();
            }
        }
    }

    private void InstAnswer()
    {
        if (i_AneswerActual < AnswersObjs.Count)
        {
            QuestionObj scr = AnswersObjs[i_AneswerActual].GetComponent<QuestionObj>();
            c_AnswerCorrect = scr.AnswerCorret;
            foreach (string str in scr.Answers)
            {
                GameObject inst = Instantiate(PrefebButton, PanelGrid.transform);
                ButtonLetra scrbt = inst.GetComponent<ButtonLetra>();
                scrbt.text.text = str;
                scrbt.scrInteracion = this;
                ButtonsInScene.Add(inst);
            }
        }
        else
        {
            Debug.Log("NAO TEM MAIS PERGUNTAS");
        }
    }
    private void ClearAnswer()
    {
        foreach (GameObject obj in ButtonsInScene)
        {
            Destroy(obj);
        }
        ButtonsInScene.Clear();
    }

    public void ButtonAction(string s)
    {
        if (s == c_AnswerCorrect)
        {
            Debug.Log("Correta");
            SoundCorrect.Play();
            i_AneswerActual++;
            AnswerActual += s;
            TextAnswer.text = AnswerActual;
            ClearAnswer();
        }
        else
        {
            Debug.Log("Errou");
            
            Life--;
            SetMaterialPlayer();
            ClearAnswer();
        }
        f_timeGame = TimerGame;
        b_TimerGame = true;
        WinGame();
    }

    private void WinGame()
    {
        if (AnswerActual == AnswerComplete)
        {
            b_TimerGame = false;
            b_TimerMessage = false;
            PanelWin.SetActive(true);
            TextWin.text = "GANHOU";
            SoundWin.Play();
        }
        else if (Life <= 0)
        {
            //PanelWin.SetActive(true);
            //TextWin.text = "PERDEU";
            //SoundError.Play();
            scrJump.Active = true;
            //Apagar panel e colocar chamada JumpScare
        }
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene("gameplay");
    }

    private void SetMaterialPlayer()
    {
        if (Life == 3)
        {
            RenderPlayer.material = MatInjured1;
            SoundDamage1.Play();
        }
        if (Life == 2)
        {
            RenderPlayer.material = MatInjured2;
            SoundDamage2.Play();
        }
        if (Life == 1)
        {
            RenderPlayer.material = MatInjured3;
            SoundDamage3.Play();
        }
        
    }
}
