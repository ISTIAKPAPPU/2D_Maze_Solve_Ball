using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WinPanel : MonoBehaviour
{
    public TextMeshProUGUI text;
    [SerializeField] private ParticleSystem winParticle;
    [SerializeField] private GameObject timer;

    private void OnEnable()
    {
        AudioManager.Instance.Play(AudioManager.SoundType.Win);
        timer.SetActive(false);
        if (GameValues.IsPlayerWin)
        {
            StartCoroutine(WaitForWinParticles());
            text.text = "You Won!";
        }
        else
        {
            text.text = "You Lose!";
        }
    }

    // private void OnDisable()
    // {
    //     winParticle.Stop();
    // }

    private IEnumerator WaitForWinParticles()
    {
        yield return new WaitForSeconds(1f);
        winParticle.Play();
    }
}