﻿using System.Xml;
using NUnit.Framework;
using Ramone.Tests.Common.CMS;
using System;


namespace Ramone.Tests.MediaTypes.Xml
{
  [TestFixture]
  public class XmlDocumentCodecTests : TestHelper
  {
    [Test]
    public void CanReadXmlDocument()
    {
      // Arrange
      Request req = Session.Bind(DossierTemplate, new { id = 5 });

      // Act
      XmlDocument doc = req.Get<XmlDocument>().Body;

      // Assert
      Assert.IsNotNull(doc.SelectSingleNode("//Dossier"));
      Assert.AreEqual("5", doc.SelectSingleNode("//Dossier/Id").InnerText);
    }


    [Test]
    public void CanWriteXmlDocument()
    {
      // Arrange
      XmlDocument dossierDoc = new XmlDocument();
      dossierDoc.LoadXml("<Dossier><Title>My dossier</Title></Dossier>");

      Request request = Session.Request(DossiersUrl);

      // Act
      Response<Dossier> response = request.ContentType("application/xml").Post<Dossier>(dossierDoc);

      // Assert
      Dossier createdDossier = response.Body;

      Assert.IsNotNull(createdDossier);
      Assert.AreEqual("My dossier", createdDossier.Title);
    }


    [Test]
    public void CanReadXmlDocumentWithEncoding(
      [Values("UTF-8", "Windows-1252", "iso-8859-1")] string charset)
    {
      // Arrange
      Request req = Session.Bind(EncodingTemplate, new { type = "xml" });

      // Act
      var response = req.AcceptCharset(charset).Get<XmlDocument>();
      XmlDocument doc = response.Body;

      // Assert
      XmlNode nameNode = doc.SelectSingleNode("/html/body");
      Assert.IsNotNull(nameNode);

      Assert.AreEqual(charset, response.WebResponse.Headers["X-accept-charset"]);
      Assert.AreEqual("ÆØÅúï´`'", nameNode.InnerText);
    }


    [Test]
    public void CanWriteXmlDocumentWithEncoding(
      [Values("UTF-8", "Windows-1252", "iso-8859-1")] string charsetIn,
      [Values("UTF-8", "Windows-1252", "iso-8859-1")] string charsetOut)
    {
      // Arrange
      XmlDocument doc = new XmlDocument();
      doc.LoadXml("<Text>ÆØÅüî</Text>");

      Request request = Session.Bind(EncodingTemplate, new { type = "xml" });

      // Act
      Response<XmlDocument> response = request.AcceptCharset(charsetOut)
                                                    .ContentType("application/xml")
                                                    .Charset(charsetIn)
                                                    .Post<XmlDocument>(doc);

      // Assert
      XmlDocument result = response.Body;

      Assert.IsNotNull(result);
      XmlNode textNode = result.SelectSingleNode("/Text");
      Assert.IsNotNull(textNode);

      Assert.AreEqual(charsetIn, response.WebResponse.Headers["X-request-charset"]);
      Assert.AreEqual(charsetOut, response.WebResponse.Headers["X-accept-charset"]);
      Assert.AreEqual("ÆØÅüî", textNode.InnerText);
    }
  }
}
