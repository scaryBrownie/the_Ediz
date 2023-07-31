using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.SceneManagement;

public class DialougaMNGiris : MonoBehaviour
{
	public TextMeshProUGUI dialogueText;

	public TextMeshProUGUI conituneText;
	public Animator animator;

	public GameObject talkBox;

	public AudioSource click;

	private Queue<string> cumleler;
	void Start()
	{
		cumleler = new Queue<string>();

		GameObject.Find("Giris").GetComponent<DialougeTrigger>().TriggerDialog();
	}

	public void StartDialouge(Dialouge dialouge)
	{
		talkBox.SetActive(true);
		cumleler.Clear();

		foreach (string cumle in dialouge.cumleler)
		{
			cumleler.Enqueue(cumle);
		}
		DisplayNextCumle();
	}

	public void DisplayNextCumle()
	{
		click.Play();

		if (cumleler.Count == 0)
		{
			conituneText.text = "Starting..";
			bekle(2);
			SceneManager.LoadScene(4);
			talkBox.SetActive(false);
			return;
		}

		string cumle = cumleler.Dequeue();
		StopAllCoroutines();
		StartCoroutine(TypeSentence(cumle));
	}

	IEnumerator TypeSentence(string cumle)
	{
		dialogueText.text = "";
		foreach (char harf in cumle.ToCharArray())
		{
			dialogueText.text += harf;
			yield return null;
		}
	}
	IEnumerator bekle(float time)
    {
		yield return new WaitForSeconds(time);
		animator.SetBool("open", false);
	}

	public void onPointEnter()
	{
		conituneText.color = Color.yellow;
	}
	public void onPointEexit()
	{
		conituneText.color = Color.black;
	}
}
