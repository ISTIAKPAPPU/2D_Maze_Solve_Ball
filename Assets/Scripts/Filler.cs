using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Serialization;

public class Filler : MonoBehaviour
{
    public Image filler;
    public bool coolingDown;
    public float waitTime = 30.0f;
    private float _remainingTime;
    public Gradient gradient;
    [SerializeField] private float waitingTimeForAnimation;
    public GameObject timerObj;

    public TextMeshProUGUI text;


    // Update is called once per frame
    private void Start()
    {
        coolingDown = true;
        _remainingTime = (int) waitTime;
        text.text = _remainingTime.ToString("0");
        StartCoroutine(WaitForAnimation());
    }

    private void Update()
    {
        if (coolingDown)
        {
            filler.fillAmount -= 1.0f / waitTime * Time.deltaTime;
            _remainingTime -= 1 * Time.deltaTime;
            text.text = _remainingTime.ToString("0");
            filler.color = gradient.Evaluate(filler.fillAmount);
            if (_remainingTime < 0)
            {
                coolingDown = false;
                GameValues.IsPlayerWin = false;
                GameValues.IsGameOver = true;
                CancelInvoke();
                PanelLoader.Instance.OnWinPanelStart();
            }
        }
    }

    private IEnumerator WaitForAnimation()
    {
        while (true)
        {
            if (_remainingTime < 20 && waitingTimeForAnimation > 1f)
            {
                waitingTimeForAnimation = 1;
            }
            else if (_remainingTime < 10 && waitingTimeForAnimation > .5f)
            {
                waitingTimeForAnimation = .5f;
            }

            timerObj.transform.DOPunchScale(new Vector3(.5f, .5f, .5f), .25f);
            yield return new WaitForSeconds(waitingTimeForAnimation);
        }
    }
}