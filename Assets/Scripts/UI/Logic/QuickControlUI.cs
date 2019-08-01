﻿using LiteMore.Combat.Skill;
using LiteMore.Data;
using LiteMore.Helper;
using UnityEngine;
using UnityEngine.UI;

namespace LiteMore.UI.Logic
{
    public class QuickControlUI : BaseUI
    {
        private readonly QuickController Controller_;
        private GameObject ProbeObj_;
        private Image ProbeIcon_;
        private Transform InputList_;
        private GameObject InputObj_;

        public QuickControlUI()
            : base()
        {
            DepthMode = UIDepthMode.Normal;
            DepthIndex = 0;

            Controller_ = new QuickController();
            SkillLibrary.PatchQuickController(Controller_);
        }

        protected override void OnOpen(params object[] Params)
        {
            AddEventToChild("Code/Metal", () =>
            {
                AddItemToInput(QuickCode.Metal);
                Controller_.ExecuteCode(QuickCode.Metal);
            });
            AddEventToChild("Code/Wood", () =>
            {
                AddItemToInput(QuickCode.Wood);
                Controller_.ExecuteCode(QuickCode.Wood);
            });
            AddEventToChild("Code/Water", () =>
            {
                AddItemToInput(QuickCode.Water);
                Controller_.ExecuteCode(QuickCode.Water);
            });
            AddEventToChild("Code/Fire", () =>
            {
                AddItemToInput(QuickCode.Fire);
                Controller_.ExecuteCode(QuickCode.Fire);
            });
            AddEventToChild("Code/Earth", () =>
            {
                AddItemToInput(QuickCode.Earth);
                Controller_.ExecuteCode(QuickCode.Earth);
            });

            ProbeObj_ = FindChild("Probe").gameObject;
            ProbeObj_.SetActive(false);
            ProbeIcon_ = FindComponent<Image>("Probe/Icon");

            InputList_ = FindChild("Input");
            InputObj_ = FindChild("InputItem").gameObject;
            InputObj_.SetActive(false);

            Controller_.OnFailed += () => { ResetQuickState(); };
            Controller_.OnSucceed += (Node) => { ResetQuickState(); };
            Controller_.OnProbe += (Node) => { UpdateProbe(Node); };
        }

        protected override void OnTick(float DeltaTime)
        {
            Controller_.Tick(DeltaTime);
        }

        private void ResetQuickState()
        {
            UIHelper.RemoveAllChildren(InputList_);
            ProbeObj_.SetActive(false);
        }

        private void AddItemToInput(QuickCode Code)
        {
            var Obj = Object.Instantiate(InputObj_);
            Obj.transform.SetParent(InputList_, false);
            Obj.SetActive(true);
            var Img = Obj.GetComponent<Image>();

            switch (Code)
            {
                case QuickCode.Metal:
                    Img.sprite = Resources.Load<Sprite>("Textures/Icon/b1_metal");
                    break;
                case QuickCode.Wood:
                    Img.sprite = Resources.Load<Sprite>("Textures/Icon/b2_wood");
                    break;
                case QuickCode.Water:
                    Img.sprite = Resources.Load<Sprite>("Textures/Icon/b3_water");
                    break;
                case QuickCode.Fire:
                    Img.sprite = Resources.Load<Sprite>("Textures/Icon/b3_water");
                    break;
                case QuickCode.Earth:
                    Img.sprite = Resources.Load<Sprite>("Textures/Icon/b5_earth");
                    break;
                default:
                    break;
            }
        }

        private void UpdateProbe(QuickNode Node)
        {
            ProbeObj_.SetActive(true);
            ProbeIcon_.sprite = Resources.Load<Sprite>(SkillLibrary.Get(Node.ID).Icon);
        }
    }
}