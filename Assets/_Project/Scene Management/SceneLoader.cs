using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
using Sirenix.OdinInspector;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private Image screen;
    [SerializeField] private GraphicRaycaster raycaster;
    [SerializeField] private float fadeTime;
    [SerializeField] private bool fadeIn;
    [SerializeField] private bool fadeOut;
    [ShowIf("fadeIn")]
    [SerializeField] private CustomGameEvent OnFadeInEnd;

    private void Start()
    {
        if (fadeIn)
        {
            screen.color = new Color(screen.color.r, screen.color.g, screen.color.b, fadeTime);
            screen.DOFade(0, fadeTime).OnComplete(() =>
            {
                raycaster.enabled = false;
                OnFadeInEnd.Invoke(this, null);
            }
            );
        }

        else
        {
            raycaster.enabled = false;
        }
    }

    public void LoadScene(Component sender, object data)
    {
        int index = (int)data;
        SwitchScenes(index);
    }

    public void RestartScene()
    {
        int index = SceneManager.GetActiveScene().buildIndex;
        SwitchScenes(index);
    }

    public void SwitchScenes(int sceneIndex)
    {
        raycaster.enabled = true;

        if (fadeOut)
        {
            screen.DOFade(1, fadeTime).OnComplete(
                () => SceneManager.LoadSceneAsync(sceneIndex)
                );
        }
        else
        {
            SceneManager.LoadSceneAsync(sceneIndex);
        }
    }

    public void ExitApplication()
    {
        Application.Quit();
    }
}
