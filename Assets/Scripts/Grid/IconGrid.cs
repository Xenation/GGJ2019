using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GGJ2019 {
	public class IconGrid : MonoBehaviour
	{
		public int height = 10, width = 12;
		public Canvas canva;
		
		public RectTransform gridParent;
		public Vector2 cellSize { get; private set; }
		public GridIcon[,] grid;
		
		void Awake()
		{
			//Instanciate grid
			grid = new GridIcon[width, height];
			cellSize = new Vector2(gridParent.rect.size.x / width, gridParent.rect.size.y / height);
			InitGrid();
		}

		private void InitGrid() {
			foreach (RectTransform transf in gridParent) {
				GridIcon icon = transf.GetComponent<GridIcon>();
				if (icon == null) continue;
				Vector2Int gridPos = GetGridPos(transf.position);
				grid[gridPos.x, gridPos.y] = icon;
				transf.position = GetCanvasPos(gridPos);
				transf.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, cellSize.x);
				transf.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, cellSize.y);
			}
		}

		public Vector2Int GetGridPos(Vector2 canvasPos) {
			return new Vector2Int((int) (canvasPos.x / cellSize.x), (int) (canvasPos.y / cellSize.y));
		}

		public Vector2 GetCanvasPos(Vector2Int gridPos) {
			return new Vector2(gridPos.x * cellSize.x + cellSize.x / 2f, gridPos.y * cellSize.y + cellSize.y / 2f);
		}

	}
}
