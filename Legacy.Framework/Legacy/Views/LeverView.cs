﻿using System;
using Legacy.Audio;
using Legacy.Core.Api;
using Legacy.Core.Entities;
using Legacy.Core.Entities.InteractiveObjects;
using Legacy.Core.EventManagement;
using Legacy.Core.UpdateLogic.Interactions;
using UnityEngine;
using Object = System.Object;

namespace Legacy.Views
{
	[AddComponentMenu("MM Legacy/Views/LeverView")]
	[RequireComponent(typeof(Animation))]
	public class LeverView : BaseView
	{
		private Animation m_Animation;

		private Single m_animEndTime = -1f;

		private Single m_interactionTime = -1f;

		private BaseInteraction m_interaction;

		[SerializeField]
		private String m_ActiveClip = "Down";

		[SerializeField]
		private String m_DeactiveClip = "Up";

		[SerializeField]
		private Single m_triggerAfterPlayTimePercentage = 0.7f;

		[SerializeField]
		private String m_leverUP = "lever_start";

		[SerializeField]
		private String m_leverDown = "lever_start";

		[SerializeField]
		private Single m_soundVolume = 1f;

		protected new Lever MyController => (Lever)base.MyController;

	    public void ObjectState(Boolean p_stateValue, Boolean p_skipAnimation)
		{
			enabled = true;
			AudioController.Stop(m_leverDown);
			AudioController.Stop(m_leverUP);
			String text = (!p_stateValue) ? m_DeactiveClip : m_ActiveClip;
			if (p_skipAnimation)
			{
				if (m_Animation[text] != null)
				{
					m_Animation.Play(text, PlayMode.StopAll);
					m_Animation[text].normalizedTime = 1f;
					m_Animation.Sample();
				}
				m_animEndTime = Time.time;
				m_interactionTime = Time.time;
				if (m_interaction != null)
				{
					enabled = false;
					m_interaction.ReleaseInteractLock();
					m_interaction.TriggerStateChange();
				}
			}
			else
			{
				AudioManager.Instance.RequestPlayAudioID((!p_stateValue) ? m_leverUP : m_leverDown, 1, -1f, gameObject.transform, m_soundVolume, 0f, 0f, null);
				m_animEndTime = Time.time;
				m_interactionTime = Time.time;
				if (m_Animation[text] != null)
				{
					m_Animation.Play(text, PlayMode.StopAll);
					m_animEndTime += m_Animation[text].clip.length;
					m_interactionTime += m_Animation[text].clip.length * m_triggerAfterPlayTimePercentage;
				}
			}
		}

		protected override void Awake()
		{
			base.Awake();
			m_Animation = this.GetComponent<Animation>(true);
		}

		protected override void OnChangeMyController(BaseObject oldController)
		{
			base.OnChangeMyController(oldController);
			if (oldController != MyController)
			{
				LegacyLogic.Instance.EventManager.UnregisterEvent(EEventType.LEVER_STATE_CHANGED, new EventHandler(OnInteractiveObjectStateChanged));
			}
			if (MyController != null)
			{
				LegacyLogic.Instance.EventManager.RegisterEvent(EEventType.LEVER_STATE_CHANGED, new EventHandler(OnInteractiveObjectStateChanged));
				ObjectState(MyController.State == EInteractiveObjectState.LEVER_DOWN, true);
			}
		}

		private void OnInteractiveObjectStateChanged(Object p_sender, EventArgs p_args)
		{
			BaseObjectEventArgs baseObjectEventArgs = p_args as BaseObjectEventArgs;
			if (baseObjectEventArgs != null && baseObjectEventArgs.Object == MyController)
			{
				m_interaction = (BaseInteraction)p_sender;
				ObjectState(MyController.State == EInteractiveObjectState.LEVER_DOWN, false);
			}
		}

		private void Update()
		{
			if (m_interactionTime != -1f && m_interactionTime <= Time.time)
			{
				m_interactionTime = -1f;
				if (m_interaction != null)
				{
					m_interaction.FinishExecution();
				}
			}
			if (m_animEndTime != -1f && m_animEndTime <= Time.time)
			{
				m_animEndTime = -1f;
				enabled = false;
				if (m_interaction != null)
				{
					m_interaction.ReleaseInteractLock();
				}
			}
		}
	}
}
