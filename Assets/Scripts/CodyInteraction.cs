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


	//---------------------------------------------------
	// settaggi all'avvio
	//---------------------------------------------------

	void Start(){

		// mi assicuro che tutte le variabil di interazione siano a zero (idle state in )
		animator = GetComponent <Animator> ();
		animator.SetBool ("lookEinstein", false);
		animator.SetBool ("lookSerena", false);
		//GetComponent<AudioSource>().PlayOneShot(CodyIdle);
	}

	//---------------------------------------------------
	// inizio interazione audio (play one time)
	//---------------------------------------------------
	void OnTriggerEnter (Collider other) {
		if (other.gameObject.name == "einstein") {
			GetComponent<AudioSource>().PlayOneShot(CodyToEinstein);
			// attivo animazione per Cody verso Einstein
			animator.SetBool ("lookEinstein", true);
		}
		if (other.gameObject.name == "serena") {
			GetComponent<AudioSource>().PlayOneShot(CodyToSerena);
			// attivo animazione per Cody verso Serena
			animator.SetBool ("lookSerena", true);
		}
	}


	//---------------------------------------------------
	// inizio interazione
	//---------------------------------------------------
	void OnTriggerStay (Collider other) {


		//-------------------------------
		// interazione Cody <-> Einstein
		//-------------------------------

		if (other.gameObject.name == "einstein") {

			// gurda Einstein
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

			// gurda Serena
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
		Debug.Log ("Uscito");

		// resetto tutte le trasformazioni
		transform.localRotation = Quaternion.identity;
		transform.localPosition = Vector3.zero;
		transform.localScale = Vector3.one;

		// setto tutte le variabili di inerazione a zero
		animator.SetBool ("lookEinstein", false);
		animator.SetBool ("lookSerena", false);

		// rilancio audio no interazione
		GetComponent<AudioSource>().PlayOneShot(CodyIdle);
	}
}
