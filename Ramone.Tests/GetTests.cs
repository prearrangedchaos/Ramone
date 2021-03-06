﻿using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Ramone.HyperMedia;
using Ramone.Tests.Common.CMS;


namespace Ramone.Tests
{
  [TestFixture]
  public class GetTests : TestHelper
  {
    [Test]
    public void CanGetDossier()
    {
      // Arrange
      Request dossierReq = Session.Bind(DossierTemplate, new { id = 8 });

      // Act
      Dossier dossier = dossierReq.Get<Dossier>().Body;

      // Assert
      Assert.AreEqual(8, dossier.Id);
      Assert.AreEqual("Dossier no. 8", dossier.Title);
      Assert.IsNotNull(dossier.Links);
    }


    [Test]
    public void CanGetDossierWithDictionaryParams()
    {
      // Arrange
      Dictionary<string, string> p = new Dictionary<string, string>();
      p["id"] = "8";
      Request dossierReq = Session.Bind(DossierTemplate, p);

      // Act
      Dossier dossier = dossierReq.Get<Dossier>().Body;

      // Assert
      Assert.AreEqual(8, dossier.Id);
      Assert.AreEqual("Dossier no. 8", dossier.Title);
      Assert.IsNotNull(dossier.Links);
    }


    [Test]
    public void CanGetDocument()
    {
      // Arrange
      Request documentReq = Session.Bind(DocumentTemplate, new { id = 1 });

      // Act
      Document document = documentReq.Get<Document>().Body;

      // Assert
      Assert.AreEqual(1, document.Id);
      Assert.AreEqual("Document no. 1", document.Title);
    }


    [Test]
    public void CanGetDossierDocuments()
    {
      // Arrange
      Request dossierDocumentsReq = Session.Bind(DossierDocumentsTemplate, new { id = 8 });

      // Act
      DossierDocumentList documents = dossierDocumentsReq.Get<DossierDocumentList>().Body;

      // Assert
      Assert.IsNotNull(documents);
      Assert.AreEqual(2, documents.Count);
    }


    [Test]
    public void CanGetParty()
    {
      // Arrange
      Request partyReq = Session.Bind(PartyTemplate, new { id = 12 });

      // Act
      Party party = partyReq.Get<Party>().Body;

      // Assert
      Assert.AreEqual(12, party.Id);
      Assert.AreEqual("Bart-12", party.FullName);
      Assert.AreEqual("bart-12@foo.bar", party.EMail);
    }


    [Test]
    public void CanGetDocumentLinksInDossier()
    {
      // Arrange
      Request dossierReq = Session.Bind(DossierTemplate, new { id = 8 });
      Dossier dossier = dossierReq.Get<Dossier>().Body;

      // Act
      ILink documentsLink = dossier.Links.Select(CMSConstants.DocumentsLinkRelType);

      // Assert
      Assert.IsNotNull(documentsLink);
      Assert.Contains(CMSConstants.DocumentsLinkRelType, documentsLink.RelationTypes.ToList());
    }


    [Test]
    public void CanGetAndIgnoreReturnedBody()
    {
      // Arrange
      Request dossierReq = Session.Bind(DossierTemplate, new { id = 8 });

      // Act
      Response response = dossierReq.Get();
    }


    [Test]
    public void WhenSpecifyingCharsetForGetItThrows()
    {
      // Arrange
      Request dossierReq = Session.Bind(DossierTemplate, new { id = 8 });

      // Act + Assert
      AssertThrows<InvalidOperationException>(() => dossierReq.Charset("utf-8").Get());
      AssertThrows<InvalidOperationException>(() => dossierReq.Charset("utf-8").Get<Dossier>());
    }


#if false
    [Test]
    public void CanGetSimpleAtomFeed()
    {
      RamoneResponse<SyndicationFeed> response = FeedEndPoint.Get<SyndicationFeed>(new { feed = "Petes" });
      Assert.AreEqual("Petes", response.Body.Title.Text);
    }


    [Test]
    public void CanGetSimpleAtomFeedThroughExplicitBinding()
    {
      RamoneResponse<SyndicationFeed> response = FeedEndPoint.Bind(new { feed = "Petes" })
                                                            .Get<SyndicationFeed>();
      Assert.AreEqual("Petes", response.Body.Title.Text);
    }


    [Test]
    public void CanGetStringEvenWhenMultipleCodecsExistsAsLongAsAMediaTypeIsReturned()
    {
      string result = TextEndPoint.Get<string>().Body;
      Assert.AreEqual("plain text", result);
    }


    [Test]
    public void WhenRequestingMissingResourceItThrowsNotFound()
    {
      AssertThrows<RamoneException>(
        () => FeedEndPoint.Get<SyndicationFeed>(new { feed = "Unknown" }),
        (e) => e.Response.StatusCode == HttpStatusCode.NotFound);
    }


    [Test]
    public void WhenRequestingUnsupportedMediaTypeItThrowsUnsupportedMediaType()
    {
      AssertThrows<RamoneException>(
        () => FeedEndPoint.Get<string>(new { feed = "Petes" }),
        (e) => e.Response.StatusCode == HttpStatusCode.NotAcceptable);
    }
#endif
  }
}
