using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace IntegrationTests
{
    internal static class XmlHelpers
    {
        internal static XmlDocument LoadXmlDocument(this string fullPath)
            => new XmlDocument().UpdateDocument(doc => doc.Load(fullPath));

        internal static XmlDocument UpdateDocument(this XmlDocument doc, Action<XmlDocument> act)
        {
            act(doc);
            return doc;
        }
    }
}
