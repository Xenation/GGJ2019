using System.Collections.Generic;
using UnityEngine;
using Xenon;

namespace GGJ2019 {
	public class WindowManager : Singleton<WindowManager> {

		public FolderWindow[] availableFolderWindows;
		public ImageWindow imgWindow;
		public ImageWindow webWindow;
		public ImageWindow textWindow;

		private Stack<Window> windowStack = new Stack<Window>();

		public void OpenWindow(GridIcon icon) {
			GameObject go = null;
			if (icon.isFolder) {
				go = Instantiate(availableFolderWindows[Random.Range(0, availableFolderWindows.Length)].gameObject, transform);
				FolderWindow foldWin = go.GetComponent<FolderWindow>();
				windowStack.Push(foldWin);
			} else {
				ImageWindow imgWin = null;
				switch (icon.ext) {
					case "png":
					case "jpg":
					case "bmp":
						go = Instantiate(imgWindow.gameObject, transform);
						break;
					case "html":
						go = Instantiate(webWindow.gameObject, transform);
						break;
					case "txt":
						go = Instantiate(textWindow.gameObject, transform);
						break;
				}
				imgWin = go?.GetComponent<ImageWindow>();
				windowStack.Push(imgWin);
			}
		}

		public void CloseTopWindow() {
			Destroy(windowStack.Pop());
		}

		public bool isPlayerOnTopMostWindow() {
			FolderWindow win = windowStack.Peek() as FolderWindow;
			if (win == null) return false;
			return win.gridRect.Contains(Player.I.GridPos);
		}

		public Window GetTopWindow() {
			try {
				return windowStack.Peek();
			} catch (System.InvalidOperationException e) {
				return null;
			}
		}

		public Vector2 GetTopWindowClosePosition() {
			Window win = GetTopWindow();
			if (win == null) return Vector2.zero;
			return win.closeBtn.position;
		}

	}
}
