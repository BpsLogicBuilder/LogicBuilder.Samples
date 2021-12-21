using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using Contoso.AutoMapperProfiles;
using Contoso.Common.Configuration.ExpressionDescriptors;
using Contoso.Bsl.Flow.Unit.Tests.Data;
using Contoso.Parameters.Expressions;
using LogicBuilder.Expressions.Utils.ExpressionBuilder.Lambda;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OData.Edm;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Xunit;

namespace Contoso.Bsl.Flow.Unit.Tests
{
    public class FilterParameterTests
    {
        public FilterParameterTests()
        {
            Initialize();
        }

        #region Fields
        private IServiceProvider serviceProvider;
        private static readonly string parameterName = "$it";
        #endregion Fields

        #region Inequalities
        [Theory]
        [InlineData(null, true)]
        [InlineData("", false)]
        [InlineData("Doritos", false)]
        public void EqualityOperatorWithNull(string productName, bool expected)
        {
            //act
            var filter = CreateFilter<Product>();
            bool result = RunFilter(filter, new Product { ProductName = productName });

            //assert
            AssertFilterStringIsCorrect(filter, "$it => ($it.ProductName == null)");
            Assert.Equal(expected, result);

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    new EqualsBinaryOperatorParameters
                    (
                        new MemberSelectorOperatorParameters("ProductName", new ParameterOperatorParameters(parameterName)),
                        new ConstantOperatorParameters(null)
                    )
                );
        }

