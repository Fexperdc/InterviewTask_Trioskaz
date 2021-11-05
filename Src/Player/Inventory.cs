using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static InterviewTask.Src.objects.Key;

namespace InterviewTask.Src.Player {
	class Inventory : Control {

		TextureRect _cell1;
		TextureRect _cell2;

		Vector2 cellsID = new Vector2(-1, -1);

		public int current_cell = 0;

		public override void _Ready() {
			base._Ready();
			_cell1 = GetNodeOrNull<TextureRect>("CenterContainer/GridContainer/Cell1");
			_cell2 = GetNodeOrNull<TextureRect>("CenterContainer/GridContainer/Cell2");
			_cell1.Connect("gui_input", this, nameof(_onCell1Input));
			_cell2.Connect("gui_input", this, nameof(_onCell2Input));
		}

		public bool IsEmpty() {
			return cellsID == new Vector2(-1, -1);
		}

		public void SetCurrentCell(int index) {
			current_cell = index;
			_cell1.Modulate = Color.ColorN("white");
			_cell2.Modulate = Color.ColorN("white");
			if(index == 0) {
				_cell1.Modulate = Color.ColorN("black");
			} else {
				_cell2.Modulate = Color.ColorN("black");
			}
		}

		public bool AddKey(int keyId) {
			if(cellsID.x == -1) {
				cellsID.x = keyId;
				_cell1.Texture = Keys.GetTexture(keyId);
				return true;
			} else if(cellsID.y == -1) {
				cellsID.y = keyId;
				_cell2.Texture = Keys.GetTexture(keyId);
				return true;
			}
			return false;
		}

		public void RemoveKey(int cellIndex) {
			if(cellIndex == 0) {
				GetNodeOrNull<TextureRect>("CenterContainer/GridContainer/Cell1").Texture = null;
				cellsID.x = -1;
			} else {
				GetNodeOrNull<TextureRect>("CenterContainer/GridContainer/Cell2").Texture = null;
				cellsID.y = -1;
			}
		}

		public int GetIDInCell(int cellIndex) {
			if(cellIndex == 0) {
				return (int) cellsID.x;
			} else {
				return (int) cellsID.y;
			}
		}

		public void _onCell1Input(InputEvent input) {
			if(input.IsActionPressed("lmb")) {
				SetCurrentCell(0);
			}
		}
		public void _onCell2Input(InputEvent input) {
			if(input.IsActionPressed("lmb")) {
				SetCurrentCell(1);
			}
		}
	}
}
