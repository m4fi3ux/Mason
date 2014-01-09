﻿namespace Mason.IssueTracker.Server
{
  public static class RelTypes
  {
    public const string NamespaceAlias = "is";
    public const string Namespace = "http://mason-issue-tracker.dk/rels/";

    public const string ResourceCommon = NamespaceAlias + ":common";
    public const string Contact = NamespaceAlias + ":contact";
    public const string Logo = NamespaceAlias + ":logo";
    public const string IssueQuery = NamespaceAlias + ":issue-query";
  }
}