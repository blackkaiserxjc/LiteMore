﻿using LiteMore.Combat.Npc;
using LiteMore.Extend;
using UnityEngine;

namespace LiteMore.Combat.Emitter
{
    public abstract class NpcEmitter : BaseEmitter
    {
        public EmitterRandFloat SpeedAttr { get; set; }
        public EmitterRandInt HpAttr { get; set; }
        public EmitterRandInt DamageAttr { get; set; }
        public EmitterRandInt GemAttr { get; set; }

        protected NpcEmitter()
            : base()
        {
        }
    }

    public class NpcRectEmitter : NpcEmitter
    {
        public EmitterRandVector2 OffsetAttr { get; set; }

        private Transform ObjOuter_;
        private LineCaller CallerOuter_;

        public NpcRectEmitter()
            : base()
        {
        }

        public override void Create()
        {
            ObjOuter_ = Object.Instantiate(Resources.Load<GameObject>("Prefabs/Line")).transform;
            ObjOuter_.name = $"NpcRectOuter<{ID}>";
            ObjOuter_.SetParent(Configure.EmitterRoot, false);
            ObjOuter_.localPosition = Vector3.zero;

            CallerOuter_ = new LineCaller(ObjOuter_.GetComponent<LineRenderer>());
            CallerOuter_.DrawRect(
                new LineCallerPoint(Position + OffsetAttr.Min, Color.blue),
                new LineCallerPoint(Position + OffsetAttr.Max, Color.blue));
        }

        public override void Destroy()
        {
            Object.Destroy(ObjOuter_.gameObject);
        }

        protected override void OnEmitted(uint Cur, uint Max)
        {
            var Pos = Position + OffsetAttr.Get();
            var InitAttr = NpcManager.GenerateInitAttr(SpeedAttr.Get(), HpAttr.Get(), 0, DamageAttr.Get(), GemAttr.Get(), 5, 5);
            var Entity = NpcManager.AddNpc(Pos, Team, InitAttr);
            //Entity.MoveTo(Configure.CoreBasePosition);
        }
    }

    public class NpcCircleEmitter : NpcEmitter
    {
        public EmitterRandFloat RadiusAttr { get; set; }

        private Transform ObjInner_;
        private LineCaller CallerInner_;

        private Transform ObjOuter_;
        private LineCaller CallerOuter_;

        public NpcCircleEmitter()
            : base()
        {
        }

        public override void Create()
        {
            ObjInner_ = Object.Instantiate(Resources.Load<GameObject>("Prefabs/Line")).transform;
            ObjInner_.name = $"NpcNormalInner<{ID}>";
            ObjInner_.SetParent(Configure.EmitterRoot, false);
            ObjInner_.localPosition = Vector3.zero;

            CallerInner_ = new LineCaller(ObjInner_.GetComponent<LineRenderer>());
            CallerInner_.DrawCircle(new LineCallerPoint(Position, Color.red), RadiusAttr.Min);

            ObjOuter_ = Object.Instantiate(Resources.Load<GameObject>("Prefabs/Line")).transform;
            ObjOuter_.name = $"NpcNormalOuter<{ID}>";
            ObjOuter_.SetParent(Configure.EmitterRoot, false);
            ObjOuter_.localPosition = Vector3.zero;

            CallerOuter_ = new LineCaller(ObjOuter_.GetComponent<LineRenderer>());
            CallerOuter_.DrawCircle(new LineCallerPoint(Position, Color.blue), RadiusAttr.Max);
        }

        public override void Destroy()
        {
            Object.Destroy(ObjInner_.gameObject);
            Object.Destroy(ObjOuter_.gameObject);
        }

        protected override void OnEmitted(uint Cur, uint Max)
        {
            var Radius = RadiusAttr.Get();
            var Angle = Random.Range(0, Mathf.PI * 2);
            var Pos = Position + new Vector2(Mathf.Sin(Angle) * Radius, Mathf.Cos(Angle) * Radius);

            var InitAttr = NpcManager.GenerateInitAttr(SpeedAttr.Get(), HpAttr.Get(), 0, DamageAttr.Get(), GemAttr.Get(), 5, 5);
            var Entity = NpcManager.AddNpc(Pos, Team, InitAttr);
            //Entity.MoveTo(Configure.CoreBasePosition);
        }
    }
}