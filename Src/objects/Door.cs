using Godot;
using InterviewTask.Src.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewTask.Src.objects {
    class Door : KinematicEntity {

        AnimationPlayer animationPlayer;
        bool opened = false;

        [Export]
        public int keyID = -1;

        public override void _Ready() {
            base._Ready();
            animationPlayer = GetNodeOrNull<AnimationPlayer>("AnimationPlayer");
        }

        public bool Open(int keyID) {
            if(!opened) {
                if(keyID == this.keyID) {
                    animationPlayer.Play("open");
                    opened = true;
                    return true;
                } else {
                    animationPlayer.Play("error");
                    return false;
                }
            }
            return false;
        }

        public void Close() {
            animationPlayer.PlayBackwards("open");
        }
    }
}
