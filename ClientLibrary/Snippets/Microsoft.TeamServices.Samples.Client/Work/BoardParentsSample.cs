using Microsoft.VisualStudio.Services.WebApi;
using Microsoft.TeamFoundation.Core.WebApi.Types;
using Microsoft.TeamFoundation.Work.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.TeamServices.Samples.Client.Work
{
    [ClientSample(WorkWebConstants.RestArea, "boardparents")]
    public class BoardParentsSample : ClientSample
    {
        [ClientSampleMethod]
        public IEnumerable<ParentChildWIMap> GetBoardMappingParentItems()
        {
            VssConnection connection = Context.Connection;
            WorkHttpClient workClient = connection.GetClient<WorkHttpClient>();

            Guid projectId = ClientSampleHelpers.FindAnyProject(this.Context).Id;
            Guid teamId = ClientSampleHelpers.FindAnyTeam(this.Context, projectId).Id;
            var context = new TeamContext(projectId, teamId);

            string childBacklogContextCategoryRefName = "Microsoft.RequirementCategory";
            List<int> workItemIds = new List<int>() { 5, 6, 7, 8};

            IEnumerable<ParentChildWIMap> result = workClient.GetBoardMappingParentItemsAsync(
                context, 
                childBacklogContextCategoryRefName, 
                workItemIds).Result;

            Console.WriteLine("{0}'s auto refresh state: {1}");

            return result;
        }
    }
}