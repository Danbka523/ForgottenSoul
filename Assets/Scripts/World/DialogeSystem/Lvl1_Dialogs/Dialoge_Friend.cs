using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class Dialoge_Friend : MonoBehaviour
{
    List<string> dialogs = new List<string>();
    List<string> speakers = new List<string>();
    [SerializeField]
    TextMeshProUGUI speaker;
    [SerializeField]
    TextMeshProUGUI speaker_text;
    float delay=0.02f;

    [SerializeField]
    GameObject dialoge;

    [SerializeField]
    PlayerMovement playerMovement;

    [SerializeField]
    GameObject player_trigger;

    bool isTriggered;
    private void Awake()
    {
        speaker.text = "";
        speaker_text.text = "";
        speakers.Add("Друг");
        speakers.Add("Игрок");
        dialogs.Add("Извини, что так внезапно вызвал. Надеюсь, не отвлекаю от чего-то важного, у тебя же сейчас перерыв?");
        dialogs.Add("Да, не извиняйся, ты же знаешь, что я готов выслушать тебя когда угодно. Или мне стоит обращаться на «вы» как к старшему по званию?");
        dialogs.Add("Не переживай из-за этого. Никогда не стесняйся обращаться ко мне, если возникнут вопросы. А сейчас я хотел лично передать кое-что. Ситуация в корпорации после изменения структуры отделов и снижения требований к трудоустройству весьма нестабильная, так что если заметишь какие-то странности, например, долгое отсутствие на работе коллег или непонятные действия с их стороны, дай мне знать.");
        dialogs.Add("Хм, но зачем?");
        dialogs.Add("Мне необходимо быть осведомлённым о том, что происходит в твоем отделе.Я знаю, как для тебя важна эта работа, поэтому если что - то пойдёт не так, постараюсь помочь, чем смогу, возможно, даже порекомендую тебя для повышения.Это все, что я хотел сказать.Сейчас можешь идти.");
        dialogs.Add("Благодарю, мистер руководитель. Ваша добродетель не будет забыта мною.");
        dialogs.Add("Ты так говоришь в издёвку? Иди давай, перерыв скоро кончится.");

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
        yield return new WaitForSeconds(3f);
        playerMovement.isPaused = false;
        player_trigger.SetActive(true);
        dialoge.SetActive(false);

    }
}
