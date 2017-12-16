﻿using System;

namespace Legacy.Core.Achievements
{
	public enum EAchievementConditionType
	{
		QUEST_COMPLETED,
		RELICS_AMOUNT_EQUIPPED,
		ALL_CHARACTERS_ON_LEVEL,
		PARTY_GOLD_AMOUNT_HIGHER_THAN_GOLD,
		INGAME_DAYS_PASSED,
		ALL_MEMBERS_UNLOCKED_ADVANCED_CLASS,
		NUMBER_OF_LEARNED_SPELLS,
		NUMBER_OF_UNIQUE_RELICS,
		NUMBER_OF_ENTERED_TILES,
		NUMBER_OF_REVIVINGS,
		NUMBER_OF_INGAME_DAYS_SINCE_RESTING,
		NUMBER_OF_BLOCKS,
		NUMBER_OF_DEFEATED_MONSTERS,
		NO_CHARACTER_DIED,
		QUEST_COMPLETED_WITH_EXPLICIT_CLASS_IDS,
		QUEST_COMPLETED_WITH_NO_DEATHS,
		QUEST_COMPLETED_WITHOUT_CASTING_SPELL,
		QUEST_COMPLETED_WITH_PARTY_OF_CLASSTYPE
	}
}
