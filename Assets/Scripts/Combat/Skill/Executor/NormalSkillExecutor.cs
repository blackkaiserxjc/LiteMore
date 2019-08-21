﻿using System.Collections.Generic;
using LiteFramework.Helper;
using LiteMore.Combat.AI.Filter;
using LiteMore.Combat.Bullet;
using LiteMore.Combat.Emitter;
using LiteMore.Combat.Npc;
using LiteMore.Player;
using UnityEngine;

namespace LiteMore.Combat.Skill.Executor
{
    /// <summary>
    /// 镭射激光
    /// </summary>
    public class SkillExecutor_2001 : BaseExecutor
    {
        public override bool Execute(SkillArgs Args)
        {
            var BulletDesc = new LaserBulletDescriptor(
                new BaseBulletDescriptor(Args.Skill.Name, Configure.CoreTopPosition, CombatTeam.A, 100),
                180, 360, 300, 500);
            BulletManager.AddLaserBullet(BulletDesc);
            return true;
        }
    }

    /// <summary>
    /// 自动弹幕
    /// </summary>
    public class SkillExecutor_2002 : BaseExecutor
    {
        public override bool Execute(SkillArgs Args)
        {
            EmitterManager.AddEmitter(new BulletCircleEmitter(Args.Skill.Name)
            {
                Master = PlayerManager.Master,
                Team = CombatTeam.A,
                TriggerCount = 10,
                EmittedCountAttr = new EmitterRandInt(100),
                Interval = 30.0f / 60.0f,
                IsAlive = true,
                IsPause = false,
                Position = Configure.CoreTopPosition,
                RadiusAttr = new EmitterRandFloat(0, 0),
                SpeedAttr = new EmitterRandFloat(1000, 2000),
                DamageAttr = new EmitterRandFloat(3, 5),
                ResName = "Blue",
            });
            return true;
        }
    }

    /// <summary>
    /// 放马过来
    /// </summary>
    public class SkillExecutor_2003 : BaseExecutor
    {
        public override bool Execute(SkillArgs Args)
        {
            EmitterManager.AddEmitter(new NpcCircleEmitter(Args.Skill.Name)
            {
                Team = CombatTeam.B,
                TriggerCount = 1,
                EmittedCountAttr = new EmitterRandInt(100),
                Interval = 1.0f / 60.0f,
                IsAlive = true,
                IsPause = false,
                Position = new Vector2(Configure.WindowLeft + 200, 0),
                RadiusAttr = new EmitterRandFloat(10, 200),
                SpeedAttr = new EmitterRandFloat(80, 180),
                HpAttr = new EmitterRandFloat(1, 5),
                DamageAttr = new EmitterRandFloat(1, 1),
                GemAttr = new EmitterRandInt(1, 1),
            });
            return true;
        }
    }

    /// <summary>
    /// 天降正义
    /// </summary>
    public class SkillExecutor_2004 : BaseExecutor
    {
        public override bool Execute(SkillArgs Args)
        {
            var BulletDesc = new BombBulletDescriptor(
                new BaseBulletDescriptor(Args.Skill.Name, new Vector2(Args.Position.x, 400), CombatTeam.A, 500),
                Args.Position, 200, Args.Skill.Radius);

            BulletManager.AddBombBullet(BulletDesc);
            return true;
        }
    }

    /// <summary>
    /// 速速退散
    /// </summary>
    public class SkillExecutor_2005 : BaseExecutor
    {
        public override bool Execute(SkillArgs Args)
        {
            var BulletDesc = new BackBulletDescriptor(
                new BaseBulletDescriptor(Args.Skill.Name, Args.Skill.Master.Position, CombatTeam.A, 1),
                Args.Direction, Configure.WindowWidth,
                new Vector2(400, 82),
                200);

            BulletManager.AddBackBullet(BulletDesc);
            return true;
        }
    }

    /// <summary>
    /// 减速陷阱
    /// </summary>
    public class SkillExecutor_2006 : BaseExecutor
    {
        public override bool Execute(SkillArgs Args)
        {
            var BulletDesc = new AttrTriggerBulletDescriptor(
                new BaseTriggerBulletDescriptor(
                    new BaseBulletDescriptor(Args.Skill.Name, Args.Position, CombatTeam.A, 1),
                    Args.Skill.Radius,
                    0.5f,
                    20,
                    new Color(90.0f / 255.0f, 220.0f / 255.0f, 1.0f)),
                new List<NpcAttrModifyInfo>
                {
                    new NpcAttrModifyInfo(NpcAttrIndex.Speed, 0.5f, 0),
                });

            BulletManager.AddAttrTriggerBullet(BulletDesc);
            return true;
        }
    }

    /// <summary>
    /// 召唤援军
    /// </summary>
    public class SkillExecutor_2007 : BaseExecutor
    {
        public override bool Execute(SkillArgs Args)
        {
            var Npc = NpcManager.AddNpc(Args.Skill.Name, Configure.CoreBasePosition, CombatTeam.A,
                NpcManager.GenerateInitAttr(200, 200, 0, 100, 10, 50, 0, 30, 30));
            Npc.Scale = new Vector2(2, 2);
            Npc.Skill.AddNpcSkill(2006); // 减速陷阱-主动
            Npc.Skill.AddNpcSkill(2009); // 嘲讽-主动
            Npc.Skill.AddNpcSkill(2010); // 分身-主动
            Npc.Skill.AddPassiveSkill(3001, -1); // 荆棘-被动
            return true;
        }
    }

    /// <summary>
    /// 持续子弹
    /// </summary>
    public class SkillExecutor_2008 : BaseExecutor
    {
        public override bool Execute(SkillArgs Args)
        {
            var Target = FilterHelper.FindNearest(PlayerManager.Master);
            if (Target != null)
            {
                BulletManager.AddTrackBullet(new TrackBulletDescriptor(
                    new BaseBulletDescriptor(Args.Skill.Name, Configure.CoreTopPosition, CombatTeam.A, 1),
                    "Blue",
                    Target,
                    1500));
                return true;
            }

            return false;
        }
    }

    /// <summary>
    /// 嘲讽
    /// </summary>
    public class SkillExecutor_2009 : BaseExecutor
    {
        public override bool Execute(SkillArgs Args)
        {
            var Master = Args.Skill.Master;
            if (Master == null)
            {
                return false;
            }

            var TargetList = FilterHelper.Find(Args.Skill, Args.LockRule);
            foreach (var Target in TargetList)
            {
                if (!Target.IsValid())
                {
                    continue;
                }

                Target.Action.ForceTarget(Master);
            }

            return true;
        }
    }

    /// <summary>
    /// 分身
    /// </summary>
    public class SkillExecutor_2010 : BaseExecutor
    {
        public override bool Execute(SkillArgs Args)
        {
            var Master = Args.Skill.Master;
            if (Master == null)
            {
                return false;
            }

            var Slave = NpcManager.AddNpc($"{Master.Name}_clone", Master.Position + UnityHelper.RandVec2(100), Master.Team, Master.Data.Attr.GetValues());
            Slave.Scale = Master.Scale;
            Slave.Actor.SetDirection(Master.Actor.Direction);
            return true;
        }
    }
}