﻿using System;
using System.Text;
using Ramone.Utility.ObjectSerialization;


namespace Ramone
{
  public interface IService : IHaveRequestInterceptors
  {
    ICodecManager CodecManager { get; }

    string UserAgent { get; set; }

    Uri BaseUri { get; }

    Encoding DefaultEncoding { get; set; }

    MediaType DefaultRequestMediaType { get; set; }

    MediaType DefaultResponseMediaType { get; set; }

    IAuthorizationDispatcher AuthorizationDispatcher { get; }

    ObjectSerializerSettings SerializerSettings { get; set; }

    void SetAllowedRedirects(int responseCode, int redirectCount);

    int GetAllowedRedirects(int responseCode);

    void CopyRedirect(ISession session);

    ISession NewSession();
  }
}
