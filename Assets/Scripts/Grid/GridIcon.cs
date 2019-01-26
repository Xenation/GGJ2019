using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ2019 {
	public class GridIcon : MonoBehaviour {
		//Tells if this is an Icon or an OpenSpace
		public bool isEmpty = true;

		//Tells if the player can go on this space
		public bool isCrossable = true;

		//Tells if this icon is interactable
		public bool isInteractable = false;

		//Tells if the current Icon is selected by the user
		[HideInInspector] public bool isSelected = false;

		private Element element;

		// Start is called before the first frame update
		void Start() {

		}

		// Update is called once per frame
		void Update() {

		}

		public virtual void Action() {

		}
	}
}
