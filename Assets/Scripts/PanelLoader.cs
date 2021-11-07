using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PanelLoader : MonoBehaviour
{
    // Start is called before the first frame update
    public RectTransform winPanel;

    public static PanelLoader Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void OnWinPanelStart()
    {
        winPanel.gameObject.SetActive(true);
        winPanel.DOAnchorPos(Vector2.zero, 0.25f).SetDelay(.5f);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}