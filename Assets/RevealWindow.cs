using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
public class RevealWindow : MonoBehaviour
{
    [Header("Clips")]
    public AudioSource Source;
    public VideoClip StartClip;
    public VideoClip ThirdWinnerClip;
    public VideoClip ThirdWinnerNameClip;
    public VideoClip SecondWinnerClip;
    public VideoClip SecondWinnerNameClip;
    public VideoClip FirstWinnerClip;
    public VideoClip FirstWinnerNameClip;

    private bool Revealed;

    private void Start()
    {
        Revealed = false;
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.X) && !Revealed)
        {
            Revealed = true;
            AmongOS.Instance.DarkScreen.SetActive(true);
            StartCoroutine(IStartVideo());
        }
    }
    public IEnumerator IStartVideo()
    {
        Source.Play();
        yield return new WaitForSeconds(2);
        AmongOS.Instance.RevealVideo.gameObject.SetActive(true);
        AmongOS.Instance.RevealVideo.clip = StartClip;

        AmongOS.Instance.RevealVideo.Prepare();
        while (!AmongOS.Instance.RevealVideo.isPrepared)
            yield return new WaitForEndOfFrame();
        AmongOS.Instance.RevealVideo.frame = 0; //just incase it's not at the first frame

        AmongOS.Instance.RevealVideo.Play();
        yield return new WaitForSeconds((float)AmongOS.Instance.RevealVideo.clip.length);

        while (!Input.GetKeyDown(KeyCode.X))
        {
            yield return null;
        }
        AmongOS.Instance.RevealVideo.clip = ThirdWinnerClip;

        AmongOS.Instance.RevealVideo.Prepare();
        while (!AmongOS.Instance.RevealVideo.isPrepared)
            yield return new WaitForEndOfFrame();
        AmongOS.Instance.RevealVideo.frame = 0; //just incase it's not at the first frame

        AmongOS.Instance.RevealVideo.Play();
        //First Winner
        yield return new WaitForSeconds((float)AmongOS.Instance.RevealVideo.clip.length);
        while (!Input.GetKeyDown(KeyCode.X))
        {
            yield return null;
        }
        AmongOS.Instance.RevealVideo.clip = ThirdWinnerNameClip;

        AmongOS.Instance.RevealVideo.Prepare();
        while (!AmongOS.Instance.RevealVideo.isPrepared)
            yield return new WaitForEndOfFrame();
        AmongOS.Instance.RevealVideo.frame = 0; //just incase it's not at the first frame

        AmongOS.Instance.RevealVideo.Play();

        //Second
        yield return new WaitForSeconds((float)AmongOS.Instance.RevealVideo.clip.length);
        while (!Input.GetKeyDown(KeyCode.X))
        {
            yield return null;
        }

        AmongOS.Instance.RevealVideo.clip = SecondWinnerClip;
        AmongOS.Instance.RevealVideo.Prepare();
        while (!AmongOS.Instance.RevealVideo.isPrepared)
            yield return new WaitForEndOfFrame();
        AmongOS.Instance.RevealVideo.frame = 0; //just incase it's not at the first frame

        AmongOS.Instance.RevealVideo.Play();
        yield return new WaitForSeconds((float)AmongOS.Instance.RevealVideo.clip.length);
        while (!Input.GetKeyDown(KeyCode.X))
        {
            yield return null;
        }
        AmongOS.Instance.RevealVideo.clip = SecondWinnerNameClip;

        AmongOS.Instance.RevealVideo.Prepare();
        while (!AmongOS.Instance.RevealVideo.isPrepared)
            yield return new WaitForEndOfFrame();
        AmongOS.Instance.RevealVideo.frame = 0; //just incase it's not at the first frame

        AmongOS.Instance.RevealVideo.Play();

        //First
        yield return new WaitForSeconds((float)AmongOS.Instance.RevealVideo.clip.length);
        while (!Input.GetKeyDown(KeyCode.X))
        {
            yield return null;
        }

        AmongOS.Instance.RevealVideo.clip = FirstWinnerClip;
        AmongOS.Instance.RevealVideo.Play();

        AmongOS.Instance.RevealVideo.Prepare();
        while (!AmongOS.Instance.RevealVideo.isPrepared)
            yield return new WaitForEndOfFrame();
        AmongOS.Instance.RevealVideo.frame = 0; //just incase it's not at the first frame

        yield return new WaitForSeconds((float)AmongOS.Instance.RevealVideo.clip.length );
        while (!Input.GetKeyDown(KeyCode.X))
        {
            yield return null;
        }
        AmongOS.Instance.RevealVideo.clip = FirstWinnerNameClip;
        AmongOS.Instance.RevealVideo.Play();

        AmongOS.Instance.RevealVideo.Prepare();
        while (!AmongOS.Instance.RevealVideo.isPrepared)
            yield return new WaitForEndOfFrame();
        AmongOS.Instance.RevealVideo.frame = 0; //just incase it's not at the first frame

    }
}
