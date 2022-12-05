using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    public Animator FadeAnimator;

    private void OnEnable()
    {
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !GameController.Instance.EndingLevel)
        {
            Resume();
        }
    }

    public void MainMenu()
    {
        StartCoroutine(ILoadScene("MainMenu"));
    }
    public void Restart()
    {
        StartCoroutine(ILoadScene("Spot"));
    }

    public void Resume()
    {
        GameController.Instance.Paused = false;
    }
    public IEnumerator ILoadScene(string Scene)
    {
        GameController.Instance.EndingLevel = true;
        FadeAnimator.SetTrigger("FadeOut");
        yield return new WaitForSecondsRealtime(1f);
        SceneManager.LoadScene(Scene);
    }
}
