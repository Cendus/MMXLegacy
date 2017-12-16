﻿using System;
using System.Xml.Serialization;
using Legacy.Core.Api;

namespace Legacy.Core.NpcInteraction.Functions
{
	public class SuppliesFunction : DialogFunction
	{
		private Int32 m_dialogID;

		private Int32 m_count;

		private Int32 m_price;

		public SuppliesFunction(Int32 p_count, Int32 p_price, Int32 p_dialogID)
		{
			m_count = p_count;
			m_price = p_price;
			m_dialogID = p_dialogID;
		}

		public SuppliesFunction()
		{
		}

		[XmlAttribute("dialogID")]
		public Int32 DialogID
		{
			get => m_dialogID;
		    set => m_dialogID = value;
		}

		[XmlAttribute("price")]
		public Int32 Price
		{
			get => m_price;
		    set => m_price = value;
		}

		public override void Trigger(ConversationManager p_manager)
		{
			LegacyLogic.Instance.WorldManager.Party.SetSupplies(m_count);
			LegacyLogic.Instance.WorldManager.Party.ChangeGold(-m_price);
			p_manager._ChangeDialog(p_manager.CurrentNpc.StaticID, m_dialogID);
		}
	}
}
