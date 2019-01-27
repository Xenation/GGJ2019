using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ2019 {
	public class GridIcon : MonoBehaviour {

		//Tells if the player can go on this space
		public bool isCrossable = true;

		//Tells if the current Icon is selected by the user
		[System.NonSerialized] public bool isSelected = false;

		public bool isFolder;
		public string ext;
		public Element element;

	}
}
