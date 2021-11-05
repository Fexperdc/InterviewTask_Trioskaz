using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewTask.Src.Utils {
	class KinematicEntity : KinematicBody {

		public Vector3 Velocity = new Vector3();

		[Export]
		public Vector3 gravity;

		public override void _PhysicsProcess(float delta) {
			base._PhysicsProcess(delta);
			Velocity += gravity * delta;

			Velocity = MoveAndSlide(Velocity, Vector3.Up);
		}

	}
}
