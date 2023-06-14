using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using Contoso.AutoMapperProfiles;
using Contoso.Common.Configuration.ExpressionDescriptors;
using Contoso.Bsl.Flow.Unit.Tests.Data;
using Contoso.Data.Entities;
using Contoso.Parameters.Expressions;
using LogicBuilder.Expressions.Utils;
using LogicBuilder.Expressions.Utils.ExpressionBuilder.Lambda;
using LogicBuilder.Expressions.Utils.Strutures;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Xunit;

namespace Contoso.Bsl.Flow.Unit.Tests
{
    public class QueryableExpressionTests
    {
        public QueryableExpressionTests()
        {
            Initialize();
        }

        #region Fields
        private IServiceProvider serviceProvider;
        private static readonly string parameterName = "$it";
        #endregion Fields

        #region Tests
        [Fact]
        public void BuildWhere_OrderBy_ThenBy_Skip_Take_Average()
        {
            //act: body = q.Where(s => ((s.ID > 1) AndAlso (s.FirstName.Compare(s.LastName) > 0))).OrderBy(v => v.LastName).ThenByDescending(v => v.FirstName).Skip(2).Take(3).Average(j => j.ID)
            var bodyParameter = new AverageOperatorParameters
            (
                new TakeOperatorParameters
                (
                    new SkipOperatorParameters
                    (
                        new ThenByOperatorParameters
                        (
                            new OrderByOperatorParameters
                            (
                                new WhereOperatorParameters
                                (//q.Where(s => ((s.ID > 1) AndAlso (Compare(s.FirstName, s.LastName) > 0)))
                                    new ParameterOperatorParameters("q"),//q. the source operand
                                    new AndBinaryOperatorParameters//((s.ID > 1) AndAlso (Compare(s.FirstName, s.LastName) > 0)
                                    (
                                        new GreaterThanBinaryOperatorParameters
                                        (
                                            new MemberSelectorOperatorParameters("Id", new ParameterOperatorParameters("s")),
                                            new ConstantOperatorParameters(1, typeof(int))
                                        ),
                                        new GreaterThanBinaryOperatorParameters
                                        (
                                            new MemberSelectorOperatorParameters("FirstName", new ParameterOperatorParameters("s")),
                                            new MemberSelectorOperatorParameters("LastName", new ParameterOperatorParameters("s"))
                                        )
                                    ),
                                    "s"//s => (created in Where operator.  The parameter type is based on the source operand underlying type in this case Student.)
                                ),
                                new MemberSelectorOperatorParameters("LastName", new ParameterOperatorParameters("v")),
                                ListSortDirection.Ascending,
                                "v"
                            ),
                            new MemberSelectorOperatorParameters("FirstName", new ParameterOperatorParameters("v")),
                            ListSortDirection.Descending,
                            "v"
                        ),
                        2
                    ),
                    3
                ),
                new MemberSelectorOperatorParameters("Id", new ParameterOperatorParameters("j")),
                "j"
            );

            //lambdaExpression q => q.Where...
            SelectorLambdaOperatorParameters expressionParameter = GetExpressionParameter<IQueryable<Student>, double>(bodyParameter, "q");

            Expression<Func<IQueryable<Student>, double>> expression = GetExpression<IQueryable<Student>, double>(expressionParameter);

            //assert
            AssertFilterStringIsCorrect(expression, "q => q.Where(s => ((s.ID > 1) AndAlso (s.FirstName.Compare(s.LastName) > 0))).OrderBy(v => v.LastName).ThenByDescending(v => v.FirstName).Skip(2).Take(3).Average(j => j.ID)");
        }

        [Fact]
        public void BuildGroupBy_OrderBy_ThenBy_Skip_Take_Average()
        {
            //arrange
            Expression<Func<IQueryable<Department>, IQueryable<object>>> expression1 =
                q => q.GroupBy(a => 1)
                    .OrderBy(b => b.Key)
                    .Select
                    (
                        c => new
                        {
                            Sum_budget = q.Where
                            (
                                d => ((d.DepartmentID == q.Count())
                                    && (d.DepartmentID == c.Key))
                            )
                            .ToList()
                        }
                    );

            //act
            var bodyParameter = new SelectOperatorParameters
            (
                new OrderByOperatorParameters
                (
                    new GroupByOperatorParameters
                    (
                        new ParameterOperatorParameters("q"),
                        new ConstantOperatorParameters(1, typeof(int)),
                        "a"
                    ),
                    new MemberSelectorOperatorParameters("Key", new ParameterOperatorParameters("b")),
                    ListSortDirection.Ascending,
                    "b"
                ),
                new MemberInitOperatorParameters
                (
                    new List<MemberBindingItem>
                    {
                        new MemberBindingItem
                        (
                            "Sum_budget",
                            new ToListOperatorParameters
                            (
                                new WhereOperatorParameters
                                (
                                    new ParameterOperatorParameters("q"),
                                    new AndBinaryOperatorParameters
                                    (
                                        new EqualsBinaryOperatorParameters
                                        (
                                            new MemberSelectorOperatorParameters("DepartmentID", new ParameterOperatorParameters("d")),
                                            new CountOperatorParameters(new ParameterOperatorParameters("q"))
                                        ),
                                        new EqualsBinaryOperatorParameters
                                        (
                                            new MemberSelectorOperatorParameters("DepartmentID", new ParameterOperatorParameters("d")),
                                            new MemberSelectorOperatorParameters("Key", new ParameterOperatorParameters("c"))
                                        )
                                    ),
                                    "d"
                                )
                            )
                        )
                    }
                ),
                "c"
            );

            //lambdaExpression q => q.Where...
            var expressionParameter = GetExpressionParameter<IQueryable<Department>, IQueryable<object>> (bodyParameter, "q");

            Expression<Func<IQueryable<Department>, IQueryable<object>>> expression = GetExpression<IQueryable<Department>, IQueryable<object>>(expressionParameter);

            //assert
            AssertFilterStringIsCorrect(expression, "q => Convert(q.GroupBy(a => 1).OrderBy(b => b.Key).Select(c => new AnonymousType() {Sum_budget = q.Where(d => ((d.DepartmentID == q.Count()) AndAlso (d.DepartmentID == c.Key))).ToList()}))");
            Assert.NotNull(expression);
        }

