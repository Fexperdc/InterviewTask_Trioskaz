using Godot;
using InterviewTask.Src.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewTask.Src.objects {
	class Key : KinematicEntity {

		int _key_id = -1;
		[Export]
		public int KeyID {
			set {
				_key_id = value;
				_SetID(value);
			}
			get {
				return _key_id;
			}
		}

		private async void _SetID(int id) {
			await ToSignal(this, "ready");
			GetNodeOrNull<Sprite3D>("Sprite3D").Texture = Keys.GetTexture(id);
		}
		public class Keys {
			public static Texture GetTexture(int id) {
				switch(id) {
					case 0:
						return GD.Load<Texture>("res://Assets/Textures/key01.png");
					case 1:
						return GD.Load<Texture>("res://Assets/Textures/key02.png");
					case 2:
						return GD.Load<Texture>("res://Assets/Textures/key03.png");
				}
				return null;
			}
		}

	}
}
