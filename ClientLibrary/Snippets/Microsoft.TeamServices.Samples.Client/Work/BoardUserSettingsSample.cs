using Microsoft.VisualStudio.Services.WebApi;
using Microsoft.TeamFoundation.Core.WebApi.Types;
using Microsoft.TeamFoundation.Work.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.TeamServices.Samples.Client.Work
{
    [ClientSample(WorkWebConstants.RestArea, "boardusersettings")]
    public class BoardUserSettingsSample : ClientSample
    {

        [ClientSampleMethod]
        public BoardUserSettings GetBoardUserSettings()
        {
            VssConnection connection = Context.Connection;
            WorkHttpClient workClient = connection.GetClient<WorkHttpClient>();

            Guid projectId = ClientSampleHelpers.FindAnyProject(this.Context).Id;
            Guid teamId = ClientSampleHelpers.FindAnyTeam(this.Context, projectId).Id;
            var context = new TeamContext(projectId, teamId);

            List<BoardReference> allBoards = workClient.GetBoardsAsync(context).Result;
            BoardReference board = allBoards.FirstOrDefault();
            BoardUserSettings result = workClient.GetBoardUserSettingsAsync(context, board.Id.ToString()).Result;

            Console.WriteLine("{0}'s auto refresh state: {1}", board.Name, result.AutoRefreshState);

            return result;
        }

        [ClientSampleMethod]
        public BoardUserSettings UpdateBoardUserSettings()
        {
            BoardUserSettings boardUserSettings = new BoardUserSettings() {
                AutoRefreshState = true
            };
            Dictionary<string, string> settings = new Dictionary<string, string>() { };
            settings.Add("AutoRefreshState", true.ToString());

            VssConnection connection = Context.Connection;
            WorkHttpClient workClient = connection.GetClient<WorkHttpClient>();

            Guid projectId = ClientSampleHelpers.FindAnyProject(this.Context).Id;
            var context = new TeamContext(projectId);
            List<BoardReference> allBoards = workClient.GetBoardsAsync(context).Result;
            BoardReference board = allBoards.FirstOrDefault();

            var result = workClient.UpdateBoardUserSettingsAsync(settings, context, board.Id.ToString()).Result;

            Console.WriteLine("{0}'s auto refresh state: {1}", board.Name, result.AutoRefreshState);

            return result;
        }
    }
}
