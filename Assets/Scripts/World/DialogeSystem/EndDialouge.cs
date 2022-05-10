using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class EndDialouge : MonoBehaviour
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
    GameObject background;

    [SerializeField]
    Animator fade;
    [SerializeField]
    GameObject card;

    //bool is_end = false;
    private void Start()
    {
        speakers.Add("»грок:");
        speakers.Add("“елевизор:");
        dialogs.Add("*т€желое дыхание*");
        dialogs.Add(" орпораци€ Ascension. ¬ основном зан€та разработками в сфере искусственного интеллекта и биотехнологий. ќдной из ее самых перспективных технологий, все еще наход€щихс€ в стадии разработки, €вл€ютс€ чипы поведенческого моделировани€, позвол€ющие создавать ии, своим поведением неотличимый от человека.");
        dialogs.Add("„то же произошло?");
        DisplayDialog(dialogs, 0);

    }


    private void DisplayDialog(List<string> dialogs, int idx)
    {
        if (idx < dialogs.Count)
        {
            StartCoroutine(display_text(dialogs[idx], idx));
        }
        else
            StartCoroutine("display_delay_fade");
    }

    private void Update()
    {
        if (Input.anyKey)
            delay = 0;
        if (Input.GetKey(KeyCode.Space))
        { 
            StopAllCoroutines();
            StartCoroutine("display_delay_fade");
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

    IEnumerator display_delay_fade()
    {
        yield return new WaitForSeconds(3);
        fade.Play("fadeIn");
        yield return new WaitForSeconds(2);
        fade.Play("fadeOut");
        background.SetActive(false);
        card.SetActive(true);
        //yield return new WaitForSeconds(3);
        //fade.Play("fadeIn");
        //card.SetActive(false);
        //SceneManager.LoadScene(3);
    }
}