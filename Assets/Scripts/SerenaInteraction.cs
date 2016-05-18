using UnityEngine;
using System.Collections;


//---------------------------------------------------
// Script interazione Serena
//---------------------------------------------------

public class SerenaInteraction : MonoBehaviour {

	// dichiarazioni variabili ed oggetti

	private Animator animator;
	public Transform targetCody;
	public Transform targetEinstein;

	//inizializzo interazioni audio
	public AudioClip SerenaIdle;
	public AudioClip SerenaToCody;
	public AudioClip SerenaToEinstein;

	public GameObject aObject;

	//---------------------------------------------------
	// settaggi all'avvio
	//---------------------------------------------------

	void Start(){

		// mi assicuro che tutte le variabil di interazione siano a zero (idle state in )
		animator = GetComponent <Animator> ();
		animator.SetBool ("lookCody", false);
		animator.SetBool ("lookEinstein", false);
	}


	//---------------------------------------------------
	// inizio interazione audio (play one time)
	//---------------------------------------------------
	void OnTriggerEnter (Collider other) {

		// trovo e stoppo l'audio di non interazione che si avvia con OnTrackingFound
		aObject.GetComponent<AudioSource> ().Stop ();

		//blocco qualsiasi audio attivo nell'oggetto corrente
		GetComponent<AudioSource>().Stop();

		if (other.gameObject.name == "cody") {
			GetComponent<AudioSource>().PlayOneShot(SerenaToCody);

			// attivo animazione per Serena verso Cody
			animator.SetBool ("lookCody", true);
		}
		if (other.gameObject.name == "einstein") {
			GetComponent<AudioSource>().PlayOneShot(SerenaToEinstein);

			// attivo animazione per Serena verso Einstein
			animator.SetBool ("lookEinstein", true);
		}
	}


	//---------------------------------------------------
	// inizio interazione
	//---------------------------------------------------
	void OnTriggerStay (Collider other) {


		//-------------------------------
		// interazione Serena <-> Cody
		//-------------------------------

		if (other.gameObject.name == "cody") {

			// guarda Cody
			transform.LookAt(targetCody); 

			// script di fix assi di rotazione 
			var rot = transform.localRotation.eulerAngles;
			rot.z = 0f;
			rot.x = 0f;
			transform.localRotation = Quaternion.Euler(rot);
		}


		//-------------------------------
		// interazione Serena <-> Einstein 
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
	}


	//---------------------------------------------------
	// fine interazione
	//---------------------------------------------------

	void OnTriggerExit (Collider other) {
		GetComponent<AudioSource>().Stop();
		Debug.Log ("Uscito");

		// resetto tutte le trasformazioni
		transform.localRotation = Quaternion.identity;
		transform.localPosition = Vector3.zero;
		transform.localScale = Vector3.one;

		// setto tutte le variabili di inerazione a zero
		animator.SetBool ("lookCody", false);
		animator.SetBool ("lookEinstein", false);

		// rilancio audio no interazione
		GetComponent<AudioSource>().PlayOneShot(SerenaIdle);
	}
}