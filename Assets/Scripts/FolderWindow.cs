using UnityEngine;

namespace GGJ2019 {
	public class FolderWindow : Window {
		
		[System.NonSerialized] public RectInt gridRect;
		private GridIcon[,] previousIcons;

		private IconGrid grid;
		private RectTransform iconsZone;

		protected override void Awake() {
			base.Awake();
			grid = Player.I.grid;
			iconsZone = rectTransform.Find("Icons").GetComponent<RectTransform>();
			Vector2Int min = grid.GetGridPos(iconsZone.GetWorldMin());
			Vector2Int max = grid.GetGridPos(iconsZone.GetWorldMax());
			gridRect = new RectInt(min, max - min);
			// Saving previous grid icons
			previousIcons = new GridIcon[gridRect.size.x, gridRect.size.y];
			for (int y = 0; y < gridRect.size.y; y++) {
				for (int x = 0; x < gridRect.size.x; x++) {
					previousIcons[x, y] = grid.grid[gridRect.x + x, gridRect.y + y];
					previousIcons[x, y]?.gameObject.SetActive(false);
				}
			}
			// Placing new grid icons
			foreach (RectTransform transf in rectTransform) {
				GridIcon icon = transf.GetComponent<GridIcon>();
				if (icon == null) continue;
				Vector2Int gridPos = grid.GetGridPos(transf.position);
				grid.grid[gridPos.x, gridPos.y] = icon;
				transf.position = grid.GetCanvasPos(gridPos);
				transf.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, grid.cellSize.x);
				transf.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, grid.cellSize.y);
			}
		}

		private void OnDestroy() {
			// Restoring previous icons
			for (int y = 0; y < gridRect.size.y; y++) {
				for (int x = 0; x < gridRect.size.x; x++) {
					grid.grid[gridRect.x + x, gridRect.y + y] = previousIcons[x, y];
					previousIcons[x, y]?.gameObject.SetActive(true);
				}
			}
		}

	}
}
