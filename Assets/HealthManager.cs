using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthManager : MonoBehaviour {
    public int upperLimit = 100;
    public TMP_Text text;
    public IEnumerator damageEffect;
    public IEnumerator currentEffect;

    private PlayerControls pc;

    public int health;

    void Start() {
        health = upperLimit;
        pc = GetComponent<PlayerControls>();
    }

    void Update() {
        text.text = "Life: " + health;

        if (health <= 0) {
            if (currentEffect != null) StopCoroutine(currentEffect);
            health = 100;
            pc.Restart();
        }
    }

    public IEnumerator Spike() {
        Debug.Log(1);
        for (int i = 0; i < 10; i++) {
            health -= 1;
            yield return new WaitForSeconds(0.3f);
        }
    }

    public IEnumerator Poison() {
        for (int i = 0; i < 10; i++) {
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
            // instantly kill the player with overwhelming power
            case "Void": {
                    health -= 999999;
                    break;
                }

            case "Spike": {
                    if (damageEffect != null) {
                        StopCoroutine(damageEffect);
                        if (damageEffect.ToString().Contains("Spike")) break;
                        damageEffect = Spike();
                        StartCoroutine(damageEffect);
                    }
                    break;
                }

            case "Trampoline": {
                    if (pc.queuedJump) {
                        pc.playerVelocity += new Vector3(0, 30, 0);
                    }
                    break;
                }

            case "Poison": {
                    if (currentEffect != null) {
                        StopCoroutine(currentEffect);
                        // don't afflict poison if they're already poisoned
                        if (currentEffect.ToString().Contains("Poison")) break;
                        currentEffect = Poison();
                        StartCoroutine(currentEffect);
                    }
                    break;
                }

            case "Fullheal": {
                    pc.checkpoint = collision.transform.position + new Vector3(0, 2, 0);
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


    private void OnCollisionExit(Collision collision) {
        switch (collision.gameObject.tag) {
            case "Spike": {
                    if (damageEffect != null) {
                        if (damageEffect.ToString().Contains("Spike")) StopCoroutine(damageEffect);
                    }
                    break;
                }
        }
    }
}