        [Fact]
        public void BuildGroupBy_AsQueryable_OrderBy_Select_FirstOrDefault()
        {
            //arrange
            Expression<Func<IQueryable<Department>, object>> expression1 =
                q => q.GroupBy(item => 1)
                .AsQueryable()
                .OrderBy(group => group.Key)
                .Select
                (
                    sel => new
                    {
                        Min_administratorName = q.Where(d => (1 == sel.Key)).Min(item => string.Concat(string.Concat(item.Administrator.LastName, " "), item.Administrator.FirstName)),
                        Count_name = q.Where(d => (1 == sel.Key)).Count(),
                        Sum_budget = q.Where(d => (1 == sel.Key)).Sum(item => item.Budget),
                        Min_budget = q.Where(d => (1 == sel.Key)).Min(item => item.Budget),
                        Min_startDate = q.Where(d => (1 == sel.Key)).Min(item => item.StartDate)
                    }
                )
                .FirstOrDefault();

            //act
            var bodyParameter = new FirstOrDefaultOperatorParameters
            (
                new SelectOperatorParameters
                (
                    new OrderByOperatorParameters
                    (
                        new AsQueryableOperatorParameters
                        (
                            new GroupByOperatorParameters
                            (
                                new ParameterOperatorParameters("q"),
                                new ConstantOperatorParameters(1, typeof(int)),
                                "item"
                            )
                        ),
                        new MemberSelectorOperatorParameters("Key", new ParameterOperatorParameters("group")),
                        ListSortDirection.Ascending,
                        "group"
                    ),
                    new MemberInitOperatorParameters
                    (
                        new List<MemberBindingItem>
                        {
                            new MemberBindingItem
                            (
                                "Min_administratorName",
                                new MinOperatorParameters
                                (
                                    new WhereOperatorParameters
                                    (
                                        new ParameterOperatorParameters("q"),
                                        new EqualsBinaryOperatorParameters
                                        (
                                            new ConstantOperatorParameters(1, typeof(int)),
                                            new MemberSelectorOperatorParameters("Key", new ParameterOperatorParameters("sel"))
                                        ),
                                        "d"
                                    ),
                                    new ConcatOperatorParameters
                                    (
                                        new ConcatOperatorParameters
                                        (
                                            new MemberSelectorOperatorParameters("Administrator.LastName", new ParameterOperatorParameters("item")),
                                            new ConstantOperatorParameters(" ", typeof(string))
                                        ),
                                        new MemberSelectorOperatorParameters("Administrator.FirstName", new ParameterOperatorParameters("item"))
                                    ),
                                    "item"
                                )
                            ),
                            new MemberBindingItem
                            (
                                "Count_name",
                                new CountOperatorParameters
                                (
                                    new WhereOperatorParameters
                                    (
                                        new ParameterOperatorParameters("q"),
                                        new EqualsBinaryOperatorParameters
                                        (
                                            new ConstantOperatorParameters(1, typeof(int)),
                                            new MemberSelectorOperatorParameters("Key", new ParameterOperatorParameters("sel"))
                                        ),
                                        "d"
                                    )
                                )
                            ),
                            new MemberBindingItem
                            (
                                "Sum_budget",
                                new SumOperatorParameters
                                (
                                    new WhereOperatorParameters
                                    (
                                        new ParameterOperatorParameters("q"),
                                        new EqualsBinaryOperatorParameters
                                        (
                                            new ConstantOperatorParameters(1, typeof(int)),
                                            new MemberSelectorOperatorParameters("Key", new ParameterOperatorParameters("sel"))
                                        ),
                                        "d"
                                    ),
                                    new MemberSelectorOperatorParameters("Budget", new ParameterOperatorParameters("item")),
                                    "item"
                                )
                            ),
                            new MemberBindingItem
                            (
                                "Min_budget",
                                new MinOperatorParameters
                                (
                                    new WhereOperatorParameters
                                    (
                                        new ParameterOperatorParameters("q"),
                                        new EqualsBinaryOperatorParameters
                                        (
                                            new ConstantOperatorParameters(1, typeof(int)),
                                            new MemberSelectorOperatorParameters("Key", new ParameterOperatorParameters("sel"))
                                        ),
                                        "d"
                                    ),
                                    new MemberSelectorOperatorParameters("Budget", new ParameterOperatorParameters("item")),
                                    "item"
                                )
                            ),
                            new MemberBindingItem
                            (
                                "Min_startDate",
                                new MinOperatorParameters
                                (
                                    new WhereOperatorParameters
                                    (
                                        new ParameterOperatorParameters("q"),
                                        new EqualsBinaryOperatorParameters
                                        (
                                            new ConstantOperatorParameters(1, typeof(int)),
                                            new MemberSelectorOperatorParameters("Key", new ParameterOperatorParameters("sel"))
                                        ),
                                        "d"
                                    ),
                                    new MemberSelectorOperatorParameters("StartDate", new ParameterOperatorParameters("item")),
                                    "item"
                                )
                            )
                        }
                    ),
                    "sel"
                )
            );

            //lambdaExpression q => q.Where...
            var expressionParameter = GetExpressionParameter<IQueryable<Department>, object>(bodyParameter, "q");

            Expression<Func<IQueryable<Department>, object>> expression = GetExpression<IQueryable<Department>, object>(expressionParameter);

            //assert
            AssertFilterStringIsCorrect(expression, "q => Convert(q.GroupBy(item => 1).AsQueryable().OrderBy(group => group.Key).Select(sel => new AnonymousType() {Min_administratorName = q.Where(d => (1 == sel.Key)).Min(item => item.Administrator.LastName.Concat(\" \").Concat(item.Administrator.FirstName)), Count_name = q.Where(d => (1 == sel.Key)).Count(), Sum_budget = q.Where(d => (1 == sel.Key)).Sum(item => item.Budget), Min_budget = q.Where(d => (1 == sel.Key)).Min(item => item.Budget), Min_startDate = q.Where(d => (1 == sel.Key)).Min(item => item.StartDate)}).FirstOrDefault())");
            Assert.NotNull(expression);
        }

        [Fact]
        public void All_Filter()
        {
            //act
            var expression = CreateExpression<IQueryable<Category>, bool>();
            var result = RunExpression(expression, GetCategories());

            //assert
            AssertFilterStringIsCorrect(expression, "$it => $it.All(a => ((a.CategoryName == \"CategoryOne\") OrElse (a.CategoryName == \"CategoryTwo\")))");
            Assert.True(result);

            Expression<Func<T, TReturn>> CreateExpression<T, TReturn>() 
                => GetExpression<T, TReturn>
                (
                    GetExpressionParameter<T, TReturn>
                    (
                        new AllOperatorParameters
                        (
                            new ParameterOperatorParameters(parameterName),
                            new OrBinaryOperatorParameters
                            (
                                new EqualsBinaryOperatorParameters
                                (
                                    new MemberSelectorOperatorParameters("CategoryName", new ParameterOperatorParameters("a")),
                                    new ConstantOperatorParameters("CategoryOne")
                                ),
                                new EqualsBinaryOperatorParameters
                                (
                                    new MemberSelectorOperatorParameters("CategoryName", new ParameterOperatorParameters("a")),
                                    new ConstantOperatorParameters("CategoryTwo")
                                )
                            ),
                            "a"
                        ),
                        parameterName
                    )
                );
        }

