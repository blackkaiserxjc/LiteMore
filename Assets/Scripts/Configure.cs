﻿using UnityEngine;

namespace LiteMore
{
    public static class Configure
    {
        public static readonly Transform CanvasRoot = GameObject.Find("Canvas").transform;
        public static readonly Transform CombatRoot = CanvasRoot.Find("Combat").transform;
        public static readonly Transform MapRoot = CombatRoot.Find("Map").transform;
        public static readonly Transform NpcRoot = CombatRoot.Find("Npc").transform;
        public static readonly Transform BulletRoot = CombatRoot.Find("Bullet").transform;
        public static readonly Transform EmitterRoot = CombatRoot.Find("Emitter").transform;
        public static readonly Transform SfxRoot = CombatRoot.Find("Sfx").transform;
        public static readonly Transform LabelRoot = CombatRoot.Find("Label").transform;

        public const int WindowWidth = 1280;
        public const int WindowHeight = 720;
        public const int WindowLeft = -640;
        public const int WindowRight = 640;
        public const int WindowTop = 320;
        public const int WindowBottom = -320;
        public static readonly Vector2 WindowSize = new Vector2(WindowWidth, WindowHeight);
        public const float EnterBackgroundMaxTime = 90.0f;
        public const float TipsHoldTime = 0.3f;

        public static readonly Vector2 CoreBasePosition = new Vector2(WindowRight - 262.0f / 2.0f, 0);
        public static readonly Vector2 CoreTopPosition = new Vector2(WindowRight - 262.0f / 2.0f, 233.0f / 2.0f - 20);
    }
}