using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ZeroToHero : MonoBehaviour {
    public int upperLimit = 100;
    public float delay = 0.05f;
    public TMP_Text text;

    private int value;

    void Start() {
        value = 0;
        text.text = value + "";

        IEnumerator a = Waaah();
        StartCoroutine(a);
    }

    IEnumerator Waaah() {
        while (value < upperLimit) {
            value++;
            text.text = value + "";
            yield return new WaitForSeconds(delay);
        }
       
    }
}