        [Theory]
        [InlineData(null, false)]
        [InlineData("", false)]
        [InlineData("Doritos", true)]
        public void EqualityOperator(string productName, bool expected)
        {
            //act
            var filter = CreateFilter<Product>();
            bool result = RunFilter(filter, new Product { ProductName = productName });

            //assert
            AssertFilterStringIsCorrect(filter, "$it => ($it.ProductName == \"Doritos\")");
            Assert.Equal(expected, result);

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    new EqualsBinaryOperatorParameters
                    (
                        new MemberSelectorOperatorParameters("ProductName", new ParameterOperatorParameters(parameterName)),
                        new ConstantOperatorParameters("Doritos", typeof(string))
                    )
                );
        }

        [Theory]
        [InlineData(null, true)]
        [InlineData("", true)]
        [InlineData("Doritos", false)]
        public void NotEqualOperatorParameter(string productName, bool expected)
        {
            //act
            var filter = CreateFilter<Product>();
            bool result = RunFilter(filter, new Product { ProductName = productName });

            //assert
            AssertFilterStringIsCorrect(filter, "$it => ($it.ProductName != \"Doritos\")");
            Assert.Equal(expected, result);

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    new NotEqualsBinaryOperatorParameters
                    (
                        new MemberSelectorOperatorParameters("ProductName", new ParameterOperatorParameters(parameterName)),
                        new ConstantOperatorParameters("Doritos", typeof(string))
                    )
                );
        }

        [Theory]
        [InlineData(null, false)]
        [InlineData(5.01, true)]
        [InlineData(4.99, false)]
        public void GreaterThanOperatorParameter(object unitPrice, bool expected)
        {
            //act
            var filter = CreateFilter<Product>();
            bool result = RunFilter(filter, new Product { UnitPrice = ToNullable<decimal>(unitPrice) });

            //assert
            AssertFilterStringIsCorrect(filter, string.Format(CultureInfo.InvariantCulture, "$it => ($it.UnitPrice > Convert({0:0.00}))", 5.0));
            Assert.Equal(expected, result);

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    new GreaterThanBinaryOperatorParameters
                    (
                        new MemberSelectorOperatorParameters("UnitPrice", new ParameterOperatorParameters(parameterName)),
                        new ConstantOperatorParameters(5.00m, typeof(decimal))
                    )
                );
        }

        [Theory]
        [InlineData(null, false)]
        [InlineData(5.0, true)]
        [InlineData(4.99, false)]
        public void GreaterThanEqualOperatorParameter(object unitPrice, bool expected)
        {
            //act
            var filter = CreateFilter<Product>();
            bool result = RunFilter(filter, new Product { UnitPrice = ToNullable<decimal>(unitPrice) });

            //assert
            AssertFilterStringIsCorrect(filter, string.Format(CultureInfo.InvariantCulture, "$it => ($it.UnitPrice >= Convert({0:0.00}))", 5.0));
            Assert.Equal(expected, result);

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    new GreaterThanOrEqualsBinaryOperatorParameters
                    (
                        new MemberSelectorOperatorParameters("UnitPrice", new ParameterOperatorParameters(parameterName)),
                        new ConstantOperatorParameters(5.00m, typeof(decimal))
                    )
                );
        }

        [Theory]
        [InlineData(null, false)]
        [InlineData(4.99, true)]
        [InlineData(5.01, false)]
        public void LessThanOperatorParameter(object unitPrice, bool expected)
        {
            //act
            var filter = CreateFilter<Product>();
            bool result = RunFilter(filter, new Product { UnitPrice = ToNullable<decimal>(unitPrice) });

            //assert
            AssertFilterStringIsCorrect(filter, string.Format(CultureInfo.InvariantCulture, "$it => ($it.UnitPrice < Convert({0:0.00}))", 5.0));
            Assert.Equal(expected, result);

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    new LessThanBinaryOperatorParameters
                    (
                        new MemberSelectorOperatorParameters("UnitPrice", new ParameterOperatorParameters(parameterName)),
                        new ConstantOperatorParameters(5.00m, typeof(decimal))
                    )
                );
        }

        [Theory]
        [InlineData(null, false)]
        [InlineData(5.0, true)]
        [InlineData(5.01, false)]
        public void LessThanOrEqualOperatorParameter(object unitPrice, bool expected)
        {
            //act
            var filter = CreateFilter<Product>();
            bool result = RunFilter(filter, new Product { UnitPrice = ToNullable<decimal>(unitPrice) });

            //assert
            AssertFilterStringIsCorrect(filter, string.Format(CultureInfo.InvariantCulture, "$it => ($it.UnitPrice <= Convert({0:0.00}))", 5.0));
            Assert.Equal(expected, result);

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    new LessThanOrEqualsBinaryOperatorParameters
                    (
                        new MemberSelectorOperatorParameters("UnitPrice", new ParameterOperatorParameters(parameterName)),
                        new ConstantOperatorParameters(5.00m, typeof(decimal))
                    )
                );
        }

        [Fact]
        public void NegativeNumbers()
        {
            //act
            var filter = CreateFilter<Product>();
            bool result = RunFilter(filter, new Product { UnitPrice = ToNullable<decimal>(44m) });

            //assert
            AssertFilterStringIsCorrect(filter, string.Format(CultureInfo.InvariantCulture, "$it => ($it.UnitPrice <= Convert({0:0.00}))", -5.0));
            Assert.False(result);

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    new LessThanOrEqualsBinaryOperatorParameters
                    (
                        new MemberSelectorOperatorParameters("UnitPrice", new ParameterOperatorParameters(parameterName)),
                        new ConstantOperatorParameters(-5.00m, typeof(decimal))
                    )
                );
        }

        public static List<object[]> DateTimeOffsetInequalities_Data
            => new List<object[]>
            {
                new object[]
                {
                    new EqualsBinaryOperatorParameters
                    (
                        new MemberSelectorOperatorParameters("DateTimeOffsetProp", new ParameterOperatorParameters(parameterName)),
                        new MemberSelectorOperatorParameters("DateTimeOffsetProp", new ParameterOperatorParameters(parameterName))
                    ),
                    "$it => ($it.DateTimeOffsetProp == $it.DateTimeOffsetProp)"
                },
                new object[]
                {
                    new NotEqualsBinaryOperatorParameters
                    (
                        new MemberSelectorOperatorParameters("DateTimeOffsetProp", new ParameterOperatorParameters(parameterName)),
                        new MemberSelectorOperatorParameters("DateTimeOffsetProp", new ParameterOperatorParameters(parameterName))
                    ),
                    "$it => ($it.DateTimeOffsetProp != $it.DateTimeOffsetProp)"
                },
                new object[]
                {
                    new GreaterThanOrEqualsBinaryOperatorParameters
                    (
                        new MemberSelectorOperatorParameters("DateTimeOffsetProp", new ParameterOperatorParameters(parameterName)),
                        new MemberSelectorOperatorParameters("DateTimeOffsetProp", new ParameterOperatorParameters(parameterName))
                    ),
                    "$it => ($it.DateTimeOffsetProp >= $it.DateTimeOffsetProp)"
                },
                new object[]
                {
                    new LessThanOrEqualsBinaryOperatorParameters
                    (
                        new MemberSelectorOperatorParameters("DateTimeOffsetProp", new ParameterOperatorParameters(parameterName)),
                        new MemberSelectorOperatorParameters("DateTimeOffsetProp", new ParameterOperatorParameters(parameterName))
                    ),
                    "$it => ($it.DateTimeOffsetProp <= $it.DateTimeOffsetProp)"
                }
            };

        [Theory]
        [MemberData(nameof(DateTimeOffsetInequalities_Data))]
        public void DateTimeOffsetInequalities(IExpressionParameter filterBody, string expectedExpression)
        {
            //act
            var filter = CreateFilter<DataTypes>();

            //assert
            AssertFilterStringIsCorrect(filter, expectedExpression);

            Expression<Func<T, bool>> CreateFilter<T>()
            {
                return GetFilter<T>
                (
                    filterBody
                );
            }
        }

        public static List<object[]> DateInEqualities_Data
            => new List<object[]>
            {
                new object[]
                {
                    new EqualsBinaryOperatorParameters
                    (
                        new MemberSelectorOperatorParameters("DateTimeProp", new ParameterOperatorParameters(parameterName)),
                        new MemberSelectorOperatorParameters("DateTimeProp", new ParameterOperatorParameters(parameterName))
                    ),
                    "$it => ($it.DateTimeProp == $it.DateTimeProp)"
                },
                new object[]
                {
                    new NotEqualsBinaryOperatorParameters
                    (
                        new MemberSelectorOperatorParameters("DateTimeProp", new ParameterOperatorParameters(parameterName)),
                        new MemberSelectorOperatorParameters("DateTimeProp", new ParameterOperatorParameters(parameterName))
                    ),
                    "$it => ($it.DateTimeProp != $it.DateTimeProp)"
                },
                new object[]
                {
                    new GreaterThanOrEqualsBinaryOperatorParameters
                    (
                        new MemberSelectorOperatorParameters("DateTimeProp", new ParameterOperatorParameters(parameterName)),
                        new MemberSelectorOperatorParameters("DateTimeProp", new ParameterOperatorParameters(parameterName))
                    ),
                    "$it => ($it.DateTimeProp >= $it.DateTimeProp)"
                },
                new object[]
                {
                    new LessThanOrEqualsBinaryOperatorParameters
                    (
                        new MemberSelectorOperatorParameters("DateTimeProp", new ParameterOperatorParameters(parameterName)),
                        new MemberSelectorOperatorParameters("DateTimeProp", new ParameterOperatorParameters(parameterName))
                    ),
                    "$it => ($it.DateTimeProp <= $it.DateTimeProp)"
                }
            };

        [Theory]
        [MemberData(nameof(DateInEqualities_Data))]
        public void DateInEqualities(IExpressionParameter filterBody, string expectedExpression)
        {
            //act
            var filter = CreateFilter<DataTypes>();

            //assert
            AssertFilterStringIsCorrect(filter, expectedExpression);

            Expression<Func<T, bool>> CreateFilter<T>()
            {
                return GetFilter<T>
                (
                    filterBody
                );
            }
        }
        #endregion Inequalities

        #region Logical Operators
        [Fact]
        public void BooleanOperatorNullableTypes()
        {
            //act
            var filter = CreateFilter<Product>();

            //assert
            AssertFilterStringIsCorrect(filter, "$it => (($it.UnitPrice == Convert(5.00)) OrElse ($it.CategoryID == 0))");

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    new OrBinaryOperatorParameters
                    (
                        new EqualsBinaryOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("UnitPrice", new ParameterOperatorParameters(parameterName)),
                            new ConstantOperatorParameters(5.00m, typeof(decimal))
                        ),
                        new EqualsBinaryOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("CategoryID", new ParameterOperatorParameters(parameterName)),
                            new ConstantOperatorParameters(0, typeof(int))
                        )
                    )
                );
        }

        [Fact]
        public void BooleanComparisonOnNullableAndNonNullableType()
        {
            //act
            var filter = CreateFilter<Product>();

            //assert
            AssertFilterStringIsCorrect(filter, "$it => ($it.Discontinued == Convert(True))");

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    new EqualsBinaryOperatorParameters
                    (
                        new MemberSelectorOperatorParameters("Discontinued", new ParameterOperatorParameters(parameterName)),
                        new ConstantOperatorParameters(true, typeof(bool))
                    )
                );
        }

        [Fact]
        public void BooleanComparisonOnNullableType()
        {
            //act
            var filter = CreateFilter<Product>();

            //assert
            AssertFilterStringIsCorrect(filter, "$it => ($it.Discontinued == $it.Discontinued)");

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    new EqualsBinaryOperatorParameters
                    (
                        new MemberSelectorOperatorParameters("Discontinued", new ParameterOperatorParameters(parameterName)),
                        new MemberSelectorOperatorParameters("Discontinued", new ParameterOperatorParameters(parameterName))
                    )
                );
        }

        [Theory]
        [InlineData(null, null, false)]
        [InlineData(5.0, 0, true)]
        [InlineData(null, 1, false)]
        public void OrOperatorParameter(object unitPrice, object unitsInStock, bool expected)
        {
            //act
            var filter = CreateFilter<Product>();
            bool result = RunFilter(filter, new Product { UnitPrice = ToNullable<decimal>(unitPrice), UnitsInStock = ToNullable<short>(unitsInStock) });

            //assert
            AssertFilterStringIsCorrect(filter, string.Format(CultureInfo.InvariantCulture, "$it => (($it.UnitPrice == Convert({0:0.00})) OrElse (Convert($it.UnitsInStock) == Convert({1})))", 5.0, 0));
            Assert.Equal(expected, result);

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    new OrBinaryOperatorParameters
                    (
                        new EqualsBinaryOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("UnitPrice", new ParameterOperatorParameters(parameterName)),
                            new ConstantOperatorParameters(5.00m, typeof(decimal))
                        ),
                        new EqualsBinaryOperatorParameters
                        (
                            new ConvertOperatorParameters(new MemberSelectorOperatorParameters("UnitsInStock", new ParameterOperatorParameters(parameterName)), typeof(int?)),
                            new ConstantOperatorParameters(0, typeof(int))
                        )
                    )
                );
        }

        [Theory]
        [InlineData(null, null, false)]
        [InlineData(5.0, 10, true)]
        [InlineData(null, 1, false)]
        public void AndOperatorParameter(object unitPrice, object unitsInStock, bool expected)
        {
            //act
            var filter = CreateFilter<Product>();
            bool result = RunFilter(filter, new Product { UnitPrice = ToNullable<decimal>(unitPrice), UnitsInStock = ToNullable<short>(unitsInStock) });

            //assert
            AssertFilterStringIsCorrect(filter, string.Format(CultureInfo.InvariantCulture, "$it => (($it.UnitPrice == Convert({0:0.00})) AndAlso (Convert($it.UnitsInStock) == Convert({1:0.00})))", 5.0, 10.0));
            Assert.Equal(expected, result);

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    new AndBinaryOperatorParameters
                    (
                        new EqualsBinaryOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("UnitPrice", new ParameterOperatorParameters(parameterName)),
                            new ConstantOperatorParameters(5.00m, typeof(decimal))
                        ),
                        new EqualsBinaryOperatorParameters
                        (
                            new ConvertOperatorParameters(new MemberSelectorOperatorParameters("UnitsInStock", new ParameterOperatorParameters(parameterName)), typeof(decimal?)),
                            new ConstantOperatorParameters(10.00m, typeof(decimal))
                        )
                    )
                );
        }

        [Theory]
        [InlineData(null, true)]
        [InlineData(5.0, false)]
        [InlineData(5.5, true)]
        public void Negation(object unitPrice, bool expected)
        {
            //act
            var filter = CreateFilter<Product>();
            bool result = RunFilter(filter, new Product { UnitPrice = ToNullable<decimal>(unitPrice) });

            //assert
            AssertFilterStringIsCorrect(filter, string.Format(CultureInfo.InvariantCulture, "$it => Not(($it.UnitPrice == Convert({0:0.00})))", 5.0));
            Assert.Equal(expected, result);

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    new NotOperatorParameters
                    (
                        new EqualsBinaryOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("UnitPrice", new ParameterOperatorParameters(parameterName)),
                            new ConstantOperatorParameters(5.00m, typeof(decimal))
                        )
                    )
                );
        }

        [Theory]
        [InlineData(true, false)]
        [InlineData(false, true)]
        public void BoolNegation(bool discontinued, bool expected)
        {
            //act
            var filter = CreateFilter<Product>();
            bool result = RunFilter(filter, new Product { Discontinued = discontinued });

            //assert
            AssertFilterStringIsCorrect(filter, "$it => Convert(Not($it.Discontinued))");
            Assert.Equal(expected, result);

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    new NotOperatorParameters
                    (
                        new MemberSelectorOperatorParameters("Discontinued", new ParameterOperatorParameters(parameterName))
                    )
                );
        }

        [Fact]
        public void NestedNegation()
        {
            //act
            var filter = CreateFilter<Product>();

            //assert
            AssertFilterStringIsCorrect(filter, "$it => Convert(Not(Not(Not($it.Discontinued))))");

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    new NotOperatorParameters
                    (
                        new NotOperatorParameters
                        (
                            new NotOperatorParameters
                            (
                                new MemberSelectorOperatorParameters("Discontinued", new ParameterOperatorParameters(parameterName))
                            )
                        )
                    )
                );
        }
        #endregion Logical Operators

        #region Arithmetic Operators
        [Theory]
        [InlineData(null, false)]
        [InlineData(5.0, true)]
        [InlineData(15.01, false)]
        public void Subtraction(object unitPrice, bool expected)
        {
            //act
            var filter = CreateFilter<Product>();
            bool result = RunFilter(filter, new Product { UnitPrice = ToNullable<decimal>(unitPrice) });

            //assert
            AssertFilterStringIsCorrect(filter, string.Format(CultureInfo.InvariantCulture, "$it => (($it.UnitPrice - Convert({0:0.00})) < Convert({1:0.00}))", 1.0, 5.0));
            Assert.Equal(expected, result);

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    new LessThanBinaryOperatorParameters
                    (
                        new SubtractBinaryOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("UnitPrice", new ParameterOperatorParameters(parameterName)),
                            new ConstantOperatorParameters(1.00m, typeof(decimal))
                        ),
                        new ConstantOperatorParameters(5.00m, typeof(decimal))
                    )
                );
        }

        [Fact]
        public void Addition()
        {
            //act
            var filter = CreateFilter<Product>();

            //assert
            AssertFilterStringIsCorrect(filter, string.Format(CultureInfo.InvariantCulture, "$it => (($it.UnitPrice + Convert({0:0.00})) < Convert({1:0.00}))", 1.0, 5.0));

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    new LessThanBinaryOperatorParameters
                    (
                        new AddBinaryOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("UnitPrice", new ParameterOperatorParameters(parameterName)),
                            new ConstantOperatorParameters(1.00m, typeof(decimal))
                        ),
                        new ConstantOperatorParameters(5.00m, typeof(decimal))
                    )
                );
        }

        [Fact]
        public void Multiplication()
        {
            //act
            var filter = CreateFilter<Product>();

            //assert
            AssertFilterStringIsCorrect(filter, string.Format(CultureInfo.InvariantCulture, "$it => (($it.UnitPrice * Convert({0:0.00})) < Convert({1:0.00}))", 1.0, 5.0));

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    new LessThanBinaryOperatorParameters
                    (
                        new MultiplyBinaryOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("UnitPrice", new ParameterOperatorParameters(parameterName)),
                            new ConstantOperatorParameters(1.00m, typeof(decimal))
                        ),
                        new ConstantOperatorParameters(5.00m, typeof(decimal))
                    )
                );
        }

        [Fact]
        public void Division()
        {
            //act
            var filter = CreateFilter<Product>();

            //assert
            AssertFilterStringIsCorrect(filter, string.Format(CultureInfo.InvariantCulture, "$it => (($it.UnitPrice / Convert({0:0.00})) < Convert({1:0.00}))", 1.0, 5.0));

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    new LessThanBinaryOperatorParameters
                    (
                        new DivideBinaryOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("UnitPrice", new ParameterOperatorParameters(parameterName)),
                            new ConstantOperatorParameters(1.00m, typeof(decimal))
                        ),
                        new ConstantOperatorParameters(5.00m, typeof(decimal))
                    )
                );
        }

        [Fact]
        public void Modulo()
        {
            //act
            var filter = CreateFilter<Product>();

            //assert
            AssertFilterStringIsCorrect(filter, string.Format(CultureInfo.InvariantCulture, "$it => (($it.UnitPrice % Convert({0:0.00})) < Convert({1:0.00}))", 1.0, 5.0));

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    new LessThanBinaryOperatorParameters
                    (
                        new ModuloBinaryOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("UnitPrice", new ParameterOperatorParameters(parameterName)),
                            new ConstantOperatorParameters(1.00m, typeof(decimal))
                        ),
                        new ConstantOperatorParameters(5.00m, typeof(decimal))
                    )
                );
        }
        #endregion Arithmetic Operators

        #region NULL handling
        public static List<object[]> NullHandling_Data
            => new List<object[]>
            {
                new object[]
                {
                    new EqualsBinaryOperatorParameters
                    (
                        new MemberSelectorOperatorParameters("UnitsInStock", new ParameterOperatorParameters(parameterName)),
                        new MemberSelectorOperatorParameters("UnitsOnOrder", new ParameterOperatorParameters(parameterName))
                    ),
                    null,
                    null,
                    true
                },
                new object[]
                {
                    new NotEqualsBinaryOperatorParameters
                    (
                        new MemberSelectorOperatorParameters("UnitsInStock", new ParameterOperatorParameters(parameterName)),
                        new MemberSelectorOperatorParameters("UnitsOnOrder", new ParameterOperatorParameters(parameterName))
                    ),
                    null,
                    null,
                    false
                },
                new object[]
                {
                    new GreaterThanBinaryOperatorParameters
                    (
                        new MemberSelectorOperatorParameters("UnitsInStock", new ParameterOperatorParameters(parameterName)),
                        new MemberSelectorOperatorParameters("UnitsOnOrder", new ParameterOperatorParameters(parameterName))
                    ),
                    null,
                    null,
                    false
                },
                new object[]
                {
                    new GreaterThanOrEqualsBinaryOperatorParameters
                    (
                        new MemberSelectorOperatorParameters("UnitsInStock", new ParameterOperatorParameters(parameterName)),
                        new MemberSelectorOperatorParameters("UnitsOnOrder", new ParameterOperatorParameters(parameterName))
                    ),
                    null,
                    null,
                    false
                },
                new object[]
                {
                    new LessThanBinaryOperatorParameters
                    (
                        new MemberSelectorOperatorParameters("UnitsInStock", new ParameterOperatorParameters(parameterName)),
                        new MemberSelectorOperatorParameters("UnitsOnOrder", new ParameterOperatorParameters(parameterName))
                    ),
                    null,
                    null,
                    false
                },
                new object[]
                {
                    new LessThanOrEqualsBinaryOperatorParameters
                    (
                        new MemberSelectorOperatorParameters("UnitsInStock", new ParameterOperatorParameters(parameterName)),
                        new MemberSelectorOperatorParameters("UnitsOnOrder", new ParameterOperatorParameters(parameterName))
                    ),
                    null,
                    null,
                    false
                },
                new object[]
                {
                    new EqualsBinaryOperatorParameters
                    (
                        new AddBinaryOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("UnitsInStock", new ParameterOperatorParameters(parameterName)),
                            new MemberSelectorOperatorParameters("UnitsOnOrder", new ParameterOperatorParameters(parameterName))
                        ),
                        new MemberSelectorOperatorParameters("UnitsInStock", new ParameterOperatorParameters(parameterName))
                    ),
                    null,
                    null,
                    true
                },
                new object[]
                {
                    new EqualsBinaryOperatorParameters
                    (
                        new SubtractBinaryOperatorParameters

                        (
                            new MemberSelectorOperatorParameters("UnitsInStock", new ParameterOperatorParameters(parameterName)),
                            new MemberSelectorOperatorParameters("UnitsOnOrder", new ParameterOperatorParameters(parameterName))
                        ),
                        new MemberSelectorOperatorParameters("UnitsInStock", new ParameterOperatorParameters(parameterName))
                    ),
                    null,
                    null,
                    true
                },
                new object[]
                {
                    new EqualsBinaryOperatorParameters
                    (
                        new MultiplyBinaryOperatorParameters

                        (
                            new MemberSelectorOperatorParameters("UnitsInStock", new ParameterOperatorParameters(parameterName)),
                            new MemberSelectorOperatorParameters("UnitsOnOrder", new ParameterOperatorParameters(parameterName))
                        ),
                        new MemberSelectorOperatorParameters("UnitsInStock", new ParameterOperatorParameters(parameterName))
                    ),
                    null,
                    null,
                    true
                },
                new object[]
                {
                    new EqualsBinaryOperatorParameters
                    (
                        new DivideBinaryOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("UnitsInStock", new ParameterOperatorParameters(parameterName)),
                            new MemberSelectorOperatorParameters("UnitsOnOrder", new ParameterOperatorParameters(parameterName))
                        ),
                        new MemberSelectorOperatorParameters("UnitsInStock", new ParameterOperatorParameters(parameterName))
                    ),
                    null,
                    null,
                    true
                },
                new object[]
                {
                    new EqualsBinaryOperatorParameters
                    (
                        new ModuloBinaryOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("UnitsInStock", new ParameterOperatorParameters(parameterName)),
                            new MemberSelectorOperatorParameters("UnitsOnOrder", new ParameterOperatorParameters(parameterName))
                        ),
                        new MemberSelectorOperatorParameters("UnitsInStock", new ParameterOperatorParameters(parameterName))
                    ),
                    null,
                    null,
                    true
                },
                new object[]
                {
                    new EqualsBinaryOperatorParameters
                    (
                        new MemberSelectorOperatorParameters("UnitsInStock", new ParameterOperatorParameters(parameterName)),
                        new MemberSelectorOperatorParameters("UnitsOnOrder", new ParameterOperatorParameters(parameterName))
                    ),
                    1,
                    null,
                    false
                },
                new object[]
                {
                    new EqualsBinaryOperatorParameters
                    (
                        new MemberSelectorOperatorParameters("UnitsInStock", new ParameterOperatorParameters(parameterName)),
                        new MemberSelectorOperatorParameters("UnitsOnOrder", new ParameterOperatorParameters(parameterName))
                    ),
                    1,
                    1,
                    true
                }
            };

        [Theory]
        [MemberData(nameof(NullHandling_Data))]
        public void NullHandling(IExpressionParameter filterBody, object unitsInStock, object unitsOnOrder, bool expected)
        {
            //act
            var filter = CreateFilter<Product>();
            bool result = RunFilter(filter, new Product { UnitsInStock = ToNullable<short>(unitsInStock), UnitsOnOrder = ToNullable<short>(unitsOnOrder) });

            //assert
            Assert.Equal(expected, result);

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    filterBody
                );
        }

        public static List<object[]> NullHandling_LiteralNull_Data
            => new List<object[]>
            {
                new object[]
                {
                    new EqualsBinaryOperatorParameters
                    (
                        new MemberSelectorOperatorParameters("UnitsInStock", new ParameterOperatorParameters(parameterName)),
                        new ConstantOperatorParameters(null)
                    ),
                    null,
                    true
                },
                new object[]
                {
                    new NotEqualsBinaryOperatorParameters
                    (
                        new MemberSelectorOperatorParameters("UnitsInStock", new ParameterOperatorParameters(parameterName)),
                        new ConstantOperatorParameters(null)
                    ),
                    null,
                    false
                }
            };

        [Theory]
        [MemberData(nameof(NullHandling_LiteralNull_Data))]
        public void NullHandling_LiteralNull(IExpressionParameter filterBody, object unitsInStock, bool expected)
        {
            //act
            var filter = CreateFilter<Product>();
            bool result = RunFilter(filter, new Product { UnitsInStock = ToNullable<short>(unitsInStock) });

            //assert
            Assert.Equal(expected, result);

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    filterBody
                );
        }
        #endregion NULL handling

        #region Other
        public static List<object[]> ComparisonsInvolvingCastsAndNullableValues_Data
            => new List<object[]>
            {
                new object[]
                {
                    new GreaterThanBinaryOperatorParameters
                    (
                        new IndexOfOperatorParameters
                        (
                            new ConstantOperatorParameters("hello"),
                            new MemberSelectorOperatorParameters("StringProp", new ParameterOperatorParameters(parameterName))
                        ),
                        new ConvertOperatorParameters

                        (
                            new MemberSelectorOperatorParameters("UIntProp", new ParameterOperatorParameters(parameterName)),
                            typeof(int?)
                        )
                    )
                },
                new object[]
                {
                    new GreaterThanBinaryOperatorParameters
                    (
                        new IndexOfOperatorParameters
                        (
                            new ConstantOperatorParameters("hello"),
                            new MemberSelectorOperatorParameters("StringProp", new ParameterOperatorParameters(parameterName))
                        ),
                        new ConvertOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("ULongProp", new ParameterOperatorParameters(parameterName)),
                            typeof(int?)
                        )
                    )
                },
                new object[]
                {
                    new GreaterThanBinaryOperatorParameters
                    (
                        new IndexOfOperatorParameters
                        (
                            new ConstantOperatorParameters("hello"),
                            new MemberSelectorOperatorParameters("StringProp", new ParameterOperatorParameters(parameterName))
                        ),
                        new ConvertOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("UShortProp", new ParameterOperatorParameters(parameterName)),
                            typeof(int?)
                        )
                    )
                },
                new object[]
                {
                    new GreaterThanBinaryOperatorParameters
                    (
                        new IndexOfOperatorParameters
                        (
                            new ConstantOperatorParameters("hello"),
                            new MemberSelectorOperatorParameters("StringProp", new ParameterOperatorParameters(parameterName))
                        ),
                        new ConvertOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("NullableUShortProp", new ParameterOperatorParameters(parameterName)),
                            typeof(int?)
                        )
                    )
                },
                new object[]
                {
                    new GreaterThanBinaryOperatorParameters
                    (
                        new IndexOfOperatorParameters
                        (
                            new ConstantOperatorParameters("hello"),
                            new MemberSelectorOperatorParameters("StringProp", new ParameterOperatorParameters(parameterName))
                        ),
                        new ConvertOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("NullableUIntProp", new ParameterOperatorParameters(parameterName)),
                            typeof(int?)
                        )
                    )
                },
                new object[]
                {
                    new GreaterThanBinaryOperatorParameters
                    (
                        new IndexOfOperatorParameters
                        (
                            new ConstantOperatorParameters("hello"),
                            new MemberSelectorOperatorParameters("StringProp", new ParameterOperatorParameters(parameterName))
                        ),
                        new ConvertOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("NullableULongProp", new ParameterOperatorParameters(parameterName)),
                            typeof(int?)
                        )
                    )
                }
            };

        [Theory]
        [MemberData(nameof(ComparisonsInvolvingCastsAndNullableValues_Data))]
        public void ComparisonsInvolvingCastsAndNullableValues(IExpressionParameter filterBody)
        {
            //act
            var filter = CreateFilter<DataTypes>();

            //assert
            Assert.Throws<ArgumentNullException>(() => RunFilter(filter, new DataTypes()));

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    filterBody
                );
        }

        [Theory]
        [InlineData(null, null, true)]
        [InlineData("not doritos", 0, true)]
        [InlineData("Doritos", 1, false)]
        public void Grouping(string productName, object unitsInStock, bool expected)
        {
            //act
            var filter = CreateFilter<Product>();
            bool result = RunFilter(filter, new Product { ProductName = productName, UnitsInStock = ToNullable<short>(unitsInStock) });

            //assert
            AssertFilterStringIsCorrect(filter, string.Format(CultureInfo.InvariantCulture, "$it => (($it.ProductName != \"Doritos\") OrElse ($it.UnitPrice < Convert({0:0.00})))", 5.0));
            Assert.Equal(expected, result);

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    new OrBinaryOperatorParameters
                    (
                        new NotEqualsBinaryOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("ProductName", new ParameterOperatorParameters(parameterName)),
                            new ConstantOperatorParameters("Doritos")
                        ),
                        new LessThanBinaryOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("UnitPrice", new ParameterOperatorParameters(parameterName)),
                            new ConstantOperatorParameters(5.00m, typeof(decimal))
                        )
                    )
                );
        }

        [Fact]
        public void MemberExpressions()
        {
            //act
            var filter = CreateFilter<Product>();
            bool result = RunFilter(filter, new Product { Category = new Category { CategoryName = "Snacks" } });

            //assert
            Assert.Throws<NullReferenceException>(() => RunFilter(filter, new Product { }));
            AssertFilterStringIsCorrect(filter, "$it => ($it.Category.CategoryName == \"Snacks\")");
            Assert.True(result);

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    new EqualsBinaryOperatorParameters
                    (
                        new MemberSelectorOperatorParameters
                        (
                            "CategoryName",
                            new MemberSelectorOperatorParameters("Category", new ParameterOperatorParameters(parameterName))
                        ),
                        new ConstantOperatorParameters("Snacks")
                    )
                );
        }

        [Fact]
        public void MemberExpressionsRecursive()
        {
            //act
            var filter = CreateFilter<Product>();

            //assert
            Assert.Throws<NullReferenceException>(() => RunFilter(filter, new Product { }));
            AssertFilterStringIsCorrect(filter, "$it => ($it.Category.Product.Category.CategoryName == \"Snacks\")");

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    new EqualsBinaryOperatorParameters
                    (
                        new MemberSelectorOperatorParameters
                        (
                            "CategoryName",
                            new MemberSelectorOperatorParameters
                            (
                                "Category",
                                new MemberSelectorOperatorParameters

                                (
                                    "Product",
                                    new MemberSelectorOperatorParameters("Category", new ParameterOperatorParameters(parameterName))
                                )
                            )
                        ),
                        new ConstantOperatorParameters("Snacks")
                    )
                );
        }

        [Fact]
        public void ComplexPropertyNavigation()
        {
            //act
            var filter = CreateFilter<Product>();
            bool result = RunFilter(filter, new Product { SupplierAddress = new Address { City = "Redmond" } });

            //assert
            Assert.Throws<NullReferenceException>(() => RunFilter(filter, new Product { }));
            AssertFilterStringIsCorrect(filter, "$it => ($it.SupplierAddress.City == \"Redmond\")");
            Assert.True(result);

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    new EqualsBinaryOperatorParameters
                    (
                        new MemberSelectorOperatorParameters
                        (
                            "City",
                            new MemberSelectorOperatorParameters("SupplierAddress", new ParameterOperatorParameters(parameterName))
                        ),
                        new ConstantOperatorParameters("Redmond")
                    )
                );
        }
        #endregion Other

        #region Any/All
        [Fact]
        public void AnyOnNavigationEnumerableCollections()
        {
            //act
            var filter = CreateFilter<Product>();

            bool result1 = RunFilter
            (
                filter,
                new Product
                {
                    Category = new Category
                    {
                        EnumerableProducts = new Product[]
                        {
                            new Product { ProductName = "Snacks" },
                            new Product { ProductName = "NonSnacks" }
                        }
                    }
                }
            );

            bool result2 = RunFilter
            (
                filter,
                new Product
                {
                    Category = new Category
                    {
                        EnumerableProducts = new Product[]
                        {
                            new Product { ProductName = "NonSnacks" }
                        }
                    }
                }
            );

            //assert
            AssertFilterStringIsCorrect(filter, "$it => $it.Category.EnumerableProducts.Any(P => (P.ProductName == \"Snacks\"))");
            Assert.True(result1);
            Assert.False(result2);

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    new AnyOperatorParameters
                    (
                        new MemberSelectorOperatorParameters
                        (
                            "EnumerableProducts",
                            new MemberSelectorOperatorParameters("Category", new ParameterOperatorParameters(parameterName))
                        ),
                        new EqualsBinaryOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("ProductName", new ParameterOperatorParameters("P")),
                            new ConstantOperatorParameters("Snacks")
                        ),
                        "P"
                    )
                );
        }

        [Fact]
        public void AnyOnNavigationQueryableCollections()
        {
            //act
            var filter = CreateFilter<Product>();

            bool result1 = RunFilter
            (
                filter,
                new Product
                {
                    Category = new Category
                    {
                        QueryableProducts = new Product[]
                        {
                            new Product { ProductName = "Snacks" },
                            new Product { ProductName = "NonSnacks" }
                        }.AsQueryable()
                    }
                }
            );

            bool result2 = RunFilter
            (
                filter,
                new Product
                {
                    Category = new Category
                    {
                        QueryableProducts = new Product[]
                        {
                            new Product { ProductName = "NonSnacks" }
                        }.AsQueryable()
                    }
                }
            );

            //assert
            AssertFilterStringIsCorrect(filter, "$it => $it.Category.QueryableProducts.Any(P => (P.ProductName == \"Snacks\"))");
            Assert.True(result1);
            Assert.False(result2);

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    new AnyOperatorParameters
                    (
                        new MemberSelectorOperatorParameters
                        (
                            "QueryableProducts",
                            new MemberSelectorOperatorParameters("Category", new ParameterOperatorParameters(parameterName))
                        ),
                        new EqualsBinaryOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("ProductName", new ParameterOperatorParameters("P")),
                            new ConstantOperatorParameters("Snacks")
                        ),
                        "P"
                    )
                );
        }

        public static List<object[]> AnyInOnNavigation_Data
            => new List<object[]>
            {
                new object[]
                {
                    new AnyOperatorParameters
                    (
                        new MemberSelectorOperatorParameters
                        (
                            "QueryableProducts",
                            new MemberSelectorOperatorParameters("Category", new ParameterOperatorParameters(parameterName))
                        ),
                        new InOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("ProductID", new ParameterOperatorParameters("P")),
                            new CollectionConstantOperatorParameters
                            (
                                new List<object>{ 1 },
                                typeof(int)
                            )
                        ),
                        "P"
                    ),
                    "$it => $it.Category.QueryableProducts.Any(P => System.Collections.Generic.List`1[System.Int32].Contains(P.ProductID))"
                },
                new object[]
                {
                    new AnyOperatorParameters
                    (
                        new MemberSelectorOperatorParameters
                        (
                            "EnumerableProducts",
                            new MemberSelectorOperatorParameters("Category", new ParameterOperatorParameters(parameterName))
                        ),
                        new InOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("ProductID", new ParameterOperatorParameters("P")),
                            new CollectionConstantOperatorParameters
                            (
                                new List<object>{ 1 },
                                typeof(int)
                            )
                        ),
                        "P"
                    ),
                    "$it => $it.Category.EnumerableProducts.Any(P => System.Collections.Generic.List`1[System.Int32].Contains(P.ProductID))"
                },
                new object[]
                {
                    new AnyOperatorParameters
                    (
                        new MemberSelectorOperatorParameters
                        (
                            "QueryableProducts",
                            new MemberSelectorOperatorParameters("Category", new ParameterOperatorParameters(parameterName))
                        ),
                        new InOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("GuidProperty", new ParameterOperatorParameters("P")),
                            new CollectionConstantOperatorParameters
                            (
                                new List<object>{ new Guid("dc75698b-581d-488b-9638-3e28dd51d8f7") },
                                typeof(Guid)
                            )
                        ),
                        "P"
                    ),
                    "$it => $it.Category.QueryableProducts.Any(P => System.Collections.Generic.List`1[System.Guid].Contains(P.GuidProperty))"
                },
                new object[]
                {
                    new AnyOperatorParameters
                    (
                        new MemberSelectorOperatorParameters
                        (
                            "EnumerableProducts",
                            new MemberSelectorOperatorParameters("Category", new ParameterOperatorParameters(parameterName))
                        ),
                        new InOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("GuidProperty", new ParameterOperatorParameters("P")),
                            new CollectionConstantOperatorParameters
                            (
                                new List<object>{ new Guid("dc75698b-581d-488b-9638-3e28dd51d8f7") },
                                typeof(Guid)
                            )
                        ),
                        "P"
                    ),
                    "$it => $it.Category.EnumerableProducts.Any(P => System.Collections.Generic.List`1[System.Guid].Contains(P.GuidProperty))"
                },
                new object[]
                {
                    new AnyOperatorParameters
                    (
                        new MemberSelectorOperatorParameters
                        (
                            "QueryableProducts",
                            new MemberSelectorOperatorParameters("Category", new ParameterOperatorParameters(parameterName))
                        ),
                        new InOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("NullableGuidProperty", new ParameterOperatorParameters("P")),
                            new CollectionConstantOperatorParameters
                            (
                                new List<object>{ new Guid("dc75698b-581d-488b-9638-3e28dd51d8f7") },
                                typeof(Guid?)
                            )
                        ),
                        "P"
                    ),
                    "$it => $it.Category.QueryableProducts.Any(P => System.Collections.Generic.List`1[System.Nullable`1[System.Guid]].Contains(P.NullableGuidProperty))"
                },
                new object[]
                {
                    new AnyOperatorParameters
                    (
                        new MemberSelectorOperatorParameters
                        (
                            "EnumerableProducts",
                            new MemberSelectorOperatorParameters("Category", new ParameterOperatorParameters(parameterName))
                        ),
                        new InOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("NullableGuidProperty", new ParameterOperatorParameters("P")),
                            new CollectionConstantOperatorParameters
                            (
                                new List<object>{ new Guid("dc75698b-581d-488b-9638-3e28dd51d8f7") },
                                typeof(Guid?)
                            )
                        ),
                        "P"
                    ),
                    "$it => $it.Category.EnumerableProducts.Any(P => System.Collections.Generic.List`1[System.Nullable`1[System.Guid]].Contains(P.NullableGuidProperty))"
                },
                new object[]
                {
                    new AnyOperatorParameters
                    (
                        new MemberSelectorOperatorParameters
                        (
                            "QueryableProducts",
                            new MemberSelectorOperatorParameters("Category", new ParameterOperatorParameters(parameterName))
                        ),
                        new InOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("Discontinued", new ParameterOperatorParameters("P")),
                            new CollectionConstantOperatorParameters
                            (
                                new List<object>{ false, null },
                                typeof(bool?)
                            )
                        ),
                        "P"
                    ),
                    "$it => $it.Category.QueryableProducts.Any(P => System.Collections.Generic.List`1[System.Nullable`1[System.Boolean]].Contains(P.Discontinued))"
                },
                new object[]
                {
                    new AnyOperatorParameters
                    (
                        new MemberSelectorOperatorParameters
                        (
                            "EnumerableProducts",
                            new MemberSelectorOperatorParameters("Category", new ParameterOperatorParameters(parameterName))
                        ),
                        new InOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("Discontinued", new ParameterOperatorParameters("P")),
                            new CollectionConstantOperatorParameters
                            (
                                new List<object>{ false, null },
                                typeof(bool?)
                            )
                        ),
                        "P"
                    ),
                    "$it => $it.Category.EnumerableProducts.Any(P => System.Collections.Generic.List`1[System.Nullable`1[System.Boolean]].Contains(P.Discontinued))"
                }
            };

        [Theory]
        [MemberData(nameof(AnyInOnNavigation_Data))]
        public void AnyInOnNavigation(IExpressionParameter filterBody, string expression)
        {
            //act
            var filter = CreateFilter<Product>();

            //assert
            AssertFilterStringIsCorrect(filter, expression);

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    filterBody
                );
        }

        public static List<object[]> AnyOnNavigation_Contradiction_Data
            => new List<object[]>
            {
                new object[]
                {
                    new AnyOperatorParameters
                    (
                        new MemberSelectorOperatorParameters
                        (
                            "QueryableProducts",
                            new MemberSelectorOperatorParameters("Category", new ParameterOperatorParameters(parameterName))
                        ),
                        new ConstantOperatorParameters(false),
                        "P"
                    ),
                    "$it => $it.Category.QueryableProducts.Any(P => False)"
                },
                new object[]
                {
                    new AnyOperatorParameters
                    (
                        new MemberSelectorOperatorParameters
                        (
                            "QueryableProducts",
                            new MemberSelectorOperatorParameters("Category", new ParameterOperatorParameters(parameterName))
                        ),
                        new AndBinaryOperatorParameters
                        (
                            new ConstantOperatorParameters(false),
                            new EqualsBinaryOperatorParameters
                            (
                                new MemberSelectorOperatorParameters("ProductName", new ParameterOperatorParameters("P")),
                                new ConstantOperatorParameters("Snacks")
                            )
                        ),
                        "P"
                    ),
                    "$it => $it.Category.QueryableProducts.Any(P => (False AndAlso (P.ProductName == \"Snacks\")))"
                },
                new object[]
                {
                    new AnyOperatorParameters
                    (
                        new MemberSelectorOperatorParameters
                        (
                            "QueryableProducts",
                            new MemberSelectorOperatorParameters("Category", new ParameterOperatorParameters(parameterName))
                        )
                    ),
                    "$it => $it.Category.QueryableProducts.Any()"
                }
            };

        [Theory]
        [MemberData(nameof(AnyOnNavigation_Contradiction_Data))]
        public void AnyOnNavigation_Contradiction(IExpressionParameter filterBody, string expression)
        {
            //act
            var filter = CreateFilter<Product>();

            //assert
            AssertFilterStringIsCorrect(filter, expression);

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    filterBody
                );
        }

        [Fact]
        public void AnyOnNavigation_NullCollection()
        {
            //act
            var filter = CreateFilter<Product>();
            bool result = RunFilter
            (
                filter,
                new Product
                {
                    Category = new Category
                    {
                        EnumerableProducts = new Product[]
                        {
                            new Product { ProductName = "Snacks" }
                        }
                    }
                }
            );

            //assert
            Assert.Throws<ArgumentNullException>(() => RunFilter(filter, new Product { Category = new Category { } }));
            AssertFilterStringIsCorrect(filter, "$it => $it.Category.EnumerableProducts.Any(P => (P.ProductName == \"Snacks\"))");
            Assert.True(result);

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    new AnyOperatorParameters
                    (
                        new MemberSelectorOperatorParameters
                        (
                            "EnumerableProducts",
                            new MemberSelectorOperatorParameters("Category", new ParameterOperatorParameters(parameterName))
                        ),
                        new EqualsBinaryOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("ProductName", new ParameterOperatorParameters("P")),
                            new ConstantOperatorParameters("Snacks")
                        ),
                        "P"
                    )
                );
        }


        [Fact]
        public void AllOnNavigation_NullCollection()
        {
            //act
            var filter = CreateFilter<Product>();
            bool result = RunFilter
            (
                filter,
                new Product
                {
                    Category = new Category
                    {
                        EnumerableProducts = new Product[]
                        {
                            new Product { ProductName = "Snacks" }
                        }
                    }
                }
            );

            //assert
            Assert.Throws<ArgumentNullException>(() => RunFilter(filter, new Product { Category = new Category { } }));
            AssertFilterStringIsCorrect(filter, "$it => $it.Category.EnumerableProducts.All(P => (P.ProductName == \"Snacks\"))");
            Assert.True(result);

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    new AllOperatorParameters
                    (
                        new MemberSelectorOperatorParameters
                        (
                            "EnumerableProducts",
                            new MemberSelectorOperatorParameters("Category", new ParameterOperatorParameters(parameterName))
                        ),
                        new EqualsBinaryOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("ProductName", new ParameterOperatorParameters("P")),
                            new ConstantOperatorParameters("Snacks")
                        ),
                        "P"
                    )
                );
        }

        [Fact]
        public void MultipleAnys_WithSameRangeVariableName()
        {
            //act
            var filter = CreateFilter<Product>();

            //assert
            AssertFilterStringIsCorrect(filter, "$it => ($it.AlternateIDs.Any(n => (n == 42)) AndAlso $it.AlternateAddresses.Any(n => (n.City == \"Redmond\")))");

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    new AndBinaryOperatorParameters
                    (
                        new AnyOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("AlternateIDs", new ParameterOperatorParameters(parameterName)),
                            new EqualsBinaryOperatorParameters
                            (
                                new ParameterOperatorParameters("n"),
                                new ConstantOperatorParameters(42)
                            ),
                            "n"
                        ),
                        new AnyOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("AlternateAddresses", new ParameterOperatorParameters(parameterName)),
                            new EqualsBinaryOperatorParameters
                            (
                                new MemberSelectorOperatorParameters("City", new ParameterOperatorParameters("n")),
                                new ConstantOperatorParameters("Redmond")
                            ),
                            "n"
                        )
                    )
                );
        }

        [Fact]
        public void MultipleAlls_WithSameRangeVariableName()
        {
            //act
            var filter = CreateFilter<Product>();

            //assert
            AssertFilterStringIsCorrect(filter, "$it => ($it.AlternateIDs.All(n => (n == 42)) AndAlso $it.AlternateAddresses.All(n => (n.City == \"Redmond\")))");
            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    new AndBinaryOperatorParameters
                    (
                        new AllOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("AlternateIDs", new ParameterOperatorParameters(parameterName)),
                            new EqualsBinaryOperatorParameters
                            (
                                new ParameterOperatorParameters("n"),
                                new ConstantOperatorParameters(42)
                            ),
                            "n"
                        ),
                        new AllOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("AlternateAddresses", new ParameterOperatorParameters(parameterName)),
                            new EqualsBinaryOperatorParameters
                            (
                                new MemberSelectorOperatorParameters("City", new ParameterOperatorParameters("n")),
                                new ConstantOperatorParameters("Redmond")
                            ),
                            "n"
                        )
                    )
                );
        }

        [Fact]
        public void AnyOnNavigationEnumerableCollections_EmptyFilter()
        {
            //act
            var filter = CreateFilter<Product>();

            //assert
            AssertFilterStringIsCorrect(filter, "$it => $it.Category.EnumerableProducts.Any()");

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    new AnyOperatorParameters
                    (
                        new MemberSelectorOperatorParameters
                        (
                            "EnumerableProducts",
                            new MemberSelectorOperatorParameters("Category", new ParameterOperatorParameters(parameterName))
                        )
                    )
                );
        }

        [Fact]
        public void AnyOnNavigationQueryableCollections_EmptyFilter()
        {
            //act
            var filter = CreateFilter<Product>();

            //assert
            AssertFilterStringIsCorrect(filter, "$it => $it.Category.QueryableProducts.Any()");

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    new AnyOperatorParameters
                    (
                        new MemberSelectorOperatorParameters
                        (
                            "QueryableProducts",
                            new MemberSelectorOperatorParameters("Category", new ParameterOperatorParameters(parameterName))
                        )
                    )
                );
        }

        [Fact]
        public void AllOnNavigationEnumerableCollections()
        {
            //act
            var filter = CreateFilter<Product>();

            //assert
            AssertFilterStringIsCorrect(filter, "$it => $it.Category.EnumerableProducts.All(P => (P.ProductName == \"Snacks\"))");

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    new AllOperatorParameters
                    (
                        new MemberSelectorOperatorParameters
                        (
                            "EnumerableProducts",
                            new MemberSelectorOperatorParameters("Category", new ParameterOperatorParameters(parameterName))
                        ),
                        new EqualsBinaryOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("ProductName", new ParameterOperatorParameters("P")),
                            new ConstantOperatorParameters("Snacks")
                        ),
                        "P"
                    )
                );
        }

        [Fact]
        public void AllOnNavigationQueryableCollections()
        {
            //act
            var filter = CreateFilter<Product>();

            //assert
            AssertFilterStringIsCorrect(filter, "$it => $it.Category.QueryableProducts.All(P => (P.ProductName == \"Snacks\"))");

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    new AllOperatorParameters
                    (
                        new MemberSelectorOperatorParameters
                        (
                            "QueryableProducts",
                            new MemberSelectorOperatorParameters("Category", new ParameterOperatorParameters(parameterName))
                        ),
                        new EqualsBinaryOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("ProductName", new ParameterOperatorParameters("P")),
                            new ConstantOperatorParameters("Snacks")
                        ),
                        "P"
                    )
                );
        }

        [Fact]
        public void AnyInSequenceNotNested()
        {
            //act
            var filter = CreateFilter<Product>();

            //assert
            AssertFilterStringIsCorrect(filter, "$it => ($it.Category.QueryableProducts.Any(P => (P.ProductName == \"Snacks\")) OrElse $it.Category.QueryableProducts.Any(P2 => (P2.ProductName == \"Snacks\")))");

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    new OrBinaryOperatorParameters
                    (
                        new AnyOperatorParameters
                        (
                            new MemberSelectorOperatorParameters
                            (
                                "QueryableProducts",
                                new MemberSelectorOperatorParameters("Category", new ParameterOperatorParameters(parameterName))
                            ),
                            new EqualsBinaryOperatorParameters
                            (
                                new MemberSelectorOperatorParameters("ProductName", new ParameterOperatorParameters("P")),
                                new ConstantOperatorParameters("Snacks")
                            ),
                            "P"
                        ),
                        new AnyOperatorParameters
                        (
                            new MemberSelectorOperatorParameters
                            (
                                "QueryableProducts",
                                new MemberSelectorOperatorParameters("Category", new ParameterOperatorParameters(parameterName))
                            ),
                            new EqualsBinaryOperatorParameters
                            (
                                new MemberSelectorOperatorParameters("ProductName", new ParameterOperatorParameters("P2")),
                                new ConstantOperatorParameters("Snacks")
                            ),
                            "P2"
                        )
                    )
                );
        }

        [Fact]
        public void AllInSequenceNotNested()
        {
            //act
            var filter = CreateFilter<Product>();

            //assert
            AssertFilterStringIsCorrect(filter, "$it => ($it.Category.QueryableProducts.All(P => (P.ProductName == \"Snacks\")) OrElse $it.Category.QueryableProducts.All(P2 => (P2.ProductName == \"Snacks\")))");

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    new OrBinaryOperatorParameters
                    (
                        new AllOperatorParameters
                        (
                            new MemberSelectorOperatorParameters
                            (
                                "QueryableProducts",
                                new MemberSelectorOperatorParameters("Category", new ParameterOperatorParameters(parameterName))
                            ),
                            new EqualsBinaryOperatorParameters
                            (
                                new MemberSelectorOperatorParameters("ProductName", new ParameterOperatorParameters("P")),
                                new ConstantOperatorParameters("Snacks")
                            ),
                            "P"
                        ),
                        new AllOperatorParameters
                        (
                            new MemberSelectorOperatorParameters
                            (
                                "QueryableProducts",
                                new MemberSelectorOperatorParameters("Category", new ParameterOperatorParameters(parameterName))
                            ),
                            new EqualsBinaryOperatorParameters
                            (
                                new MemberSelectorOperatorParameters("ProductName", new ParameterOperatorParameters("P2")),
                                new ConstantOperatorParameters("Snacks")
                            ),
                            "P2"
                        )
                    )
                );
        }

        [Fact]
        public void AnyOnPrimitiveCollection()
        {
            //act
            var filter = CreateFilter<Product>();

            bool result1 = RunFilter
            (
                filter,
                new Product { AlternateIDs = new[] { 1, 2, 42 } }
            );

            bool result2 = RunFilter
            (
                filter,
                new Product { AlternateIDs = new[] { 1, 2 } }
            );

            //assert
            AssertFilterStringIsCorrect(filter, "$it => $it.AlternateIDs.Any(id => (id == 42))");
            Assert.True(result1);
            Assert.False(result2);

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    new AnyOperatorParameters
                    (
                        new MemberSelectorOperatorParameters("AlternateIDs", new ParameterOperatorParameters(parameterName)),
                        new EqualsBinaryOperatorParameters
                        (
                            new ParameterOperatorParameters("id"),
                            new ConstantOperatorParameters(42)
                        ),
                        "id"
                    )
                );
        }

        [Fact]
        public void AllOnPrimitiveCollection()
        {
            //act
            var filter = CreateFilter<Product>();

            //assert
            AssertFilterStringIsCorrect(filter, "$it => $it.AlternateIDs.All(id => (id == 42))");

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    new AllOperatorParameters
                    (
                        new MemberSelectorOperatorParameters("AlternateIDs", new ParameterOperatorParameters(parameterName)),
                        new EqualsBinaryOperatorParameters
                        (
                            new ParameterOperatorParameters("id"),
                            new ConstantOperatorParameters(42)
                        ),
                        "id"
                    )
                );
        }

        [Fact]
        public void AnyOnComplexCollection()
        {
            //act
            var filter = CreateFilter<Product>();

            bool result = RunFilter
            (
                filter,
                new Product { AlternateAddresses = new[] { new Address { City = "Redmond" } } }
            );

            //assert
            Assert.Throws<ArgumentNullException>(() => RunFilter(filter, new Product { }));
            AssertFilterStringIsCorrect(filter, "$it => $it.AlternateAddresses.Any(address => (address.City == \"Redmond\"))");
            Assert.True(result);

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    new AnyOperatorParameters
                    (
                        new MemberSelectorOperatorParameters("AlternateAddresses", new ParameterOperatorParameters(parameterName)),
                        new EqualsBinaryOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("City", new ParameterOperatorParameters("address")),
                            new ConstantOperatorParameters("Redmond")
                        ),
                        "address"
                    )
                );
        }

        [Fact]
        public void AllOnComplexCollection()
        {
            //act
            var filter = CreateFilter<Product>();

            //assert
            AssertFilterStringIsCorrect(filter, "$it => $it.AlternateAddresses.All(address => (address.City == \"Redmond\"))");

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    new AllOperatorParameters
                    (
                        new MemberSelectorOperatorParameters("AlternateAddresses", new ParameterOperatorParameters(parameterName)),
                        new EqualsBinaryOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("City", new ParameterOperatorParameters("address")),
                            new ConstantOperatorParameters("Redmond")
                        ),
                        "address"
                    )
                );
        }

        [Fact]
        public void RecursiveAllAny()
        {
            //act
            var filter = CreateFilter<Product>();

            //assert
            AssertFilterStringIsCorrect(filter, "$it => $it.Category.QueryableProducts.All(P => P.Category.EnumerableProducts.Any(PP => (PP.ProductName == \"Snacks\")))");

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    new AllOperatorParameters
                    (
                        new MemberSelectorOperatorParameters
                        (
                            "QueryableProducts",
                            new MemberSelectorOperatorParameters("Category", new ParameterOperatorParameters(parameterName))
                        ),
                        new AnyOperatorParameters
                        (
                            new MemberSelectorOperatorParameters
                            (
                                "EnumerableProducts",
                                new MemberSelectorOperatorParameters("Category", new ParameterOperatorParameters("P"))
                            ),
                            new EqualsBinaryOperatorParameters
                            (
                                new MemberSelectorOperatorParameters("ProductName", new ParameterOperatorParameters("PP")),
                                new ConstantOperatorParameters("Snacks")
                            ),
                            "PP"
                        ),
                        "P"
                    )
                );
        }
        #endregion Any/All

        #region String Functions
        [Theory]
        [InlineData("Abcd", 0, "Abcd", true)]
        [InlineData("Abcd", 1, "bcd", true)]
        [InlineData("Abcd", 3, "d", true)]
        [InlineData("Abcd", 4, "", true)]
        public void StringSubstringStart(string productName, int startIndex, string compareString, bool expected)
        {
            //act
            var filter = CreateFilter<Product>();
            bool result = RunFilter
            (
                filter,
                new Product { ProductName = productName }
            );

            //assert
            Assert.Equal(expected, result);

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    new EqualsBinaryOperatorParameters
                    (
                        new SubstringOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("ProductName", new ParameterOperatorParameters(parameterName)),
                            new ConstantOperatorParameters(startIndex)
                        ),
                        new ConstantOperatorParameters(compareString)
                    )
                );
        }

        [Theory]
        [InlineData("Abcd", -1, "Abcd")]
        [InlineData("Abcd", 5, "")]
        public void StringSubstringStartOutOfRange(string productName, int startIndex, string compareString)
        {
            //act
            var filter = CreateFilter<Product>();

            //assert
            Assert.Throws<ArgumentOutOfRangeException>(() => RunFilter(filter, new Product { ProductName = productName }));

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    new EqualsBinaryOperatorParameters
                    (
                        new SubstringOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("ProductName", new ParameterOperatorParameters(parameterName)),
                            new ConstantOperatorParameters(startIndex)
                        ),
                        new ConstantOperatorParameters(compareString)
                    )
                );
        }

        [Theory]
        [InlineData("Abcd", 0, 1, "A", true)]
        [InlineData("Abcd", 0, 4, "Abcd", true)]
        [InlineData("Abcd", 0, 3, "Abc", true)]
        [InlineData("Abcd", 1, 3, "bcd", true)]
        [InlineData("Abcd", 2, 1, "c", true)]
        [InlineData("Abcd", 3, 1, "d", true)]
        public void StringSubstringStartAndLength(string productName, int startIndex, int length, string compareString, bool expected)
        {
            //act
            var filter = CreateFilter<Product>();
            bool result = RunFilter
            (
                filter,
                new Product { ProductName = productName }
            );

            //assert
            Assert.Equal(expected, result);

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    new EqualsBinaryOperatorParameters
                    (
                        new SubstringOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("ProductName", new ParameterOperatorParameters(parameterName)),
                            new ConstantOperatorParameters(startIndex),
                            new ConstantOperatorParameters(length)
                        ),
                        new ConstantOperatorParameters(compareString)
                    )
                );
        }

        [Theory]
        [InlineData("Abcd", -1, 4, "Abcd")]
        [InlineData("Abcd", -1, 3, "Abc")]
        [InlineData("Abcd", 0, 5, "Abcd")]
        [InlineData("Abcd", 1, 5, "bcd")]
        [InlineData("Abcd", 4, 1, "")]
        [InlineData("Abcd", 0, -1, "")]
        [InlineData("Abcd", 5, -1, "")]
        public void StringSubstringStartAndLengthOutOfRange(string productName, int startIndex, int length, string compareString)
        {
            //act
            var filter = CreateFilter<Product>();

            //assert
            Assert.Throws<ArgumentOutOfRangeException>(() => RunFilter(filter, new Product { ProductName = productName }));

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    new EqualsBinaryOperatorParameters
                    (
                        new SubstringOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("ProductName", new ParameterOperatorParameters(parameterName)),
                            new ConstantOperatorParameters(startIndex),
                            new ConstantOperatorParameters(length)
                        ),
                        new ConstantOperatorParameters(compareString)
                    )
                );
        }

        [Theory]
        [InlineData("Abcd", true)]
        [InlineData("Abd", false)]
        public void StringContains(string productName, bool expected)
        {
            //act
            var filter = CreateFilter<Product>();
            bool result = RunFilter(filter, new Product { ProductName = productName });

            //assert
            AssertFilterStringIsCorrect(filter, "$it => $it.ProductName.Contains(\"Abc\")");
            Assert.Equal(expected, result);

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    new ContainsOperatorParameters
                    (
                        new MemberSelectorOperatorParameters("ProductName", new ParameterOperatorParameters(parameterName)),
                        new ConstantOperatorParameters("Abc")
                    )
                );
        }

        [Fact]
        public void StringContainsNullReferenceException()
        {
            //act
            var filter = CreateFilter<Product>();

            //assert
            AssertFilterStringIsCorrect(filter, "$it => $it.ProductName.Contains(\"Abc\")");
            Assert.Throws<NullReferenceException>(() => RunFilter(filter, new Product { }));

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    new ContainsOperatorParameters
                    (
                        new MemberSelectorOperatorParameters("ProductName", new ParameterOperatorParameters(parameterName)),
                        new ConstantOperatorParameters("Abc")
                    )
                );
        }

        [Theory]
        [InlineData("Abcd", true)]
        [InlineData("Abd", false)]
        public void StringStartsWith(string productName, bool expected)
        {
            //act
            var filter = CreateFilter<Product>();
            bool result = RunFilter(filter, new Product { ProductName = productName });

            //assert
            AssertFilterStringIsCorrect(filter, "$it => $it.ProductName.StartsWith(\"Abc\")");
            Assert.Equal(expected, result);

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    new StartsWithOperatorParameters
                    (
                        new MemberSelectorOperatorParameters("ProductName", new ParameterOperatorParameters(parameterName)),
                        new ConstantOperatorParameters("Abc")
                    )
                );
        }

        [Fact]
        public void StringStartsWithNullReferenceException()
        {
            //act
            var filter = CreateFilter<Product>();

            //assert
            AssertFilterStringIsCorrect(filter, "$it => $it.ProductName.StartsWith(\"Abc\")");
            Assert.Throws<NullReferenceException>(() => RunFilter(filter, new Product { }));

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    new StartsWithOperatorParameters
                    (
                        new MemberSelectorOperatorParameters("ProductName", new ParameterOperatorParameters(parameterName)),
                        new ConstantOperatorParameters("Abc")
                    )
                );
        }

        [Theory]
        [InlineData("AAbc", true)]
        [InlineData("Abcd", false)]
        public void StringEndsWith(string productName, bool expected)
        {
            //act
            var filter = CreateFilter<Product>();
            bool result = RunFilter(filter, new Product { ProductName = productName });

            //assert
            AssertFilterStringIsCorrect(filter, "$it => $it.ProductName.EndsWith(\"Abc\")");
            Assert.Equal(expected, result);

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    new EndsWithOperatorParameters
                    (
                        new MemberSelectorOperatorParameters("ProductName", new ParameterOperatorParameters(parameterName)),
                        new ConstantOperatorParameters("Abc")
                    )
                );
        }

        [Fact]
        public void StringEndsWithNullReferenceException()
        {
            //act
            var filter = CreateFilter<Product>();

            //assert
            AssertFilterStringIsCorrect(filter, "$it => $it.ProductName.EndsWith(\"Abc\")");
            Assert.Throws<NullReferenceException>(() => RunFilter(filter, new Product { }));

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    new EndsWithOperatorParameters
                    (
                        new MemberSelectorOperatorParameters("ProductName", new ParameterOperatorParameters(parameterName)),
                        new ConstantOperatorParameters("Abc")
                    )
                );
        }

        [Theory]
        [InlineData("AAbc", true)]
        [InlineData("", false)]
        public void StringLength(string productName, bool expected)
        {
            //act
            var filter = CreateFilter<Product>();
            bool result = RunFilter(filter, new Product { ProductName = productName });

            //assert
            AssertFilterStringIsCorrect(filter, "$it => ($it.ProductName.Length > 0)");
            Assert.Equal(expected, result);

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    new GreaterThanBinaryOperatorParameters
                    (
                        new LengthOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("ProductName", new ParameterOperatorParameters(parameterName))
                        ),
                        new ConstantOperatorParameters(0)
                    )
                );
        }

        [Fact]
        public void StringLengthNullReferenceException()
        {
            //act
            var filter = CreateFilter<Product>();

            //assert
            AssertFilterStringIsCorrect(filter, "$it => ($it.ProductName.Length > 0)");
            Assert.Throws<NullReferenceException>(() => RunFilter(filter, new Product { }));

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    new GreaterThanBinaryOperatorParameters
                    (
                        new LengthOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("ProductName", new ParameterOperatorParameters(parameterName))
                        ),
                        new ConstantOperatorParameters(0)
                    )
                );
        }

        [Theory]
        [InlineData("12345Abc", true)]
        [InlineData("1234Abc", false)]
        public void StringIndexOf(string productName, bool expected)
        {
            //act
            var filter = CreateFilter<Product>();
            bool result = RunFilter(filter, new Product { ProductName = productName });

            //assert
            AssertFilterStringIsCorrect(filter, "$it => ($it.ProductName.IndexOf(\"Abc\") == 5)");
            Assert.Equal(expected, result);

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    new EqualsBinaryOperatorParameters
                    (
                        new IndexOfOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("ProductName", new ParameterOperatorParameters(parameterName)),
                            new ConstantOperatorParameters("Abc")
                        ),
                        new ConstantOperatorParameters(5)
                    )
                );
        }

        [Fact]
        public void StringIndexOfNullReferenceException()
        {
            //act
            var filter = CreateFilter<Product>();

            //assert
            AssertFilterStringIsCorrect(filter, "$it => ($it.ProductName.IndexOf(\"Abc\") == 5)");
            Assert.Throws<NullReferenceException>(() => RunFilter(filter, new Product { }));

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    new EqualsBinaryOperatorParameters
                    (
                        new IndexOfOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("ProductName", new ParameterOperatorParameters(parameterName)),
                            new ConstantOperatorParameters("Abc")
                        ),
                        new ConstantOperatorParameters(5)
                    )
                );
        }

        [Theory]
        [InlineData("123uctName", true)]
        [InlineData("1234Abc", false)]
        public void StringSubstring(string productName, bool expected)
        {
            //act
            var filter1 = CreateFilter1<Product>();
            var filter2 = CreateFilter2<Product>();
            bool result = RunFilter(filter1, new Product { ProductName = productName });

            //assert
            AssertFilterStringIsCorrect(filter1, "$it => ($it.ProductName.Substring(3) == \"uctName\")");
            AssertFilterStringIsCorrect(filter2, "$it => ($it.ProductName.Substring(3, 4) == \"uctN\")");
            Assert.Equal(expected, result);

            Expression<Func<T, bool>> CreateFilter1<T>()
                => GetFilter<T>
                (
                    new EqualsBinaryOperatorParameters
                    (
                        new SubstringOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("ProductName", new ParameterOperatorParameters(parameterName)),
                            new ConstantOperatorParameters(3)
                        ),
                        new ConstantOperatorParameters("uctName")
                    )
                );

            Expression<Func<T, bool>> CreateFilter2<T>()
                => GetFilter<T>
                (
                    new EqualsBinaryOperatorParameters
                    (
                        new SubstringOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("ProductName", new ParameterOperatorParameters(parameterName)),
                            new ConstantOperatorParameters(3),
                            new ConstantOperatorParameters(4)
                        ),
                        new ConstantOperatorParameters("uctN")
                    )
                );
        }

        [Fact]
        public void StringSubstringNullReferenceException()
        {
            //act
            var filter1 = CreateFilter1<Product>();
            var filter2 = CreateFilter2<Product>();

            //assert
            AssertFilterStringIsCorrect(filter1, "$it => ($it.ProductName.Substring(3) == \"uctName\")");
            AssertFilterStringIsCorrect(filter2, "$it => ($it.ProductName.Substring(3, 4) == \"uctN\")");
            Assert.Throws<NullReferenceException>(() => RunFilter(filter1, new Product { }));

            Expression<Func<T, bool>> CreateFilter1<T>()
                => GetFilter<T>
                (
                    new EqualsBinaryOperatorParameters
                    (
                        new SubstringOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("ProductName", new ParameterOperatorParameters(parameterName)),
                            new ConstantOperatorParameters(3)
                        ),
                        new ConstantOperatorParameters("uctName")
                    )
                );

            Expression<Func<T, bool>> CreateFilter2<T>()
                => GetFilter<T>
                (
                    new EqualsBinaryOperatorParameters
                    (
                        new SubstringOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("ProductName", new ParameterOperatorParameters(parameterName)),
                            new ConstantOperatorParameters(3),
                            new ConstantOperatorParameters(4)
                        ),
                        new ConstantOperatorParameters("uctN")
                    )
                );
        }

        [Theory]
        [InlineData("Tasty Treats", true)]
        [InlineData("Tasty Treatss", false)]
        public void StringToLower(string productName, bool expected)
        {
            //act
            var filter = CreateFilter<Product>();
            bool result = RunFilter(filter, new Product { ProductName = productName });

            //assert
            AssertFilterStringIsCorrect(filter, "$it => ($it.ProductName.ToLower() == \"tasty treats\")");
            Assert.Equal(expected, result);

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    new EqualsBinaryOperatorParameters
                    (
                        new ToLowerOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("ProductName", new ParameterOperatorParameters(parameterName))
                        ),
                        new ConstantOperatorParameters("tasty treats")
                    )
                );
        }

        [Fact]
        public void StringToLowerNullReferenceException()
        {
            //act
            var filter = CreateFilter<Product>();

            //assert
            AssertFilterStringIsCorrect(filter, "$it => ($it.ProductName.ToLower() == \"tasty treats\")");
            Assert.Throws<NullReferenceException>(() => RunFilter(filter, new Product { }));

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    new EqualsBinaryOperatorParameters
                    (
                        new ToLowerOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("ProductName", new ParameterOperatorParameters(parameterName))
                        ),
                        new ConstantOperatorParameters("tasty treats")
                    )
                );
        }

        [Theory]
        [InlineData("Tasty Treats", true)]
        [InlineData("Tasty Treatss", false)]
        public void StringToUpper(string productName, bool expected)
        {
            //act
            var filter = CreateFilter<Product>();
            bool result = RunFilter(filter, new Product { ProductName = productName });

            //assert
            AssertFilterStringIsCorrect(filter, "$it => ($it.ProductName.ToUpper() == \"TASTY TREATS\")");
            Assert.Equal(expected, result);

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    new EqualsBinaryOperatorParameters
                    (
                        new ToUpperOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("ProductName", new ParameterOperatorParameters(parameterName))
                        ),
                        new ConstantOperatorParameters("TASTY TREATS")
                    )
                );
        }

        [Fact]
        public void StringToUpperNullReferenceException()
        {
            //act
            var filter = CreateFilter<Product>();

            //assert
            AssertFilterStringIsCorrect(filter, "$it => ($it.ProductName.ToUpper() == \"TASTY TREATS\")");
            Assert.Throws<NullReferenceException>(() => RunFilter(filter, new Product { }));

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    new EqualsBinaryOperatorParameters
                    (
                        new ToUpperOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("ProductName", new ParameterOperatorParameters(parameterName))
                        ),
                        new ConstantOperatorParameters("TASTY TREATS")
                    )
                );
        }

        [Theory]
        [InlineData(" Tasty Treats  ", true)]
        [InlineData(" Tasty Treatss  ", false)]
        public void StringTrim(string productName, bool expected)
        {
            //act
            var filter = CreateFilter<Product>();
            bool result = RunFilter(filter, new Product { ProductName = productName });

            //assert
            AssertFilterStringIsCorrect(filter, "$it => ($it.ProductName.Trim() == \"Tasty Treats\")");
            Assert.Equal(expected, result);

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    new EqualsBinaryOperatorParameters
                    (
                        new TrimOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("ProductName", new ParameterOperatorParameters(parameterName))
                        ),
                        new ConstantOperatorParameters("Tasty Treats")
                    )
                );
        }

        [Fact]
        public void StringTrimNullReferenceException()
        {
            //act
            var filter = CreateFilter<Product>();

            //assert
            AssertFilterStringIsCorrect(filter, "$it => ($it.ProductName.Trim() == \"Tasty Treats\")");
            Assert.Throws<NullReferenceException>(() => RunFilter(filter, new Product { }));

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    new EqualsBinaryOperatorParameters
                    (
                        new TrimOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("ProductName", new ParameterOperatorParameters(parameterName))
                        ),
                        new ConstantOperatorParameters("Tasty Treats")
                    )
                );
        }

        [Fact]
        public void StringConcat()
        {
            //act
            var filter = CreateFilter<Product>();
            bool result = RunFilter(filter, new Product { });

            //assert
            AssertFilterStringIsCorrect(filter, "$it => (\"Food\".Concat(\"Bar\") == \"FoodBar\")");
            Assert.True(result);

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    new EqualsBinaryOperatorParameters
                    (
                        new ConcatOperatorParameters
                        (
                            new ConstantOperatorParameters("Food"),
                            new ConstantOperatorParameters("Bar")
                        ),
                        new ConstantOperatorParameters("FoodBar")
                    )
                );
        }
        #endregion String Functions

        #region Date Functions
        [Fact]
        public void DateDay()
        {
            //act
            var filter = CreateFilter<Product>();
            bool result = RunFilter(filter, new Product { DiscontinuedDate = new DateTime(2000, 10, 8) });

            //assert
            AssertFilterStringIsCorrect(filter, "$it => ($it.DiscontinuedDate.Value.Day == 8)");
            Assert.Throws<InvalidOperationException>(() => RunFilter(filter, new Product { }));
            Assert.True(result);

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    new EqualsBinaryOperatorParameters
                    (
                        new DayOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("DiscontinuedDate", new ParameterOperatorParameters(parameterName))
                        ),
                        new ConstantOperatorParameters(8)
                    )
                );
        }

        [Fact]
        public void DateDayNonNullable()
        {
            //act
            var filter = CreateFilter<Product>();

            //assert
            AssertFilterStringIsCorrect(filter, "$it => ($it.NonNullableDiscontinuedDate.Day == 8)");

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    new EqualsBinaryOperatorParameters
                    (
                        new DayOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("NonNullableDiscontinuedDate", new ParameterOperatorParameters(parameterName))
                        ),
                        new ConstantOperatorParameters(8)
                    )
                );
        }

        [Fact]
        public void DateMonth()
        {
            //act
            var filter = CreateFilter<Product>();

            //assert
            AssertFilterStringIsCorrect(filter, "$it => ($it.DiscontinuedDate.Value.Month == 8)");

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    new EqualsBinaryOperatorParameters
                    (
                        new MonthOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("DiscontinuedDate", new ParameterOperatorParameters(parameterName))
                        ),
                        new ConstantOperatorParameters(8)
                    )
                );
        }

        [Fact]
        public void DateYear()
        {
            //act
            var filter = CreateFilter<Product>();

            //assert
            AssertFilterStringIsCorrect(filter, "$it => ($it.DiscontinuedDate.Value.Year == 1974)");

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    new EqualsBinaryOperatorParameters
                    (
                        new YearOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("DiscontinuedDate", new ParameterOperatorParameters(parameterName))
                        ),
                        new ConstantOperatorParameters(1974)
                    )
                );
        }

        [Fact]
        public void DateHour()
        {
            //act
            var filter = CreateFilter<Product>();

            //assert
            AssertFilterStringIsCorrect(filter, "$it => ($it.DiscontinuedDate.Value.Hour == 8)");

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    new EqualsBinaryOperatorParameters
                    (
                        new HourOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("DiscontinuedDate", new ParameterOperatorParameters(parameterName))
                        ),
                        new ConstantOperatorParameters(8)
                    )
                );
        }

        [Fact]
        public void DateMinute()
        {
            //act
            var filter = CreateFilter<Product>();

            //assert
            AssertFilterStringIsCorrect(filter, "$it => ($it.DiscontinuedDate.Value.Minute == 12)");

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    new EqualsBinaryOperatorParameters
                    (
                        new MinuteOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("DiscontinuedDate", new ParameterOperatorParameters(parameterName))
                        ),
                        new ConstantOperatorParameters(12)
                    )
                );
        }

        [Fact]
        public void DateSecond()
        {
            //act
            var filter = CreateFilter<Product>();

            //assert
            AssertFilterStringIsCorrect(filter, "$it => ($it.DiscontinuedDate.Value.Second == 33)");

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    new EqualsBinaryOperatorParameters
                    (
                        new SecondOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("DiscontinuedDate", new ParameterOperatorParameters(parameterName))
                        ),
                        new ConstantOperatorParameters(33)
                    )
                );
        }

        public static List<object[]> DateTimeOffsetFunctions_Data
            => new List<object[]>
            {
                new object[]
                {
                    new EqualsBinaryOperatorParameters
                    (
                        new YearOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("DiscontinuedOffset", new ParameterOperatorParameters(parameterName))
                        ),
                        new ConstantOperatorParameters(100)
                    ),
                    "$it => ($it.DiscontinuedOffset.Year == 100)"
                },
                new object[]
                {
                    new EqualsBinaryOperatorParameters
                    (
                        new MonthOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("DiscontinuedOffset", new ParameterOperatorParameters(parameterName))
                        ),
                        new ConstantOperatorParameters(100)
                    ),
                    "$it => ($it.DiscontinuedOffset.Month == 100)"
                },
                new object[]
                {
                    new EqualsBinaryOperatorParameters
                    (
                        new DayOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("DiscontinuedOffset", new ParameterOperatorParameters(parameterName))
                        ),
                        new ConstantOperatorParameters(100)
                    ),
                    "$it => ($it.DiscontinuedOffset.Day == 100)"
                },
                new object[]
                {
                    new EqualsBinaryOperatorParameters
                    (
                        new HourOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("DiscontinuedOffset", new ParameterOperatorParameters(parameterName))
                        ),
                        new ConstantOperatorParameters(100)
                    ),
                    "$it => ($it.DiscontinuedOffset.Hour == 100)"
                },
                new object[]
                {
                    new EqualsBinaryOperatorParameters
                    (
                        new MinuteOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("DiscontinuedOffset", new ParameterOperatorParameters(parameterName))
                        ),
                        new ConstantOperatorParameters(100)
                    ),
                    "$it => ($it.DiscontinuedOffset.Minute == 100)"
                },
                new object[]
                {
                    new EqualsBinaryOperatorParameters
                    (
                        new SecondOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("DiscontinuedOffset", new ParameterOperatorParameters(parameterName))
                        ),
                        new ConstantOperatorParameters(100)
                    ),
                    "$it => ($it.DiscontinuedOffset.Second == 100)"
                },
                new object[]
                {
                    new EqualsBinaryOperatorParameters
                    (
                        new NowDateTimeOperatorParameters(),
                        new ConstantOperatorParameters(new DateTimeOffset(new DateTime(2016, 11, 8), new TimeSpan(0)))
                    ),
                    "$it => (DateTimeOffset.UtcNow == 11/08/2016 00:00:00 +00:00)"
                },
            };

        [Theory]
        [MemberData(nameof(DateTimeOffsetFunctions_Data))]
        public void DateTimeOffsetFunctions(IExpressionParameter filterBody, string expression)
        {
            //act
            var filter = CreateFilter<Product>();

            //assert
            AssertFilterStringIsCorrect(filter, expression);

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    filterBody
                );
        }

        public static List<object[]> DateTimeFunctions_Data
            => new List<object[]>
            {
                new object[]
                {
                    new EqualsBinaryOperatorParameters
                    (
                        new YearOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("Birthday", new ParameterOperatorParameters(parameterName))
                        ),
                        new ConstantOperatorParameters(100)
                    ),
                    "$it => ({0}.Year == 100)"
                },
                new object[]
                {
                    new EqualsBinaryOperatorParameters
                    (
                        new MonthOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("Birthday", new ParameterOperatorParameters(parameterName))
                        ),
                        new ConstantOperatorParameters(100)
                    ),
                    "$it => ({0}.Month == 100)"
                },
                new object[]
                {
                    new EqualsBinaryOperatorParameters
                    (
                        new DayOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("Birthday", new ParameterOperatorParameters(parameterName))
                        ),
                        new ConstantOperatorParameters(100)
                    ),
                    "$it => ({0}.Day == 100)"
                },
                new object[]
                {
                    new EqualsBinaryOperatorParameters
                    (
                        new HourOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("Birthday", new ParameterOperatorParameters(parameterName))
                        ),
                        new ConstantOperatorParameters(100)
                    ),
                    "$it => ({0}.Hour == 100)"
                },
                new object[]
                {
                    new EqualsBinaryOperatorParameters
                    (
                        new MinuteOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("Birthday", new ParameterOperatorParameters(parameterName))
                        ),
                        new ConstantOperatorParameters(100)
                    ),
                    "$it => ({0}.Minute == 100)"
                },
                new object[]
                {
                    new EqualsBinaryOperatorParameters
                    (
                        new SecondOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("Birthday", new ParameterOperatorParameters(parameterName))
                        ),
                        new ConstantOperatorParameters(100)
                    ),
                    "$it => ({0}.Second == 100)"
                },
            };

        [Theory]
        [MemberData(nameof(DateTimeFunctions_Data))]
        public void DateTimeFunctions(IExpressionParameter filterBody, string expression)
        {
            //act
            var filter = CreateFilter<Product>();

            //assert
            AssertFilterStringIsCorrect(filter, String.Format(expression, "$it.Birthday"));

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    filterBody
                );
        }

        public static List<object[]> DateFunctions_Nullable_Data
            => new List<object[]>
            {
                new object[]
                {
                    new EqualsBinaryOperatorParameters
                    (
                        new YearOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("NullableDateProperty", new ParameterOperatorParameters(parameterName))
                        ),
                        new ConstantOperatorParameters(2015)
                    ),
                    "$it => ($it.NullableDateProperty.Value.Year == 2015)"
                },
                new object[]
                {
                    new EqualsBinaryOperatorParameters
                    (
                        new MonthOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("NullableDateProperty", new ParameterOperatorParameters(parameterName))
                        ),
                        new ConstantOperatorParameters(12)
                    ),
                    "$it => ($it.NullableDateProperty.Value.Month == 12)"
                },
                new object[]
                {
                    new EqualsBinaryOperatorParameters
                    (
                        new DayOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("NullableDateProperty", new ParameterOperatorParameters(parameterName))
                        ),
                        new ConstantOperatorParameters(23)
                    ),
                    "$it => ($it.NullableDateProperty.Value.Day == 23)"
                },
            };

        [Theory]
        [MemberData(nameof(DateFunctions_Nullable_Data))]
        public void DateFunctions_Nullable(IExpressionParameter filterBody, string expression)
        {
            //act
            var filter = CreateFilter<Product>();

            //assert
            AssertFilterStringIsCorrect(filter, expression);

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    filterBody
                );
        }

        public static List<object[]> DateFunctions_NonNullable_Data
            => new List<object[]>
            {
                new object[]
                {
                    new EqualsBinaryOperatorParameters
                    (
                        new YearOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("DateProperty", new ParameterOperatorParameters(parameterName))
                        ),
                        new ConstantOperatorParameters(2015)
                    ),
                    "$it => ($it.DateProperty.Year == 2015)"
                },
                new object[]
                {
                    new EqualsBinaryOperatorParameters
                    (
                        new MonthOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("DateProperty", new ParameterOperatorParameters(parameterName))
                        ),
                        new ConstantOperatorParameters(12)
                    ),
                    "$it => ($it.DateProperty.Month == 12)"
                },
                new object[]
                {
                    new EqualsBinaryOperatorParameters
                    (
                        new DayOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("DateProperty", new ParameterOperatorParameters(parameterName))
                        ),
                        new ConstantOperatorParameters(23)
                    ),
                    "$it => ($it.DateProperty.Day == 23)"
                },
            };

        [Theory]
        [MemberData(nameof(DateFunctions_NonNullable_Data))]
        public void DateFunctions_NonNullable(IExpressionParameter filterBody, string expression)
        {
            //act
            var filter = CreateFilter<Product>();

            //assert
            AssertFilterStringIsCorrect(filter, expression);

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    filterBody
                );
        }

        public static List<object[]> TimeOfDayFunctions_Nullable_Data
            => new List<object[]>
            {
                new object[]
                {
                    new EqualsBinaryOperatorParameters
                    (
                        new HourOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("NullableTimeOfDayProperty", new ParameterOperatorParameters(parameterName))
                        ),
                        new ConstantOperatorParameters(10)
                    ),
                    "$it => ($it.NullableTimeOfDayProperty.Value.Hours == 10)"
                },
                new object[]
                {
                    new EqualsBinaryOperatorParameters
                    (
                        new MinuteOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("NullableTimeOfDayProperty", new ParameterOperatorParameters(parameterName))
                        ),
                        new ConstantOperatorParameters(20)
                    ),
                    "$it => ($it.NullableTimeOfDayProperty.Value.Minutes == 20)"
                },
                new object[]
                {
                    new EqualsBinaryOperatorParameters
                    (
                        new SecondOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("NullableTimeOfDayProperty", new ParameterOperatorParameters(parameterName))
                        ),
                        new ConstantOperatorParameters(30)
                    ),
                    "$it => ($it.NullableTimeOfDayProperty.Value.Seconds == 30)"
                },
            };

        [Theory]
        [MemberData(nameof(TimeOfDayFunctions_Nullable_Data))]
        public void TimeOfDayFunctions_Nullable(IExpressionParameter filterBody, string expression)
        {
            //act
            var filter = CreateFilter<Product>();

            //assert
            AssertFilterStringIsCorrect(filter, expression);

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    filterBody
                );
        }

        public static List<object[]> TimeOfDayFunctions_NonNullable_Data
            => new List<object[]>
            {
                new object[]
                {
                    new EqualsBinaryOperatorParameters
                    (
                        new HourOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("TimeOfDayProperty", new ParameterOperatorParameters(parameterName))
                        ),
                        new ConstantOperatorParameters(10)
                    ),
                    "$it => ($it.TimeOfDayProperty.Hours == 10)"
                },
                new object[]
                {
                    new EqualsBinaryOperatorParameters
                    (
                        new MinuteOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("TimeOfDayProperty", new ParameterOperatorParameters(parameterName))
                        ),
                        new ConstantOperatorParameters(20)
                    ),
                    "$it => ($it.TimeOfDayProperty.Minutes == 20)"
                },
                new object[]
                {
                    new EqualsBinaryOperatorParameters
                    (
                        new SecondOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("TimeOfDayProperty", new ParameterOperatorParameters(parameterName))
                        ),
                        new ConstantOperatorParameters(30)
                    ),
                    "$it => ($it.TimeOfDayProperty.Seconds == 30)"
                },
            };

        [Theory]
        [MemberData(nameof(TimeOfDayFunctions_NonNullable_Data))]
        public void TimeOfDayFunctions_NonNullable(IExpressionParameter filterBody, string expression)
        {
            //act
            var filter = CreateFilter<Product>();

            //assert
            AssertFilterStringIsCorrect(filter, expression);

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    filterBody
                );
        }

        public static List<object[]> FractionalsecondsFunction_Nullable_Data
            => new List<object[]>
            {
                new object[]
                {
                    new EqualsBinaryOperatorParameters
                    (
                        new FractionalSecondsOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("DiscontinuedDate", new ParameterOperatorParameters(parameterName))
                        ),
                        new ConstantOperatorParameters(0.2m)
                    ),
                    "$it => ((Convert($it.DiscontinuedDate.Value.Millisecond) / 1000) == 0.2)"
                },
                new object[]
                {
                    new EqualsBinaryOperatorParameters
                    (
                        new FractionalSecondsOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("NullableTimeOfDayProperty", new ParameterOperatorParameters(parameterName))
                        ),
                        new ConstantOperatorParameters(0.2m)
                    ),
                    "$it => ((Convert($it.NullableTimeOfDayProperty.Value.Milliseconds) / 1000) == 0.2)"
                },
            };

        [Theory]
        [MemberData(nameof(FractionalsecondsFunction_Nullable_Data))]
        public void FractionalsecondsFunction_Nullable(IExpressionParameter filterBody, string expression)
        {
            //act
            var filter = CreateFilter<Product>();

            //assert
            AssertFilterStringIsCorrect(filter, expression);

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    filterBody
                );
        }

        public static List<object[]> FractionalsecondsFunction_NonNullable_Data
            => new List<object[]>
            {
                new object[]
                {
                    new EqualsBinaryOperatorParameters
                    (
                        new FractionalSecondsOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("NonNullableDiscontinuedDate", new ParameterOperatorParameters(parameterName))
                        ),
                        new ConstantOperatorParameters(0.2m)
                    ),
                    "$it => ((Convert($it.NonNullableDiscontinuedDate.Millisecond) / 1000) == 0.2)"
                },
                new object[]
                {
                    new EqualsBinaryOperatorParameters
                    (
                        new FractionalSecondsOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("TimeOfDayProperty", new ParameterOperatorParameters(parameterName))
                        ),
                        new ConstantOperatorParameters(0.2m)
                    ),
                    "$it => ((Convert($it.TimeOfDayProperty.Milliseconds) / 1000) == 0.2)"
                },
            };

        [Theory]
        [MemberData(nameof(FractionalsecondsFunction_NonNullable_Data))]
        public void FractionalsecondsFunction_NonNullable(IExpressionParameter filterBody, string expression)
        {
            //act
            var filter = CreateFilter<Product>();

            //assert
            AssertFilterStringIsCorrect(filter, expression);

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    filterBody
                );
        }

        public static List<object[]> DateFunction_Nullable_Data
            => new List<object[]>
            {
                new object[]
                {
                    new EqualsBinaryOperatorParameters
                    (
                        new ConvertToNumericDateOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("DiscontinuedDate", new ParameterOperatorParameters(parameterName))
                        ),
                        new ConvertToNumericDateOperatorParameters
                        (
                            new ConstantOperatorParameters(new Date(2015, 2, 26))
                        )
                    ),
                    "$it => (((($it.DiscontinuedDate.Value.Year * 10000) + ($it.DiscontinuedDate.Value.Month * 100)) + $it.DiscontinuedDate.Value.Day) == (((2015-02-26.Year * 10000) + (2015-02-26.Month * 100)) + 2015-02-26.Day))"
                },
                new object[]
                {
                    new LessThanBinaryOperatorParameters
                    (
                        new ConvertToNumericDateOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("DiscontinuedDate", new ParameterOperatorParameters(parameterName))
                        ),
                        new ConvertToNumericDateOperatorParameters
                        (
                            new ConstantOperatorParameters(new Date(2016, 2, 26))
                        )
                    ),
                    "$it => (((($it.DiscontinuedDate.Value.Year * 10000) + ($it.DiscontinuedDate.Value.Month * 100)) + $it.DiscontinuedDate.Value.Day) < (((2016-02-26.Year * 10000) + (2016-02-26.Month * 100)) + 2016-02-26.Day))"
                },
                new object[]
                {
                    new GreaterThanOrEqualsBinaryOperatorParameters
                    (
                        new ConvertToNumericDateOperatorParameters
                        (
                            new ConstantOperatorParameters(new Date(2015, 2, 26))
                        ),
                        new ConvertToNumericDateOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("DiscontinuedDate", new ParameterOperatorParameters(parameterName))
                        )
                    ),
                    "$it => ((((2015-02-26.Year * 10000) + (2015-02-26.Month * 100)) + 2015-02-26.Day) >= ((($it.DiscontinuedDate.Value.Year * 10000) + ($it.DiscontinuedDate.Value.Month * 100)) + $it.DiscontinuedDate.Value.Day))"
                },
                new object[]
                {
                    new NotEqualsBinaryOperatorParameters
                    (
                        new ConstantOperatorParameters(null),
                        new MemberSelectorOperatorParameters("DiscontinuedDate", new ParameterOperatorParameters(parameterName))
                    ),
                    "$it => (null != $it.DiscontinuedDate)"
                },
                new object[]
                {
                    new EqualsBinaryOperatorParameters
                    (
                        new MemberSelectorOperatorParameters("DiscontinuedDate", new ParameterOperatorParameters(parameterName)),
                        new ConstantOperatorParameters(null)
                    ),
                    "$it => ($it.DiscontinuedDate == null)"
                },
            };

        [Theory]
        [MemberData(nameof(DateFunction_Nullable_Data))]
        public void DateFunction_Nullable(IExpressionParameter filterBody, string expression)
        {
            //act
            var filter = CreateFilter<Product>();

            //assert
            AssertFilterStringIsCorrect(filter, expression);

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    filterBody
                );
        }

        public static List<object[]> DateFunction_NonNullable_Data
            => new List<object[]>
            {
                new object[]
                {
                    new EqualsBinaryOperatorParameters
                    (
                        new ConvertToNumericDateOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("NonNullableDiscontinuedDate", new ParameterOperatorParameters(parameterName))
                        ),
                        new ConvertToNumericDateOperatorParameters
                        (
                            new ConstantOperatorParameters(new Date(2015, 2, 26))
                        )
                    ),
                    "$it => (((($it.NonNullableDiscontinuedDate.Year * 10000) + ($it.NonNullableDiscontinuedDate.Month * 100)) + $it.NonNullableDiscontinuedDate.Day) == (((2015-02-26.Year * 10000) + (2015-02-26.Month * 100)) + 2015-02-26.Day))"
                },
                new object[]
                {
                    new LessThanBinaryOperatorParameters
                    (
                        new ConvertToNumericDateOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("NonNullableDiscontinuedDate", new ParameterOperatorParameters(parameterName))
                        ),
                        new ConvertToNumericDateOperatorParameters
                        (
                            new ConstantOperatorParameters(new Date(2016, 2, 26))
                        )
                    ),
                    "$it => (((($it.NonNullableDiscontinuedDate.Year * 10000) + ($it.NonNullableDiscontinuedDate.Month * 100)) + $it.NonNullableDiscontinuedDate.Day) < (((2016-02-26.Year * 10000) + (2016-02-26.Month * 100)) + 2016-02-26.Day))"
                },
                new object[]
                {
                    new GreaterThanOrEqualsBinaryOperatorParameters
                    (
                        new ConvertToNumericDateOperatorParameters
                        (
                            new ConstantOperatorParameters(new Date(2015, 2, 26))
                        ),
                        new ConvertToNumericDateOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("NonNullableDiscontinuedDate", new ParameterOperatorParameters(parameterName))
                        )
                    ),
                    "$it => ((((2015-02-26.Year * 10000) + (2015-02-26.Month * 100)) + 2015-02-26.Day) >= ((($it.NonNullableDiscontinuedDate.Year * 10000) + ($it.NonNullableDiscontinuedDate.Month * 100)) + $it.NonNullableDiscontinuedDate.Day))"
                }
            };

        [Theory]
        [MemberData(nameof(DateFunction_NonNullable_Data))]
        public void DateFunction_NonNullable(IExpressionParameter filterBody, string expression)
        {
            //act
            var filter = CreateFilter<Product>();

            //assert
            AssertFilterStringIsCorrect(filter, expression);

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    filterBody
                );
        }

        public static List<object[]> TimeFunction_Nullable_Data
            => new List<object[]>
            {
                new object[]
                {
                    new EqualsBinaryOperatorParameters
                    (
                        new ConvertToNumericTimeOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("DiscontinuedDate", new ParameterOperatorParameters(parameterName))
                        ),
                        new ConvertToNumericTimeOperatorParameters
                        (
                            new ConstantOperatorParameters(new TimeOfDay(1, 2, 3, 4))
                        )
                    ),
                    "$it => (((Convert($it.DiscontinuedDate.Value.Hour) * 36000000000) + ((Convert($it.DiscontinuedDate.Value.Minute) * 600000000) + ((Convert($it.DiscontinuedDate.Value.Second) * 10000000) + Convert($it.DiscontinuedDate.Value.Millisecond)))) == ((Convert(01:02:03.0040000.Hours) * 36000000000) + ((Convert(01:02:03.0040000.Minutes) * 600000000) + ((Convert(01:02:03.0040000.Seconds) * 10000000) + Convert(01:02:03.0040000.Milliseconds)))))"
                },
                new object[]
                {
                    new GreaterThanOrEqualsBinaryOperatorParameters
                    (
                        new ConvertToNumericTimeOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("DiscontinuedDate", new ParameterOperatorParameters(parameterName))
                        ),
                        new ConvertToNumericTimeOperatorParameters
                        (
                            new ConstantOperatorParameters(new TimeOfDay(1, 2, 3, 4))
                        )
                    ),
                    "$it => (((Convert($it.DiscontinuedDate.Value.Hour) * 36000000000) + ((Convert($it.DiscontinuedDate.Value.Minute) * 600000000) + ((Convert($it.DiscontinuedDate.Value.Second) * 10000000) + Convert($it.DiscontinuedDate.Value.Millisecond)))) >= ((Convert(01:02:03.0040000.Hours) * 36000000000) + ((Convert(01:02:03.0040000.Minutes) * 600000000) + ((Convert(01:02:03.0040000.Seconds) * 10000000) + Convert(01:02:03.0040000.Milliseconds)))))"
                },
                new object[]
                {
                    new LessThanOrEqualsBinaryOperatorParameters
                    (
                        new ConvertToNumericTimeOperatorParameters
                        (
                            new ConstantOperatorParameters(new TimeOfDay(1, 2, 3, 4))
                        ),
                        new ConvertToNumericTimeOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("DiscontinuedDate", new ParameterOperatorParameters(parameterName))
                        )
                    ),
                    "$it => (((Convert(01:02:03.0040000.Hours) * 36000000000) + ((Convert(01:02:03.0040000.Minutes) * 600000000) + ((Convert(01:02:03.0040000.Seconds) * 10000000) + Convert(01:02:03.0040000.Milliseconds)))) <= ((Convert($it.DiscontinuedDate.Value.Hour) * 36000000000) + ((Convert($it.DiscontinuedDate.Value.Minute) * 600000000) + ((Convert($it.DiscontinuedDate.Value.Second) * 10000000) + Convert($it.DiscontinuedDate.Value.Millisecond)))))"
                },
                new object[]
                {
                    new NotEqualsBinaryOperatorParameters
                    (
                        new ConstantOperatorParameters(null),
                        new MemberSelectorOperatorParameters("DiscontinuedDate", new ParameterOperatorParameters(parameterName))
                    ),
                    "$it => (null != $it.DiscontinuedDate)"
                },
                new object[]
                {
                    new EqualsBinaryOperatorParameters
                    (
                        new MemberSelectorOperatorParameters("DiscontinuedDate", new ParameterOperatorParameters(parameterName)),
                        new ConstantOperatorParameters(null)
                    ),
                    "$it => ($it.DiscontinuedDate == null)"
                }
            };

        [Theory]
        [MemberData(nameof(TimeFunction_Nullable_Data))]
        public void TimeFunction_Nullable(IExpressionParameter filterBody, string expression)
        {
            //act
            var filter = CreateFilter<Product>();

            //assert
            AssertFilterStringIsCorrect(filter, expression);

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    filterBody
                );
        }

        public static List<object[]> TimeFunction_NonNullable_Data
            => new List<object[]>
            {
                new object[]
                {
                    new EqualsBinaryOperatorParameters
                    (
                        new ConvertToNumericTimeOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("NonNullableDiscontinuedDate", new ParameterOperatorParameters(parameterName))
                        ),
                        new ConvertToNumericTimeOperatorParameters
                        (
                            new ConstantOperatorParameters(new TimeOfDay(1, 2, 3, 4))
                        )
                    ),
                    "$it => (((Convert($it.NonNullableDiscontinuedDate.Hour) * 36000000000) + ((Convert($it.NonNullableDiscontinuedDate.Minute) * 600000000) + ((Convert($it.NonNullableDiscontinuedDate.Second) * 10000000) + Convert($it.NonNullableDiscontinuedDate.Millisecond)))) == ((Convert(01:02:03.0040000.Hours) * 36000000000) + ((Convert(01:02:03.0040000.Minutes) * 600000000) + ((Convert(01:02:03.0040000.Seconds) * 10000000) + Convert(01:02:03.0040000.Milliseconds)))))"
                },
                new object[]
                {
                    new GreaterThanOrEqualsBinaryOperatorParameters
                    (
                        new ConvertToNumericTimeOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("NonNullableDiscontinuedDate", new ParameterOperatorParameters(parameterName))
                        ),
                        new ConvertToNumericTimeOperatorParameters
                        (
                            new ConstantOperatorParameters(new TimeOfDay(1, 2, 3, 4))
                        )
                    ),
                    "$it => (((Convert($it.NonNullableDiscontinuedDate.Hour) * 36000000000) + ((Convert($it.NonNullableDiscontinuedDate.Minute) * 600000000) + ((Convert($it.NonNullableDiscontinuedDate.Second) * 10000000) + Convert($it.NonNullableDiscontinuedDate.Millisecond)))) >= ((Convert(01:02:03.0040000.Hours) * 36000000000) + ((Convert(01:02:03.0040000.Minutes) * 600000000) + ((Convert(01:02:03.0040000.Seconds) * 10000000) + Convert(01:02:03.0040000.Milliseconds)))))"
                },
                new object[]
                {
                    new LessThanOrEqualsBinaryOperatorParameters
                    (
                        new ConvertToNumericTimeOperatorParameters
                        (
                            new ConstantOperatorParameters(new TimeOfDay(1, 2, 3, 4))
                        ),
                        new ConvertToNumericTimeOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("NonNullableDiscontinuedDate", new ParameterOperatorParameters(parameterName))
                        )
                    ),
                    "$it => (((Convert(01:02:03.0040000.Hours) * 36000000000) + ((Convert(01:02:03.0040000.Minutes) * 600000000) + ((Convert(01:02:03.0040000.Seconds) * 10000000) + Convert(01:02:03.0040000.Milliseconds)))) <= ((Convert($it.NonNullableDiscontinuedDate.Hour) * 36000000000) + ((Convert($it.NonNullableDiscontinuedDate.Minute) * 600000000) + ((Convert($it.NonNullableDiscontinuedDate.Second) * 10000000) + Convert($it.NonNullableDiscontinuedDate.Millisecond)))))"
                }
            };

        [Theory]
        [MemberData(nameof(TimeFunction_NonNullable_Data))]
        public void TimeFunction_NonNullable(IExpressionParameter filterBody, string expression)
        {
            //act
            var filter = CreateFilter<Product>();

            //assert
            AssertFilterStringIsCorrect(filter, expression);

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    filterBody
                );
        }
        #endregion Date Functions

        #region Math Functions
        [Fact]
        public void RecursiveMethodCall()
        {
            //act
            var filter = CreateFilter<Product>();
            bool result = RunFilter(filter, new Product { UnitPrice = 123.3m });

            //assert
            AssertFilterStringIsCorrect(filter, "$it => ($it.UnitPrice.Value.Floor().Floor() == 123)");
            Assert.True(result);

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    new EqualsBinaryOperatorParameters
                    (
                        new FloorOperatorParameters
                        (
                            new FloorOperatorParameters
                            (
                                new MemberSelectorOperatorParameters("UnitPrice", new ParameterOperatorParameters(parameterName))
                            )
                        ),
                        new ConstantOperatorParameters(123m)
                    )
                );
        }

        [Fact]
        public void RecursiveMethodCallInvalidOperationException()
        {
            //act
            var filter = CreateFilter<Product>();

            //assert
            AssertFilterStringIsCorrect(filter, "$it => ($it.UnitPrice.Value.Floor().Floor() == 123)");
            Assert.Throws<InvalidOperationException>(() => RunFilter(filter, new Product { }));

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    new EqualsBinaryOperatorParameters
                    (
                        new FloorOperatorParameters
                        (
                            new FloorOperatorParameters
                            (
                                new MemberSelectorOperatorParameters("UnitPrice", new ParameterOperatorParameters(parameterName))
                            )
                        ),
                        new ConstantOperatorParameters(123m)
                    )
                );
        }

        [Fact]
        public void MathRoundDecimalInvalidOperationException()
        {
            //act
            var filter = CreateFilter<Product>();

            //assert
            AssertFilterStringIsCorrect(filter, string.Format(CultureInfo.InvariantCulture, "$it => ($it.UnitPrice.Value.Round() > {0:0.00})", 5.0));
            Assert.Throws<InvalidOperationException>(() => RunFilter(filter, new Product { }));

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    new GreaterThanBinaryOperatorParameters
                    (
                        new RoundOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("UnitPrice", new ParameterOperatorParameters(parameterName))
                        ),
                        new ConstantOperatorParameters(5.00m)
                    )
                );
        }

        public static IEnumerable<object[]> MathRoundDecimal_DataSet
            => new List<object[]>
                {
                    new object[] { 5.9m, true },
                    new object[] { 5.4m, false }
                };

        [Theory, MemberData(nameof(MathRoundDecimal_DataSet))]
        public void MathRoundDecimal(decimal? unitPrice, bool expected)
        {
            //act
            var filter = CreateFilter<Product>();
            bool result = RunFilter(filter, new Product { UnitPrice = ToNullable<decimal>(unitPrice) });

            //assert
            AssertFilterStringIsCorrect(filter, string.Format(CultureInfo.InvariantCulture, "$it => ($it.UnitPrice.Value.Round() > {0:0.00})", 5.0));
            Assert.Equal(expected, result);

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    new GreaterThanBinaryOperatorParameters
                    (
                        new RoundOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("UnitPrice", new ParameterOperatorParameters(parameterName))
                        ),
                        new ConstantOperatorParameters(5.00m)
                    )
                );
        }

        [Fact]
        public void MathRoundDoubleInvalidOperationException()
        {
            //act
            var filter = CreateFilter<Product>();

            //assert
            AssertFilterStringIsCorrect(filter, string.Format(CultureInfo.InvariantCulture, "$it => ($it.Weight.Value.Round() > {0})", 5));
            Assert.Throws<InvalidOperationException>(() => RunFilter(filter, new Product { }));

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    new GreaterThanBinaryOperatorParameters
                    (
                        new RoundOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("Weight", new ParameterOperatorParameters(parameterName))
                        ),
                        new ConstantOperatorParameters(5d)
                    )
                );
        }

        [Theory]
        [InlineData(5.9d, true)]
        [InlineData(5.4d, false)]
        public void MathRoundDouble(double? weight, bool expected)
        {
            //act
            var filter = CreateFilter<Product>();
            bool result = RunFilter(filter, new Product { Weight = ToNullable<double>(weight) });

            //assert
            AssertFilterStringIsCorrect(filter, string.Format(CultureInfo.InvariantCulture, "$it => ($it.Weight.Value.Round() > {0})", 5));
            Assert.Equal(expected, result);

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    new GreaterThanBinaryOperatorParameters
                    (
                        new RoundOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("Weight", new ParameterOperatorParameters(parameterName))
                        ),
                        new ConstantOperatorParameters(5d)
                    )
                );
        }

        [Fact]
        public void MathRoundFloatInvalidOperationException()
        {
            //act
            var filter = CreateFilter<Product>();

            //assert
            AssertFilterStringIsCorrect(filter, string.Format(CultureInfo.InvariantCulture, "$it => (Convert($it.Width).Value.Round() > {0})", 5));
            Assert.Throws<InvalidOperationException>(() => RunFilter(filter, new Product { }));

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    new GreaterThanBinaryOperatorParameters
                    (
                        new RoundOperatorParameters
                        (
                            new ConvertOperatorParameters(new MemberSelectorOperatorParameters("Width", new ParameterOperatorParameters(parameterName)), typeof(double?))
                        ),
                        new ConstantOperatorParameters(5d)
                    )
                );
        }

        [Theory]
        [InlineData(5.9f, true)]
        [InlineData(5.4f, false)]
        public void MathRoundFloat(float? width, bool expected)
        {
            //act
            var filter = CreateFilter<Product>();
            bool result = RunFilter(filter, new Product { Width = ToNullable<float>(width) });

            //assert
            AssertFilterStringIsCorrect(filter, string.Format(CultureInfo.InvariantCulture, "$it => (Convert($it.Width).Value.Round() > {0})", 5));
            Assert.Equal(expected, result);

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    new GreaterThanBinaryOperatorParameters
                    (
                        new RoundOperatorParameters
                        (
                            new ConvertOperatorParameters(new MemberSelectorOperatorParameters("Width", new ParameterOperatorParameters(parameterName)), typeof(double?))
                        ),
                        new ConstantOperatorParameters(5d)
                    )
                );
        }

        [Fact]
        public void MathFloorDecimalInvalidOperationException()
        {
            //act
            var filter = CreateFilter<Product>();

            //assert
            AssertFilterStringIsCorrect(filter, "$it => ($it.UnitPrice.Value.Floor() == 5)");
            Assert.Throws<InvalidOperationException>(() => RunFilter(filter, new Product { }));

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    new EqualsBinaryOperatorParameters
                    (
                        new FloorOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("UnitPrice", new ParameterOperatorParameters(parameterName))
                        ),
                        new ConstantOperatorParameters(5m)
                    )
                );
        }

        public static IEnumerable<object[]> MathFloorDecimal_DataSet
            => new List<object[]>
                {
                    new object[] { 5.4m, true },
                    new object[] { 4.4m, false }
                };

        [Theory, MemberData(nameof(MathFloorDecimal_DataSet))]
        public void MathFloorDecimal(decimal? unitPrice, bool expected)
        {
            //act
            var filter = CreateFilter<Product>();
            bool result = RunFilter(filter, new Product { UnitPrice = ToNullable<decimal>(unitPrice) });

            //assert
            AssertFilterStringIsCorrect(filter, "$it => ($it.UnitPrice.Value.Floor() == 5)");
            Assert.Equal(expected, result);

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    new EqualsBinaryOperatorParameters
                    (
                        new FloorOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("UnitPrice", new ParameterOperatorParameters(parameterName))
                        ),
                        new ConstantOperatorParameters(5m)
                    )
                );
        }

        [Fact]
        public void MathFloorDoubleInvalidOperationException()
        {
            //act
            var filter = CreateFilter<Product>();

            //assert
            AssertFilterStringIsCorrect(filter, "$it => ($it.Weight.Value.Floor() == 5)");
            Assert.Throws<InvalidOperationException>(() => RunFilter(filter, new Product { }));

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    new EqualsBinaryOperatorParameters
                    (
                        new FloorOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("Weight", new ParameterOperatorParameters(parameterName))
                        ),
                        new ConstantOperatorParameters(5d)
                    )
                );
        }

        [Theory]
        [InlineData(5.4d, true)]
        [InlineData(4.4d, false)]
        public void MathFloorDouble(double? weight, bool expected)
        {
            //act
            var filter = CreateFilter<Product>();
            bool result = RunFilter(filter, new Product { Weight = ToNullable<double>(weight) });

            //assert
            AssertFilterStringIsCorrect(filter, "$it => ($it.Weight.Value.Floor() == 5)");
            Assert.Equal(expected, result);

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    new EqualsBinaryOperatorParameters
                    (
                        new FloorOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("Weight", new ParameterOperatorParameters(parameterName))
                        ),
                        new ConstantOperatorParameters(5d)
                    )
                );
        }

        [Fact]
        public void MathFloorFloatInvalidOperationException()
        {
            //act
            var filter = CreateFilter<Product>();

            //assert
            AssertFilterStringIsCorrect(filter, "$it => (Convert($it.Width).Value.Floor() == 5)");
            Assert.Throws<InvalidOperationException>(() => RunFilter(filter, new Product { }));

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    new EqualsBinaryOperatorParameters
                    (
                        new FloorOperatorParameters
                        (
                            new ConvertOperatorParameters(new MemberSelectorOperatorParameters("Width", new ParameterOperatorParameters(parameterName)), typeof(double?))
                        ),
                        new ConstantOperatorParameters(5d)
                    )
                );
        }

        [Theory]
        [InlineData(5.4f, true)]
        [InlineData(4.4f, false)]
        public void MathFloorFloat(float? width, bool expected)
        {
            //act
            var filter = CreateFilter<Product>();
            bool result = RunFilter(filter, new Product { Width = ToNullable<float>(width) });

            //assert
            AssertFilterStringIsCorrect(filter, "$it => (Convert($it.Width).Value.Floor() == 5)");
            Assert.Equal(expected, result);

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    new EqualsBinaryOperatorParameters
                    (
                        new FloorOperatorParameters
                        (
                            new ConvertOperatorParameters(new MemberSelectorOperatorParameters("Width", new ParameterOperatorParameters(parameterName)), typeof(double?))
                        ),
                        new ConstantOperatorParameters(5d)
                    )
                );
        }

        [Fact]
        public void MathCeilingDecimalInvalidOperationException()
        {
            //act
            var filter = CreateFilter<Product>();

            //assert
            AssertFilterStringIsCorrect(filter, "$it => ($it.UnitPrice.Value.Ceiling() == 5)");
            Assert.Throws<InvalidOperationException>(() => RunFilter(filter, new Product { }));

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    new EqualsBinaryOperatorParameters
                    (
                        new CeilingOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("UnitPrice", new ParameterOperatorParameters(parameterName))
                        ),
                        new ConstantOperatorParameters(5m)
                    )
                );
        }

        public static IEnumerable<object[]> MathCeilingDecimal_DataSet
            => new List<object[]>
                {
                    new object[] { 4.1m, true },
                    new object[] { 5.9m, false }
                };

        [Theory, MemberData(nameof(MathCeilingDecimal_DataSet))]
        public void MathCeilingDecimal(object unitPrice, bool expected)
        {
            //act
            var filter = CreateFilter<Product>();
            bool result = RunFilter(filter, new Product { UnitPrice = ToNullable<decimal>(unitPrice) });

            //assert
            AssertFilterStringIsCorrect(filter, "$it => ($it.UnitPrice.Value.Ceiling() == 5)");
            Assert.Equal(expected, result);

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    new EqualsBinaryOperatorParameters
                    (
                        new CeilingOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("UnitPrice", new ParameterOperatorParameters(parameterName))
                        ),
                        new ConstantOperatorParameters(5m)
                    )
                );
        }

        [Fact]
        public void MathCeilingDoubleInvalidOperationException()
        {
            //act
            var filter = CreateFilter<Product>();

            //assert
            AssertFilterStringIsCorrect(filter, "$it => ($it.Weight.Value.Ceiling() == 5)");
            Assert.Throws<InvalidOperationException>(() => RunFilter(filter, new Product { }));

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    new EqualsBinaryOperatorParameters
                    (
                        new CeilingOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("Weight", new ParameterOperatorParameters(parameterName))
                        ),
                        new ConstantOperatorParameters(5d)
                    )
                );
        }

        [Theory]
        [InlineData(4.1d, true)]
        [InlineData(5.9d, false)]
        public void MathCeilingDouble(double? weight, bool expected)
        {
            //act
            var filter = CreateFilter<Product>();
            bool result = RunFilter(filter, new Product { Weight = ToNullable<double>(weight) });

            //assert
            AssertFilterStringIsCorrect(filter, "$it => ($it.Weight.Value.Ceiling() == 5)");
            Assert.Equal(expected, result);

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    new EqualsBinaryOperatorParameters
                    (
                        new CeilingOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("Weight", new ParameterOperatorParameters(parameterName))
                        ),
                        new ConstantOperatorParameters(5d)
                    )
                );
        }

        [Fact]
        public void MathCeilingFloatInvalidOperationException()
        {
            //act
            var filter = CreateFilter<Product>();

            //assert
            AssertFilterStringIsCorrect(filter, "$it => (Convert($it.Width).Value.Ceiling() == 5)");
            Assert.Throws<InvalidOperationException>(() => RunFilter(filter, new Product { }));

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    new EqualsBinaryOperatorParameters
                    (
                        new CeilingOperatorParameters
                        (
                            new ConvertOperatorParameters(new MemberSelectorOperatorParameters("Width", new ParameterOperatorParameters(parameterName)), typeof(double?))
                        ),
                        new ConstantOperatorParameters(5d)
                    )
                );
        }

        [Theory]
        [InlineData(4.1f, true)]
        [InlineData(5.9f, false)]
        public void MathCeilingFloat(float? width, bool expected)
        {
            //act
            var filter = CreateFilter<Product>();
            bool result = RunFilter(filter, new Product { Width = ToNullable<float>(width) });

            //assert
            AssertFilterStringIsCorrect(filter, "$it => (Convert($it.Width).Value.Ceiling() == 5)");
            Assert.Equal(expected, result);

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    new EqualsBinaryOperatorParameters
                    (
                        new CeilingOperatorParameters
                        (
                            new ConvertOperatorParameters(new MemberSelectorOperatorParameters("Width", new ParameterOperatorParameters(parameterName)), typeof(double?))
                        ),
                        new ConstantOperatorParameters(5d)
                    )
                );
        }

        public static List<object[]> MathFunctions_VariousTypes_Data
            => new List<object[]>
            {
                new object[]
                {
                    new EqualsBinaryOperatorParameters
                    (
                        new FloorOperatorParameters
                        (
                            new ConvertOperatorParameters(new MemberSelectorOperatorParameters("FloatProp", new ParameterOperatorParameters(parameterName)), typeof(double))
                        ),
                        new FloorOperatorParameters
                        (
                            new ConvertOperatorParameters(new MemberSelectorOperatorParameters("FloatProp", new ParameterOperatorParameters(parameterName)), typeof(double))
                        )
                    )
                },
                new object[]
                {
                    new EqualsBinaryOperatorParameters
                    (
                        new RoundOperatorParameters
                        (
                            new ConvertOperatorParameters(new MemberSelectorOperatorParameters("FloatProp", new ParameterOperatorParameters(parameterName)), typeof(double))
                        ),
                        new RoundOperatorParameters
                        (
                            new ConvertOperatorParameters(new MemberSelectorOperatorParameters("FloatProp", new ParameterOperatorParameters(parameterName)), typeof(double))
                        )
                    )
                },
                new object[]
                {
                    new EqualsBinaryOperatorParameters
                    (
                        new CeilingOperatorParameters
                        (
                            new ConvertOperatorParameters(new MemberSelectorOperatorParameters("FloatProp", new ParameterOperatorParameters(parameterName)), typeof(double))
                        ),
                        new CeilingOperatorParameters
                        (
                            new ConvertOperatorParameters(new MemberSelectorOperatorParameters("FloatProp", new ParameterOperatorParameters(parameterName)), typeof(double))
                        )
                    )
                },
                new object[]
                {
                    new EqualsBinaryOperatorParameters
                    (
                        new FloorOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("DoubleProp", new ParameterOperatorParameters(parameterName))
                        ),
                        new FloorOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("DoubleProp", new ParameterOperatorParameters(parameterName))
                        )
                    )
                },
                new object[]
                {
                    new EqualsBinaryOperatorParameters
                    (
                        new RoundOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("DoubleProp", new ParameterOperatorParameters(parameterName))
                        ),
                        new RoundOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("DoubleProp", new ParameterOperatorParameters(parameterName))
                        )
                    )
                },
                new object[]
                {
                    new EqualsBinaryOperatorParameters
                    (
                        new CeilingOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("DoubleProp", new ParameterOperatorParameters(parameterName))
                        ),
                        new CeilingOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("DoubleProp", new ParameterOperatorParameters(parameterName))
                        )
                    )
                },
                    new object[]
                {
                    new EqualsBinaryOperatorParameters
                    (
                        new FloorOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("DecimalProp", new ParameterOperatorParameters(parameterName))
                        ),
                        new FloorOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("DecimalProp", new ParameterOperatorParameters(parameterName))
                        )
                    )
                },
                new object[]
                {
                    new EqualsBinaryOperatorParameters
                    (
                        new RoundOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("DecimalProp", new ParameterOperatorParameters(parameterName))
                        ),
                        new RoundOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("DecimalProp", new ParameterOperatorParameters(parameterName))
                        )
                    )
                },
                new object[]
                {
                    new EqualsBinaryOperatorParameters
                    (
                        new CeilingOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("DecimalProp", new ParameterOperatorParameters(parameterName))
                        ),
                        new CeilingOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("DecimalProp", new ParameterOperatorParameters(parameterName))
                        )
                    )
                },
            };

        [Theory]
        [MemberData(nameof(MathFunctions_VariousTypes_Data))]
        public void MathFunctions_VariousTypes(IExpressionParameter filterBody)
        {
            //act
            var filter = CreateFilter<DataTypes>();
            bool result = RunFilter(filter, new DataTypes { });

            //assert
            Assert.True(result);

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    filterBody
                );
        }
        #endregion Math Functions

        #region Custom Functions
        [Fact]
        public void CustomMethod_InstanceMethodOfDeclaringType()
        {
            //arrange
            const string productName = "Abcd";
            const int totalWidth = 5;
            const string expectedProductName = "Abcd ";

            //act
            var filter = CreateFilter<Product>();
            bool result = RunFilter(filter, new Product { ProductName = productName });

            //assert
            Assert.True(result);

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    new EqualsBinaryOperatorParameters
                    (
                        new CustomMethodOperatorParameters
                        (
                            typeof(string).GetMethod("PadRight", new Type[] { typeof(int) }),
                            new IExpressionParameter[]
                            {
                                new MemberSelectorOperatorParameters("ProductName", new ParameterOperatorParameters(parameterName)),
                                new ConstantOperatorParameters(totalWidth)
                            }
                        ),
                        new ConstantOperatorParameters(expectedProductName)
                    )
                );
        }

        [Fact]
        public void CustomMethod_StaticExtensionMethod()
        {
            //arrange
            const string productName = "Abcd";
            const int totalWidth = 5;
            const string expectedProductName = "Abcd ";

            //act
            var filter = CreateFilter<Product>();
            bool result = RunFilter(filter, new Product { ProductName = productName });

            //assert
            Assert.True(result);

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    new EqualsBinaryOperatorParameters
                    (
                        new CustomMethodOperatorParameters
                        (
                            typeof(StringExtender).GetMethod("PadRightExStatic", BindingFlags.Public | BindingFlags.Static),
                            new IExpressionParameter[]
                            {
                                new MemberSelectorOperatorParameters("ProductName", new ParameterOperatorParameters(parameterName)),
                                new ConstantOperatorParameters(totalWidth)
                            }
                        ),
                        new ConstantOperatorParameters(expectedProductName)
                    )
                );
        }

        [Fact]
        public void CustomMethod_StaticMethodNotOfDeclaringType()
        {
            //arrange
            const string productName = "Abcd";
            const int totalWidth = 5;
            const string expectedProductName = "Abcd ";

            //act
            var filter = CreateFilter<Product>();
            bool result = RunFilter(filter, new Product { ProductName = productName });

            //assert
            Assert.True(result);

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    new EqualsBinaryOperatorParameters
                    (
                        new CustomMethodOperatorParameters
                        (
                            typeof(FilterParameterTests).GetMethod("PadRightStatic", BindingFlags.NonPublic | BindingFlags.Static),
                            new IExpressionParameter[]
                            {
                                new MemberSelectorOperatorParameters("ProductName", new ParameterOperatorParameters(parameterName)),
                                new ConstantOperatorParameters(totalWidth)
                            }
                        ),
                        new ConstantOperatorParameters(expectedProductName)
                    )
                );
        }
        #endregion Custom Functions

        #region Data Types
        [Fact]
        public void GuidExpression()
        {
            //act
            var filter1 = CreateFilter1<DataTypes>();
            var filter2 = CreateFilter2<DataTypes>();

            //assert
            AssertFilterStringIsCorrect(filter1, "$it => ($it.GuidProp == 0efdaecf-a9f0-42f3-a384-1295917af95e)");
            AssertFilterStringIsCorrect(filter2, "$it => ($it.GuidProp == 0efdaecf-a9f0-42f3-a384-1295917af95e)");

            Expression<Func<T, bool>> CreateFilter1<T>()
                => GetFilter<T>
                (
                    new EqualsBinaryOperatorParameters
                    (
                        new MemberSelectorOperatorParameters("GuidProp", new ParameterOperatorParameters(parameterName)),
                        new ConstantOperatorParameters(new Guid("0EFDAECF-A9F0-42F3-A384-1295917AF95E"))
                    )
                );

            Expression<Func<T, bool>> CreateFilter2<T>()
                => GetFilter<T>
                (
                    new EqualsBinaryOperatorParameters
                    (
                        new MemberSelectorOperatorParameters("GuidProp", new ParameterOperatorParameters(parameterName)),
                        new ConstantOperatorParameters(new Guid("0efdaecf-a9f0-42f3-a384-1295917af95e"))
                    )
                );
        }

        public static List<object[]> DateTimeExpression_Data
            => new List<object[]>
            {
                new object[]
                {
                    new EqualsBinaryOperatorParameters
                    (
                        new MemberSelectorOperatorParameters("DateTimeProp", new ParameterOperatorParameters(parameterName)),
                        new ConstantOperatorParameters(new DateTimeOffset(new DateTime(2000, 12, 12, 12, 0, 0), TimeSpan.Zero))
                    ),
                    "$it => ($it.DateTimeProp == {0})"
                },
                new object[]
                {
                    new LessThanBinaryOperatorParameters
                    (
                        new MemberSelectorOperatorParameters("DateTimeProp", new ParameterOperatorParameters(parameterName)),
                        new ConstantOperatorParameters(new DateTimeOffset(new DateTime(2000, 12, 12, 12, 0, 0), TimeSpan.Zero))
                    ),
                    "$it => ($it.DateTimeProp < {0})"
                }
            };

        [Theory]
        [MemberData(nameof(DateTimeExpression_Data))]
        public void DateTimeExpression(IExpressionParameter filterBody, string expectedExpression)
        {
            //arrange
            var dateTime = new DateTimeOffset(new DateTime(2000, 12, 12, 12, 0, 0), TimeSpan.Zero);

            //act
            var filter = CreateFilter<DataTypes>();

            //assert
            AssertFilterStringIsCorrect(filter, string.Format(CultureInfo.InvariantCulture, expectedExpression, dateTime));

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    filterBody
                );
        }

        [Fact]
        public void IntegerLiteralSuffix()
        {
            //act
            var filter1 = CreateFilter1<DataTypes>();
            var filter2 = CreateFilter2<DataTypes>();

            //assert
            AssertFilterStringIsCorrect(filter1, "$it => (($it.LongProp < 987654321) AndAlso ($it.LongProp > 123456789))");
            AssertFilterStringIsCorrect(filter2, "$it => (($it.LongProp < -987654321) AndAlso ($it.LongProp > -123456789))");

            Expression<Func<T, bool>> CreateFilter1<T>()
                => GetFilter<T>
                (
                    new AndBinaryOperatorParameters
                    (
                        new LessThanBinaryOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("LongProp", new ParameterOperatorParameters(parameterName)),
                            new ConstantOperatorParameters((long)987654321, typeof(long))
                        ),
                        new GreaterThanBinaryOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("LongProp", new ParameterOperatorParameters(parameterName)),
                            new ConstantOperatorParameters((long)123456789, typeof(long))
                        )
                    )
                );

            Expression<Func<T, bool>> CreateFilter2<T>()
                => GetFilter<T>
                (
                    new AndBinaryOperatorParameters
                    (
                        new LessThanBinaryOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("LongProp", new ParameterOperatorParameters(parameterName)),
                            new ConstantOperatorParameters((long)-987654321, typeof(long))
                        ),
                        new GreaterThanBinaryOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("LongProp", new ParameterOperatorParameters(parameterName)),
                            new ConstantOperatorParameters((long)-123456789, typeof(long))
                        )
                    )
                );
        }

        [Fact]
        public void EnumInExpression()
        {
            //act
            var filter = CreateFilter<DataTypes>();
            var constant = (ConstantExpression)((MethodCallExpression)filter.Body).Arguments[0];
            var values = (IList<SimpleEnum>)constant.Value;

            //assert
            AssertFilterStringIsCorrect(filter, "$it => System.Collections.Generic.List`1[Contoso.Bsl.Flow.Unit.Tests.Data.SimpleEnum].Contains($it.SimpleEnumProp)");
            Assert.Equal(new[] { SimpleEnum.First, SimpleEnum.Second }, values);

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    new InOperatorParameters
                    (
                        new MemberSelectorOperatorParameters("SimpleEnumProp", new ParameterOperatorParameters(parameterName)),
                        new CollectionConstantOperatorParameters(new List<object> { SimpleEnum.First, SimpleEnum.Second }, typeof(SimpleEnum))
                    )
                );
        }

        [Fact]
        public void EnumInExpression_NullableEnum_WithNullable()
        {
            //act
            var filter = CreateFilter<DataTypes>();
            var constant = (ConstantExpression)((MethodCallExpression)filter.Body).Arguments[0];
            var values = (IList<SimpleEnum?>)constant.Value;

            //assert
            AssertFilterStringIsCorrect(filter, "$it => System.Collections.Generic.List`1[System.Nullable`1[Contoso.Bsl.Flow.Unit.Tests.Data.SimpleEnum]].Contains($it.NullableSimpleEnumProp)");
            Assert.Equal(new SimpleEnum?[] { SimpleEnum.First, SimpleEnum.Second }, values);

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    new InOperatorParameters
                    (
                        new MemberSelectorOperatorParameters("NullableSimpleEnumProp", new ParameterOperatorParameters(parameterName)),
                        new CollectionConstantOperatorParameters(new List<object> { SimpleEnum.First, SimpleEnum.Second }, typeof(SimpleEnum?))
                    )
                );
        }

        [Fact]
        public void EnumInExpression_NullableEnum_WithNullValue()
        {
            //act
            var filter = CreateFilter<DataTypes>();
            var constant = (ConstantExpression)((MethodCallExpression)filter.Body).Arguments[0];
            var values = (IList<SimpleEnum?>)constant.Value;

            //assert
            AssertFilterStringIsCorrect(filter, "$it => System.Collections.Generic.List`1[System.Nullable`1[Contoso.Bsl.Flow.Unit.Tests.Data.SimpleEnum]].Contains($it.NullableSimpleEnumProp)");
            Assert.Equal(new SimpleEnum?[] { SimpleEnum.First, null }, values);

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    new InOperatorParameters
                    (
                        new MemberSelectorOperatorParameters("NullableSimpleEnumProp", new ParameterOperatorParameters(parameterName)),
                        new CollectionConstantOperatorParameters(new List<object> { SimpleEnum.First, null }, typeof(SimpleEnum?))
                    )
                );
        }

        [Fact]
        public void RealLiteralSuffixes()
        {
            //act
            var filter1 = CreateFilter1<DataTypes>();
            var filter2 = CreateFilter2<DataTypes>();

            //assert
            AssertFilterStringIsCorrect(filter1, string.Format(CultureInfo.InvariantCulture, "$it => (($it.FloatProp < {0:0.00}) AndAlso ($it.FloatProp > {1:0.00}))", 4321.56, 1234.56));
            AssertFilterStringIsCorrect(filter2, string.Format(CultureInfo.InvariantCulture, "$it => (($it.DecimalProp < {0:0.00}) AndAlso ($it.DecimalProp > {1:0.00}))", 4321.56, 1234.56));

            Expression<Func<T, bool>> CreateFilter1<T>()
                => GetFilter<T>
                (
                    new AndBinaryOperatorParameters
                    (
                        new LessThanBinaryOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("FloatProp", new ParameterOperatorParameters(parameterName)),
                            new ConstantOperatorParameters(4321.56F)
                        ),
                        new GreaterThanBinaryOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("FloatProp", new ParameterOperatorParameters(parameterName)),
                            new ConstantOperatorParameters(1234.56f)
                        )
                    )
                );

            Expression<Func<T, bool>> CreateFilter2<T>()
                => GetFilter<T>
                (
                    new AndBinaryOperatorParameters
                    (
                        new LessThanBinaryOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("DecimalProp", new ParameterOperatorParameters(parameterName)),
                            new ConstantOperatorParameters(4321.56M)
                        ),
                        new GreaterThanBinaryOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("DecimalProp", new ParameterOperatorParameters(parameterName)),
                            new ConstantOperatorParameters(1234.56m)
                        )
                    )
                );
        }

        [Theory]
        [InlineData("hello,world", "hello,world")]
        [InlineData("'hello,world", "'hello,world")]
        [InlineData("hello,world'", "hello,world'")]
        [InlineData("hello,'wor'ld", "hello,'wor'ld")]
        [InlineData("hello,''world", "hello,''world")]
        [InlineData("\"hello,world\"", "\"hello,world\"")]
        [InlineData("\"hello,world", "\"hello,world")]
        [InlineData("hello,world\"", "hello,world\"")]
        [InlineData("hello,\"world", "hello,\"world")]
        [InlineData("México D.F.", "México D.F.")]
        [InlineData("æææøøøååå", "æææøøøååå")]
        [InlineData("いくつかのテキスト", "いくつかのテキスト")]
        public void StringLiterals(string literal, string expected)
        {
            //act
            var filter = CreateFilter<Product>();

            //assert
            AssertFilterStringIsCorrect(filter, string.Format("$it => ($it.ProductName == \"{0}\")", expected));

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    new EqualsBinaryOperatorParameters
                    (
                        new MemberSelectorOperatorParameters("ProductName", new ParameterOperatorParameters(parameterName)),
                        new ConstantOperatorParameters(literal)
                    )
                );
        }

        [Theory]
        [InlineData('$')]
        [InlineData('&')]
        [InlineData('+')]
        [InlineData(',')]
        [InlineData('/')]
        [InlineData(':')]
        [InlineData(';')]
        [InlineData('=')]
        [InlineData('?')]
        [InlineData('@')]
        [InlineData(' ')]
        [InlineData('<')]
        [InlineData('>')]
        [InlineData('#')]
        [InlineData('%')]
        [InlineData('{')]
        [InlineData('}')]
        [InlineData('|')]
        [InlineData('\\')]
        [InlineData('^')]
        [InlineData('~')]
        [InlineData('[')]
        [InlineData(']')]
        [InlineData('`')]
        public void SpecialCharactersInStringLiteral(char c)
        {
            //act
            var filter = CreateFilter<Product>();
            bool result = RunFilter(filter, new Product { ProductName = c.ToString() });

            //assert
            AssertFilterStringIsCorrect(filter, string.Format("$it => ($it.ProductName == \"{0}\")", c));
            Assert.True(result);

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    new EqualsBinaryOperatorParameters
                    (
                        new MemberSelectorOperatorParameters("ProductName", new ParameterOperatorParameters(parameterName)),
                        new ConstantOperatorParameters(c.ToString())
                    )
                );
        }
        #endregion Data Types

        #region Casts
        [Fact]
        public void NSCast_OnEnumerableEntityCollection_GeneratesExpression_WithOfTypeOnEnumerable()
        {
            //act
            var filter = CreateFilter<Product>();

            //assert
            AssertFilterStringIsCorrect(filter, "$it => $it.Category.EnumerableProducts.OfType().Any(p => (p.ProductName == \"ProductName\"))");

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    new AnyOperatorParameters
                    (
                        new CollectionCastOperatorParameters
                        (
                            new MemberSelectorOperatorParameters
                            (
                                "EnumerableProducts",
                                new MemberSelectorOperatorParameters("Category", new ParameterOperatorParameters(parameterName))
                            ),
                            typeof(DerivedProduct)
                        ),
                        new EqualsBinaryOperatorParameters
                        (
                             new MemberSelectorOperatorParameters("ProductName", new ParameterOperatorParameters("p")),
                             new ConstantOperatorParameters("ProductName")
                        ),
                        "p"
                    )
                );
        }

        [Fact]
        public void NSCast_OnQueryableEntityCollection_GeneratesExpression_WithOfTypeOnQueryable()
        {
            //act
            var filter = CreateFilter<Product>();

            //assert
            AssertFilterStringIsCorrect(filter, "$it => $it.Category.QueryableProducts.OfType().Any(p => (p.ProductName == \"ProductName\"))");

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    new AnyOperatorParameters
                    (
                        new CollectionCastOperatorParameters
                        (
                            new MemberSelectorOperatorParameters
                            (
                                "QueryableProducts",
                                new MemberSelectorOperatorParameters("Category", new ParameterOperatorParameters(parameterName))
                            ),
                            typeof(DerivedProduct)
                        ),
                        new EqualsBinaryOperatorParameters
                        (
                             new MemberSelectorOperatorParameters("ProductName", new ParameterOperatorParameters("p")),
                             new ConstantOperatorParameters("ProductName")
                        ),
                        "p"
                    )
                );
        }

        [Fact]
        public void NSCast_OnEntityCollection_CanAccessDerivedInstanceProperty()
        {
            //act
            var filter = CreateFilter<Product>();
            bool result1 = RunFilter(filter, new Product { Category = new Category { Products = new Product[] { new DerivedProduct { DerivedProductName = "DerivedProductName" } } } });
            bool result2 = RunFilter(filter, new Product { Category = new Category { Products = new Product[] { new DerivedProduct { DerivedProductName = "NotDerivedProductName" } } } });

            //assert
            Assert.True(result1);
            Assert.False(result2);

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    new AnyOperatorParameters
                    (
                        new CollectionCastOperatorParameters
                        (
                            new MemberSelectorOperatorParameters
                            (
                                "Products",
                                new MemberSelectorOperatorParameters("Category", new ParameterOperatorParameters(parameterName))
                            ),
                            typeof(DerivedProduct)
                        ),
                        new EqualsBinaryOperatorParameters
                        (
                             new MemberSelectorOperatorParameters("DerivedProductName", new ParameterOperatorParameters("p")),
                             new ConstantOperatorParameters("DerivedProductName")
                        ),
                        "p"
                    )
                );
        }

        [Fact]
        public void NSCast_OnSingleEntity_GeneratesExpression_WithAsOperatorParameter()
        {
            //act
            var filter = CreateFilter<DerivedProduct>();

            //assert
            AssertFilterStringIsCorrect(filter, "$it => (($it As Product).ProductName == \"ProductName\")");

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    new EqualsBinaryOperatorParameters
                    (
                        new MemberSelectorOperatorParameters
                        (
                            "ProductName",
                            new CastOperatorParameters
                            (
                                new ParameterOperatorParameters(parameterName),
                                typeof(Product)
                            )
                        ),
                        new ConstantOperatorParameters("ProductName")
                    )
                );
        }

        public static List<object[]> Inheritance_WithDerivedInstance_Data
            => new List<object[]>
            {
                new object[]
                {
                    new EqualsBinaryOperatorParameters
                    (
                        new MemberSelectorOperatorParameters
                        (
                            "ProductName",
                            new CastOperatorParameters
                            (
                                new ParameterOperatorParameters(parameterName),
                                typeof(Product)
                            )
                        ),
                        new ConstantOperatorParameters("ProductName")
                    )
                },
                new object[]
                {
                    new EqualsBinaryOperatorParameters
                    (
                        new MemberSelectorOperatorParameters
                        (
                            "DerivedProductName",
                            new CastOperatorParameters
                            (
                                new ParameterOperatorParameters(parameterName),
                                typeof(DerivedProduct)
                            )
                        ),
                        new ConstantOperatorParameters("DerivedProductName")
                    )
                },
                new object[]
                {
                    new EqualsBinaryOperatorParameters
                    (
                        new MemberSelectorOperatorParameters
                        (
                            "CategoryID",
                            new MemberSelectorOperatorParameters
                            (
                                "Category",
                                new CastOperatorParameters
                                (
                                    new ParameterOperatorParameters(parameterName),
                                    typeof(DerivedProduct)
                                )
                            )
                        ),
                        new ConstantOperatorParameters(123)
                    )
                },
                new object[]
                {
                    new EqualsBinaryOperatorParameters
                    (
                        new MemberSelectorOperatorParameters
                        (
                            "CategoryID",
                            new CastOperatorParameters
                            (
                                new MemberSelectorOperatorParameters
                                (
                                    "Category",
                                    new CastOperatorParameters
                                    (
                                        new ParameterOperatorParameters(parameterName),
                                        typeof(DerivedProduct)
                                    )
                                ),
                                typeof(DerivedCategory)
                            )
                        ),
                        new ConstantOperatorParameters(123)
                    )
                },
            };

        [Theory]
        [MemberData(nameof(Inheritance_WithDerivedInstance_Data))]
        public void Inheritance_WithDerivedInstance(IExpressionParameter filterBody)
        {
            //act
            var filter = CreateFilter<DerivedProduct>();
            bool result = RunFilter(filter, new DerivedProduct { Category = new DerivedCategory { CategoryID = 123 }, ProductName = "ProductName", DerivedProductName = "DerivedProductName" });

            //assert
            Assert.True(result);

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    filterBody
                );
        }

        public static List<object[]> Inheritance_WithBaseInstance_Data
            => new List<object[]>
            {
                new object[]
                {
                    new EqualsBinaryOperatorParameters
                    (
                        new MemberSelectorOperatorParameters
                        (
                            "DerivedProductName",
                            new CastOperatorParameters
                            (
                                new ParameterOperatorParameters(parameterName),
                                typeof(DerivedProduct)
                            )
                        ),
                        new ConstantOperatorParameters("DerivedProductName")
                    )
                },
                new object[]
                {
                    new EqualsBinaryOperatorParameters
                    (
                        new MemberSelectorOperatorParameters
                        (
                            "CategoryID",
                            new MemberSelectorOperatorParameters
                            (
                                "Category",
                                new CastOperatorParameters
                                (
                                    new ParameterOperatorParameters(parameterName),
                                    typeof(DerivedProduct)
                                )
                            )
                        ),
                        new ConstantOperatorParameters(123)
                    )
                },
                new object[]
                {
                    new EqualsBinaryOperatorParameters
                    (
                        new MemberSelectorOperatorParameters
                        (
                            "CategoryID",
                            new CastOperatorParameters
                            (
                                new MemberSelectorOperatorParameters
                                (
                                    "Category",
                                    new CastOperatorParameters
                                    (
                                        new ParameterOperatorParameters(parameterName),
                                        typeof(DerivedProduct)
                                    )
                                ),
                                typeof(DerivedCategory)
                            )
                        ),
                        new ConstantOperatorParameters(123)
                    )
                },
            };

        [Theory]
        [MemberData(nameof(Inheritance_WithBaseInstance_Data))]
        public void Inheritance_WithBaseInstance(IExpressionParameter filterBody)
        {
            //act
            var filter = CreateFilter<Product>();

            //assert
            Assert.Throws<NullReferenceException>(() => RunFilter(filter, new Product()));

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    filterBody
                );
        }

        public static List<object[]> CastMethod_Succeeds_Data
            => new List<object[]>
            {
                new object[]
                {
                    new EqualsBinaryOperatorParameters
                    (
                        new ConstantOperatorParameters(null),
                        new ConstantOperatorParameters(null)
                    ),
                    "$it => (null == null)"
                },
                new object[]
                {
                    new EqualsBinaryOperatorParameters
                    (
                        new ConstantOperatorParameters(null),
                        new ConstantOperatorParameters(123)
                    ),
                    "$it => (null == Convert(123))"
                },
                new object[]
                {
                    new NotEqualsBinaryOperatorParameters
                    (
                        new ConstantOperatorParameters(null),
                        new ConstantOperatorParameters(123)
                    ),
                    "$it => (null != Convert(123))"
                },
                new object[]
                {
                    new NotEqualsBinaryOperatorParameters
                    (
                        new ConstantOperatorParameters(null),
                        new ConstantOperatorParameters(true)
                    ),
                    "$it => (null != Convert(True))"
                },
                new object[]
                {
                    new NotEqualsBinaryOperatorParameters
                    (
                        new ConstantOperatorParameters(null),
                        new ConstantOperatorParameters(1)
                    ),
                    "$it => (null != Convert(1))"
                },
                new object[]
                {
                    new EqualsBinaryOperatorParameters
                    (
                        new ConstantOperatorParameters(null),
                        new ConstantOperatorParameters(new Guid())
                    ),
                    "$it => (null == Convert(00000000-0000-0000-0000-000000000000))"
                },
                new object[]
                {
                    new NotEqualsBinaryOperatorParameters
                    (
                        new ConstantOperatorParameters(null),
                        new ConstantOperatorParameters("123")
                    ),
                    "$it => (null != \"123\")"
                },
                new object[]
                {
                    new EqualsBinaryOperatorParameters
                    (
                        new ConstantOperatorParameters(null),
                        new ConstantOperatorParameters(new DateTimeOffset(new DateTime(2001, 1, 1, 12, 0, 0), new TimeSpan(8, 0, 0)))
                    ),
                    "$it => (null == Convert(01/01/2001 12:00:00 +08:00))"
                },
                new object[]
                {
                    new EqualsBinaryOperatorParameters
                    (
                        new ConstantOperatorParameters(null),
                        new ConstantOperatorParameters(new TimeSpan(7775999999000))
                    ),
                    "$it => (null == Convert(8.23:59:59.9999000))"
                },
                new object[]
                {
                    new EqualsBinaryOperatorParameters
                    (
                        new ConvertToStringOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("IntProp", new ParameterOperatorParameters(parameterName))
                        ),
                        new ConstantOperatorParameters("123")
                    ),
                    "$it => ($it.IntProp.ToString() == \"123\")"
                },
                new object[]
                {
                    new EqualsBinaryOperatorParameters
                    (
                        new ConvertToStringOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("LongProp", new ParameterOperatorParameters(parameterName))
                        ),
                        new ConstantOperatorParameters("123")
                    ),
                    "$it => ($it.LongProp.ToString() == \"123\")"
                },
                new object[]
                {
                    new EqualsBinaryOperatorParameters
                    (
                        new ConvertToStringOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("SingleProp", new ParameterOperatorParameters(parameterName))
                        ),
                        new ConstantOperatorParameters("123")
                    ),
                    "$it => ($it.SingleProp.ToString() == \"123\")"
                },
                new object[]
                {
                    new EqualsBinaryOperatorParameters
                    (
                        new ConvertToStringOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("DoubleProp", new ParameterOperatorParameters(parameterName))
                        ),
                        new ConstantOperatorParameters("123")
                    ),
                    "$it => ($it.DoubleProp.ToString() == \"123\")"
                },
                new object[]
                {
                    new EqualsBinaryOperatorParameters
                    (
                        new ConvertToStringOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("DecimalProp", new ParameterOperatorParameters(parameterName))
                        ),
                        new ConstantOperatorParameters("123")
                    ),
                    "$it => ($it.DecimalProp.ToString() == \"123\")"
                },
                new object[]
                {
                    new EqualsBinaryOperatorParameters
                    (
                        new ConvertToStringOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("BoolProp", new ParameterOperatorParameters(parameterName))
                        ),
                        new ConstantOperatorParameters("123")
                    ),
                    "$it => ($it.BoolProp.ToString() == \"123\")"
                },
                new object[]
                {
                    new EqualsBinaryOperatorParameters
                    (
                        new ConvertToStringOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("ByteProp", new ParameterOperatorParameters(parameterName))
                        ),
                        new ConstantOperatorParameters("123")
                    ),
                    "$it => ($it.ByteProp.ToString() == \"123\")"
                },
                new object[]
                {
                    new EqualsBinaryOperatorParameters
                    (
                        new ConvertToStringOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("GuidProp", new ParameterOperatorParameters(parameterName))
                        ),
                        new ConstantOperatorParameters("123")
                    ),
                    "$it => ($it.GuidProp.ToString() == \"123\")"
                },
                new object[]
                {
                    new EqualsBinaryOperatorParameters
                    (
                        new MemberSelectorOperatorParameters("StringProp", new ParameterOperatorParameters(parameterName)),
                        new ConstantOperatorParameters("123")
                    ),
                    "$it => ($it.StringProp == \"123\")"
                },
                new object[]
                {
                    new EqualsBinaryOperatorParameters
                    (
                        new ConvertToStringOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("DateTimeOffsetProp", new ParameterOperatorParameters(parameterName))
                        ),
                        new ConstantOperatorParameters("123")
                    ),
                    "$it => ($it.DateTimeOffsetProp.ToString() == \"123\")"
                },
                new object[]
                {
                    new EqualsBinaryOperatorParameters
                    (
                        new ConvertToStringOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("TimeSpanProp", new ParameterOperatorParameters(parameterName))
                        ),
                        new ConstantOperatorParameters("123")
                    ),
                    "$it => ($it.TimeSpanProp.ToString() == \"123\")"
                },
                new object[]
                {
                    new EqualsBinaryOperatorParameters
                    (
                        new ConvertToStringOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("SimpleEnumProp", new ParameterOperatorParameters(parameterName))
                        ),
                        new ConstantOperatorParameters("123")
                    ),
                    "$it => (Convert($it.SimpleEnumProp).ToString() == \"123\")"
                },
                new object[]
                {
                    new EqualsBinaryOperatorParameters
                    (
                        new ConvertToStringOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("FlagsEnumProp", new ParameterOperatorParameters(parameterName))
                        ),
                        new ConstantOperatorParameters("123")
                    ),
                    "$it => (Convert($it.FlagsEnumProp).ToString() == \"123\")"
                },
                new object[]
                {
                    new EqualsBinaryOperatorParameters
                    (
                        new ConvertToStringOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("LongEnumProp", new ParameterOperatorParameters(parameterName))
                        ),
                        new ConstantOperatorParameters("123")
                    ),
                    "$it => (Convert($it.LongEnumProp).ToString() == \"123\")"
                },
                new object[]
                {
                    new EqualsBinaryOperatorParameters
                    (
                        new ConvertToStringOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("NullableIntProp", new ParameterOperatorParameters(parameterName))
                        ),
                        new ConstantOperatorParameters("123")
                    ),
                    "$it => (IIF($it.NullableIntProp.HasValue, $it.NullableIntProp.Value.ToString(), null) == \"123\")"
                },
                new object[]
                {
                    new EqualsBinaryOperatorParameters
                    (
                        new ConvertToStringOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("NullableLongProp", new ParameterOperatorParameters(parameterName))
                        ),
                        new ConstantOperatorParameters("123")
                    ),
                    "$it => (IIF($it.NullableLongProp.HasValue, $it.NullableLongProp.Value.ToString(), null) == \"123\")"
                },
                new object[]
                {
                    new EqualsBinaryOperatorParameters
                    (
                        new ConvertToStringOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("NullableSingleProp", new ParameterOperatorParameters(parameterName))
                        ),
                        new ConstantOperatorParameters("123")
                    ),
                    "$it => (IIF($it.NullableSingleProp.HasValue, $it.NullableSingleProp.Value.ToString(), null) == \"123\")"
                },
                new object[]
                {
                    new EqualsBinaryOperatorParameters
                    (
                        new ConvertToStringOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("NullableDoubleProp", new ParameterOperatorParameters(parameterName))
                        ),
                        new ConstantOperatorParameters("123")
                    ),
                    "$it => (IIF($it.NullableDoubleProp.HasValue, $it.NullableDoubleProp.Value.ToString(), null) == \"123\")"
                },
                new object[]
                {
                    new EqualsBinaryOperatorParameters
                    (
                        new ConvertToStringOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("NullableDecimalProp", new ParameterOperatorParameters(parameterName))
                        ),
                        new ConstantOperatorParameters("123")
                    ),
                    "$it => (IIF($it.NullableDecimalProp.HasValue, $it.NullableDecimalProp.Value.ToString(), null) == \"123\")"
                },
                new object[]
                {
                    new EqualsBinaryOperatorParameters
                    (
                        new ConvertToStringOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("NullableBoolProp", new ParameterOperatorParameters(parameterName))
                        ),
                        new ConstantOperatorParameters("123")
                    ),
                    "$it => (IIF($it.NullableBoolProp.HasValue, $it.NullableBoolProp.Value.ToString(), null) == \"123\")"
                },
                new object[]
                {
                    new EqualsBinaryOperatorParameters
                    (
                        new ConvertToStringOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("NullableByteProp", new ParameterOperatorParameters(parameterName))
                        ),
                        new ConstantOperatorParameters("123")
                    ),
                    "$it => (IIF($it.NullableByteProp.HasValue, $it.NullableByteProp.Value.ToString(), null) == \"123\")"
                },
                new object[]
                {
                    new EqualsBinaryOperatorParameters
                    (
                        new ConvertToStringOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("NullableGuidProp", new ParameterOperatorParameters(parameterName))
                        ),
                        new ConstantOperatorParameters("123")
                    ),
                    "$it => (IIF($it.NullableGuidProp.HasValue, $it.NullableGuidProp.Value.ToString(), null) == \"123\")"
                },
                new object[]
                {
                    new EqualsBinaryOperatorParameters
                    (
                        new ConvertToStringOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("NullableDateTimeOffsetProp", new ParameterOperatorParameters(parameterName))
                        ),
                        new ConstantOperatorParameters("123")
                    ),
                    "$it => (IIF($it.NullableDateTimeOffsetProp.HasValue, $it.NullableDateTimeOffsetProp.Value.ToString(), null) == \"123\")"
                },
                new object[]
                {
                    new EqualsBinaryOperatorParameters
                    (
                        new ConvertToStringOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("NullableTimeSpanProp", new ParameterOperatorParameters(parameterName))
                        ),
                        new ConstantOperatorParameters("123")
                    ),
                    "$it => (IIF($it.NullableTimeSpanProp.HasValue, $it.NullableTimeSpanProp.Value.ToString(), null) == \"123\")"
                },
                new object[]
                {
                    new EqualsBinaryOperatorParameters
                    (
                        new ConvertToStringOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("NullableSimpleEnumProp", new ParameterOperatorParameters(parameterName))
                        ),
                        new ConstantOperatorParameters("123")
                    ),
                    "$it => (IIF($it.NullableSimpleEnumProp.HasValue, Convert($it.NullableSimpleEnumProp.Value).ToString(), null) == \"123\")"
                },
                new object[]
                {
                    new EqualsBinaryOperatorParameters
                    (
                        new ConvertOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("IntProp", new ParameterOperatorParameters(parameterName)),
                            typeof(long)
                        ),
                        new ConstantOperatorParameters((long)123)
                    ),
                    "$it => (Convert($it.IntProp) == 123)"
                },
                new object[]
                {
                    new EqualsBinaryOperatorParameters
                    (
                        new ConvertOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("NullableLongProp", new ParameterOperatorParameters(parameterName)),
                            typeof(double)
                        ),
                        new ConstantOperatorParameters(1.23d)
                    ),
                    "$it => (Convert($it.NullableLongProp) == 1.23)"
                },
                new object[]
                {
                    new NotEqualsBinaryOperatorParameters
                    (
                        new ConvertOperatorParameters
                        (
                            new ConstantOperatorParameters(2147483647),
                            typeof(short)
                        ),
                        new ConstantOperatorParameters(null)
                    ),
                    "$it => (Convert(Convert(2147483647)) != null)"
                },
                new object[]
                {
                    new EqualsBinaryOperatorParameters
                    (
                        new ConvertToStringOperatorParameters
                        (
                            new ConstantOperatorParameters(SimpleEnum.Second, typeof(SimpleEnum))
                        ),
                        new ConstantOperatorParameters("1")
                    ),
                    "$it => (Convert(Second).ToString() == \"1\")"
                },
                new object[]
                {
                    new EqualsBinaryOperatorParameters
                    (
                        new ConvertToStringOperatorParameters
                        (
                            new ConvertOperatorParameters
                            (
                                new ConvertOperatorParameters
                                (
                                    new MemberSelectorOperatorParameters("IntProp", new ParameterOperatorParameters(parameterName)),
                                    typeof(long)
                                ),
                                typeof(short)
                            )
                        ),
                        new ConstantOperatorParameters("123")
                    ),
                    "$it => (Convert(Convert($it.IntProp)).ToString() == \"123\")"
                },
                new object[]
                {
                    new NotEqualsBinaryOperatorParameters
                    (
                        new ConvertToEnumOperatorParameters
                        (
                            "123",
                            typeof(SimpleEnum)
                        ),
                        new ConstantOperatorParameters(null)
                    ),
                    "$it => (Convert(123) != null)"
                }
            };

        [Theory]
        [MemberData(nameof(CastMethod_Succeeds_Data))]
        public void CastMethod_Succeeds(IExpressionParameter filterBody, string expectedResult)
        {
            //act
            var filter = CreateFilter<DataTypes>();

            //assert
            AssertFilterStringIsCorrect(filter, expectedResult);

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    filterBody
                );
        }
        #endregion Casts

        #region 'isof' in query option
        public static List<object[]> IsofMethod_Succeeds_Data
            => new List<object[]>
            {
                new object []
                {
                    new IsOfOperatorParameters
                    (
                        new ParameterOperatorParameters(parameterName),
                        typeof(short)
                    ),
                    "$it => IIF(($it Is System.Int16), True, False)"
                },
                new object []
                {
                    new IsOfOperatorParameters
                    (
                        new ParameterOperatorParameters(parameterName),
                        typeof(Product)
                    ),
                    "$it => IIF(($it Is Contoso.Bsl.Flow.Unit.Tests.Data.Product), True, False)"
                },
                new object []
                {
                    new IsOfOperatorParameters
                    (
                        new MemberSelectorOperatorParameters("ProductName", new ParameterOperatorParameters(parameterName)),
                        typeof(string)
                    ),
                    "$it => IIF(($it.ProductName Is System.String), True, False)"
                },
                new object []
                {
                    new IsOfOperatorParameters
                    (
                        new MemberSelectorOperatorParameters("Category", new ParameterOperatorParameters(parameterName)),
                        typeof(Category)
                    ),
                    "$it => IIF(($it.Category Is Contoso.Bsl.Flow.Unit.Tests.Data.Category), True, False)"
                },
                new object []
                {
                    new IsOfOperatorParameters
                    (
                        new MemberSelectorOperatorParameters("Category", new ParameterOperatorParameters(parameterName)),
                        typeof(DerivedCategory)
                    ),
                    "$it => IIF(($it.Category Is Contoso.Bsl.Flow.Unit.Tests.Data.DerivedCategory), True, False)"
                },
                new object []
                {
                    new IsOfOperatorParameters
                    (
                        new MemberSelectorOperatorParameters("Ranking", new ParameterOperatorParameters(parameterName)),
                        typeof(SimpleEnum)
                    ),
                    "$it => IIF(($it.Ranking Is Contoso.Bsl.Flow.Unit.Tests.Data.SimpleEnum), True, False)"
                },
            };

        [Theory]
        [MemberData(nameof(IsofMethod_Succeeds_Data))]
        public void IsofMethod_Succeeds(IExpressionParameter filterBody, string expectedExpression)
        {
            //act
            var filter = CreateFilter<Product>();

            //assert
            AssertFilterStringIsCorrect(filter, expectedExpression);

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    filterBody
                );
        }

        public static List<object[]> IsOfPrimitiveType_Succeeds_WithFalse_Data
            => new List<object[]>
            {
                new object []
                {
                    new IsOfOperatorParameters
                    (
                        new ConstantOperatorParameters(null),
                        typeof(byte[])
                    )
                },
                new object []
                {
                    new IsOfOperatorParameters
                    (
                        new ConstantOperatorParameters(null),
                        typeof(bool)
                    )
                },
                new object []
                {
                    new IsOfOperatorParameters
                    (
                        new ConstantOperatorParameters(null),
                        typeof(byte)
                    )
                },
                new object []
                {
                    new IsOfOperatorParameters
                    (
                        new ConstantOperatorParameters(null),
                        typeof(DateTimeOffset)
                    )
                },
                new object []
                {
                    new IsOfOperatorParameters
                    (
                        new ConstantOperatorParameters(null),
                        typeof(Decimal)
                    )
                },
                new object []
                {
                    new IsOfOperatorParameters
                    (
                        new ConstantOperatorParameters(null),
                        typeof(double)
                    )
                },
                new object []
                {
                    new IsOfOperatorParameters
                    (
                        new ConstantOperatorParameters(null),
                        typeof(TimeSpan)
                    )
                },
                new object []
                {
                    new IsOfOperatorParameters
                    (
                        new ConstantOperatorParameters(null),
                        typeof(Guid)
                    )
                },
                new object []
                {
                    new IsOfOperatorParameters
                    (
                        new ConstantOperatorParameters(null),
                        typeof(Int16)
                    )
                },
                new object []
                {
                    new IsOfOperatorParameters
                    (
                        new ConstantOperatorParameters(null),
                        typeof(Int32)
                    )
                },
                new object []
                {
                    new IsOfOperatorParameters
                    (
                        new ConstantOperatorParameters(null),
                        typeof(Int64)
                    )
                },
                new object []
                {
                    new IsOfOperatorParameters
                    (
                        new ConstantOperatorParameters(null),
                        typeof(sbyte)
                    )
                },
                new object []
                {
                    new IsOfOperatorParameters
                    (
                        new ConstantOperatorParameters(null),
                        typeof(Single)
                    )
                },
                new object []
                {
                    new IsOfOperatorParameters
                    (
                        new ConstantOperatorParameters(null),
                        typeof(System.IO.Stream)
                    )
                },
                new object []
                {
                    new IsOfOperatorParameters
                    (
                        new ConstantOperatorParameters(null),
                        typeof(string)
                    )
                },
                new object []
                {
                    new IsOfOperatorParameters
                    (
                        new ConstantOperatorParameters(null),
                        typeof(SimpleEnum)
                    )
                },
                new object []
                {
                    new IsOfOperatorParameters
                    (
                        new ConstantOperatorParameters(null),
                        typeof(FlagsEnum)
                    )
                },
                new object []
                {
                    new IsOfOperatorParameters
                    (
                        new MemberSelectorOperatorParameters("ByteArrayProp", new ParameterOperatorParameters(parameterName)),
                        typeof(byte[])
                    )
                },
                new object []
                {
                    new IsOfOperatorParameters
                    (
                        new MemberSelectorOperatorParameters("IntProp", new ParameterOperatorParameters(parameterName)),
                        typeof(SimpleEnum)
                    )
                },
                new object []
                {
                    new IsOfOperatorParameters
                    (
                        new MemberSelectorOperatorParameters("NullableShortProp", new ParameterOperatorParameters(parameterName)),
                        typeof(short)
                    )
                },
                new object []
                {
                    new IsOfOperatorParameters
                    (
                        new ParameterOperatorParameters(parameterName),
                        typeof(byte[])
                    )
                },
                new object []
                {
                    new IsOfOperatorParameters
                    (
                        new ParameterOperatorParameters(parameterName),
                        typeof(bool)
                    )
                },
                new object []
                {
                    new IsOfOperatorParameters
                    (
                        new ParameterOperatorParameters(parameterName),
                        typeof(byte)
                    )
                },
                new object []
                {
                    new IsOfOperatorParameters
                    (
                        new ParameterOperatorParameters(parameterName),
                        typeof(DateTimeOffset)
                    )
                },
                new object []
                {
                    new IsOfOperatorParameters
                    (
                        new ParameterOperatorParameters(parameterName),
                        typeof(Decimal)
                    )
                },
                new object []
                {
                    new IsOfOperatorParameters
                    (
                        new ParameterOperatorParameters(parameterName),
                        typeof(double)
                    )
                },
                new object []
                {
                    new IsOfOperatorParameters
                    (
                        new ParameterOperatorParameters(parameterName),
                        typeof(TimeSpan)
                    )
                },
                new object []
                {
                    new IsOfOperatorParameters
                    (
                        new ParameterOperatorParameters(parameterName),
                        typeof(Guid)
                    )
                },
                new object []
                {
                    new IsOfOperatorParameters
                    (
                        new ParameterOperatorParameters(parameterName),
                        typeof(Int16)
                    )
                },
                new object []
                {
                    new IsOfOperatorParameters
                    (
                        new ParameterOperatorParameters(parameterName),
                        typeof(Int32)
                    )
                },
                new object []
                {
                    new IsOfOperatorParameters
                    (
                        new ParameterOperatorParameters(parameterName),
                        typeof(Int64)
                    )
                },
                new object []
                {
                    new IsOfOperatorParameters
                    (
                        new ParameterOperatorParameters(parameterName),
                        typeof(sbyte)
                    )
                },
                new object []
                {
                    new IsOfOperatorParameters
                    (
                        new ParameterOperatorParameters(parameterName),
                        typeof(Single)
                    )
                },
                new object []
                {
                    new IsOfOperatorParameters
                    (
                        new ParameterOperatorParameters(parameterName),
                        typeof(System.IO.Stream)
                    )
                },
                new object []
                {
                    new IsOfOperatorParameters
                    (
                        new ParameterOperatorParameters(parameterName),
                        typeof(string)
                    )
                },
                new object []
                {
                    new IsOfOperatorParameters
                    (
                        new ParameterOperatorParameters(parameterName),
                        typeof(SimpleEnum)
                    )
                },
                new object []
                {
                    new IsOfOperatorParameters
                    (
                        new ParameterOperatorParameters(parameterName),
                        typeof(FlagsEnum)
                    )
                },
                new object []
                {
                    new IsOfOperatorParameters
                    (
                        new ConstantOperatorParameters(23),
                        typeof(byte)
                    )
                },
                new object []
                {
                    new IsOfOperatorParameters
                    (
                        new ConstantOperatorParameters(23),
                        typeof(decimal)
                    )
                },
                new object []
                {
                    new IsOfOperatorParameters
                    (
                        new ConstantOperatorParameters(23),
                        typeof(double)
                    )
                },
                new object []
                {
                    new IsOfOperatorParameters
                    (
                        new ConstantOperatorParameters(23),
                        typeof(short)
                    )
                },
                new object []
                {
                    new IsOfOperatorParameters
                    (
                        new ConstantOperatorParameters(23),
                        typeof(long)
                    )
                },
                new object []
                {
                    new IsOfOperatorParameters
                    (
                        new ConstantOperatorParameters(23),
                        typeof(sbyte)
                    )
                },
                new object []
                {
                    new IsOfOperatorParameters
                    (
                        new ConstantOperatorParameters(23),
                        typeof(float)
                    )
                },
                new object []
                {
                    new IsOfOperatorParameters
                    (
                        new ConstantOperatorParameters("hello"),
                        typeof(Stream)
                    )
                },
                new object []
                {
                    new IsOfOperatorParameters
                    (
                        new ConstantOperatorParameters(0),
                        typeof(FlagsEnum)
                    )
                },
                new object []
                {
                    new IsOfOperatorParameters
                    (
                        new ConstantOperatorParameters(0),
                        typeof(SimpleEnum)
                    )
                },
                new object []
                {
                    new IsOfOperatorParameters
                    (
                        new ConstantOperatorParameters("2001-01-01T12:00:00.000+08:00"),
                        typeof(DateTimeOffset)
                    )
                },
                new object []
                {
                    new IsOfOperatorParameters
                    (
                        new ConstantOperatorParameters("00000000-0000-0000-0000-000000000000"),
                        typeof(Guid)
                    )
                },
                new object []
                {
                    new IsOfOperatorParameters
                    (
                        new ConstantOperatorParameters("23"),
                        typeof(byte)
                    )
                },
                new object []
                {
                    new IsOfOperatorParameters
                    (
                        new ConstantOperatorParameters("23"),
                        typeof(short)
                    )
                },
                new object []
                {
                    new IsOfOperatorParameters
                    (
                        new ConstantOperatorParameters("23"),
                        typeof(int)
                    )
                },
                new object []
                {
                    new IsOfOperatorParameters
                    (
                        new ConstantOperatorParameters("false"),
                        typeof(bool)
                    )
                },
                new object []
                {
                    new IsOfOperatorParameters
                    (
                        new ConstantOperatorParameters("OData"),
                        typeof(byte[])
                    )
                },
                new object []
                {
                    new IsOfOperatorParameters
                    (
                        new ConstantOperatorParameters("PT12H'"),
                        typeof(TimeSpan)
                    )
                },
                new object []
                {
                    new IsOfOperatorParameters
                    (
                        new ConstantOperatorParameters(23),
                        typeof(string)
                    )
                },
                new object []
                {
                    new IsOfOperatorParameters
                    (
                        new ConstantOperatorParameters("0"),
                        typeof(FlagsEnum)
                    )
                },
                new object []
                {
                    new IsOfOperatorParameters
                    (
                        new ConstantOperatorParameters("0"),
                        typeof(SimpleEnum)
                    )
                }
            };

        [Theory]
        [MemberData(nameof(IsOfPrimitiveType_Succeeds_WithFalse_Data))]
        public void IsOfPrimitiveType_Succeeds_WithFalse(IExpressionParameter filterBody)
        {
            //arrange
            var model = new DataTypes();

            //act
            var filter = CreateFilter<DataTypes>();
            bool result = RunFilter(filter, model);

            //assert
            Assert.False(result);

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    filterBody
                );
        }

        public static List<object[]> IsOfQuotedNonPrimitiveType
            => new List<object[]>
            {
                new object []
                {
                    new IsOfOperatorParameters
                    (
                        new ParameterOperatorParameters(parameterName),
                        typeof(DerivedProduct)
                    )
                },
                new object []
                {
                    new IsOfOperatorParameters
                    (
                        new MemberSelectorOperatorParameters("SupplierAddress", new ParameterOperatorParameters(parameterName)),
                        typeof(Address)
                    )
                },
                new object []
                {
                    new IsOfOperatorParameters
                    (
                        new MemberSelectorOperatorParameters("Category", new ParameterOperatorParameters(parameterName)),
                        typeof(DerivedCategory)
                    )
                }
            };

        [Theory]
        [MemberData(nameof(IsOfQuotedNonPrimitiveType))]
        public void IsOfQuotedNonPrimitiveType_Succeeds(IExpressionParameter filterBody)
        {
            //arrange
            var model = new DerivedProduct
            {
                SupplierAddress = new Address { City = "Redmond", },
                Category = new DerivedCategory { DerivedCategoryName = "DerivedCategory" }
            };

            //act
            var filter = CreateFilter<Product>();
            bool result = RunFilter(filter, model);

            //assert
            Assert.True(result);

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    filterBody
                );
        }

        public static List<object[]> IsOfQuotedNonPrimitiveTypeWithNull_Succeeds_WithFalse_Data
            => new List<object[]>
            {
                new object []
                {
                    new IsOfOperatorParameters
                    (
                        new ConstantOperatorParameters(null),
                        typeof(Address)
                    )
                },
                new object []
                {
                    new IsOfOperatorParameters
                    (
                        new ConstantOperatorParameters(null),
                        typeof(DerivedCategory)
                    )
                }
            };

        [Theory]
        [MemberData(nameof(IsOfQuotedNonPrimitiveTypeWithNull_Succeeds_WithFalse_Data))]
        public void IsOfQuotedNonPrimitiveTypeWithNull_Succeeds_WithFalse(IExpressionParameter filterBody)
        {
            //arrange
            var model = new DerivedProduct
            {
                SupplierAddress = new Address { City = "Redmond", },
                Category = new DerivedCategory { DerivedCategoryName = "DerivedCategory" }
            };

            //act
            var filter = CreateFilter<Product>();
            bool result = RunFilter(filter, model);

            //assert
            Assert.False(result);

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    filterBody
                );
        }
        #endregion 'isof' in query option

        #region
        public static List<object[]> ByteArrayComparisons_Data
            => new List<object[]>
            {
                new object []
                {
                    new EqualsBinaryOperatorParameters
                    (
                        new MemberSelectorOperatorParameters("ByteArrayProp", new ParameterOperatorParameters(parameterName)),
                        new ConstantOperatorParameters(Convert.FromBase64String("I6v/"))
                    ),
                    "$it => ($it.ByteArrayProp == System.Byte[])",
                    true
                },
                new object []
                {
                    new NotEqualsBinaryOperatorParameters
                    (
                        new MemberSelectorOperatorParameters("ByteArrayProp", new ParameterOperatorParameters(parameterName)),
                        new ConstantOperatorParameters(Convert.FromBase64String("I6v/"))
                    ),
                    "$it => ($it.ByteArrayProp != System.Byte[])",
                    false
                },
                new object []
                {
                    new EqualsBinaryOperatorParameters
                    (
                        new ConstantOperatorParameters(Convert.FromBase64String("I6v/")),
                        new ConstantOperatorParameters(Convert.FromBase64String("I6v/"))
                    ),
                    "$it => (System.Byte[] == System.Byte[])",
                    true
                },
                new object []
                {
                    new NotEqualsBinaryOperatorParameters
                    (
                        new ConstantOperatorParameters(Convert.FromBase64String("I6v/")),
                        new ConstantOperatorParameters(Convert.FromBase64String("I6v/"))
                    ),
                    "$it => (System.Byte[] != System.Byte[])",
                    false
                },
                new object []
                {
                    new NotEqualsBinaryOperatorParameters
                    (
                        new MemberSelectorOperatorParameters("ByteArrayPropWithNullValue", new ParameterOperatorParameters(parameterName)),
                        new ConstantOperatorParameters(Convert.FromBase64String("I6v/"))
                    ),
                    "$it => ($it.ByteArrayPropWithNullValue != System.Byte[])",
                    true
                },
                new object []
                {
                    new NotEqualsBinaryOperatorParameters
                    (
                        new MemberSelectorOperatorParameters("ByteArrayPropWithNullValue", new ParameterOperatorParameters(parameterName)),
                        new MemberSelectorOperatorParameters("ByteArrayPropWithNullValue", new ParameterOperatorParameters(parameterName))
                    ),
                    "$it => ($it.ByteArrayPropWithNullValue != $it.ByteArrayPropWithNullValue)",
                    false
                },
                new object []
                {
                    new NotEqualsBinaryOperatorParameters
                    (
                        new MemberSelectorOperatorParameters("ByteArrayPropWithNullValue", new ParameterOperatorParameters(parameterName)),
                        new ConstantOperatorParameters(null)
                    ),
                    "$it => ($it.ByteArrayPropWithNullValue != null)",
                    false
                },
                new object []
                {
                    new EqualsBinaryOperatorParameters
                    (
                        new MemberSelectorOperatorParameters("ByteArrayPropWithNullValue", new ParameterOperatorParameters(parameterName)),
                        new ConstantOperatorParameters(null)
                    ),
                    "$it => ($it.ByteArrayPropWithNullValue == null)",
                    true
                },
                new object []
                {
                    new NotEqualsBinaryOperatorParameters
                    (
                        new ConstantOperatorParameters(null),
                        new MemberSelectorOperatorParameters("ByteArrayPropWithNullValue", new ParameterOperatorParameters(parameterName))
                    ),
                    "$it => (null != $it.ByteArrayPropWithNullValue)",
                    false
                },
                new object []
                {
                    new EqualsBinaryOperatorParameters
                    (
                        new ConstantOperatorParameters(null),
                        new MemberSelectorOperatorParameters("ByteArrayPropWithNullValue", new ParameterOperatorParameters(parameterName))
                    ),
                    "$it => (null == $it.ByteArrayPropWithNullValue)",
                    true
                },
            };

        [Theory]
        [MemberData(nameof(ByteArrayComparisons_Data))]
        public void ByteArrayComparisons(IExpressionParameter filterBody, string expectedExpression, bool expected)
        {
            //act
            var filter = CreateFilter<DataTypes>();
            bool result = RunFilter
            (
                filter,
                new DataTypes
                {
                    ByteArrayProp = new byte[] { 35, 171, 255 }
                }
            );

            //assert
            Assert.Equal(expected, result);
            AssertFilterStringIsCorrect(filter, expectedExpression);

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    filterBody
                );
        }

        public static List<object[]> DisAllowed_ByteArrayComparisons_Data
            => new List<object[]>
            {
                new object []
                {
                    new GreaterThanOrEqualsBinaryOperatorParameters
                    (
                        new ConstantOperatorParameters(Convert.FromBase64String("AP8Q")),
                        new ConstantOperatorParameters(Convert.FromBase64String("AP8Q"))
                    )
                },
                new object []
                {
                    new LessThanOrEqualsBinaryOperatorParameters
                    (
                        new ConstantOperatorParameters(Convert.FromBase64String("AP8Q")),
                        new ConstantOperatorParameters(Convert.FromBase64String("AP8Q"))
                    )
                },
                new object []
                {
                    new LessThanBinaryOperatorParameters
                    (
                        new ConstantOperatorParameters(Convert.FromBase64String("AP8Q")),
                        new ConstantOperatorParameters(Convert.FromBase64String("AP8Q"))
                    )
                },
                new object []
                {
                    new GreaterThanBinaryOperatorParameters
                    (
                        new ConstantOperatorParameters(Convert.FromBase64String("AP8Q")),
                        new ConstantOperatorParameters(Convert.FromBase64String("AP8Q"))
                    )
                },
            };

        [Theory]
        [MemberData(nameof(DisAllowed_ByteArrayComparisons_Data))]
        public void DisAllowed_ByteArrayComparisons(IExpressionParameter filterBody)
        {
            //assert
            Assert.Throws<InvalidOperationException>(() => CreateFilter<DataTypes>());
            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    filterBody
                );
        }

        public static List<object[]> Nullable_NonstandardEdmPrimitives_Data
            => new List<object[]>
            {
                new object []
                {
                    new EqualsBinaryOperatorParameters
                    (
                        new ConvertOperatorParameters
                        (
                            new ConvertToNullableUnderlyingValueOperatorParameters
                            (
                                new MemberSelectorOperatorParameters("NullableUShortProp", new ParameterOperatorParameters(parameterName))
                            ),
                            typeof(int?)
                        ),
                        new ConstantOperatorParameters(12)
                    ),
                    "$it => (Convert($it.NullableUShortProp.Value) == Convert(12))"
                },
                new object []
                {
                    new EqualsBinaryOperatorParameters
                    (
                        new ConvertOperatorParameters
                        (
                            new ConvertToNullableUnderlyingValueOperatorParameters
                            (
                                new MemberSelectorOperatorParameters("NullableULongProp", new ParameterOperatorParameters(parameterName))
                            ),
                            typeof(long?)
                        ),
                        new ConstantOperatorParameters(12L)
                    ),
                    "$it => (Convert($it.NullableULongProp.Value) == Convert(12))"
                },
                new object []
                {
                    new EqualsBinaryOperatorParameters
                    (
                        new ConvertOperatorParameters
                        (
                            new ConvertToNullableUnderlyingValueOperatorParameters
                            (
                                new MemberSelectorOperatorParameters("NullableUIntProp", new ParameterOperatorParameters(parameterName))
                            ),
                            typeof(int?)
                        ),
                        new ConstantOperatorParameters(12)
                    ),
                    "$it => (Convert($it.NullableUIntProp.Value) == Convert(12))"
                },
                new object []
                {
                    new EqualsBinaryOperatorParameters
                    (
                        new ConvertToStringOperatorParameters
                        (
                            new ConvertToNullableUnderlyingValueOperatorParameters
                            (
                                new MemberSelectorOperatorParameters("NullableCharProp", new ParameterOperatorParameters(parameterName))
                            )
                        ),
                        new ConstantOperatorParameters("a")
                    ),
                    "$it => ($it.NullableCharProp.Value.ToString() == \"a\")"
                },
            };

        [Theory]
        [MemberData(nameof(Nullable_NonstandardEdmPrimitives_Data))]
        public void Nullable_NonstandardEdmPrimitives(IExpressionParameter filterBody, string expectedExpression)
        {
            //act
            var filter = CreateFilter<DataTypes>();

            //assert
            AssertFilterStringIsCorrect(filter, expectedExpression);
            Assert.Throws<InvalidOperationException>(() => RunFilter(filter, new DataTypes()));

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    filterBody
                );
        }

        public static List<object[]> InOnNavigation_Data
            => new List<object[]>
                {
                    new object []
                    {
                        new InOperatorParameters
                        (
                            new MemberSelectorOperatorParameters
                            (
                                "ProductID",
                                new MemberSelectorOperatorParameters
                                (
                                    "Product",
                                    new MemberSelectorOperatorParameters("Category", new ParameterOperatorParameters(parameterName))
                                )
                            ),
                            new CollectionConstantOperatorParameters
                            (
                                new List<object>{ 1 },
                                typeof(int)
                            )
                        ),
                        "$it => System.Collections.Generic.List`1[System.Int32].Contains($it.Category.Product.ProductID)"
                    },
                    new object []
                    {
                        new InOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("Category.Product.ProductID", new ParameterOperatorParameters(parameterName)),
                            new CollectionConstantOperatorParameters
                            (
                                new List<object>{ 1 },
                                typeof(int)
                            )
                        ),
                        "$it => System.Collections.Generic.List`1[System.Int32].Contains($it.Category.Product.ProductID)"
                    },
                    new object []
                    {
                        new InOperatorParameters
                        (
                            new MemberSelectorOperatorParameters
                            (
                                "GuidProperty",
                                new MemberSelectorOperatorParameters
                                (
                                    "Product",
                                    new MemberSelectorOperatorParameters("Category", new ParameterOperatorParameters(parameterName))
                                )
                            ),
                            new CollectionConstantOperatorParameters
                            (
                                new List<object>{ new Guid("dc75698b-581d-488b-9638-3e28dd51d8f7") },
                                typeof(Guid)
                            )
                        ),
                        "$it => System.Collections.Generic.List`1[System.Guid].Contains($it.Category.Product.GuidProperty)"
                    },
                    new object []
                    {
                        new InOperatorParameters
                        (
                            new MemberSelectorOperatorParameters
                            (
                                "NullableGuidProperty",
                                new MemberSelectorOperatorParameters
                                (
                                    "Product",
                                    new MemberSelectorOperatorParameters("Category", new ParameterOperatorParameters(parameterName))
                                )
                            ),
                            new CollectionConstantOperatorParameters
                            (
                                new List<object>{ new Guid("dc75698b-581d-488b-9638-3e28dd51d8f7") },
                                typeof(Guid?)
                            )
                        ),
                        "$it => System.Collections.Generic.List`1[System.Nullable`1[System.Guid]].Contains($it.Category.Product.NullableGuidProperty)"
                    }
                };

        [Theory]
        [MemberData(nameof(InOnNavigation_Data))]
        public void InOnNavigation(IExpressionParameter filterBody, string expectedExpression)
        {
            //act
            var filter = CreateFilter<Product>();

            //assert
            AssertFilterStringIsCorrect(filter, expectedExpression);

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    filterBody
                );
        }

        [Fact]
        public void MultipleConstants_Are_Parameterized()
        {
            //act
            var filter = CreateFilter<Product>();

            //assert
            AssertFilterStringIsCorrect(filter, "$it => (((($it.ProductName == \"1\") OrElse ($it.ProductName == \"2\")) OrElse ($it.ProductName == \"3\")) OrElse ($it.ProductName == \"4\"))");

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    new OrBinaryOperatorParameters
                    (
                        new OrBinaryOperatorParameters
                        (
                            new OrBinaryOperatorParameters
                            (
                                new EqualsBinaryOperatorParameters
                                (
                                    new MemberSelectorOperatorParameters("ProductName", new ParameterOperatorParameters(parameterName)),
                                    new ConstantOperatorParameters("1")
                                ),
                                new EqualsBinaryOperatorParameters
                                (
                                    new MemberSelectorOperatorParameters("ProductName", new ParameterOperatorParameters(parameterName)),
                                    new ConstantOperatorParameters("2")
                                )
                            ),
                            new EqualsBinaryOperatorParameters
                            (
                                new MemberSelectorOperatorParameters("ProductName", new ParameterOperatorParameters(parameterName)),
                                new ConstantOperatorParameters("3")
                            )
                        ),
                        new EqualsBinaryOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("ProductName", new ParameterOperatorParameters(parameterName)),
                            new ConstantOperatorParameters("4")
                        )
                    )
                );
        }

        [Fact]
        public void Constants_Are_Not_Parameterized_IfDisabled()
        {
            //act
            var filter = CreateFilter<Product>();

            //assert
            AssertFilterStringIsCorrect(filter, "$it => ($it.ProductName == \"1\")");

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    new EqualsBinaryOperatorParameters
                    (
                        new MemberSelectorOperatorParameters("ProductName", new ParameterOperatorParameters(parameterName)),
                        new ConstantOperatorParameters("1")
                    )
                );
        }

        [Fact]
        public void CollectionConstants_Are_Parameterized()
        {
            //act
            var filter = CreateFilter<Product>();

            //assert
            AssertFilterStringIsCorrect(filter, "$it => System.Collections.Generic.List`1[System.String].Contains($it.ProductName)");

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    new InOperatorParameters
                    (
                        new MemberSelectorOperatorParameters("ProductName", new ParameterOperatorParameters(parameterName)),
                        new CollectionConstantOperatorParameters
                        (
                            new List<object> { "Prod1", "Prod2" },
                            typeof(string)
                        )
                    )
                );
        }

        [Fact]
        public void CollectionConstants_OfEnums_Are_Not_Parameterized_If_Disabled()
        {
            //act
            var filter = CreateFilter<DataTypes>();

            //assert
            AssertFilterStringIsCorrect(filter, "$it => System.Collections.Generic.List`1[Contoso.Bsl.Flow.Unit.Tests.Data.SimpleEnum].Contains($it.SimpleEnumProp)");

            Expression<Func<T, bool>> CreateFilter<T>()
                => GetFilter<T>
                (
                    new InOperatorParameters
                    (
                        new MemberSelectorOperatorParameters("SimpleEnumProp", new ParameterOperatorParameters(parameterName)),
                        new CollectionConstantOperatorParameters
                        (
                            new List<object> { SimpleEnum.First, SimpleEnum.Second },
                            typeof(SimpleEnum)
                        )
                    )
                );
        }
        #endregion

        #region Helpers
        private Expression<Func<T, bool>> GetFilter<T>(IExpressionParameter filterBody)
            => CreateFilterLambdaExpression<T>
            (
                GetExpressionParameter<T>(filterBody)//Create IExpressionParameter for lambda expression e.g. $it => $it.Any()
            );

        /// <summary>
        /// Takes an object describing the body e.g. $it.Any() and returns an object describing the lambda expressiom e.g. $it => $it.Any()
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filterBody"></param>
        /// <param name="parameterName"></param>
        /// <returns></returns>
        private IExpressionParameter GetExpressionParameter<T>(IExpressionParameter filterBody, string parameterName = "$it")
            => new FilterLambdaOperatorParameters
            (
                filterBody,
                typeof(T),
                parameterName
            );

        /// <summary>
        /// Takes an object describing the lambda expressiom e.g. $it => $it.Any() and returns the lambda expression.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="completeLambda"></param>
        /// <returns></returns>
        private Expression<Func<T, bool>> CreateFilterLambdaExpression<T>(IExpressionParameter completeLambda)
        {
            IMapper mapper = serviceProvider.GetRequiredService<IMapper>();

            return (Expression<Func<T, bool>>)mapper.Map<FilterLambdaOperator>//map the complete lambda from decriptor object to operator object
            (
                mapper.Map<OperatorDescriptorBase>(completeLambda),//map the complete lambda from parameter object to decriptor object
                opts => opts.Items["parameters"] = GetParameters()
            ).Build();//create the lambda expression from the operator object
        }

        private bool RunFilter<TModel>(Expression<Func<TModel, bool>> filter, TModel instance)
            => filter.Compile().Invoke(instance);

        // Used by Custom Method binder tests - by reflection
