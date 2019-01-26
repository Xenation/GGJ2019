﻿using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace GGJ2019 {
	public class Folder : Element {

		private List<Element> elements = new List<Element>();

		private void Awake() {
			InitElements();
		}

		private void InitElements() {
			foreach (Transform child in transform) {
				Element elem = child.GetComponent<Element>();
				if (elem == null) continue;
				elements.Add(elem);
			}
		}

		public void AddElement(Element elem) {
			elements.Add(elem);
		}

		public void RemoveElement(Element elem) {
			elements.Remove(elem);
		}

		public override string ToString() {
			StringBuilder sb = new StringBuilder();
			sb.Append(name + '\n');
			for (int i = 0; i < elements.Count - 1; i++) {
				sb.Append(elements[i].ToString().Indent(1) + '\n');
			}
			if (elements.Count > 0) {
				sb.Append(elements[elements.Count - 1].ToString().Indent(1));
			}
			return sb.ToString();
		}

	}
}
