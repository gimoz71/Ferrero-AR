using UnityEngine;
using System.Collections;


//---------------------------------------------------
// Script interazione Cody Simpson
//---------------------------------------------------

public class CodyInteraction : MonoBehaviour {

	// dichiarazioni variabili ed oggetti

	private Animator animator;
	public Transform targetEinstein;
	public Transform targetSerena;

	//inizializzo interazioni audio
	public AudioClip CodyIdle;
	public AudioClip CodyToEinstein;
	public AudioClip CodyToSerena;

	public GameObject aObject;

	// Nuova gestione clip audio

	public void startCodyIdle () {
		GetComponent<AudioSource>().PlayOneShot(CodyIdle);
	}

	public void startCodyToSerena () {
		GetComponent<AudioSource>().PlayOneShot(CodyToSerena);
	}

	public void startCodyToEinstein () {
		GetComponent<AudioSource>().PlayOneShot(CodyToEinstein);
	}

	//---------------------------------------------------
	// settaggi all'avvio
	//---------------------------------------------------

	void Start(){

		// mi assicuro che tutte le variabil di interazione siano a zero (idle state in )
		animator = GetComponent <Animator> ();
		animator.SetBool ("lookEinstein", false);
		animator.SetBool ("lookSerena", false);
	}


	//---------------------------------------------------
	// inizio interazione audio (play one time)
	//---------------------------------------------------
	void OnTriggerEnter (Collider other) {

		// trovo e stoppo l'audio di non interazione che si avvia con OnTrackingFound
		// disabled per test sui singoli script per personaggio -> aObject.GetComponent<AudioSource> ().Stop ();

		// blocco qualsiasi audio attivo nell'oggetto corrente
		GetComponent<AudioSource>().Stop();

		if (other.gameObject.name == "einstein") {
			// disabled per test sui singoli script per personaggio -> GetComponent<AudioSource>().PlayOneShot(CodyToEinstein);

			// attivo animazione per Cody verso Einstein
			animator.SetBool ("lookEinstein", true);
		}
		if (other.gameObject.name == "serena") {
			// disabled per test sui singoli script per personaggio -> //GetComponent<AudioSource>().PlayOneShot(CodyToSerena);
			// disabled per test sui singoli script per personaggio -> //GetComponent<AudioSource>().Play();
			// attivo animazione per Cody verso Serena
			animator.SetBool ("lookSerena", true);
		}
	}


	//---------------------------------------------------
	// inizio interazione
	//---------------------------------------------------
	void OnTriggerStay (Collider other) {

		// blocco qualsiasi audio attivo nell'oggetto corrente
		//GetComponent<AudioSource>().Stop();

		//-------------------------------
		// interazione Cody <-> Einstein
		//-------------------------------

		if (other.gameObject.name == "einstein") {

			// guarda Einstein
			transform.LookAt(targetEinstein); 

			// script di fix assi di rotazione 
			var rot = transform.localRotation.eulerAngles;
			rot.z = 0f;
			rot.x = 0f;
			transform.localRotation = Quaternion.Euler(rot);
		}


		//-------------------------------
		// interazione Cody <-> Serena 
		//-------------------------------

		if (other.gameObject.name == "serena") {

			// guarda Serena
			transform.LookAt(targetSerena); 

			// script di fix assi di rotazione 
			var rot = transform.localRotation.eulerAngles;
			rot.z = 0f;
			rot.x = 0f;
			transform.localRotation = Quaternion.Euler(rot);
		}
	}


	//---------------------------------------------------
	// fine interazione
	//---------------------------------------------------

	void OnTriggerExit (Collider other) {
		GetComponent<AudioSource>().Stop();
		Debug.Log ("Uscito");

		// resetto tutte le trasformazioni
		//transform.localRotation = Quaternion.identity;
		//transform.localPosition = Vector3.zero;
		//transform.localScale = Vector3.one;

		// setto tutte le variabili di inerazione a zero
		animator.SetBool ("lookEinstein", false);
		animator.SetBool ("lookSerena", false);

		// rilancio audio no interazione
		// disabled per test sui singoli script per personaggio -> GetComponent<AudioSource>().PlayOneShot(CodyIdle);
	}
}