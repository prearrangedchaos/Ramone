﻿using System.ServiceModel.Syndication;
using NUnit.Framework;
using Ramone.Tests.Common.CMS;


namespace Ramone.Tests
{
  [TestFixture]
  public class PostTests : TestHelper
  {
    Dossier MyDossier = new Dossier
    {
      Title = "A new dossier"
    };

    RamoneRequest DossiersReq;


    protected override void SetUp()
    {
      base.SetUp();
      DossiersReq = Session.Request(DossiersUrl);
    }


    [Test]
    public void CanPostAndIgnoreReturnedBody()
    {
      // Act
      RamoneResponse response = DossiersReq.Post(MyDossier);

      // Assert
      Assert.IsNotNull(response);
    }


    [Test]
    public void CanPostAndGetResult()
    {
      // Act
      RamoneResponse<Dossier> response = DossiersReq.Post<Dossier>(MyDossier);
      Dossier newDossier = response.Body;

      // Assert
      Assert.IsNotNull(newDossier);
    }


    [Test]
    public void CanPostAndGetResultWithAccept()
    {
      // Act
      Dossier newDossier = DossiersReq.Accept<Dossier>().Post(MyDossier).Body;

      // Assert
      Assert.IsNotNull(newDossier);
    }


    [Test]
    public void CanPostAndGetResultWithAcceptMediaType()
    {
      // Act
      Dossier newDossier = DossiersReq.Accept<Dossier>(CMSConstants.CMSMediaType).Post(MyDossier).Body;

      // Assert
      Assert.IsNotNull(newDossier);
    }
  }
}
