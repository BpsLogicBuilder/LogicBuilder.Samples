using Contoso.Utils;
using LogicBuilder.Expressions.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Contoso.Domain.Attributes
{
    abstract internal class AttributeRewriter
    {
        #region Constants
        private const string STRINGLENGTHATTRIBUTE = "System.ComponentModel.DataAnnotations.StringLengthAttribute";
        private const string MAXLENGTHATTRIBUTE = "System.ComponentModel.DataAnnotations.MaxLengthAttribute";
        private const string MINLENGTHATTRIBUTE = "System.ComponentModel.DataAnnotations.MinLengthAttribute";
        private const string RANGEATTRIBUTE = "System.ComponentModel.DataAnnotations.RangeAttribute";
        private const string REQUIREDATTRIBUTE = "System.ComponentModel.DataAnnotations.RequiredAttribute";
        private const string DISPLAYATTRIBUTE = "System.ComponentModel.DataAnnotations.DisplayAttribute";
        private const string DISPLAYFORMATATTRIBUTE = "System.ComponentModel.DataAnnotations.DisplayFormatAttribute";
        private const string DATATYPEATTRIBUTE = "System.ComponentModel.DataAnnotations.DataTypeAttribute";
        private const string REGULAREXPRESSIONATTRIBUTE = "System.ComponentModel.DataAnnotations.RegularExpressionAttribute";
        public const string EQUALSIGN = "=";


        #endregion Constants

        #region Properties
        public abstract string AttributeString { get; }
        #endregion Properties

        #region Methods
        private static AttributeRewriter CreateAttributeRewriter(object attr)
        {
            if (attr == null)
                return null;

            string typeName = attr.GetType().FullName;
            switch (typeName)
            {
                case STRINGLENGTHATTRIBUTE:
                    return new StringLengthAttributeRewriter(attr as StringLengthAttribute);
                case RANGEATTRIBUTE:
                    return new RangeAttributeRewriter(attr as RangeAttribute);
                case REQUIREDATTRIBUTE:
                    return new RequiredAttributeRewriter(attr as RequiredAttribute);
                case DISPLAYATTRIBUTE:
                    return new DisplayAttributeRewriter(attr as DisplayAttribute);
                case DISPLAYFORMATATTRIBUTE:
                    return new DisplayFormatAttributeRewriter(attr as DisplayFormatAttribute);
                case DATATYPEATTRIBUTE:
                    return new DataTypeAttributeRewriter(attr as DataTypeAttribute);
                case REGULAREXPRESSIONATTRIBUTE:
                    return new RegularExpressionAttributeRewriter(attr as RegularExpressionAttribute);
                //case CONTROLNAMEATTRIBUTE:
                //    return new ControlNameAttributeReader(attr as ControlAttribute);
                //case NAMEVALUEATTRIBUTE:
                //    return new NameValueAttributeReader(attr as NameValueAttribute);
                //case ERRORMESSAGEATTRIBUTE:
                //    return new ErrorMessageAttributeReader(attr as ErrorMessageAttribute);
                default:
                    return null;
            }
        }

        internal static List<string> GetParameterAttributes(PropertyInfo pInfo)
        {
            List<string> attributeStrings = pInfo.GetCustomAttributes(true).Aggregate(new List<string>(), (list, next) =>
            {
                AttributeRewriter attributeRewriter = AttributeRewriter.CreateAttributeRewriter(next);
                if (attributeRewriter != null && !string.IsNullOrEmpty(attributeRewriter.AttributeString))
                    list.Add(attributeRewriter.AttributeString);
                return list;
            });

            if (pInfo.PropertyType.IsLiteralType())
                attributeStrings.Add(new VariableEditorControlAttributeRewriter(pInfo).AttributeString);

            //This may become an option when we can update literal lists for variables like we do for parameters.
            if (pInfo.PropertyType.IsValidList())
                attributeStrings.Add(new ListEditorControlAttributeRewriter(pInfo).AttributeString);

            attributeStrings.Add(new AlsoKnownAsAttributeRewriter(pInfo).AttributeString);
            return attributeStrings;
        }
        #endregion Methods
    }
}
