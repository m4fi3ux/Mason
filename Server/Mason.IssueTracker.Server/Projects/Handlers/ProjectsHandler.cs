﻿using Mason.IssueTracker.Server.Domain.Projects;
using Mason.IssueTracker.Server.Projects.Resources;
using OpenRasta.Web;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Mason.IssueTracker.Server.Projects.Handlers
{
  public class ProjectsHandler : BaseHandler
  {
    #region Dependencies

    public IProjectRepository ProjectRepository { get; set; }

    #endregion


    public object Get()
    {
      return ExecuteInUnitOfWork(() =>
      {
        IEnumerable<Project> projects = ProjectRepository.FindAll();

        ProjectCollectionResource result = new ProjectCollectionResource();
        result.Projects = projects.ToList();

        return result;
      });
    }


    public object Post(AddProjectArgs args)
    {
      return ExecuteInUnitOfWork(() =>
      {
        Project p = new Project(args.Code, args.Title, args.Description);
        ProjectRepository.Add(p);

        Uri projectUrl = typeof(ProjectResource).CreateUri(new { id = p.Id });

        return new OperationResult.Created { RedirectLocation = projectUrl };
      });
    }
  }
}