        [Fact]
        public void Any_Filter()
        {
            //act
            var expression = CreateExpression<IQueryable<Category>, bool>();
            var result = RunExpression(expression, GetCategories());

            //assert
            AssertFilterStringIsCorrect(expression, "$it => $it.Any(a => (a.CategoryName == \"CategoryOne\"))");
            Assert.True(result);

            Expression<Func<T, TReturn>> CreateExpression<T, TReturn>() 
                => GetExpression<T, TReturn>
                (
                    GetExpressionParameter<T, TReturn>
                    (
                        new AnyOperatorParameters
                        (
                            new ParameterOperatorParameters(parameterName),
                            new EqualsBinaryOperatorParameters
                            (
                                new MemberSelectorOperatorParameters("CategoryName", new ParameterOperatorParameters("a")),
                                new ConstantOperatorParameters("CategoryOne")
                            ),
                            "a"
                        ),
                        parameterName
                    )
                );
        }

        [Fact]
        public void Any()
        {
            //act
            var expression = CreateExpression<IQueryable<Category>, bool>();
            var result = RunExpression(expression, GetCategories());

            //assert
            AssertFilterStringIsCorrect(expression, "$it => $it.Any()");
            Assert.True(result);

            Expression<Func<T, TReturn>> CreateExpression<T, TReturn>() 
                => GetExpression<T, TReturn>
                (
                    GetExpressionParameter<T, TReturn>
                    (
                        new AnyOperatorParameters
                        (
                            new ParameterOperatorParameters(parameterName)
                        ),
                        parameterName
                    )
                );
        }

        [Fact]
        public void AsQueryable()
        {
            //act
            var expression = CreateExpression<IEnumerable<Category>, IQueryable<Category>>();
            var result = RunExpression(expression, new List<Category> { new Category() });

            //assert
            AssertFilterStringIsCorrect(expression, "$it => $it.AsQueryable()");
            Assert.True(result.GetType().IsIQueryable());

            Expression<Func<T, TReturn>> CreateExpression<T, TReturn>() 
                => GetExpression<T, TReturn>
                (
                    GetExpressionParameter<T, TReturn>
                    (
                        new AsQueryableOperatorParameters
                        (
                            new ParameterOperatorParameters(parameterName)
                        ),
                        parameterName
                    )
                );
        }

        [Fact]
        public void Average_Selector()
        {
            //act
            var expression = CreateExpression<IQueryable<Category>, double>();
            var result = RunExpression(expression, GetCategories());

            //assert
            AssertFilterStringIsCorrect(expression, "$it => $it.Average(a => a.CategoryID)");
            Assert.Equal(1.5, result);

            Expression<Func<T, TReturn>> CreateExpression<T, TReturn>() 
                => GetExpression<T, TReturn>
                (
                    GetExpressionParameter<T, TReturn>
                    (
                        new AverageOperatorParameters
                        (
                            new ParameterOperatorParameters(parameterName),
                            new MemberSelectorOperatorParameters("CategoryID", new ParameterOperatorParameters("a")),
                            "a"
                        ),
                        parameterName
                    )
                );
        }

        [Fact]
        public void Average()
        {
            //act
            var expression = CreateExpression<IQueryable<Category>, double>();
            var result = RunExpression(expression, GetCategories());

            //assert
            AssertFilterStringIsCorrect(expression, "$it => $it.Select(a => a.CategoryID).Average()");
            Assert.Equal(1.5, result);

            Expression<Func<T, TReturn>> CreateExpression<T, TReturn>() 
                => GetExpression<T, TReturn>
                (
                    GetExpressionParameter<T, TReturn>
                    (
                        new AverageOperatorParameters
                        (
                            new SelectOperatorParameters
                            (
                                new ParameterOperatorParameters(parameterName),
                                new MemberSelectorOperatorParameters("CategoryID", new ParameterOperatorParameters("a")),
                                "a"
                            )
                        ),
                        parameterName
                    )
                );
        }

        [Fact]
        public void Count_Filter()
        {
            //act
            var expression = CreateExpression<IQueryable<Category>, int>();
            var result = RunExpression(expression, GetCategories());

            //assert
            AssertFilterStringIsCorrect(expression, "$it => $it.Count(a => (a.CategoryID == 1))");
            Assert.Equal(1, result);

            Expression<Func<T, TReturn>> CreateExpression<T, TReturn>() 
                => GetExpression<T, TReturn>
                (
                    GetExpressionParameter<T, TReturn>
                    (
                        new CountOperatorParameters
                        (
                            new ParameterOperatorParameters(parameterName),
                            new EqualsBinaryOperatorParameters
                            (
                                new MemberSelectorOperatorParameters("CategoryID", new ParameterOperatorParameters("a")),
                                new ConstantOperatorParameters(1)
                            ),
                            "a"
                        ),
                        parameterName
                    )
                );
        }

        [Fact]
        public void Count()
        {
            //act
            var expression = CreateExpression<IQueryable<Category>, int>();
            var result = RunExpression(expression, GetCategories());

            //assert
            AssertFilterStringIsCorrect(expression, "$it => $it.Count()");
            Assert.Equal(2, result);

            Expression<Func<T, TReturn>> CreateExpression<T, TReturn>() 
                => GetExpression<T, TReturn>
                (
                    GetExpressionParameter<T, TReturn>
                    (
                        new CountOperatorParameters
                        (
                            new ParameterOperatorParameters(parameterName)
                        ),
                        parameterName
                    )
                );
        }

        [Fact]
        public void Distinct()
        {
            //act
            var expression = CreateExpression<IQueryable<Category>, IQueryable<Category>>();
            var result = RunExpression(expression, GetCategories());

            //assert
            AssertFilterStringIsCorrect(expression, "$it => $it.Distinct()");
            Assert.Equal(2, result.Count());

            Expression<Func<T, TReturn>> CreateExpression<T, TReturn>() 
                => GetExpression<T, TReturn>
                (
                    GetExpressionParameter<T, TReturn>
                    (
                        new DistinctOperatorParameters
                        (
                            new ParameterOperatorParameters(parameterName)
                        ),
                        parameterName
                    )
                );
        }

