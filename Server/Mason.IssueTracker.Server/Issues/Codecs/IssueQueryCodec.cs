﻿using Mason.IssueTracker.Server.Issues.Resources;
using Mason.IssueTracker.Server.Codecs;
using Mason.IssueTracker.Server.Domain.Issues;
using MasonBuilder.Net;
using OpenRasta.Web;
using System;
using System.Collections.Generic;


namespace Mason.IssueTracker.Server.Issues.Codecs
{
  public class IssueQueryCodec : IssueTrackerMasonCodec<IssueQueryResource>
  {
    protected override Resource ConvertToIssueTracker(IssueQueryResource resource)
    {
      Resource result = new Resource();

      if (!CommunicationContext.PreferMinimalResponse())
      {
        result.Meta.Title = "Query result";
        result.Meta.Description = "This is the result of a query for issues. Follow issues 'self' link to get more information about individual issues.";
      }

      result.AddNavigation(CommunicationContext.BuildIssueQueryTemplate());

      ((dynamic)result).Issues = new List<SubResource>();

      foreach (Issue i in resource.Issues)
      {
        dynamic item = new SubResource();
        item.ID = i.Id.ToString();
        item.Title = i.Title;

        Uri itemSelfUri = typeof(IssueResource).CreateUri(new { id = i.Id });
        Link itemSelfLink = CommunicationContext.NewLink("self", itemSelfUri);
        item.AddNavigation(itemSelfLink);

        ((dynamic)result).Issues.Add(item);
      }

      Link selfLink = CommunicationContext.NewLink("self", resource.SelfUri);
      result.AddNavigation(selfLink);

      return result;
    }
  }
}
