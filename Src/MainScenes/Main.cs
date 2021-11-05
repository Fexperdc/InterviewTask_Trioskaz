using Godot;
using InterviewTask.Src.objects;
using InterviewTask.Src.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewTask.Src.MainScenes {
	class Main : Node2D {

		[Export]
		NodePath player;

		GamePlayer _player;

		public override void _Ready() {
			base._Ready();
			GD.Print("Hello!");

			_player = GetNodeOrNull<GamePlayer>(player);
			Input.SetMouseMode(Input.MouseMode.Captured);
		}

		public override void _Input(InputEvent @event) {
			base._Input(@event);
			if(@event.IsActionPressed("tab")) {
				if(Input.GetMouseMode() == Input.MouseMode.Captured) {
					Input.SetMouseMode(Input.MouseMode.Visible);
					_player.SetCameraActive(false);
				} else {
					Input.SetMouseMode(Input.MouseMode.Captured);
					_player.SetCameraActive(true);
				}
			}
		}
	}
}


