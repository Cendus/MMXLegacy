﻿using System;
using Legacy.Core.Entities.Items;

namespace Legacy.Game.MMGUI
{
	public class LootDragObject : BaseDragObject
	{
		private ItemSlotLoot m_itemSlot;

		private BaseItem m_item;

		public LootDragObject(ItemSlotLoot p_slot)
		{
			m_itemSlot = p_slot;
			m_item = p_slot.Item;
		}

		public BaseItem Item => m_item;

	    public ItemSlotLoot ItemSlot => m_itemSlot;

	    public override void SetActive(Boolean p_active)
		{
			base.SetActive(p_active);
			if (p_active)
			{
				m_sprite.atlas = m_itemSlot.ItemTexture.atlas;
				m_sprite.spriteName = m_itemSlot.ItemTexture.spriteName;
				MMGUI.ItemSlot.UpdateItemBackground(m_itemBackground, m_item);
			}
			if (m_item is Consumable)
			{
				Consumable consumable = (Consumable)m_item;
				if (m_item is Scroll)
				{
					m_sprite.spriteName = "ITM_consumable_scroll";
					m_scrollSprite.spriteName = m_item.Icon;
					NGUITools.SetActiveSelf(m_scrollSprite.gameObject, p_active);
				}
				NGUITools.SetActiveSelf(m_itemCounter.gameObject, p_active);
				m_itemCounter.text = consumable.Counter.ToString();
			}
			else if (m_item is GoldStack)
			{
				GoldStack goldStack = (GoldStack)m_item;
				NGUITools.SetActiveSelf(m_itemCounter.gameObject, p_active);
				m_itemCounter.text = goldStack.Amount.ToString();
			}
		}
	}
}
