using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PanelLoadingBar : MonoBehaviour
{
    [SerializeField] Image imgFill;
    [SerializeField] TextMeshProUGUI txtFillPercent;

    private static bool isGameLoaded = false;
    float fillValue;
    private void Awake()
    {
        if (isGameLoaded) Destroy(gameObject);
        imgFill.fillAmount = 0;
        imgFill.fillAmount = 0;

        StartCoroutine(StartLoading());
    }

    public IEnumerator StartLoading()
    {
        while (true)
        {
            float randValue = Random.Range(.01f, .1f);
            yield return new WaitForSecondsRealtime(randValue);
            fillValue += 1f;
            if (fillValue > 100)
            {
                StartCoroutine(OnLoadingComplete());
                yield break;
            }
            imgFill.fillAmount = fillValue / 100f;
            txtFillPercent.text = fillValue + "%";
        }
    }

    public IEnumerator OnLoadingComplete()
    {
        isGameLoaded = true;
        yield return new WaitForSeconds(.2f);
        Destroy(this.gameObject);
    }
}
