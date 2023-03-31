using Crankshaft.Physics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Crankshaft.Animation
{
    public class Animations
    {
        protected List<Keyframe> keys;
        protected int currentKey;
        protected float duration;
        protected float lastKey;
        protected UniVector3 difference;
        protected UniVector3 position;
        protected bool loop;
        protected bool playing;
        private float scale = 1;
        private float speed = 1;
        public UniVector3 Position { get => position; set => position = value; }
        public bool Loop { get => loop; set => loop = value; }
        public bool Playing { get => playing; set => playing = value; }
        public float Scale { get => scale; set => scale = value; }
        public float Speed { get => speed; set => speed = value; }

        public Animations(Keyframe[] keyframes)
        {
            keys = new List<Keyframe>();
            currentKey = 0;
            Playing = false;
            UniVector3 v = new UniVector3(0,0,0);
            if (keyframes == null)
            {
                return;
            }
            if (keyframes[0].Position != v)
            {
                keys.Add(new Keyframe(0, v));
                foreach(Keyframe k in keyframes)
                {
                    keys.Add(k);
                }
            } else
            {
                foreach (Keyframe k in keyframes)
                {
                    keys.Add(k);
                }
            }
        }

        public virtual void stepAnimation(double time)
        {
            if (Playing == false)
            {
                return;
            }

            duration += (float) time*speed;
            if (duration > keys[currentKey].Time)
            {
                if (keys.Count-1 < currentKey+1 && loop != true)
                {
                    Playing = false;
                    position = new UniVector3(0, 0, 0);
                    return;
                } else if (keys.Count - 1 < currentKey + 1 && loop == true)
                {
                    duration = 0;
                    currentKey = 0;
                }
                lastKey = keys[currentKey].Time;
                currentKey += 1;
                difference = keys[currentKey].Position - keys[currentKey - 1].Position;
            }
            position.X = keys[currentKey-1].Position.X + (difference.X) * ((duration-lastKey) / (keys[currentKey].Time - lastKey));
            position.Y = keys[currentKey-1].Position.Y + (difference.Y) * ((duration-lastKey) / (keys[currentKey].Time - lastKey));
            position.Z = keys[currentKey-1].Position.Z + (difference.Z) * ((duration-lastKey) / (keys[currentKey].Time - lastKey));
            position *= scale;
        }

        public virtual void playAnimation()
        {
            duration = 0;
            currentKey = 0;
            Playing = true;
        }

        public virtual void updateAimation(double time)
        {

        }
    }
}