        [Fact]
        public void First_Filter_Throws_Exception()
        {
            //act
            var expression = CreateExpression<IQueryable<Category>, Category>();

            //assert
            AssertFilterStringIsCorrect(expression, "$it => $it.First(a => (a.CategoryID == -1))");
            Assert.Throws<InvalidOperationException>(() => RunExpression(expression, GetCategories()));

            Expression<Func<T, TReturn>> CreateExpression<T, TReturn>() 
                => GetExpression<T, TReturn>
                (
                    GetExpressionParameter<T, TReturn>
                    (
                        new FirstOperatorParameters
                        (
                            new ParameterOperatorParameters(parameterName),
                            new EqualsBinaryOperatorParameters
                            (
                                new MemberSelectorOperatorParameters("CategoryID", new ParameterOperatorParameters("a")),
                                new ConstantOperatorParameters(-1)
                            ),
                            "a"
                        ),
                        parameterName
                    )
                );
        }

        [Fact]
        public void First_Filter_Returns_match()
        {
            //act
            var expression = CreateExpression<IQueryable<Category>, Category>();
            var result = RunExpression(expression, GetCategories());

            //assert
            AssertFilterStringIsCorrect(expression, "$it => $it.First(a => (a.CategoryID == 1))");
            Assert.Equal(1, result.CategoryID);

            Expression<Func<T, TReturn>> CreateExpression<T, TReturn>() 
                => GetExpression<T, TReturn>
                (
                    GetExpressionParameter<T, TReturn>
                    (
                        new FirstOperatorParameters
                        (
                            new ParameterOperatorParameters(parameterName),
                            new EqualsBinaryOperatorParameters
                            (
                                new MemberSelectorOperatorParameters("CategoryID", new ParameterOperatorParameters("a")),
                                new ConstantOperatorParameters(1)
                            ),
                            "a"
                        ),
                        parameterName
                    )
                );
        }

        [Fact]
        public void First()
        {
            //act
            var expression = CreateExpression<IQueryable<Category>, Category>();
            var result = RunExpression(expression, GetCategories());

            //assert
            AssertFilterStringIsCorrect(expression, "$it => $it.First()");
            Assert.NotNull(result);

            Expression<Func<T, TReturn>> CreateExpression<T, TReturn>() 
                => GetExpression<T, TReturn>
                (
                    GetExpressionParameter<T, TReturn>
                    (
                        new FirstOperatorParameters
                        (
                            new ParameterOperatorParameters(parameterName)
                        ),
                        parameterName
                    )
                );
        }

        [Fact]
        public void FirstOrDefault_Filter_Returns_null()
        {
            //act
            var expression = CreateExpression<IQueryable<Category>, Category>();
            var result = RunExpression(expression, GetCategories());

            //assert
            AssertFilterStringIsCorrect(expression, "$it => $it.FirstOrDefault(a => (a.CategoryID == -1))");
            Assert.Null(result);

            Expression<Func<T, TReturn>> CreateExpression<T, TReturn>() 
                => GetExpression<T, TReturn>
                (
                    GetExpressionParameter<T, TReturn>
                    (
                        new FirstOrDefaultOperatorParameters
                        (
                            new ParameterOperatorParameters(parameterName),
                            new EqualsBinaryOperatorParameters
                            (
                                new MemberSelectorOperatorParameters("CategoryID", new ParameterOperatorParameters("a")),
                                new ConstantOperatorParameters(-1)
                            ),
                            "a"
                        ),
                        parameterName
                    )
                );
        }

        [Fact]
        public void FirstOrDefault_Filter_Returns_match()
        {
            //act
            var expression = CreateExpression<IQueryable<Category>, Category>();
            var result = RunExpression(expression, GetCategories());

            //assert
            AssertFilterStringIsCorrect(expression, "$it => $it.FirstOrDefault(a => (a.CategoryID == 1))");
            Assert.Equal(1, result.CategoryID);

            Expression<Func<T, TReturn>> CreateExpression<T, TReturn>() 
                => GetExpression<T, TReturn>
                (
                    GetExpressionParameter<T, TReturn>
                    (
                        new FirstOrDefaultOperatorParameters
                        (
                            new ParameterOperatorParameters(parameterName),
                            new EqualsBinaryOperatorParameters
                            (
                                new MemberSelectorOperatorParameters("CategoryID", new ParameterOperatorParameters("a")),
                                new ConstantOperatorParameters(1)
                            ),
                            "a"
                        ),
                        parameterName
                    )
                );
        }

        [Fact]
        public void FirstOrDefault()
        {
            //act
            var expression = CreateExpression<IQueryable<Category>, Category>();
            var result = RunExpression(expression, GetCategories());

            //assert
            AssertFilterStringIsCorrect(expression, "$it => $it.FirstOrDefault()");
            Assert.NotNull(result);

            Expression<Func<T, TReturn>> CreateExpression<T, TReturn>() 
                => GetExpression<T, TReturn>
                (
                    GetExpressionParameter<T, TReturn>
                    (
                        new FirstOrDefaultOperatorParameters
                        (
                            new ParameterOperatorParameters(parameterName)
                        ),
                        parameterName
                    )
                );
        }

        [Fact]
        public void GroupBy()
        {
            //act
            var expression = CreateExpression<IQueryable<Product>, IQueryable<IGrouping<int, Product>>>();
            var result = RunExpression(expression, GetProducts());

            //assert
            AssertFilterStringIsCorrect(expression, "$it => $it.GroupBy(a => a.SupplierID)");
            Assert.Equal(1, result.Count());
            Assert.Equal(2, result.First().Count());
            Assert.Equal(3, result.First().First().SupplierID);

            Expression<Func<T, TReturn>> CreateExpression<T, TReturn>() 
                => GetExpression<T, TReturn>
                (
                    GetExpressionParameter<T, TReturn>
                    (
                        new GroupByOperatorParameters
                        (
                            new ParameterOperatorParameters(parameterName),
                            new MemberSelectorOperatorParameters("SupplierID", new ParameterOperatorParameters("a")),
                            "a"
                        ),
                        parameterName
                    )
                );
        }

