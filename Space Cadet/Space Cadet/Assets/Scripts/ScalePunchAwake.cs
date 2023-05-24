using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ScalePunchAwake : MonoBehaviour
{
    [SerializeField] Vector3 punchVal;
    [SerializeField] float durationVal, elasticityVal;
    [SerializeField] int vibrato;

    private void Awake() {
        //DOTween.Init();
        //transform.DOPunchScale(punchVal, durationVal, vibrato, elasticityVal);
    }
    public void PunchScaleMe()
    {
       transform.DOPunchScale(punchVal, durationVal, vibrato, elasticityVal); 
    }
}
