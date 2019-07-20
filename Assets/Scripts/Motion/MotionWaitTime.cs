﻿namespace LiteMore.Motion
{
    public class MotionWaitTime : MotionBase
    {
        private readonly float TotalTime_;
        private float CurrentTime_;

        public MotionWaitTime(float Time)
            : base()
        {
            TotalTime_ = Time;
        }

        public override void Enter()
        {
            CurrentTime_ = TotalTime_;
            IsEnd = false;
        }

        public override void Exit()
        {
        }

        public override void Tick(float DeltaTime)
        {
            CurrentTime_ -= DeltaTime;

            if (CurrentTime_ <= 0.0f)
            {
                IsEnd = true;
            }
        }
    }
}