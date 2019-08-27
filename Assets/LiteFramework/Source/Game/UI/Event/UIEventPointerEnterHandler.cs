﻿using LiteFramework.Helper;
using UnityEngine.EventSystems;

namespace LiteFramework.Game.UI.Event
{
    public class UIEventPointerEnterHandler : UIEventBaseHandler, IPointerEnterHandler
    {
        public void OnPointerEnter(PointerEventData EventData)
        {
            // 穿透问题
            if (EventData.rawPointerPress != null && gameObject != EventData.rawPointerPress)
            {
                return;
            }

            EventCallback_?.Invoke(EventData.pointerPress, UnityHelper.ScreenPosToCanvasPos(EventData.position));
            EventCallbackEx_?.Invoke();
        }
    }
}