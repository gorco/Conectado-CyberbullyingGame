using UnityEngine;
using UniRx;
using UnityFx.Async.Promises;
using UnityFx.Async;
using Simva.Model;

namespace Simva
{
	// Manager for "Simva.Survey"
	public class SurveyController : MonoBehaviour
	{
		private bool surveyOpened;
		private bool ready;
		private GameObject surveyOpener;

		public void OpenSurvey()
		{
			SimvaExtension.Instance.NotifyLoading(true);
			string activityId = SimvaExtension.Instance.CurrentActivityId;
			string username = SimvaExtension.Instance.API.AuthorizationInfo.Username;
			
			SimvaExtension.Instance.API.Api.GetActivityTarget(activityId)
				.Then(result =>
				{
					SimvaExtension.Instance.NotifyLoading(false);
					surveyOpened = true;
					Application.OpenURL(result[username]);
				})
				.Catch(error =>
				{
					SimvaExtension.Instance.NotifyManagers(error.Message);
					SimvaExtension.Instance.NotifyLoading(false);
				});
		}

		protected void OnApplicationResume()
		{
			if (surveyOpened)
			{
				surveyOpened = false;
				CheckSurvey();
			}
		}

		public void CheckSurvey()
		{
			SimvaExtension.Instance.NotifyLoading(true);
			string activityId = SimvaExtension.Instance.CurrentActivityId;
			string token = SimvaExtension.Instance.Token;
			string username = SimvaExtension.Instance.API.AuthorizationInfo.Username;
			SimvaExtension.Instance.API.Api.GetCompletion(activityId, username)
				.Then(result =>
				{
					if (result[username])
					{
						return SimvaExtension.Instance.UpdateSchedule();
					}
					else
					{
						SimvaExtension.Instance.NotifyManagers("Survey not completed");
						SimvaExtension.Instance.NotifyLoading(false);
						var nullresult = new AsyncCompletionSource<Schedule>();
						nullresult.SetResult(null);
						return nullresult;
					}
				})
				.Then(schedule =>
				{
					if (schedule != null)
					{
						SimvaExtension.Instance.LaunchActivity(schedule.Next);
					}
				})
				.Catch(error =>
				{
					SimvaExtension.Instance.NotifyManagers(error.Message);
					SimvaExtension.Instance.NotifyLoading(false);
				});
		}
	}
}

