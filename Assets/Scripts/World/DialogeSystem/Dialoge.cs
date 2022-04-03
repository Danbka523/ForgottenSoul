using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class Dialoge : MonoBehaviour
{
    List<string> dialogs = new List<string>();
    List<string> speakers = new List<string>();
    [SerializeField]
    TextMeshProUGUI speaker;
    [SerializeField]
    TextMeshProUGUI speaker_text;
    [SerializeField]
    float delay;

    [SerializeField]
    Animator fade;
    [SerializeField]
    GameObject card;

    //bool is_end = false;
    private void Start()
    {
        speakers.Add("Игрок");
        speakers.Add("Телевизор");
        dialogs.Add("И что это было?");
        dialogs.Add("Корпорация Ascension. В основном занята разработками в сфере искусственного интеллекта и биотехнологий. Одной из ее самых перспективных технологий, все еще находящихся в стадии разработки, являются чипы поведенческого моделирования, позволяющие создавать ии, своим поведением неотличимый от человека.");
        dialogs.Add("Ах... Неважно... Пора собираться.");
        DisplayDialog(dialogs, 0);
        
    }

    private void DisplayDialog(List<string> dialogs, int idx) {
        if (idx < dialogs.Count)
        {
            StartCoroutine(display_text(dialogs[idx], idx));
        }
        else
            StartCoroutine("display_delay_fade");
    }

    private void Update() {
        if (Input.anyKey)
            delay = 0;
    }

    IEnumerator display_text(string str,int i) {
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

    IEnumerator display_delay_fade() { 
        yield return new WaitForSeconds(3);
        fade.Play("fadeIn");
        yield return new WaitForSeconds(2);
        fade.Play("fadeOut");
        card.SetActive(true);
        yield return new WaitForSeconds(3);
        fade.Play("fadeIn");
        card.SetActive(false);
        SceneManager.LoadScene(3);
    }
}
