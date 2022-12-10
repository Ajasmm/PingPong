using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountDown : MonoBehaviour
{
    [SerializeField] TMP_Text countDownText;

    float countDownTime;

    // Start is called before the first frame update
    private void Start()
    {
        if(countDownText == null) countDownText = GetComponent<TMP_Text>();
        if (countDownText == null)
        {
            Debug.LogError("No textMeshPro object attached!");
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        countDownTime = 3.99F;
        StartCoroutine(StartCountDown());
    }

    private void Update()
    {
          countDownTime -= Time.deltaTime;
    }

    IEnumerator StartCountDown()
    {
        yield return null;
        while(countDownTime >= 0F)
        {
            countDownText.text = ((int)(countDownTime / 1)).ToString();
            yield return null;
        }
        GamePlayManager.instance.GamePlayState = GamePlayState.Playing;
        gameObject.SetActive(false);
    }
}
