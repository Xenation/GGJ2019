using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GGJ2019 {
	public class IconGrid : MonoBehaviour
	{
		public int height = 10, width = 12;
		public Canvas canva;
		private float stepVertical, stepHorizontal;

		public List<List<GridIcon>> grid;

		// Start is called before the first frame update
		void Start()
		{
			//Instanciate grid
			grid = new List<List<GridIcon>>(height);
			for(int i = 0; i < height; i++)
			{
				grid[i] = new List<GridIcon>(width);
			}

			//Step for positions of the icons
			//+2 for space on each extremity
			stepVertical = Screen.height / (height + 2);
			stepHorizontal = Screen.width / (width + 2);
		}

		// Update is called once per frame
		void Update()
		{
			DrawGrid();
		}

		private void DrawGrid()
		{
        
		}

		//Returns a vector containing both steps
		public Vector2 GetStep()
		{
			return new Vector2(stepVertical, stepHorizontal);
		}
	}
}
