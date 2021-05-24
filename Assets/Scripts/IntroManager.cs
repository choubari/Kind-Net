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
	// sentences for dialogue
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

// --------------------

/*
2- Itʼs important to remind ourselves that behind every username and avatar thereʼs a real person with real feelings, and we should treat them as we would want to be treated. 

1- Whether standing up for others, reporting something hurtful, or ignoring something to keep it from being amplified even more, you have a variety of strategies to choose from depending on the situation. With a little kindness, anyone can make a huge difference in turning bad situations around.

3-
-Use the power of the Internet to spread positivity.
-Stop the spread of harmful or untrue messages by not passing them on to others.
-Respect others’ differences.

-Block mean-spirited or inappropriate behavior online.
-Make an effort to provide support to those being bullied.

*/