        [Fact]
        public void Last_Filter_Throws_Exception()
        {
            //act
            var expression = CreateExpression<IQueryable<Category>, Category>();

            //assert
            AssertFilterStringIsCorrect(expression, "$it => $it.Last(a => (a.CategoryID == -1))");
            Assert.Throws<InvalidOperationException>(() => RunExpression(expression, GetCategories()));

            Expression<Func<T, TReturn>> CreateExpression<T, TReturn>() 
                => GetExpression<T, TReturn>
                (
                    GetExpressionParameter<T, TReturn>
                    (
                        new LastOperatorParameters
                        (
                            new ParameterOperatorParameters(parameterName),
                            new EqualsBinaryOperatorParameters
                            (
                                new MemberSelectorOperatorParameters("CategoryID", new ParameterOperatorParameters("a")),
                                new ConstantOperatorParameters(-1)
                            ),
                            "a"
                        ),
                        parameterName
                    )
                );
        }

        [Fact]
        public void Last_Filter_Returns_match()
        {
            //act
            var expression = CreateExpression<IQueryable<Category>, Category>();
            var result = RunExpression(expression, GetCategories());

            //assert
            AssertFilterStringIsCorrect(expression, "$it => $it.Last(a => (a.CategoryID == 2))");
            Assert.Equal(2, result.CategoryID);

            Expression<Func<T, TReturn>> CreateExpression<T, TReturn>() 
                => GetExpression<T, TReturn>
                (
                    GetExpressionParameter<T, TReturn>
                    (
                        new LastOperatorParameters
                        (
                            new ParameterOperatorParameters(parameterName),
                            new EqualsBinaryOperatorParameters
                            (
                                new MemberSelectorOperatorParameters("CategoryID", new ParameterOperatorParameters("a")),
                                new ConstantOperatorParameters(2)
                            ),
                            "a"
                        ),
                        parameterName
                    )
                );
        }

        [Fact]
        public void Last()
        {
            //act
            var expression = CreateExpression<IQueryable<Category>, Category>();
            var result = RunExpression(expression, GetCategories());

            //assert
            AssertFilterStringIsCorrect(expression, "$it => $it.Last()");
            Assert.NotNull(result);

            Expression<Func<T, TReturn>> CreateExpression<T, TReturn>() 
                => GetExpression<T, TReturn>
                (
                    GetExpressionParameter<T, TReturn>
                    (
                        new LastOperatorParameters
                        (
                            new ParameterOperatorParameters(parameterName)
                        ),
                        parameterName
                    )
                );
        }

        [Fact]
        public void LastOrDefault_Filter_Returns_null()
        {
            //act
            var expression = CreateExpression<IQueryable<Category>, Category>();
            var result = RunExpression(expression, GetCategories());

            //assert
            AssertFilterStringIsCorrect(expression, "$it => $it.LastOrDefault(a => (a.CategoryID == -1))");
            Assert.Null(result);

            Expression<Func<T, TReturn>> CreateExpression<T, TReturn>() 
                => GetExpression<T, TReturn>
                (
                    GetExpressionParameter<T, TReturn>
                    (
                        new LastOrDefaultOperatorParameters
                        (
                            new ParameterOperatorParameters(parameterName),
                            new EqualsBinaryOperatorParameters
                            (
                                new MemberSelectorOperatorParameters("CategoryID", new ParameterOperatorParameters("a")),
                                new ConstantOperatorParameters(-1)
                            ),
                            "a"
                        ),
                        parameterName
                    )
                );
        }

        [Fact]
        public void LastOrDefault_Filter_Returns_match()
        {
            //act
            var expression = CreateExpression<IQueryable<Category>, Category>();
            var result = RunExpression(expression, GetCategories());

            //assert
            AssertFilterStringIsCorrect(expression, "$it => $it.LastOrDefault(a => (a.CategoryID == 2))");
            Assert.Equal(2, result.CategoryID);

            Expression<Func<T, TReturn>> CreateExpression<T, TReturn>() 
                => GetExpression<T, TReturn>
                (
                    GetExpressionParameter<T, TReturn>
                    (
                        new LastOrDefaultOperatorParameters
                        (
                            new ParameterOperatorParameters(parameterName),
                            new EqualsBinaryOperatorParameters
                            (
                                new MemberSelectorOperatorParameters("CategoryID", new ParameterOperatorParameters("a")),
                                new ConstantOperatorParameters(2)
                            ),
                            "a"
                        ),
                        parameterName
                    )
                );
        }

        [Fact]
        public void LastOrDefault()
        {
            //act
            var expression = CreateExpression<IQueryable<Category>, Category>();
            var result = RunExpression(expression, GetCategories());

            //assert
            AssertFilterStringIsCorrect(expression, "$it => $it.LastOrDefault()");
            Assert.NotNull(result);

            Expression<Func<T, TReturn>> CreateExpression<T, TReturn>() 
                => GetExpression<T, TReturn>
                (
                    GetExpressionParameter<T, TReturn>
                    (
                        new LastOrDefaultOperatorParameters
                        (
                            new ParameterOperatorParameters(parameterName)
                        ),
                        parameterName
                    )
                );
        }

        [Fact]
        public void Max_Selector()
        {
            //act
            var expression = CreateExpression<IQueryable<Category>, int>();
            var result = RunExpression(expression, GetCategories());

            //assert
            AssertFilterStringIsCorrect(expression, "$it => $it.Max(a => a.CategoryID)");
            Assert.Equal(2, result);

            Expression<Func<T, TReturn>> CreateExpression<T, TReturn>() 
                => GetExpression<T, TReturn>
                (
                    GetExpressionParameter<T, TReturn>
                    (
                        new MaxOperatorParameters
                        (
                            new ParameterOperatorParameters(parameterName),
                            new MemberSelectorOperatorParameters("CategoryID", new ParameterOperatorParameters("a")),
                            "a"
                        ),
                        parameterName
                    )
                );
        }

        [Fact]
        public void Max()
        {
            //act
            var expression = CreateExpression<IQueryable<Category>, int>();
            var result = RunExpression(expression, GetCategories());

            //assert
            AssertFilterStringIsCorrect(expression, "$it => $it.Select(a => a.CategoryID).Max()");
            Assert.Equal(2, result);

            Expression<Func<T, TReturn>> CreateExpression<T, TReturn>() 
                => GetExpression<T, TReturn>
                (
                    GetExpressionParameter<T, TReturn>
                    (
                        new MaxOperatorParameters
                        (
                            new SelectOperatorParameters
                            (
                                new ParameterOperatorParameters(parameterName),
                                new MemberSelectorOperatorParameters("CategoryID", new ParameterOperatorParameters("a")),
                                "a"
                            )
                        ),
                        parameterName
                    )
                );
        }

