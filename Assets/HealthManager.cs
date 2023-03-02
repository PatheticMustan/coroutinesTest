using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthManager : MonoBehaviour {
    public int upperLimit = 100;
    public TMP_Text text;

    private int health;

    void Start() {
        health = upperLimit;
        text.text = health + "";

        IEnumerator a = Waaah();
        StartCoroutine(a);
    }

    IEnumerator Waaah() {
        while (health < upperLimit) {
            health++;
            text.text = health + "";
            yield return new WaitForSeconds(0.05f);
        }

    }
}