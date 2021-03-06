﻿using System;
using System.IO;
using JsonFx.Json;


namespace Ramone.MediaTypes.Json
{
  public class JsonSerializerCodec : TextCodecBase<object>
  {
    protected override object ReadFrom(TextReader reader, ReaderContext context)
    {
      JsonReader jsr = new JsonReader();
      string text = reader.ReadToEnd();
      // FIXME: should be able to read directly from reader, but fails
      return jsr.Read(text, context.DataType);
    }


    protected override void WriteTo(object item, TextWriter writer, WriterContext context)
    {
      if (item == null)
        throw new ArgumentNullException("item");

      JsonWriter jsw = new JsonWriter();
      jsw.Write(item, writer);
    }
  }
}