#pragma warning disable IDE0051 // Remove unused private members
        private static string PadRightStatic(string str, int number)
#pragma warning restore IDE0051 // Remove unused private members
        {
            return str.PadRight(number);
        }

        private T? ToNullable<T>(object value) where T : struct =>
            value == null ? null : (T?)Convert.ChangeType(value, typeof(T));

        private static IDictionary<string, ParameterExpression> GetParameters()
            => new Dictionary<string, ParameterExpression>();

        private void AssertFilterStringIsCorrect(Expression expression, string expected)
        {
            string resultExpression = ExpressionStringBuilder.ToString(expression);
            Assert.True(expected == resultExpression, string.Format("Expected expression '{0}' but the deserializer produced '{1}'", expected, resultExpression));
        }

        static MapperConfiguration MapperConfiguration;
        private void Initialize()
        {
            if (MapperConfiguration == null)
            {
                MapperConfiguration = new MapperConfiguration(cfg =>
                {
                    cfg.AddExpressionMapping();
                    cfg.AddProfile<ParameterToDescriptorMappingProfile>();
                    cfg.AddProfile<DescriptorToOperatorMappingProfile>();
                });
            }

            MapperConfiguration.AssertConfigurationIsValid();

            serviceProvider = new ServiceCollection()
                .AddSingleton<AutoMapper.IConfigurationProvider>
                (
                    MapperConfiguration
                )
                .AddTransient<IMapper>(sp => new Mapper(sp.GetRequiredService<AutoMapper.IConfigurationProvider>(), sp.GetService))
                .BuildServiceProvider();
        }
        #endregion Helpers
    }

    public static class StringExtender
    {
        public static string PadRightExStatic(this string str, int width)
        {
            return str.PadRight(width);
        }
    }
}
