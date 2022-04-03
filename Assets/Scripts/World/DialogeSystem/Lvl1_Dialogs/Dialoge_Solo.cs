using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dialoge_Solo : MonoBehaviour
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
    GameObject player_trigger;
    [SerializeField]
    GameObject friend;

    bool isTriggered;
    private void Awake()
    {
        speaker.text = "";
        speaker_text.text = "";
        speakers.Add("Друг");
        dialogs.Add("И последнее, зайди ко мне в финансовый отдел");

    }

    private void DisplayDialog(List<string> dialogs, int idx)
    {
        isTriggered = true;
        dialoge.SetActive(true);
        if (idx < dialogs.Count)
        {
            StartCoroutine(display_text(dialogs[idx], idx));
        }
        else
        {
            StartCoroutine("end_dialoge");

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
        playerMovement.isPaused = true;
        if (!isTriggered)
            DisplayDialog(dialogs, 0);
    }


    IEnumerator end_dialoge()
    {
        yield return new WaitForSeconds(3);
        playerMovement.isPaused = false;
        dialoge.SetActive(false);
        friend.SetActive(false);
        player_trigger.SetActive(false);
    }
}
