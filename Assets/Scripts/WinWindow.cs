using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WinWindow : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    [SerializeField] ParticleSystem particlesSystem;

    bool listen = false;
    float timeCount = 2;

    private void OnEnable()
    {
        listen = false;
        timeCount = 2;
        if (text != null) text.text = "Won";
        StartCoroutine(StartCountDown());
        particlesSystem.Play();
    }

    private void Start()
    {
        if(text == null) text = GetComponent<TMP_Text>();
        if (text == null) Debug.LogError("No text mesh component found on this game object");
    }

    // Update is called once per frame
    void Update()
    {
        if (!listen) { timeCount -= Time.deltaTime; return; }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GamePlayManager.instance.GamePlayState = GamePlayState.Ready;
            gameObject.SetActive(false);
        } 
    }

    IEnumerator StartCountDown()
    {
        while (timeCount > 0)
            yield return null;

        listen = true;
        text.text += "\n\n<size=132px>Press 'Space'\n to \nPlay Again";
    }
}
