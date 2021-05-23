using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Cinemachine;

public class DialogueManager : MonoBehaviour {

	public Text nameText;
	public Text dialogueText;
	public Dialogue dialogue;
	public string sceneName;
	//public Animator animator;

	private Queue<string> sentences;
	int dialogueCount=-1;
	public CinemachineVirtualCamera[] cameras;
	public CinemachineVirtualCamera homeCamera;
	public Canvas dialogueCard;
        
	// Use this for initialization
	void Start () {

		sentences = new Queue<string>();
		StartDialogue(dialogue);
		//animator.SetBool("IsOpen", false);
	}

	public void StartDialogue (Dialogue dialogue)
	{
	//	animator.SetBool("IsOpen", true);

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
		changeCamera();
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
		if (dialogueCard !=null){
		dialogueCard.transform.GetChild(0).gameObject.SetActive(false);
		//.GetComponent<SpriteRenderer>().enabled = false;
		//dialogueCard.enabled=false;
	    homeCamera.gameObject.SetActive(true);
        cameras[cameras.Length - 1].gameObject.SetActive(false);

		}
		
	}
	void changeCamera(){
		if (cameras!=null || homeCamera!=null){
			if (dialogueCount>=0){
				cameras[dialogueCount+1].gameObject.SetActive(true);
	        	cameras[dialogueCount].gameObject.SetActive(false);
			}
			dialogueCount+=1;			
		}
		
	}


}
