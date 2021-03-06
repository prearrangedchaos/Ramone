﻿using NUnit.Framework;
using Ramone.Tests.Common;
using System;
using System.Xml;
using Ramone.Tests.Common.CMS;


namespace Ramone.Tests
{
  [TestFixture]
  public class ContentNegotiationTests : TestHelper
  {
    [Test]
    public void CanGetCatUsingDefaultContentType()
    {
      // Arrange
      Request catReq = Session.Bind(CatTemplate, new { name = "Fiona" });

      // Act
      Cat c = catReq.Get<Cat>().Body;

      // Assert
      Assert.AreEqual("Fiona", c.Name);
    }


    [Test]
    public void CanGetCatAsHtml()
    {
      // Arrange
      Request catReq = Session.Bind(CatTemplate, new { name = "Fiona" });

      // Act
      Cat c = catReq.Accept("text/html").Get<Cat>().Body;

      // Assert
      Assert.AreEqual("<html><body><p>Fiona</p></body></html>", c.Name);
    }


    [Test]
    public void CanGetCatAsText()
    {
      // Arrange
      Request catReq = Session.Bind(CatTemplate, new { name = "Fiona" });

      // Act
      Cat c = catReq.Accept("text/plain").Get<Cat>().Body;

      // Assert
      Assert.AreEqual("Fiona", c.Name);
    }


    [Test]
    public void CanGetCatAsTextWithShorthandNotation()
    {
      // Arrange
      Request catReq = Session.Bind(CatTemplate, new { name = "Fiona" });

      // Act
      Cat c = catReq.Accept("text/plain").Get<Cat>().Body;

      // Assert
      Assert.AreEqual("Fiona", c.Name);
    }


    [Test]
    public void CanGetCatAsRawXml()
    {
      // Arrange
      Request catReq = Session.Bind(CatTemplate, new { name = "Fiona" });

      // Act
      XmlDocument c = catReq.Accept("application/xml").Get<XmlDocument>().Body;

      // Assert
      Assert.IsNotNull(c);
      XmlNode nameNode = c.SelectSingleNode("//Cat/Name");
      Assert.IsNotNull(nameNode);
      Assert.AreEqual("Fiona", nameNode.InnerText);
    }


    [Test]
    public void WhenPostingWithoutSpecifyingContentTypeItSelectsRandomCodec()
    {
      // Arrange
      Cat c1 = new Cat { Name = "Monster Baby" };
      Request catReq = Session.Bind(CatTemplate, new { name = "Fiona" });

      // Act + Assert
      Cat c2 = catReq.Post<Cat>(c1).Body;
      Assert.AreEqual("Fiona", c2.Name);
    }


    [Test]
    public void CanPostCatAsText()
    {
      // Arrange
      Cat c = new Cat { Name = "Monster Baby" };
      Request catReq = Session.Bind(CatTemplate, new { name = "Fiona" });

      // Act + Assert
      catReq.ContentType("text/plain").Post<Cat>(c);
    }


    [Test]
    public void CanPostCatAsHtml()
    {
      // Arrange
      Cat c = new Cat { Name = "Monster Baby" };
      Request catReq = Session.Bind(CatTemplate, new { name = "Fiona" });

      // Act + Assert
      catReq.ContentType("text/html").Post<Cat>(c);
    }


    [Test]
    public void CanSpecifyAcceptAsGenericWithoutMediaType()
    {
      // Arrange
      Request dossierReq = Session.Bind(DossierTemplate, new { id = 8 });

      // Act
      Dossier dossier = dossierReq.Accept<Dossier>().Get().Body;

      // Assert
      Assert.IsNotNull(dossier);
    }


    [Test]
    public void CanSpecifyAcceptAsGenericWithMediaType()
    {
      // Arrange
      Request catReq = Session.Bind(CatTemplate, new { name = "Fiona" });

      // Act
      Cat c = catReq.Accept<Cat>("text/plain").Get().Body;

      // Assert
      Assert.IsNotNull(c);
    }


    [Test]
    public void WhenNoReaderCodecExistsItThrowsInvalidOperation()
    {
      AssertThrows<InvalidOperationException>(() => Session.Request("http://knoiiukjkjh").Get<UnsupportClass>());
    }


    public class UnsupportClass
    {
    }
  }
}