        [Fact]
        public void Min_Selector()
        {
            //act
            var expression = CreateExpression<IQueryable<Category>, int>();
            var result = RunExpression(expression, GetCategories());

            //assert
            AssertFilterStringIsCorrect(expression, "$it => $it.Min(a => a.CategoryID)");
            Assert.Equal(1, result);

            Expression<Func<T, TReturn>> CreateExpression<T, TReturn>() 
                => GetExpression<T, TReturn>
                (
                    GetExpressionParameter<T, TReturn>
                    (
                        new MinOperatorParameters
                        (
                            new ParameterOperatorParameters(parameterName),
                            new MemberSelectorOperatorParameters("CategoryID", new ParameterOperatorParameters("a")),
                            "a"
                        ),
                        parameterName
                    )
                );
        }

        [Fact]
        public void Min()
        {
            //act
            var expression = CreateExpression<IQueryable<Category>, int>();
            var result = RunExpression(expression, GetCategories());

            //assert
            AssertFilterStringIsCorrect(expression, "$it => $it.Select(a => a.CategoryID).Min()");
            Assert.Equal(1, result);

            Expression<Func<T, TReturn>> CreateExpression<T, TReturn>() 
                => GetExpression<T, TReturn>
                (
                    GetExpressionParameter<T, TReturn>
                    (
                        new MinOperatorParameters
                        (
                            new SelectOperatorParameters
                            (
                                new ParameterOperatorParameters(parameterName),
                                new MemberSelectorOperatorParameters("CategoryID", new ParameterOperatorParameters("a")),
                                "a"
                            )
                        ),
                        parameterName
                    )
                );
        }

        [Fact]
        public void OrderBy()
        {
            //act
            var expression = CreateExpression<IQueryable<Category>, IOrderedQueryable<Category>>();
            var result = RunExpression(expression, GetCategories());

            //assert
            AssertFilterStringIsCorrect(expression, "$it => $it.OrderBy(a => a.CategoryID)");
            Assert.Equal(1, result.First().CategoryID);

            Expression<Func<T, TReturn>> CreateExpression<T, TReturn>() 
                => GetExpression<T, TReturn>
                (
                    GetExpressionParameter<T, TReturn>
                    (
                        new OrderByOperatorParameters
                        (
                            new ParameterOperatorParameters(parameterName),
                            new MemberSelectorOperatorParameters("CategoryID", new ParameterOperatorParameters("a")),
                            ListSortDirection.Ascending,
                            "a"
                        ),
                        parameterName
                    )
                );
        }

        [Fact]
        public void OrderByDescending()
        {
            //act
            var expression = CreateExpression<IQueryable<Category>, IOrderedQueryable<Category>>();
            var result = RunExpression(expression, GetCategories());

            //assert
            AssertFilterStringIsCorrect(expression, "$it => $it.OrderByDescending(a => a.CategoryID)");
            Assert.Equal(2, result.First().CategoryID);

            Expression<Func<T, TReturn>> CreateExpression<T, TReturn>() 
                => GetExpression<T, TReturn>
                (
                    GetExpressionParameter<T, TReturn>
                    (
                        new OrderByOperatorParameters
                        (
                            new ParameterOperatorParameters(parameterName),
                            new MemberSelectorOperatorParameters("CategoryID", new ParameterOperatorParameters("a")),
                            ListSortDirection.Descending,
                            "a"
                        ),
                        parameterName
                    )
                );
        }

        [Fact]
        public void OrderByThenBy()
        {
            //act
            var expression = CreateExpression<IQueryable<Product>, IOrderedQueryable<Product>>();
            var result = RunExpression(expression, GetProducts());

            //assert
            AssertFilterStringIsCorrect(expression, "$it => $it.OrderBy(a => a.SupplierID).ThenBy(a => a.ProductID)");
            Assert.Equal(1, result.First().ProductID);

            Expression<Func<T, TReturn>> CreateExpression<T, TReturn>() 
                => GetExpression<T, TReturn>
                (
                    GetExpressionParameter<T, TReturn>
                    (
                        new ThenByOperatorParameters
                        (
                            new OrderByOperatorParameters
                            (
                                new ParameterOperatorParameters(parameterName),
                                new MemberSelectorOperatorParameters("SupplierID", new ParameterOperatorParameters("a")),
                                ListSortDirection.Ascending,
                                "a"
                            ),
                            new MemberSelectorOperatorParameters("ProductID", new ParameterOperatorParameters("a")),
                            ListSortDirection.Ascending,
                            "a"
                        ),
                        parameterName
                    )
                );
        }

        [Fact]
        public void OrderByThenByDescending()
        {
            //act
            var expression = CreateExpression<IQueryable<Product>, IOrderedQueryable<Product>>();
            var result = RunExpression(expression, GetProducts());

            //assert
            AssertFilterStringIsCorrect(expression, "$it => $it.OrderBy(a => a.SupplierID).ThenByDescending(a => a.ProductID)");
            Assert.Equal(2, result.First().ProductID);

            Expression<Func<T, TReturn>> CreateExpression<T, TReturn>() 
                => GetExpression<T, TReturn>
                (
                    GetExpressionParameter<T, TReturn>
                    (
                        new ThenByOperatorParameters
                        (
                            new OrderByOperatorParameters
                            (
                                new ParameterOperatorParameters(parameterName),
                                new MemberSelectorOperatorParameters("SupplierID", new ParameterOperatorParameters("a")),
                                ListSortDirection.Ascending,
                                "a"
                            ),
                            new MemberSelectorOperatorParameters("ProductID", new ParameterOperatorParameters("a")),
                            ListSortDirection.Descending,
                            "a"
                        ),
                        parameterName
                    )
                );
        }

        [Fact]
        public void Paging()
        {
            //act
            var expression = CreateExpression<IQueryable<Product>, IQueryable<Address>>();
            var result = RunExpression(expression, GetProducts());

            //assert
            AssertFilterStringIsCorrect
            (
                expression,
                "$it => $it.SelectMany(a => a.AlternateAddresses).OrderBy(a => a.State).ThenBy(a => a.AddressID).Skip(1).Take(2)"
            );
            Assert.Equal(2, result.Count());
            Assert.Equal(4, result.First().AddressID);

            Expression<Func<T, TReturn>> CreateExpression<T, TReturn>()
                => GetExpression<T, TReturn>
                (
                    GetExpressionParameter<T, TReturn>
                    (
                        new TakeOperatorParameters
                        (
                            new SkipOperatorParameters
                            (
                                new ThenByOperatorParameters
                                (
                                    new OrderByOperatorParameters
                                    (
                                        new SelectManyOperatorParameters
                                        (
                                            new ParameterOperatorParameters(parameterName),
                                            new MemberSelectorOperatorParameters("AlternateAddresses", new ParameterOperatorParameters("a")),
                                            "a"
                                        ),
                                        new MemberSelectorOperatorParameters("State", new ParameterOperatorParameters("a")),
                                        ListSortDirection.Ascending,
                                        "a"
                                    ),
                                    new MemberSelectorOperatorParameters("AddressID", new ParameterOperatorParameters("a")),
                                    ListSortDirection.Ascending,
                                    "a"
                                ),
                                1
                            ),
                            2
                        ),
                        parameterName
                    )
                );
        }

