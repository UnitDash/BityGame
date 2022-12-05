using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AmongOSLoginScreen : MonoBehaviour
{
    public PassCode[] PassCodes;
    public AmongOS Os;
    public GameObject[] LoginObjects;
    public TextMeshPro CraftOS;
    [Header("Sounds")]
    public AudioClip StartupSound;
    public AudioClip LoginSound;
    public AudioClip CorrectPassSound;
    public AudioClip WrongPassSound;

    private IEnumerator IType;
    [System.Serializable]
    public struct PassCode
    {
        public int CorrectPassCode;
        public TextMeshPro PassCodeText;
    }

    private void Start()
    {
        StartCoroutine(IStartOS());
    }

    public IEnumerator IStartOS()
    {
        Os.Source.clip = StartupSound;
        Os.Source.Play();
        for (int i = 0; i < LoginObjects.Length; i++)
        {
            LoginObjects[i].SetActive(false);
        }
        CraftOS.gameObject.SetActive(true);
        yield return new WaitForSeconds(StartupSound.length);
        CraftOS.gameObject.SetActive(false);

        StartCoroutine(ILogin());
    }

    public IEnumerator ILogin()
    {

        for (int i = 0; i < LoginObjects.Length; i++)
        {
            LoginObjects[i].SetActive(true);
        }

        List<int> Codes = new List<int>();
        for(int i = 0; i < PassCodes.Length; i++)
        {
            GUIController.Instance.OneFrameText(GUIController.Instance.InputsText, "Enter the code");
            bool MoveNext =false;
            IType = IReadyToType(PassCodes[i].PassCodeText);
            StartCoroutine(IType);
            while (!MoveNext)
            {
                GUIController.Instance.OneFrameText(GUIController.Instance.InputsText, "Enter the code");
                string input = Input.inputString;
                int code;
                if(int.TryParse(input,out code))
                {
                    StopCoroutine(IType);
                    code = code % 10;
                    Codes.Add(code);
                    PassCodes[i].PassCodeText.text = code.ToString();
                    MoveNext = true;
                }

                yield return null;
            }
        }

        bool Correct = true;
        for(int i = 0; i < PassCodes.Length; i++)
        {
            if(Codes[i] != PassCodes[i].CorrectPassCode)
            {
                Correct = false;
            }
        }

        
        if (!Correct)
        {
            yield return new WaitForSeconds(1f);
            Os.Source.clip = WrongPassSound;
            Os.Source.Play();
            for (int i = 0; i < PassCodes.Length; i++)
            {
                PassCodes[i].PassCodeText.text = "";
            }

            StartCoroutine(ILogin());
        }
        else
        {
            yield return new WaitForSeconds(0.5f);
            Os.Source.clip = CorrectPassSound;
            Os.Source.Play();
            for (int i = 0; i < LoginObjects.Length; i++)
            {
                LoginObjects[i].SetActive(false);
            }

            yield return new WaitForSeconds(4f);
            Os.LoggedIn = true;
            Os.Source.clip = LoginSound;
            Os.Source.Play();
        }
        
    } 

    public IEnumerator IReadyToType(TextMeshPro Text)
    {
        while (true)
        {
            Text.text = "|";
            yield return new WaitForSeconds(0.5f);
            Text.text = "";
            yield return new WaitForSeconds(0.5f);
        }
    }
}
