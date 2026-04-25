using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FortuneLucky : MonoBehaviour
{
    public List<int> Price;
    public List<AnimationCurve> animationCurves;

    bool isSpinning;
    float anglePerItem;
    int itemNumber;
    int randromTime;

    private void Start()
    {
        isSpinning = false;
        anglePerItem = 360 / Price.Count;

    }
    public void DoSpinnnig()
    {
        if (isSpinning) return;
        randromTime = Random.Range(1, 4);
        itemNumber = Random.Range(0, Price.Count);
        float maxAngle = 160 * randromTime + (itemNumber * anglePerItem);
        StartCoroutine(SpinTheWheel(5 * randromTime, maxAngle));
    }

    private IEnumerator SpinTheWheel(float time, float maxAngle)
    {
        isSpinning = true;
        float timer = 0.0f;
        float startAngle = transform.eulerAngles.z;
        maxAngle = maxAngle - startAngle;

        int animationCurveNumber = Random.Range(0, animationCurves.Count);

        while (timer < time)
        {
            float angle = maxAngle * animationCurves[animationCurveNumber].Evaluate(timer / time);
            transform.eulerAngles = new Vector3(0, 0, angle + startAngle);
            timer += Time.deltaTime;
            yield return null;
        }
        transform.eulerAngles = new Vector3(0, 0, maxAngle + startAngle);
        isSpinning = false;
    }
}
