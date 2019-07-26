﻿using LiteMore.Combat.Bullet;
using LiteMore.Combat.Npc;
using UnityEngine;

namespace LiteMore.Combat.Emitter
{
    public abstract class BulletEmitter : BaseEmitter
    {
        public EmitterRandFloat SpeedAttr { get; set; }
        public EmitterRandInt DamageAttr { get; set; }
        public string ResName { get; set; }

        protected BulletEmitter()
            : base()
        {
        }
    }

    public class BulletCircleEmitter : BulletEmitter
    {
        public EmitterRandFloat RadiusAttr { get; set; }

        public BulletCircleEmitter()
            : base()
        {
        }

        protected override void OnEmitted(uint Cur, uint Max)
        {
            var Target = NpcManager.GetRandomNpc(Team.Opposite());
            if (Target != null)
            {
                var Radius = RadiusAttr.Get();
                var Angle = Random.Range(0, Mathf.PI * 2);
                var Pos = Position + new Vector2(Mathf.Sin(Angle) * Radius, Mathf.Cos(Angle) * Radius);

                var Desc = new TrackBulletDescriptor(
                    new BaseBulletDescriptor(Pos, Team, DamageAttr.Get()),
                    ResName, Target, SpeedAttr.Get());

                BulletManager.AddTrackBullet(Desc);
            }
        }
    }
}