        [Fact]
        public void Select_New()
        {
            var expression = CreateExpression<IQueryable<Category>, IQueryable<dynamic>>();
            var result = RunExpression(expression, GetCategories());

            Assert.Equal(2, result.First().CategoryID);

            Expression<Func<T, TReturn>> CreateExpression<T, TReturn>()
                => GetExpression<T, TReturn>
                (
                    GetExpressionParameter<T, TReturn>
                    (
                        new SelectOperatorParameters
                        (
                            new OrderByOperatorParameters
                            (
                                new ParameterOperatorParameters(parameterName),
                                new MemberSelectorOperatorParameters("CategoryID", new ParameterOperatorParameters("a")),
                                ListSortDirection.Descending,
                                "a"
                            ),
                            new MemberInitOperatorParameters
                            (
                                new List<MemberBindingItem>
                                {
                                    new MemberBindingItem("CategoryID", new MemberSelectorOperatorParameters("CategoryID", new ParameterOperatorParameters("a"))),
                                    new MemberBindingItem("CategoryName", new MemberSelectorOperatorParameters("CategoryName", new ParameterOperatorParameters("a"))),
                                    new MemberBindingItem("Products", new MemberSelectorOperatorParameters("Products", new ParameterOperatorParameters("a")))
                                }
                            ),
                            "a"
                        ),
                        parameterName
                    )
                );
        }

        [Fact]
        public void SelectMany()
        {
            //act
            var expression = CreateExpression<IQueryable<Category>, IQueryable<Product>>();
            var result = RunExpression(expression, GetCategories());

            //assert
            AssertFilterStringIsCorrect(expression, "$it => $it.SelectMany(a => a.Products)");
            Assert.Equal(3, result.Count());

            Expression<Func<T, TReturn>> CreateExpression<T, TReturn>()
                => GetExpression<T, TReturn>
                (
                    GetExpressionParameter<T, TReturn>
                    (
                        new SelectManyOperatorParameters
                        (
                            new ParameterOperatorParameters(parameterName),
                            new MemberSelectorOperatorParameters("Products", new ParameterOperatorParameters("a")),
                            "a"
                        ),
                        parameterName
                    )
                );
        }

        [Fact]
        public void Single_Filter_Throws_Exception()
        {
            //act
            var expression = CreateExpression<IQueryable<Category>, Category>();

            //assert
            AssertFilterStringIsCorrect(expression, "$it => $it.Single(a => (a.CategoryID == -1))");
            Assert.Throws<InvalidOperationException>(() => RunExpression(expression, GetCategories()));

            Expression<Func<T, TReturn>> CreateExpression<T, TReturn>()
                => GetExpression<T, TReturn>
                (
                    GetExpressionParameter<T, TReturn>
                    (
                        new SingleOperatorParameters
                        (
                            new ParameterOperatorParameters(parameterName),
                            new EqualsBinaryOperatorParameters
                            (
                                new MemberSelectorOperatorParameters("CategoryID", new ParameterOperatorParameters("a")),
                                new ConstantOperatorParameters(-1)
                            ),
                            "a"
                        ),
                        parameterName
                    )
                );
        }

        [Fact]
        public void Single_Filter_Returns_match()
        {
            //act
            var expression = CreateExpression<IQueryable<Category>, Category>();
            var result = RunExpression(expression, GetCategories());

            //assert
            AssertFilterStringIsCorrect(expression, "$it => $it.Single(a => (a.CategoryID == 1))");
            Assert.Equal(1, result.CategoryID);

            Expression<Func<T, TReturn>> CreateExpression<T, TReturn>()
                => GetExpression<T, TReturn>
                (
                    GetExpressionParameter<T, TReturn>
                    (
                        new SingleOperatorParameters
                        (
                            new ParameterOperatorParameters(parameterName),
                            new EqualsBinaryOperatorParameters
                            (
                                new MemberSelectorOperatorParameters("CategoryID", new ParameterOperatorParameters("a")),
                                new ConstantOperatorParameters(1)
                            ),
                            "a"
                        ),
                        parameterName
                    )
                );
        }

        [Fact]
        public void Single_with_multiple_matches_Throws_Exception()
        {
            //act
            var expression = CreateExpression<IQueryable<Category>, Category>();

            //assert
            AssertFilterStringIsCorrect(expression, "$it => $it.Single()");
            Assert.Throws<InvalidOperationException>(() => RunExpression(expression, GetCategories()));

            Expression<Func<T, TReturn>> CreateExpression<T, TReturn>()
                => GetExpression<T, TReturn>
                (
                    GetExpressionParameter<T, TReturn>
                    (
                        new SingleOperatorParameters
                        (
                            new ParameterOperatorParameters(parameterName)
                        ),
                        parameterName
                    )
                );
        }

        [Fact]
        public void Sum_Selector()
        {
            //act
            var expression = CreateExpression<IQueryable<Category>, int>();
            var result = RunExpression(expression, GetCategories());

            //assert
            AssertFilterStringIsCorrect(expression, "$it => $it.Sum(a => a.CategoryID)");
            Assert.Equal(3, result);

            Expression<Func<T, TReturn>> CreateExpression<T, TReturn>()
                => GetExpression<T, TReturn>
                (
                    GetExpressionParameter<T, TReturn>
                    (
                        new SumOperatorParameters
                        (
                            new ParameterOperatorParameters(parameterName),
                            new MemberSelectorOperatorParameters("CategoryID", new ParameterOperatorParameters("a")),
                            "a"
                        ),
                        parameterName
                    )
                );
        }

        [Fact]
        public void Sum()
        {
            //act
            var expression = CreateExpression<IQueryable<Category>, int>();
            var result = RunExpression(expression, GetCategories());

            //assert
            AssertFilterStringIsCorrect(expression, "$it => $it.Select(a => a.CategoryID).Sum()");
            Assert.Equal(3, result);
            
            Expression<Func<T, TReturn>> CreateExpression<T, TReturn>()
                => GetExpression<T, TReturn>
                (
                    GetExpressionParameter<T, TReturn>
                    (
                        new SumOperatorParameters
                        (
                            new SelectOperatorParameters
                            (
                                new ParameterOperatorParameters(parameterName),
                                new MemberSelectorOperatorParameters("CategoryID", new ParameterOperatorParameters("a")),
                                "a"
                            )
                        ),
                        parameterName
                    )
                );
        }

