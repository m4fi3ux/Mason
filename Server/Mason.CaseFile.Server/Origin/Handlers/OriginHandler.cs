﻿using Mason.CaseFile.Server.Origin.Resources;
using Mason.CaseFile.Server.ServiceIndex.Resources;
using Mason.CaseFile.Server.Utility;
using Mason.Net;
using OpenRasta.Web;
using System;
using System.Collections.Generic;


namespace Mason.CaseFile.Server.Origin.Handlers
{
  public class OriginHandler
  {
    public ICommunicationContext Context { get; set; }


    public object Get()
    {
      OriginResource result = new OriginResource
      {
        Value = new Contract.Origin
        {
          Title = "Ministry of Fun",
          Description = "This is the external case file API for the Ministry of Fun",
          Contact = new SubResource()
        }
      };

      ContactInformation c = new OriginContactHandler().Get();
      dynamic contact = result.Value.Contact;
      contact.Address1 = c.Address1;
      contact.Address2 = c.Address2;
      contact.PostalCode = c.PostalCode;
      contact.City = c.City;
      contact.Phone = c.Phone;
      contact.Country = c.Country;

      Uri contactSelfUri = typeof(OriginContactResource).CreateUri();
      Link contactSelfLink = new Link("self", contactSelfUri, "Complete contact information in standard formats such as vCard and jCard");
      contact.Links.Add(contactSelfLink);

      Uri logoUri = new Uri(Context.ApplicationBaseUri.EnsureHasTrailingSlash(), "Origins/MinistryOfFun/logo.png");
      Link logoLink = new Link("cf:logo", logoUri);
      result.Value.Links.Add(logoLink);

      Uri serviceIndexUri = typeof(ServiceIndexResource).CreateUri();
      Link serviceIndexLink = new Link("cf:service-index", serviceIndexUri);
      result.Value.Links.Add(serviceIndexLink);

      return result;
    }
  }
}