using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dialoge_Note : MonoBehaviour
{
    List<string> dialogs = new List<string>();
    List<string> speakers = new List<string>();
    [SerializeField]
    TextMeshProUGUI speaker;
    [SerializeField]
    TextMeshProUGUI speaker_text;
    float delay = 0.02f;

    [SerializeField]
    GameObject dialoge;

    [SerializeField]
    PlayerMovement playerMovement;

    [SerializeField]
    GameObject enemies;
    [SerializeField]
    GameObject weapon;
    [SerializeField]
    GameObject inGameUI;

    bool isTriggered;
    private void Awake()
    {
        speaker.text = "";
        speaker_text.text = "";
        speakers.Add("Записка");
        dialogs.Add("Мои опасения подтверждаются. Если это все - таки произойдет, в ящике стола я оставил «инструмент», он определенно пригодится.");

    }

    private void DisplayDialog(List<string> dialogs, int idx)
    {
 
        dialoge.SetActive(true);
        if (idx < dialogs.Count)
        {
            StartCoroutine(display_text(dialogs[idx], idx));
        }
        else
        {
            StartCoroutine(end_dialoge(3));

        }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StopAllCoroutines();
            StartCoroutine(end_dialoge());
        }
    }

    IEnumerator display_text(string str, int i)
    {
        yield return new WaitForSeconds(3f);
        speaker_text.text = "";
        if (i % 2 == 0)
            speaker.text = speakers[0] + ":";
        else
            speaker.text = speakers[1] + ":";
        foreach (var sym in str)
        {
            speaker_text.text += sym;
            yield return new WaitForSeconds(delay);
        }
        DisplayDialog(dialogs, ++i);
    }

    private void OnTriggerEnter(Collider other)
    {
      

        if (!isTriggered)
        {
            playerMovement.isPaused = true;
            DisplayDialog(dialogs, 0);
        }
        isTriggered = true;
    }


    IEnumerator end_dialoge(float delay=0)
    {
        yield return new WaitForSeconds(delay);
        playerMovement.isPaused = false;
        weapon.SetActive(true);
        dialoge.SetActive(false);
        inGameUI.SetActive(true);
        enemies.SetActive(true);
    }
}