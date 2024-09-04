using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    public GameObject dialoguePanel;
    public Text dialogueText;
    public string[] dialogue;
    private int index;

    public GameObject contButton;
    public float wordSpeed = 0.05f;
    public bool playerIsClose;

    public GameObject keyIndicator;
    public int maxCharactersPerLine = 100;  // Limite de caracteres por linha

    private bool isTyping;
    private bool skipTyping;

    void Start()
    {
        keyIndicator.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerIsClose)
        {
            if (dialoguePanel.activeInHierarchy)
            {
                zeroText();
            }
            else if (!isTyping)  // Apenas inicie uma nova linha se não estiver digitando
            {
                dialoguePanel.SetActive(true);
                index = 0;
                StartCoroutine(Typing());
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isTyping)
            {
                skipTyping = true;  // Pular o efeito de digitação
            }
            else if (dialogueText.text == dialogue[index])
            {
                NextLine();  // Avançar para a próxima linha se a frase estiver completa
            }
        }

        if (dialogueText.text == dialogue[index])
        {
            contButton.SetActive(true);
        }
    }

    public void zeroText()
    {
        dialogueText.text = "";
        index = 0;
        dialoguePanel.SetActive(false);
    }

    IEnumerator Typing()
    {
        dialogueText.text = "";  // Limpa o texto antes de começar a digitar
        string currentDialogue = dialogue[index];
        
        // Se o diálogo for muito longo, divida em partes
        if (currentDialogue.Length > maxCharactersPerLine)
        {
            currentDialogue = currentDialogue.Substring(0, maxCharactersPerLine) + "...";
        }

        isTyping = true;
        skipTyping = false;

        foreach (char letter in currentDialogue.ToCharArray())
        {
            if (skipTyping)
            {
                dialogueText.text = currentDialogue;
                break;
            }

            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }

        isTyping = false;

        // Depois de digitar o texto, ative o botão de continuar
        if (index < dialogue.Length - 1)
        {
            contButton.SetActive(true);
        }
    }

    public void NextLine()
    {
        contButton.SetActive(false);

        if (index < dialogue.Length - 1)
        {
            index++;
            dialogueText.text = "";
            StartCoroutine(Typing());
        }
        else
        {
            zeroText();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entrou na área do NPC");
            playerIsClose = true;
            keyIndicator.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player saiu da área do NPC");
            playerIsClose = false;
            keyIndicator.SetActive(false);
            zeroText();
        }
    }
}
