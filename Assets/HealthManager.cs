using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthManager : MonoBehaviour {
    public int upperLimit = 100;
    public TMP_Text text;
    public IEnumerator currentEffect;

    public int health;

    void Start() {
        health = upperLimit;
    }

    void Update() {
        text.text = "Life: " + health;
    }

    public IEnumerator Spike() {
        health -= 10;
        yield return null;
    }

    public IEnumerator Poison() {
        for (int i=0; i<10; i++) {
            health -= 2;
            yield return new WaitForSeconds(1);
        }
    }

    public IEnumerator Fullheal() {
        while (health < upperLimit) {
            health += 1;
            yield return new WaitForSeconds(0.03f);
        }
    }

    public IEnumerator Smallheal() {
        health += 10;
        yield return null;
    }

    private void OnCollisionEnter(Collision collision) {
        switch (collision.gameObject.tag) {
            case "Spike": {
                    IEnumerator a = Spike();
                    StartCoroutine(a);
                    break;
            }

            case "Poison": {
                    if (currentEffect != null) StopCoroutine(currentEffect);
                    currentEffect = Poison();
                    StartCoroutine(currentEffect);
                    break;
            }

            case "Fullheal": {
                    if (currentEffect != null) StopCoroutine(currentEffect);
                    currentEffect = Fullheal();
                    StartCoroutine(currentEffect);
                    break;
            }

            case "Smallheal": {
                    IEnumerator a = Smallheal();
                    StartCoroutine(a);
                    break;
            }
        }
    }
}