//using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.Playables;

public class DialougeManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;

	public TextMeshProUGUI conituneText;

	public Animator animator;

	public bool isOvertalking;

	public GameObject darkArrow;

    private Queue<string> cumleler;
	public GameObject talkBox;

	public AudioSource click;

	ParticleSystem yikil1, yikil2, yikil3, yikil4;
    void Start()
    {
		talkBox.SetActive(false);
		isOvertalking = false;
		cumleler = new Queue<string>();
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
			EndDialogue();
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

	void EndDialogue()
	{
		talkBox.SetActive(false);

		mcScript mc = GameObject.FindWithTag("Player").GetComponent<mcScript>();
        if (mc.ilkGorev)
        {
			GameObject.FindWithTag("Player").GetComponent<mcScript>().ikinciGorev = true;
			GameObject.Find("2ndAnim").GetComponent<PlayableDirector>().Play();

			StartCoroutine(bekleAz());

			GameObject.Find("MissionText").GetComponent<TextMeshProUGUI>().text =
				"Find Saffron and talk to her.";
			mc.ilkGorev = false;
			mc.ikinciGorev = true;
			mc.goSaffron = true;
			mc.goPepper = mc.goWP = false;

			GameObject.FindWithTag("Player").GetComponent<mcScript>().hiz = 4;
		}
        else if (mc.ikinciGorev)
        {
			mc.ikinciGorev = false;
			mc.bekleUla(1);
			mc.ucuncuGorev = true;
			mc.goPepper = mc.goWP = mc.goSaffron = false;

			GameObject.Find("MissionText").GetComponent<TextMeshProUGUI>().text =
				"Go to the dark forest and talk to Jenny.";

			GameObject.FindWithTag("Player").GetComponent<mcScript>().hiz = 4;
		}
        else if (mc.ucuncuGorev)
        {
			mc.ucuncuGorev = false;
			mc.bekleUla(1);
			mc.dorduncuGorev = true;
			mc.goBackkDungeon = true;
			StartCoroutine(waitBE());

			GameObject.Find("MissionText").GetComponent<TextMeshProUGUI>().text 
				= "Go to the dark forest dungeon and kill slime king.";

			GameObject.FindWithTag("Player").GetComponent<mcScript>().hiz = 4;
		}
        else if (mc.dorduncuGorev)
        {
			mc.dorduncuGorev = false;
			mc.bekleUla(2);
			mc.besinciGorev = true;
			mc.goPepper = true;
			mc.goWP = mc.goSaffron = false;

			GameObject.Find("MissionText").GetComponent<TextMeshProUGUI>().text = "Find Pepper and talk to her.";

			GameObject.FindWithTag("Player").GetComponent<mcScript>().hiz = 4;
		}
        else if (mc.besinciGorev)
        {
			mc.besinciGorev = false;
			mc.bekleUla(2);
			mc.altincigorev = true;
			mc.goPepper = mc.goWP = mc.goSaffron = false;

			GameObject.Find("MissionText").GetComponent<TextMeshProUGUI>().text = "Go to the dark forest and clear the field.";

			GameObject.FindWithTag("Player").GetComponent<mcScript>().hiz = 4;
		}
        else if(mc.altincigorev)
        {
			mc.altincigorev = false;
			mc.bekleUla(2);
			mc.yedincigorev = true;
			mc.goWP = true;
			mc.goPepper = mc.goSaffron = false;

			GameObject.Find("MissionText").GetComponent<TextMeshProUGUI>().text = "Go to the Weapon dealer and talk to him.";

			GameObject.FindWithTag("Player").GetComponent<mcScript>().hiz = 4;
		}
        else if (mc.yedincigorev)
        {
			Instantiate(darkArrow, GameObject.FindWithTag("Player").transform.position, Quaternion.identity);

			mc.yedincigorev = false;
			mc.bekleUla(2);
			mc.sekizincigorev = true;
			mc.goSaffron = true;
			mc.ikidn = true;
			mc.goPepper = mc.goWP = false;

			GameObject.Find("MissionText").GetComponent<TextMeshProUGUI>().text = "Go to the Saffron and talk to her.";

			GameObject.FindWithTag("Player").GetComponent<mcScript>().hiz = 4;
		}
        else if (mc.sekizincigorev)
        {
			mc.sekizincigorev = false;
			mc.bekleUla(2);
			mc.dokuzuncugorev = true;
			mc.goBackkDungeon = true;
			mc.goPepper = mc.goWP = mc.goSaffron = false;
			GameObject.Find("MissionText").GetComponent<TextMeshProUGUI>().text = "Go to the dark forest dungeon and kill necromancer.";

			GameObject.FindWithTag("Player").GetComponent<mcScript>().hiz = 4;
		}
        else if (mc.dokuzuncugorev)
        {
			mc.dokuzuncugorev = false;
			GameObject.Find("Necromancer").GetComponent<Necromancer>().talkisover = true;
			mc.onuncugorev = true;

			GameObject.Find("MissionText").GetComponent<TextMeshProUGUI>().text = "Kill the necromancer.";

			GameObject.FindWithTag("Player").GetComponent<mcScript>().hiz = 4;
		}
        else if (mc.onuncugorev)
        {
			mc.onuncugorev = false;
			mc.onbirincigorev = true;
			StartCoroutine(waitBE());
			mc.goSaffron = true;
			mc.goPepper = mc.goWP = false;

			GameObject.Find("MissionText").GetComponent<TextMeshProUGUI>().text = "Go to the Saffron and talk to her.";

			GameObject.FindWithTag("Player").GetComponent<mcScript>().hiz = 4;
		}
        else if (mc.onbirincigorev)
        {
			mc.onbirincigorev = false;
			mc.bekleUla(1);
			mc.onikincigorev = true;
			mc.goBackkDungeon = true;
			mc.jennyActive = true;
			mc.goPepper = mc.goWP = mc.goSaffron = false;

			GameObject.Find("MissionText").GetComponent<TextMeshProUGUI>().text = "Go to the dark forest dungeon.";

			GameObject.FindWithTag("Player").GetComponent<mcScript>().hiz = 4;
		}
        else if (mc.onikincigorev)
        {
			mc.onikincigorev = false;
			GameObject.Find("Jenny").GetComponent<Jenny>().talkover = true;
			GameObject.Find("Jenny").GetComponent<Jenny>().DestroyCollider();
			mc.mcAttackJenny = true;
			mc.onUcuncuGorev = true;

			GameObject.Find("MissionText").GetComponent<TextMeshProUGUI>().text = "Kill Jenny!";

			GameObject.FindWithTag("Player").GetComponent<mcScript>().hiz = 4;
		}
        else if (mc.onUcuncuGorev)
        {
			GameObject.Find("MissionText").GetComponent<TextMeshProUGUI>().text = "";
			StartCoroutine(SonBekleme());
        }
	}


	IEnumerator SonBekleme()
	{
		mcScript mc = GameObject.FindWithTag("Player").GetComponent<mcScript>();
		mc.onUcuncuGorev = false;
		Saffron sf = GameObject.Find("Saffron").GetComponent<Saffron>();
		Instantiate(sf.atesTopu, sf.transform.position, Quaternion.identity);
		sf.atesParticle.Play();

		yield return new WaitForSeconds(1);

		sf.anim.SetTrigger("goBack");
		sf.GetComponent<SpriteRenderer>().sprite = sf.spUp;

		GameObject.Find("Dn").GetComponent<Animator>().SetTrigger("yiki");
		GameObject.Find("Particle System (1)").GetComponent<ParticleSystem>().Play();
		GameObject.Find("Particle System (2)").GetComponent<ParticleSystem>().Play();
		GameObject.Find("Particle System (3)").GetComponent<ParticleSystem>().Play();
		GameObject.Find("Particle System (4)").GetComponent<ParticleSystem>().Play();
		cameraShake.Instance.CamShake(3, 3.5f, 12);

		yield return new WaitForSeconds(7);

		GameObject.Find("Jenny").GetComponent<Animator>().SetTrigger("dus");
		mc.animator.SetTrigger("die");

		yield return new WaitForSeconds(6);

		Time.timeScale = 0;
		mc.deathPanel.SetActive(true);
	}
	IEnumerator waitBE()
    {
		yield return new WaitForSeconds(2);
		GameObject.FindWithTag("Player").GetComponent<mcScript>().talkJenny2ndisOver = true;
	}
	public void onPointEnter()
    {
		conituneText.color = Color.yellow;
    }
	public void onPointEexit()
	{
		conituneText.color = Color.black;
	}
	IEnumerator bekleAz()
    {
		yield return new WaitForSeconds(4);

		GameObject.Find("Saffron").GetComponent<Saffron>().PressImage.color =
			GameObject.Find("Saffron").GetComponent<Saffron>().color;
		GameObject.Find("Saffron").GetComponent<Saffron>().PressText.text = "Press 'E'.";
	}
	IEnumerator bekleAnm(float time)
	{
		yield return new WaitForSeconds(time);
	}
}