        [Fact]
        public void ToList()
        {
            var expression = CreateExpression<IQueryable<Category>, List<Category>>();
            var result = RunExpression(expression, GetCategories());

            Assert.Equal(2, result.Count);

            Expression<Func<T, TReturn>> CreateExpression<T, TReturn>()
                => GetExpression<T, TReturn>
                (
                    GetExpressionParameter<T, TReturn>
                    (
                        new ToListOperatorParameters
                        (
                           new ParameterOperatorParameters(parameterName)
                        ),
                        parameterName
                    )
                );
        }

        [Fact]
        public void Where_with_matches()
        {
            var expression = CreateExpression<IQueryable<Category>, IQueryable<Category>>();
            var result = RunExpression(expression, GetCategories());

            Assert.Equal(2, result.First().CategoryID);

            Expression<Func<T, TReturn>> CreateExpression<T, TReturn>()
                => GetExpression<T, TReturn>
                (
                    GetExpressionParameter<T, TReturn>
                    (
                        new WhereOperatorParameters
                        (
                            new OrderByOperatorParameters
                            (
                                new ParameterOperatorParameters(parameterName),
                                new MemberSelectorOperatorParameters("CategoryID", new ParameterOperatorParameters("a")),
                                ListSortDirection.Descending,
                                "a"
                            ),
                            new NotEqualsBinaryOperatorParameters
                            (
                                new MemberSelectorOperatorParameters("CategoryID", new ParameterOperatorParameters("a")),
                                new ConstantOperatorParameters(1)
                            ),
                            "a"
                        ),
                        parameterName
                    )
                );
        }

        [Fact]
        public void Where_without_matches()
        {
            var expression = CreateExpression<IQueryable<Category>, IQueryable<Category>>();
            var result = RunExpression(expression, GetCategories());

            Assert.Empty(result);

            Expression<Func<T, TReturn>> CreateExpression<T, TReturn>()
                => GetExpression<T, TReturn>
                (
                    GetExpressionParameter<T, TReturn>
                    (
                        new WhereOperatorParameters
                        (
                            new ParameterOperatorParameters(parameterName),
                            new EqualsBinaryOperatorParameters
                            (
                                new MemberSelectorOperatorParameters("CategoryID", new ParameterOperatorParameters("a")),
                                new ConstantOperatorParameters(-1)
                            ),
                            "a"
                        ),
                        parameterName
                    )
                );
        }
        #endregion Tests

        #region Helpers
        /// <summary>
        /// Takes an object describing the body e.g. $it.Any() and returns an object describing the lambda expressiom e.g. $it => $it.Any()
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="selectorBody"></param>
        /// <param name="parameterName"></param>
        /// <returns></returns>
        private SelectorLambdaOperatorParameters GetExpressionParameter<T, TResult>(IExpressionParameter selectorBody, string parameterName = "$it")
            => new SelectorLambdaOperatorParameters
            (
                selectorBody,
                typeof(T),
                parameterName,
                typeof(TResult)
            );

        /// <summary>
        /// Takes an object describing the lambda expressiom e.g. $it => $it.Any() and returns the lambda expression.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="completeLambda"></param>
        /// <returns></returns>
        private Expression<Func<T, TResult>> GetExpression<T, TResult>(IExpressionParameter completeLambda)
        {
            IMapper mapper = serviceProvider.GetRequiredService<IMapper>();

            return (Expression<Func<T, TResult>>)mapper.Map<SelectorLambdaOperator>//map the complete lambda from decriptor object to operator object
            (
                mapper.Map<OperatorDescriptorBase>(completeLambda),//map the complete lambda from parameter object to decriptor object
                opts => opts.Items["parameters"] = GetParameters()
            ).Build();//create the lambda expression from the operator object
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

        private static IDictionary<string, ParameterExpression> GetParameters()
            => new Dictionary<string, ParameterExpression>();

        private TResult RunExpression<T, TResult>(Expression<Func<T, TResult>> filter, T instance)
            => filter.Compile().Invoke(instance);

        private void AssertFilterStringIsCorrect(Expression expression, string expected)
        {
            AssertStringIsCorrect(ExpressionStringBuilder.ToString(expression));

            void AssertStringIsCorrect(string resultExpression)
                => Assert.True
                (
                    expected == resultExpression,
                    $"Expected expression '{expected}' but the deserializer produced '{resultExpression}'"
                );
        }
        #endregion Helpers

        #region Queryables
        private IQueryable<Category> GetCategories()
         => new Category[]
            {
                new Category
                {
                    CategoryID = 1,
                    CategoryName = "CategoryOne",
                    Products = new Product[]
                    {
                        new Product
                        {
                            ProductID = 1,
                            ProductName = "ProductOne",
                            AlternateAddresses = new Address[]
                            {
                                new Address { AddressID = 1, City = "CityOne" },
                                new Address { AddressID = 2, City = "CityTwo"  },
                            }
                        },
                        new Product
                        {
                            ProductID = 2,
                            ProductName = "ProductTwo",
                            AlternateAddresses = new Address[]
                            {
                                new Address { AddressID = 3, City = "CityThree" },
                                new Address { AddressID = 4, City = "CityFour"  },
                            }
                        }
                    }
                },
                new Category
                {
                    CategoryID = 2,
                    CategoryName = "CategoryTwo",
                    Products =  new Product[]
                    {
                        new Product
                        {
                            AlternateAddresses = new Address[0]
                        }
                    }
                }
            }.AsQueryable();

        private IQueryable<Product> GetProducts()
         => new Product[]
         {
             new Product
             {
                 ProductID = 1,
                 ProductName = "ProductOne",
                 SupplierID = 3,
                 AlternateAddresses = new Address[]
                 {
                     new Address { AddressID = 1, City = "CityOne", State = "OH" },
                     new Address { AddressID = 2, City = "CityTwo", State = "MI"   },
                 }
             },
             new Product
             {
                 ProductID = 2,
                 ProductName = "ProductTwo",
                 SupplierID = 3,
                 AlternateAddresses = new Address[]
                 {
                     new Address { AddressID = 3, City = "CityThree", State = "OH"  },
                     new Address { AddressID = 4, City = "CityFour", State = "MI"   },
                 }
             }
         }.AsQueryable();
        #endregion Queryables
    }
}
