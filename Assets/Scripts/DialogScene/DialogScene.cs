using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogScene : MonoBehaviour
{
    private Animator playerAnim, uiAnimator, dialogAnimator;
    private GameObject TapDialog;
    private Text dialogText;
    private Replica[] dialog;
    private int countReply;
    private GameManager gameManager;
    private bool coroutineFinished, coroutineStarted, dialogstarted, dialogfinished, scenefinished;
    public bool scenestarted;
    private PlayerController player;
    private Image icon;

    public Sprite healerIcon, playerIcon;

    public enum DialogParticipants
    {
        Player,
        Healer
    }

    struct Replica
    {
        public DialogParticipants person;
        public string text;
    }

    void Start()
    {
        //PlayerPrefs.DeleteKey("Progress");
        scenefinished = false;
        dialogfinished = false;
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        playerAnim = GameObject.Find("Player").GetComponent<Animator>();
        scenestarted = false;
        dialogstarted = false;
        coroutineFinished = true;
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        TapDialog = GameObject.Find("TapDialog");
        countReply = 0;
        dialog = new Replica[12];
        dialogText = GameObject.Find("DialogText").GetComponent<Text>();
        dialogAnimator = GameObject.Find("Canvas2").GetComponent<Animator>();
        uiAnimator = GameObject.Find("Canvas").GetComponent<Animator>();
        icon = GameObject.Find("DialogIcon").GetComponent<Image>();

        dialog[0].text = "Приветствую тебя, мой юный друг! У меня для тебя плохие новости.";
        dialog[0].person = DialogParticipants.Healer;

        dialog[1].text = "О нет, только не говорите что, что-то случилось с моим младшим братом. Я уже пол дня немогу нигде его найти.";
        dialog[1].person = DialogParticipants.Player;

        dialog[2].text = "Вынужден тебя огорчить, твой брат серьезно поранился в лесу.";
        dialog[2].person = DialogParticipants.Healer;

        dialog[3].text = "Когда я нашел его, тот был без сознания. Он попал в человеческий капкан, будь они прокляты, чертовы живодеры! А все из-за какого-то там модного тренда.";
        dialog[3].person = DialogParticipants.Healer;

        dialog[4].text = "Так погоди-ка, извини, кажется я потерял ход мыслей. Эмм... Ах-да речь шла о твоем брате.";
        dialog[4].person = DialogParticipants.Healer;

        dialog[5].text = "Он сейчас в не очень стабильном состоянии, мягко говоря, и как на зло у меня закончились ингридиенты для целебных прибамбасов.";
        dialog[5].person = DialogParticipants.Healer;

        dialog[6].text = "Ты должен помочь мне кое в чем, что-бы я мог изготовить очередную порцию лекарств для твоего брата.";
        dialog[6].person = DialogParticipants.Healer;

        dialog[7].text = "Хорошо, я готов приступать! Что мне делать?";
        dialog[7].person = DialogParticipants.Player;

        dialog[8].text = "Тебе придется добыть для меня 'корень какого то растения', в нашем лесу, но будь осторожен, по пути тебя будет поджидать много испытаний.";
        dialog[8].person = DialogParticipants.Healer;

        dialog[9].text = "Меня немного настораживает слово 'добудь', надеюсь, мне не придется ни с кем драться?";
        dialog[9].person = DialogParticipants.Player;

        dialog[10].text = "Кто знает, малыш, кто знает... Удачи тебе, и возвращайся по-скорее моих оставшихся лекарств хватит не на долго.";
        dialog[10].person = DialogParticipants.Healer;

        dialog[11].text = "Ааа к черту, надеру зад любому кто встанет у меня на пути!";
        dialog[11].person = DialogParticipants.Player;

        TapDialog.SetActive(false);
    }

    public void PrintText()
    {
        

        if(countReply < 12 && coroutineFinished){
            StartCoroutine(TextCoroutine(dialog[countReply].text));
            if (dialog[countReply].person == DialogParticipants.Healer)
            {
                icon.sprite = healerIcon;
                //icon.color = Color.white;
            }
            else
            {
                icon.sprite = playerIcon;
                //icon.color = Color.black;
            }
            countReply++;
        } else if (countReply == 12) {
            dialogfinished = true;
            dialogAnimator.Play("CloseDialogPanel");
        }
    }

    private void OpenDialog()
    {
        TapDialog.SetActive(true);
        dialogAnimator.Play("OpenDialogPanel");
    }

    IEnumerator TextCoroutine(String text)
    {
        coroutineFinished = false;

        dialogText.text = "";

        foreach(char c in text)
        {
            dialogText.text += c;
            yield return new WaitForFixedUpdate();
        }

        coroutineFinished = true;
    }

    void Update()
    {
        if(scenestarted)
        {
            if(player.transform.position.x < -1.5f)
            {
                player.SetStateWalk();
                player.Move(2.5f);

            } else  {
                if(!dialogstarted && !dialogfinished)
                {
                    dialogstarted = true;
                    OpenDialog();
                    Invoke("PrintText", 1f);
                }
            }

            if(dialogfinished && player.transform.position.x < 5.5f)
            {
                playerAnim.Play("Walk");
                player.SetStateWalk();
                player.Move(2.5f);
            }else
            { }
        }
    }
}
