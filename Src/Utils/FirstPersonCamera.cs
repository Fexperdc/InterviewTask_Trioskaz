using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewTask.Src.Utils {
	class FirstPersonCamera : Camera {

		[Export]
		private NodePath _person;
		public Spatial person;

		private Vector2 mouseDelta;

		public bool active = true;

		[Export]
		private float interpolateWeight = 0;

		public override void _Ready() {
			base._Ready();
			person = GetNodeOrNull<Spatial>(_person);
		}

		[Export]
		public float sensitivity = 1;

		public override void _PhysicsProcess(float delta) {
			base._PhysicsProcess(delta);
			if(active) {
				mouseDelta = mouseDelta.LinearInterpolate(Vector2.Zero, interpolateWeight);
				RotationDegrees += -new Vector3(mouseDelta.y * sensitivity, 0, 0);
				person.RotationDegrees += -new Vector3(0, mouseDelta.x * sensitivity, 0);
				RotationDegrees = new Vector3(Mathf.Clamp(RotationDegrees.x, -80, 80), RotationDegrees.y, RotationDegrees.z);
			}
		}

		public override void _Input(InputEvent @event) {
			base._Input(@event);

			if(@event is InputEventMouseMotion) {
				var emm = (InputEventMouseMotion) @event;
				mouseDelta = emm.Relative;
			}
		}

	}
}
