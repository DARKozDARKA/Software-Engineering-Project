using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ExitText : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _text;

    private void Start()
    {
        StartCoroutine(ChangeText());
    }

    private IEnumerator ChangeText()
    {
        _text.text = "You will leave this place in 5...";
        yield return new WaitForSeconds(1);
        _text.text = "You will leave this place in 4...";
        yield return new WaitForSeconds(1);
        _text.text = "You will leave this place in 3...";
        yield return new WaitForSeconds(1);
        _text.text = "You will leave this place in 2...";
        yield return new WaitForSeconds(1);
        _text.text = "You will leave this place in 1...";
        yield return new WaitForSeconds(1);
        Application.Quit();
    }
}
