using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class ScoreSet : MonoBehaviour
{
    public static int countHits;

    [SerializeField] private TextMeshProUGUI tmpCounterHits;

    private void Start()
    {
        SetDamage.OnSetDamage += IncreaseScore;
        tmpCounterHits.enabled = !tmpCounterHits.enabled;
    }
    private void OnDestroy()
    {
        SetDamage.OnSetDamage -= IncreaseScore;
    }

    public void IncreaseScore()
    {
        countHits++;
        tmpCounterHits.text = countHits.ToString();
        if (tmpCounterHits.enabled)
        {
        }
        else
        {
            tmpCounterHits.enabled = !tmpCounterHits.enabled;
        }
    }
}
