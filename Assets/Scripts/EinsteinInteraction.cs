using UnityEngine;
using System.Collections;


//---------------------------------------------------
// Script interazione Einstein
//---------------------------------------------------

public class EinsteinInteraction : MonoBehaviour {

	// dichiarazioni variabili ed oggetti

	private Animator animator;
	public Transform targetCody;
	public Transform targetSerena;

	//inizializzo interazioni audio
	public AudioClip EinsteinIdle;
	public AudioClip EinsteinToCody;
	public AudioClip EinsteinToSerena;



	//---------------------------------------------------
	// settaggi all'avvio
	//---------------------------------------------------

	void Start(){

		// mi assicuro che tutte le variabil di interazione siano a zero (idle state in )
		animator = GetComponent <Animator> ();
		animator.SetBool ("lookCody", false);
		animator.SetBool ("lookSerena", false);
		//GetComponent<AudioSource>().PlayOneShot(CodyIdle);
	}



	//---------------------------------------------------
	// inizio interazione audio (play one time)
	//---------------------------------------------------
	void OnTriggerEnter (Collider other) {
		if (other.gameObject.name == "cody") {
			GetComponent<AudioSource>().PlayOneShot(EinsteinToCody);
			// attivo animazione per Einstein verso Cody
			animator.SetBool ("lookCody", true);
		}
		if (other.gameObject.name == "serena") {
			GetComponent<AudioSource>().PlayOneShot(EinsteinToSerena);
			// attivo animazione per Einstein verso Serena
			animator.SetBool ("lookSerena", true);
		}
	}


	//---------------------------------------------------
	// inizio interazione
	//---------------------------------------------------
	void OnTriggerStay (Collider other) {


		//-------------------------------
		// interazione Einstein <-> Cody
		//-------------------------------

		if (other.gameObject.name == "cody") {

			// gurda Cody
			transform.LookAt(targetCody); 

			// script di fix assi di rotazione 
			var rot = transform.localRotation.eulerAngles;
			rot.z = 0f;
			rot.x = 0f;
			transform.localRotation = Quaternion.Euler(rot);


		}


		//-------------------------------
		// interazione Einstein <-> Serena 
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
		animator.SetBool ("lookCody", false);
		animator.SetBool ("lookSerena", false);

		// rilancio audio no interazione
		GetComponent<AudioSource>().PlayOneShot(EinsteinIdle);
	}
}