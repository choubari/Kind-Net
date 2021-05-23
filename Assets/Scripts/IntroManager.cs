using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Cinemachine;

public class IntroManager : MonoBehaviour {

	public Text nameText;
	public Text dialogueText;
	public Dialogue dialogue;
	public string sceneName;

	private Queue<string> sentences;

	// Use this for initialization
	void Start () {

		sentences = new Queue<string>();
		StartDialogue(dialogue);
	}

	public void StartDialogue (Dialogue dialogue)
	{
	
		nameText.text = dialogue.name;

		sentences.Clear();

		foreach (string sentence in dialogue.sentences)
		{
			sentences.Enqueue(sentence);
		}

		DisplayNextSentence();
	}

	public void DisplayNextSentence ()
	{
		if (sentences.Count == 0)
		{
        	EndDialogue();
			return;
		}
		string sentence = sentences.Dequeue();
		StopAllCoroutines();
		StartCoroutine(TypeSentence(sentence));
	}

	IEnumerator TypeSentence (string sentence)
	{
		dialogueText.text = "";
		foreach (char letter in sentence.ToCharArray())
		{
			dialogueText.text += letter;
			yield return null;
		}
	}

	void EndDialogue()
	{
		SceneManager.LoadScene(sceneName);
	}
	


}

// Welcome to Kind Net!

// It's a 2D Game that teaches kids the fundamentals of digital citizenship so they can explore the online world with confidence.

// The digital world creates new challenges and opportunities for social interaction, for kids and all the rest of us.

// The Internet is a powerful amplifier that can be used to spread positivity or negativity. 

// -------------------

// Kind Net: The Power of Online Positivity!

// People take the high road by applying the concept of “treat others as you would like to be treated” to their actions online.

// People take the high road by applying the concept of “treat others as you would like to be treated” to their actions online.