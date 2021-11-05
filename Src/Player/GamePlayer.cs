using Godot;
using InterviewTask.Src.objects;
using InterviewTask.Src.Player;
using InterviewTask.Src.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class GamePlayer : KinematicEntity {

	[Export]
	float movementSpeed = 1;

	[Export]
	float movementLerpW = 1;

	RayCast _eyesRayCast;
	Inventory _inventory;

	public override void _Ready() {
		base._Ready();
		_eyesRayCast = GetNodeOrNull<RayCast>("FirstPersonCamera/EyesRayCast");
		_inventory = GetNodeOrNull<Inventory>("Inventory");
	}

	public override void _PhysicsProcess(float delta) {
		base._PhysicsProcess(delta);

		if(Input.IsActionPressed("ui_up")) {
			Velocity += (GlobalTransform.basis.z * movementSpeed) * delta;
		}
		if(Input.IsActionPressed("ui_down")) {
			Velocity -= (GlobalTransform.basis.z * movementSpeed) * delta;
		}
		if(Input.IsActionPressed("ui_left")) {
			Velocity += (GlobalTransform.basis.x * movementSpeed) * delta;
		}
		if(Input.IsActionPressed("ui_right")) {
			Velocity -= (GlobalTransform.basis.x * movementSpeed) * delta;
		}
		if(Input.IsActionPressed("interact")) {
			var collider = _eyesRayCast.GetCollider();
			if(collider != null) {
				if(collider is Key) {
					var key = (Key) collider;
					if(_inventory.AddKey(key.KeyID)) {
						key.QueueFree();
					}
				}
				if(collider is Door) {
					var door = (Door) collider;
					if(door.Open(_inventory.GetIDInCell(_inventory.current_cell))) {
						_inventory.RemoveKey(_inventory.current_cell);
					}
				}
			}
		}
		Velocity = new Vector3(Mathf.Lerp(Velocity.x, 0, movementLerpW * delta), Velocity.y, Mathf.Lerp(Velocity.z, 0, movementLerpW * delta));
	}

	public override void _Input(InputEvent @event) {
		base._Input(@event);
		if(@event.IsActionPressed("drop")) {
			if(_inventory.current_cell != -1) {
				Key key = (Key) GD.Load<PackedScene>("res://Src/objects/Key.tscn").Instance();
				key.KeyID = _inventory.GetIDInCell(_inventory.current_cell);
				GetNode("..").AddChild(key);
				key.GlobalTranslate(GlobalTransform.origin);
				_inventory.RemoveKey(_inventory.current_cell);
			}
		}
	}

	public void SetCameraActive(bool active) {
		GetNodeOrNull<FirstPersonCamera>("FirstPersonCamera").active = active;
	}

}
