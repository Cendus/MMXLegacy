﻿using System;
using Legacy.Core.Combat;
using Legacy.Core.Entities.InteractiveObjects;
using Legacy.Core.PartyManagement;

namespace Legacy.Core.Entities.TrapEffects
{
	public class FrostcloudTrapEffect : DamageDealingTrapEffect
	{
		public FrostcloudTrapEffect(Int32 p_staticID, InteractiveObject p_parent) : base(p_staticID, p_parent)
		{
		}

		public override void ResolveEffect(Party p_party)
		{
			Damage p_damages = Damage.Create(new DamageData(EDamageType.WATER, m_staticData.AbsoluteValueMin, m_staticData.AbsoluteValueMax), 1f);
			Attack p_attack = new Attack(1f, 0f, p_damages);
			for (Int32 i = 0; i < 4; i++)
			{
				Character member = p_party.GetMember(i);
				AttackResult attackResult = member.FightHandler.AttackEntity(p_attack, false, EDamageType.WATER, false, 0, false);
				if (attackResult.Result != EResultType.EVADE)
				{
					member.ChangeHP(-attackResult.DamageDone);
					if (Random.Range(0, 100) < m_staticData.PercentageValue)
					{
						if (!member.ConditionHandler.HasCondition(ECondition.PARALYZED))
						{
							conditionsReceived[i] = ECondition.PARALYZED;
						}
						member.ConditionHandler.AddCondition(ECondition.PARALYZED);
						conditionsReceived[i] = ECondition.PARALYZED;
					}
				}
				attackResults[i] = attackResult;
			}
			NotifyListeners(p_party);
		}
	}
}
