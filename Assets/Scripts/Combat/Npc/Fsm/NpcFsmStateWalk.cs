﻿using UnityEngine;

namespace LiteMore.Combat.Npc.Fsm
{
    public class NpcFsmStateWalk : NpcFsmStateBase
    {
        private Vector2 BeginPos_;
        private Vector2 EndPos_;
        private float MoveTime_;
        private float MoveTotalTime_;
        private bool IsMove_;

        public NpcFsmStateWalk(NpcFsm Fsm)
            : base(NpcFsmStateName.Walk, Fsm)
        {
        }

        public override void OnEnter(NpcEvent Event)
        {
            BeginPos_ = Fsm.Master.Position;
            EndPos_ = (Event as NpcMoveEvent).TargetPos;
            MoveTime_ = 0;
            MoveTotalTime_ = (EndPos_ - BeginPos_).magnitude / Fsm.Master.CalcFinalAttr(NpcAttrIndex.Speed);
            IsMove_ = true;

            Fsm.Master.PlayAnimation("Walk", true);
        }

        public override void OnTick(float DeltaTime)
        {
            if (!IsMove_)
            {
                Fsm.ChangeToState(NpcFsmStateName.Attack, null);
                return;
            }

            MoveTime_ += DeltaTime;
            var T = MoveTime_ / MoveTotalTime_;
            if (T >= 1.0f)
            {
                T = 1.0f;
                IsMove_ = false;
            }

            Fsm.Master.Position = Vector2.Lerp(BeginPos_, EndPos_, T);
        }

        public override void OnEvent(NpcEvent Event)
        {
            if (Event is NpcMoveEvent)
            {
                Fsm.ChangeToState(NpcFsmStateName.Walk, Event);
            }
            else if (Event is NpcIdleEvent)
            {
                Fsm.ChangeToIdleState();
            }
            else if (Event is NpcDieEvent)
            {
                Fsm.ChangeToState(NpcFsmStateName.Die, Event);
            }
            else if (Event is NpcBackEvent)
            {
                Fsm.ChangeToState(NpcFsmStateName.Back, Event);
            }
        }
    }
}