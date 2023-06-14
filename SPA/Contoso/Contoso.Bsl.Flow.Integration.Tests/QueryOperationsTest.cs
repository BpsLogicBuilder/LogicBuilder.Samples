using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using Contoso.AutoMapperProfiles;
using Contoso.BSL.AutoMapperProfiles;
using Contoso.Common.Utils;
using Contoso.Contexts;
using Contoso.Data.Entities;
using Contoso.Domain.Entities;
using Contoso.Parameters.Expressions;
using Contoso.Repositories;
using Contoso.Stores;
using LogicBuilder.Expressions.Utils.Strutures;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Xunit;

namespace Contoso.Bsl.Flow.Integration.Tests
{
    public class QueryOperationsTest
    {
        public QueryOperationsTest()
        {
            Initialize();
        }

        #region Fields
        private IServiceProvider serviceProvider;
        private static readonly string parameterName = "$it";
        #endregion Fields

        #region Tests
        [Fact]
        public void SelectNewCourseAssignment()
        {
            var bodyParameter = new SelectOperatorParameters
            (
                new OrderByOperatorParameters
                (
                    new ParameterOperatorParameters("q"),
                    new MemberSelectorOperatorParameters
                    (
                        "Title",
                        new ParameterOperatorParameters("s")
                    ),
                    ListSortDirection.Ascending,
                    "s"
                ),
                new MemberInitOperatorParameters
                (
                    new List<MemberBindingItem>
                    {
                        new MemberBindingItem("CourseID", new MemberSelectorOperatorParameters("CourseID", new ParameterOperatorParameters("a"))),
                        new MemberBindingItem("CourseTitle", new MemberSelectorOperatorParameters("Title", new ParameterOperatorParameters("a"))),
                        new MemberBindingItem
                        (
                            "CourseNumberAndTitle", new ConcatOperatorParameters
                            (
                                new ConcatOperatorParameters
                                (
                                    new ConvertToStringOperatorParameters
                                    (
                                        new MemberSelectorOperatorParameters("CourseID", new ParameterOperatorParameters("a"))
                                    ),
                                    new ConstantOperatorParameters(" ", typeof(string))
                                ),
                                new MemberSelectorOperatorParameters("Title", new ParameterOperatorParameters("a"))
                            )
                        )
                    },
                    typeof(CourseAssignmentModel)
                ),
                "a"
            );

            //act
            DoTest<CourseModel, Course, IQueryable<CourseAssignmentModel>, IQueryable<CourseAssignment>>
            (
                bodyParameter,
                "q",
                returnValue =>
                {
                    Assert.Equal("Calculus", returnValue.First().CourseTitle);
                },
                "q => q.OrderBy(s => s.Title).Select(a => new CourseAssignmentModel() {CourseID = a.CourseID, CourseTitle = a.Title, CourseNumberAndTitle = a.CourseID.ToString().Concat(\" \").Concat(a.Title)})"
            );
        }

        [Fact]
        public void SelectInstructorFullNames()
        {
            var bodyParameter = new SelectOperatorParameters
            (
                new OrderByOperatorParameters
                (
                    new ParameterOperatorParameters("q"),
                    new MemberSelectorOperatorParameters
                    (
                        "FullName", 
                        new ParameterOperatorParameters("s")
                    ),
                    ListSortDirection.Ascending,
                    "s"
                ),
                new MemberSelectorOperatorParameters("FullName", new ParameterOperatorParameters("a")),
                "a"
            );

            //act
            DoTest<InstructorModel, Instructor, IQueryable<string>, IQueryable<string>>
            (
                bodyParameter,
                "q",
                returnValue => Assert.Equal("Candace Kapoor", returnValue.First()),
                "q => q.OrderBy(s => s.FullName).Select(a => a.FullName)"
            );
        }

        [Fact]
        public void SelectNewInstructor()
        {
            var bodyParameter = new SelectOperatorParameters
            (
                new OrderByOperatorParameters
                (
                    new ParameterOperatorParameters("q"),
                    new MemberSelectorOperatorParameters
                    (
                        "FullName",
                        new ParameterOperatorParameters("s")
                    ),
                    ListSortDirection.Ascending,
                    "s"
                ),
                new MemberInitOperatorParameters
                (
                    new List<MemberBindingItem>
                    {
                        new MemberBindingItem("ID", new MemberSelectorOperatorParameters("ID", new ParameterOperatorParameters("a"))),
                        new MemberBindingItem("FirstName", new MemberSelectorOperatorParameters("FirstName", new ParameterOperatorParameters("a"))),
                        new MemberBindingItem("LastName", new MemberSelectorOperatorParameters("LastName", new ParameterOperatorParameters("a"))),
                        new MemberBindingItem("FullName", new MemberSelectorOperatorParameters("FullName", new ParameterOperatorParameters("a")))
                    },
                    typeof(InstructorModel)
                ),
                "a"
            );

            //act
            DoTest<InstructorModel, Instructor, IQueryable<InstructorModel>, IQueryable<Instructor>>
            (
                bodyParameter,
                "q",
                returnValue => Assert.Equal("Candace Kapoor", returnValue.First().FullName),
                "q => q.OrderBy(s => s.FullName).Select(a => new InstructorModel() {ID = a.ID, FirstName = a.FirstName, LastName = a.LastName, FullName = a.FullName})"
            );
        }

        [Fact]
        public void SelectNewInstructor_FullNameOnly()
        {
            var bodyParameter = new SelectOperatorParameters
            (
                new OrderByOperatorParameters
                (
                    new ParameterOperatorParameters("q"),
                    new MemberSelectorOperatorParameters
                    (
                        "FullName",
                        new ParameterOperatorParameters("s")
                    ),
                    ListSortDirection.Ascending,
                    "s"
                ),
                new MemberInitOperatorParameters
                (
                    new List<MemberBindingItem>
                    {
                        new MemberBindingItem("FullName", new MemberSelectorOperatorParameters("FullName", new ParameterOperatorParameters("a")))
                    },
                    typeof(InstructorModel)
                ),
                "a"
            );

            //act
            DoTest<InstructorModel, Instructor, IQueryable<InstructorModel>, IQueryable<Instructor>>
            (
                bodyParameter,
                "q",
                returnValue => Assert.Equal(" ", returnValue.First().FullName),
                //No way to create FirstName and LastName in the translated expressions
                //See the test "SelectNewInstructor()"
                "q => q.OrderBy(s => s.FullName).Select(a => new InstructorModel() {FullName = a.FullName})"
            );
        }

        [Fact]
        public void SelectNewInstructor_FirstNameOnly()
        {
            var bodyParameter = new SelectOperatorParameters
            (
                new OrderByOperatorParameters
                (
                    new ParameterOperatorParameters("q"),
                    new MemberSelectorOperatorParameters
                    (
                        "FullName",
                        new ParameterOperatorParameters("s")
                    ),
                    ListSortDirection.Ascending,
                    "s"
                ),
                new MemberInitOperatorParameters
                (
                    new List<MemberBindingItem>
                    {
                        new MemberBindingItem("FirstName", new MemberSelectorOperatorParameters("FirstName", new ParameterOperatorParameters("a")))
                    },
                    typeof(InstructorModel)
                ),
                "a"
            );

            //act
            DoTest<InstructorModel, Instructor, IQueryable<InstructorModel>, IQueryable<Instructor>>
            (
                bodyParameter,
                "q",
                returnValue => Assert.Equal("Candace", returnValue.First().FirstName),
                "q => q.OrderBy(s => s.FullName).Select(a => new InstructorModel() {FirstName = a.FirstName})"
            );
        }

        [Fact]
        public void SelectNewAnonymousType_FullNameOnly()
        {
            var bodyParameter = new SelectOperatorParameters
            (
                new OrderByOperatorParameters
                (
                    new ParameterOperatorParameters("q"),
                    new MemberSelectorOperatorParameters
                    (
                        "FullName",
                        new ParameterOperatorParameters("s")
                    ),
                    ListSortDirection.Ascending,
                    "s"
                ),
                new MemberInitOperatorParameters
                (
                    new List<MemberBindingItem>
                    {
                        new MemberBindingItem("FullName", new MemberSelectorOperatorParameters("FullName", new ParameterOperatorParameters("a")))
                    }
                ),
                "a"
            );

            //act
            DoTest<InstructorModel, Instructor, IQueryable<dynamic>, IQueryable<dynamic>>
            (
                bodyParameter,
                "q",
                returnValue => Assert.Equal("Candace Kapoor", returnValue.First().FullName),
                "q => Convert(q.OrderBy(s => s.FullName).Select(a => new AnonymousType() {FullName = a.FullName}))"
            );
        }

        [Fact]
        public void BuildWhere_OrderBy_ThenBy_Skip_Take_Average()
        {
            //arrange
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

            //act
            DoTest<StudentModel, Student, double, double>
            (
                bodyParameter, 
                "q",
                returnValue => Assert.True(returnValue > 1),
                "q => q.Where(s => ((s.ID > 1) AndAlso (s.FirstName.Compare(s.LastName) > 0))).OrderBy(v => v.LastName).ThenByDescending(v => v.FirstName).Skip(2).Take(3).Average(j => j.ID)"
            );
        }

        [Fact]
        public void BuildGroupBy_OrderBy_ThenBy_Skip_Take_Average()
        {
            //arrange
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
                            new SumOperatorParameters
                            (
                                new WhereOperatorParameters
                                (
                                    new ParameterOperatorParameters("q"),
                                    new AndBinaryOperatorParameters
                                    (
                                        new NotEqualsBinaryOperatorParameters
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
                                ),
                                new MemberSelectorOperatorParameters("Budget", new ParameterOperatorParameters("item")),
                                "item"
                            )
                        )
                    }
                ),
                "c"
            );

            //act
            DoTest<DepartmentModel, Department, IQueryable<dynamic>, IQueryable<object>>
            (
                bodyParameter,
                "q",
                returnValue => Assert.True(returnValue.First().Sum_budget == 350000),
                "q => Convert(q.GroupBy(a => 1).OrderBy(b => b.Key).Select(c => new AnonymousType() {Sum_budget = q.Where(d => ((d.DepartmentID != q.Count()) AndAlso (d.DepartmentID == c.Key))).Sum(item => item.Budget)}))"
            );
        }

        [Fact]
        public void BuildGroupBy_AsQueryable_OrderBy_Select_FirstOrDefault()
        {
            //arrange
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
                                    new MemberSelectorOperatorParameters("AdministratorName", new ParameterOperatorParameters("item")),
                                    "item"
                                )
                            ),
                            new MemberBindingItem
                            (
                                "Count",
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

            //act
            DoTest<DepartmentModel, Department, dynamic, object>
            (
                bodyParameter,
                "q",
                returnValue => 
                {
                    Assert.True(returnValue.Min_administratorName == "Candace Kapoor");
                    Assert.True(returnValue.Count == 4);
                    Assert.True(returnValue.Sum_budget == 900000);
                    Assert.True(returnValue.Min_budget == 100000);
                    Assert.True(returnValue.Min_startDate == DateTime.Parse("2007-09-01"));
                },
                "q => Convert(q.GroupBy(item => 1).AsQueryable().OrderBy(group => group.Key).Select(sel => new AnonymousType() {Min_administratorName = q.Where(d => (1 == sel.Key)).Min(item => item.AdministratorName), Count = q.Where(d => (1 == sel.Key)).Count(), Sum_budget = q.Where(d => (1 == sel.Key)).Sum(item => item.Budget), Min_budget = q.Where(d => (1 == sel.Key)).Min(item => item.Budget), Min_startDate = q.Where(d => (1 == sel.Key)).Min(item => item.StartDate)}).FirstOrDefault())"
            );
        }

        [Fact]
        public void All_Filter()
        {
            //arrange
            var bodyParameter = new AllOperatorParameters
            (
                new ParameterOperatorParameters(parameterName),
                new OrBinaryOperatorParameters
                (
                    new EqualsBinaryOperatorParameters
                    (
                        new MemberSelectorOperatorParameters("AdministratorName", new ParameterOperatorParameters("a")),
                        new ConstantOperatorParameters("Kim Abercrombie")
                    ),
                    new EqualsBinaryOperatorParameters
                    (
                        new MemberSelectorOperatorParameters("AdministratorName", new ParameterOperatorParameters("a")),
                        new ConstantOperatorParameters("Fadi Fakhouri")
                    )
                ),
                "a"
            );

            //act
            DoTest<DepartmentModel, Department, bool, bool>
            (
                bodyParameter,
                parameterName,
                returnValue => Assert.False(returnValue),
                "$it => $it.All(a => ((a.AdministratorName == \"Kim Abercrombie\") OrElse (a.AdministratorName == \"Fadi Fakhouri\")))"
            );
        }

        [Fact]
        public void Any_Filter()
        {
            //arrange
            var bodyParameter = new AnyOperatorParameters
            (
                new ParameterOperatorParameters(parameterName),
                new EqualsBinaryOperatorParameters
                (
                    new MemberSelectorOperatorParameters("AdministratorName", new ParameterOperatorParameters("a")),
                    new ConstantOperatorParameters("Kim Abercrombie")
                ),
                "a"
            );

            //act
            DoTest<DepartmentModel, Department, bool, bool>
            (
                bodyParameter,
                parameterName,
                returnValue => Assert.True(returnValue),
                "$it => $it.Any(a => (a.AdministratorName == \"Kim Abercrombie\"))"
            );
        }

        [Fact]
        public void Any()
        {
            //arrange
            var bodyParameter = new AnyOperatorParameters
            (
                new ParameterOperatorParameters(parameterName)
            );

            //act
            DoTest<DepartmentModel, Department, bool, bool>
            (
                bodyParameter,
                parameterName,
                returnValue => Assert.True(returnValue),
                "$it => $it.Any()"
            );
        }

        [Fact]
        public void AsQueryable()
        {
            //arrange
            var bodyParameter = new AsQueryableOperatorParameters
            (
                new ParameterOperatorParameters(parameterName)
            );

            //act
            DoTest<DepartmentModel, Department, IQueryable<DepartmentModel>, IQueryable<Department>>
            (
                bodyParameter,
                parameterName,
                returnValue =>
                {
                    Assert.True(returnValue.Count() == 4);
                },
                "$it => $it.AsQueryable()"
            );
        }

        [Fact]
        public void Average_Selector()
        {
            //arrange
            var bodyParameter = new AverageOperatorParameters
            (
                new ParameterOperatorParameters(parameterName),
                new MemberSelectorOperatorParameters("DepartmentID", new ParameterOperatorParameters("a")),
                "a"
            );

            //act
            DoTest<DepartmentModel, Department, double, double>
            (
                bodyParameter,
                parameterName,
                returnValue => Assert.Equal(2.5, returnValue),
                "$it => $it.Average(a => a.DepartmentID)"
            );
        }

        [Fact]
        public void Average()
        {
            //arrange
            var bodyParameter = new AverageOperatorParameters
            (
                new SelectOperatorParameters
                (
                    new ParameterOperatorParameters(parameterName),
                    new MemberSelectorOperatorParameters("DepartmentID", new ParameterOperatorParameters("a")),
                    "a"
                )
            );

            //act
            DoTest<DepartmentModel, Department, double, double>
            (
                bodyParameter,
                parameterName,
                returnValue => Assert.Equal(2.5, returnValue),
                "$it => $it.Select(a => a.DepartmentID).Average()"
            );
        }

        [Fact]
        public void Count_Filter()
        {
            //arrange
            var bodyParameter = new CountOperatorParameters
            (
                new ParameterOperatorParameters(parameterName),
                new EqualsBinaryOperatorParameters
                (
                    new MemberSelectorOperatorParameters("DepartmentID", new ParameterOperatorParameters("a")),
                    new ConstantOperatorParameters(1)
                ),
                "a"
            );

            //act
            DoTest<DepartmentModel, Department, int, int>
            (
                bodyParameter,
                parameterName,
                returnValue => Assert.Equal(1, returnValue),
                "$it => $it.Count(a => (a.DepartmentID == 1))"
            );
        }

        [Fact]
        public void Count()
        {
            //arrange
            var bodyParameter = new CountOperatorParameters
            (
                new ParameterOperatorParameters(parameterName)
            );

            //act
            DoTest<DepartmentModel, Department, int, int>
            (
                bodyParameter,
                parameterName,
                returnValue => Assert.Equal(4, returnValue),
                "$it => $it.Count()"
            );
        }

        [Fact]
        public void Distinct()
        {
            //arrange
            var bodyParameter = new ToListOperatorParameters
            (
                new DistinctOperatorParameters
                (
                    new ParameterOperatorParameters(parameterName)
                )
            );

            //act
            DoTest<DepartmentModel, Department, List<DepartmentModel>, List<Department>>
            (
                bodyParameter,
                parameterName,
                returnValue => Assert.Equal(4, returnValue.Count()),
                "$it => $it.Distinct().ToList()"
            );
        }

        [Fact()]
        public void First_Filter_Throws_Exception()
        {
            //arrange
            var bodyParameter = new FirstOperatorParameters
            (
                new ParameterOperatorParameters(parameterName),
                new EqualsBinaryOperatorParameters
                (
                    new MemberSelectorOperatorParameters("DepartmentID", new ParameterOperatorParameters("a")),
                    new ConstantOperatorParameters(-1)
                ),
                "a"
            );

            //act
            Assert.Throws<AggregateException>
            (
                () => DoTest<DepartmentModel, Department, DepartmentModel, Department>
                (
                    bodyParameter,
                    parameterName,
                    returnValue => { },
                    "$it => $it.First(a => (a.DepartmentID == -1))"
                )
            );
        }

        [Fact]
        public void First_Filter_Returns_match()
        {
            //arrange
            var bodyParameter = new FirstOperatorParameters
            (
                new ParameterOperatorParameters(parameterName),
                new EqualsBinaryOperatorParameters
                (
                    new MemberSelectorOperatorParameters("DepartmentID", new ParameterOperatorParameters("a")),
                    new ConstantOperatorParameters(1)
                ),
                "a"
            );

            //act
            DoTest<DepartmentModel, Department, DepartmentModel, Department>
            (
                bodyParameter,
                parameterName,
                returnValue => Assert.Equal(1, returnValue.DepartmentID),
                "$it => $it.First(a => (a.DepartmentID == 1))"
            );
        }

        [Fact]
        public void First()
        {
            //arrange
            var bodyParameter = new FirstOperatorParameters
            (
                new ParameterOperatorParameters(parameterName)
            );

            //act
            DoTest<DepartmentModel, Department, DepartmentModel, Department>
            (
                bodyParameter,
                parameterName,
                returnValue => Assert.NotNull(returnValue),
                "$it => $it.First()"
            );
        }

        [Fact]
        public void FirstOrDefault_Filter_Returns_null()
        {
            //arrange
            var bodyParameter = new FirstOrDefaultOperatorParameters
            (
                new ParameterOperatorParameters(parameterName),
                new EqualsBinaryOperatorParameters
                (
                    new MemberSelectorOperatorParameters("DepartmentID", new ParameterOperatorParameters("a")),
                    new ConstantOperatorParameters(-1)
                ),
                "a"
            );

            //act
            DoTest<DepartmentModel, Department, DepartmentModel, Department>
            (
                bodyParameter,
                parameterName,
                returnValue => Assert.Null(returnValue),
                "$it => $it.FirstOrDefault(a => (a.DepartmentID == -1))"
            );
        }

        [Fact]
        public void FirstOrDefault_Filter_Returns_match()
        {
            //arrange
            var bodyParameter = new FirstOrDefaultOperatorParameters
            (
                new ParameterOperatorParameters(parameterName),
                new EqualsBinaryOperatorParameters
                (
                    new MemberSelectorOperatorParameters("DepartmentID", new ParameterOperatorParameters("a")),
                    new ConstantOperatorParameters(1)
                ),
                "a"
            );

            //act
            DoTest<DepartmentModel, Department, DepartmentModel, Department>
            (
                bodyParameter,
                parameterName,
                returnValue => Assert.Equal(1, returnValue.DepartmentID),
                "$it => $it.FirstOrDefault(a => (a.DepartmentID == 1))"
            );
        }

        [Fact]
        public void FirstOrDefault()
        {
            //arrange
            var bodyParameter = new FirstOrDefaultOperatorParameters
            (
                new ParameterOperatorParameters(parameterName)
            );

            //act
            DoTest<DepartmentModel, Department, DepartmentModel, Department>
            (
                bodyParameter,
                parameterName,
                returnValue => Assert.NotNull(returnValue),
                "$it => $it.FirstOrDefault()"
            );
        }

        [Fact(Skip = "Can't map/project IGrouping<,>")]
        public void GroupBy()
        {
            //arrange
            var bodyParameter = new GroupByOperatorParameters
            (
                new ParameterOperatorParameters(parameterName),
                new MemberSelectorOperatorParameters("DepartmentID", new ParameterOperatorParameters("a")),
                "a"
            );

            //act
            DoTest<CourseModel, Course, IQueryable<IGrouping<int, CourseModel>>, IQueryable<IGrouping<int, Course>>>
            (
                bodyParameter,
                parameterName,
                returnValue =>
                {
                    Assert.True(returnValue.Count() > 2);
                },
                "$it => $it.GroupBy(a => a.DepartmentID)"
            );
        }

        [Fact]
        public void GroupBy_Select()
        {
            //arrange
            var bodyParameter = new SelectOperatorParameters
            (
                new GroupByOperatorParameters
                (
                    new ParameterOperatorParameters(parameterName),
                    new MemberSelectorOperatorParameters("DepartmentID", new ParameterOperatorParameters("a")),
                    "a"
                ),
                new MemberSelectorOperatorParameters("Key", new ParameterOperatorParameters("b")),
                "b"
            );

            //act
            DoTest<CourseModel, Course, IQueryable<int>, IQueryable<int>>
            (
                bodyParameter,
                parameterName,
                returnValue =>
                {
                    Assert.NotNull(returnValue);
                },
                "$it => $it.GroupBy(a => a.DepartmentID).Select(b => b.Key)"
            );
        }

        [Fact]
        public void GroupBy_SelectCount()
        {
            //arrange
            var bodyParameter = new CountOperatorParameters
            (
                new SelectOperatorParameters
                (
                    new GroupByOperatorParameters
                    (
                        new ParameterOperatorParameters(parameterName),
                        new MemberSelectorOperatorParameters("DepartmentID", new ParameterOperatorParameters("a")),
                        "a"
                    ),
                    new MemberSelectorOperatorParameters("Key", new ParameterOperatorParameters("b")),
                    "b"
                )
            );

            //act
            DoTest<CourseModel, Course, int, int>
            (
                bodyParameter,
                parameterName,
                returnValue =>
                {
                    Assert.Equal(4, returnValue);
                },
                "$it => $it.GroupBy(a => a.DepartmentID).Select(b => b.Key).Count()"
            );
        }

        [Fact]
        public void Last_Filter_Throws_Exception()
        {
            //arrange
            var bodyParameter = new LastOperatorParameters
            (
                new ParameterOperatorParameters(parameterName),
                new EqualsBinaryOperatorParameters
                (
                    new MemberSelectorOperatorParameters("DepartmentID", new ParameterOperatorParameters("a")),
                    new ConstantOperatorParameters(-1)
                ),
                "a"
            );

            //act
            Assert.Throws<AggregateException>
            (
                () => DoTest<DepartmentModel, Department, DepartmentModel, Department>
                (
                    bodyParameter,
                    parameterName,
                    returnValue => { },
                    "$it => $it.Last(a => (a.DepartmentID == -1))"
                )
            );
        }

        [Fact]
        public void Last_Filter_Returns_match()
        {
            //arrange
            var bodyParameter = new LastOperatorParameters
            (
                new ToListOperatorParameters
                ( 
                    new ParameterOperatorParameters(parameterName)
                ),
                new EqualsBinaryOperatorParameters
                (
                    new MemberSelectorOperatorParameters("DepartmentID", new ParameterOperatorParameters("a")),
                    new ConstantOperatorParameters(2)
                ),
                "a"
            );

            //act
            DoTest<DepartmentModel, Department, DepartmentModel, Department>
            (
                bodyParameter,
                parameterName,
                returnValue => Assert.Equal(2, returnValue.DepartmentID),
                "$it => $it.ToList().Last(a => (a.DepartmentID == 2))"
            );
        }

        [Fact]
        public void Last()
        {
            //arrange
            var bodyParameter = new LastOperatorParameters
            (
                new ToListOperatorParameters
                (
                    new ParameterOperatorParameters(parameterName)
                )
            );

            //act
            DoTest<DepartmentModel, Department, DepartmentModel, Department>
            (
                bodyParameter,
                parameterName,
                returnValue => Assert.NotNull(returnValue),
                "$it => $it.ToList().Last()"
            );
        }

        [Fact]
        public void LastOrDefault_Filter_Returns_null()
        {
            //arrange
            var bodyParameter = new LastOrDefaultOperatorParameters
            (
                new ToListOperatorParameters
                (
                    new ParameterOperatorParameters(parameterName)
                ),
                new EqualsBinaryOperatorParameters
                (
                    new MemberSelectorOperatorParameters("DepartmentID", new ParameterOperatorParameters("a")),
                    new ConstantOperatorParameters(-1)
                ),
                "a"
            );

            //act
            DoTest<DepartmentModel, Department, DepartmentModel, Department>
            (
                bodyParameter,
                parameterName,
                returnValue => Assert.Null(returnValue),
                "$it => $it.ToList().LastOrDefault(a => (a.DepartmentID == -1))"
            );
        }

        [Fact]
        public void LastOrDefault_Filter_Returns_match()
        {
            //arrange
            var bodyParameter = new LastOrDefaultOperatorParameters
            (
                new ToListOperatorParameters
                (
                    new ParameterOperatorParameters(parameterName)
                ),
                new EqualsBinaryOperatorParameters
                (
                    new MemberSelectorOperatorParameters("DepartmentID", new ParameterOperatorParameters("a")),
                    new ConstantOperatorParameters(2)
                ),
                "a"
            );

            //act
            DoTest<DepartmentModel, Department, DepartmentModel, Department>
            (
                bodyParameter,
                parameterName,
                returnValue => Assert.Equal(2, returnValue.DepartmentID),
                "$it => $it.ToList().LastOrDefault(a => (a.DepartmentID == 2))"
            );
        }

        [Fact]
        public void LastOrDefault()
        {
            //arrange
            var bodyParameter = new LastOrDefaultOperatorParameters
            (
                new ToListOperatorParameters
                (
                    new ParameterOperatorParameters(parameterName)
                )
            );

            //act
            DoTest<DepartmentModel, Department, DepartmentModel, Department>
            (
                bodyParameter,
                parameterName,
                returnValue => Assert.NotNull(returnValue),
                "$it => $it.ToList().LastOrDefault()"
            );
        }

        [Fact]
        public void Max_Selector()
        {
            //arrange
            var bodyParameter = new MaxOperatorParameters
            (
                new ParameterOperatorParameters(parameterName),
                new MemberSelectorOperatorParameters("DepartmentID", new ParameterOperatorParameters("a")),
                "a"
            );

            //act
            DoTest<DepartmentModel, Department, int, int>
            (
                bodyParameter,
                parameterName,
                returnValue => Assert.Equal(4, returnValue),
                "$it => $it.Max(a => a.DepartmentID)"
            );
        }

        [Fact]
        public void Max()
        {
            var bodyParameter = new MaxOperatorParameters
            (
                new SelectOperatorParameters
                (
                    new ParameterOperatorParameters(parameterName),
                    new MemberSelectorOperatorParameters("DepartmentID", new ParameterOperatorParameters("a")),
                    "a"
                )
            );

            //act
            DoTest<DepartmentModel, Department, int, int>
            (
                bodyParameter,
                parameterName,
                returnValue => Assert.Equal(4, returnValue),
                "$it => $it.Select(a => a.DepartmentID).Max()"
            );
        }

        [Fact]
        public void Min_Selector()
        {
            //arrange
            var bodyParameter = new MinOperatorParameters
            (
                new ParameterOperatorParameters(parameterName),
                new MemberSelectorOperatorParameters("DepartmentID", new ParameterOperatorParameters("a")),
                "a"
            );

            //act
            DoTest<DepartmentModel, Department, int, int>
            (
                bodyParameter,
                parameterName,
                returnValue => Assert.Equal(1, returnValue),
                "$it => $it.Min(a => a.DepartmentID)"
            );
        }

        [Fact]
        public void Min()
        {
            //arrange
            var bodyParameter = new MinOperatorParameters
            (
                new SelectOperatorParameters
                (
                    new ParameterOperatorParameters(parameterName),
                    new MemberSelectorOperatorParameters("DepartmentID", new ParameterOperatorParameters("a")),
                    "a"
                )
            );

            //act
            DoTest<DepartmentModel, Department, int, int>
            (
                bodyParameter,
                parameterName,
                returnValue => Assert.Equal(1, returnValue),
                "$it => $it.Select(a => a.DepartmentID).Min()"
            );
        }

        [Fact]
        public void OrderBy()
        {
            //arrange
            var bodyParameter = new OrderByOperatorParameters
            (
                new ParameterOperatorParameters(parameterName),
                new MemberSelectorOperatorParameters("DepartmentID", new ParameterOperatorParameters("a")),
                ListSortDirection.Ascending,
                "a"
            );

            //act
            DoTest<DepartmentModel, Department, IOrderedQueryable<DepartmentModel>, IOrderedQueryable<Department>>
            (
                bodyParameter,
                parameterName,
                returnValue => Assert.Equal(1, returnValue.First().DepartmentID),
                "$it => $it.OrderBy(a => a.DepartmentID)"
            );
        }

        [Fact]
        public void OrderByDescending()
        {
            //arrange
            var bodyParameter = new OrderByOperatorParameters
            (
                new ParameterOperatorParameters(parameterName),
                new MemberSelectorOperatorParameters("DepartmentID", new ParameterOperatorParameters("a")),
                ListSortDirection.Descending,
                "a"
            );

            //act
            DoTest<DepartmentModel, Department, IOrderedQueryable<DepartmentModel>, IOrderedQueryable<Department>>
            (
                bodyParameter,
                parameterName,
                returnValue => Assert.Equal(4, returnValue.First().DepartmentID),
                "$it => $it.OrderByDescending(a => a.DepartmentID)"
            );
        }

        [Fact]
        public void OrderByThenBy()
        {
            //arrange
            var bodyParameter = new ThenByOperatorParameters
            (
                new OrderByOperatorParameters
                (
                    new ParameterOperatorParameters(parameterName),
                    new MemberSelectorOperatorParameters("Credits", new ParameterOperatorParameters("a")),
                    ListSortDirection.Ascending,
                    "a"
                ),
                new MemberSelectorOperatorParameters("CourseID", new ParameterOperatorParameters("a")),
                ListSortDirection.Ascending,
                "a"
            );

            //act
            DoTest<CourseModel, Course, IOrderedQueryable<CourseModel>, IOrderedQueryable<Course>>
            (
                bodyParameter,
                parameterName,
                returnValue => Assert.Equal(1050, returnValue.First().CourseID),
                "$it => $it.OrderBy(a => a.Credits).ThenBy(a => a.CourseID)"
            );
        }

        [Fact]
        public void OrderByThenByDescending()
        {
            //arrange
            var bodyParameter = new ThenByOperatorParameters
            (
                new OrderByOperatorParameters
                (
                    new ParameterOperatorParameters(parameterName),
                    new MemberSelectorOperatorParameters("Credits", new ParameterOperatorParameters("a")),
                    ListSortDirection.Ascending,
                    "a"
                ),
                new MemberSelectorOperatorParameters("CourseID", new ParameterOperatorParameters("a")),
                ListSortDirection.Descending,
                "a"
            );

            //act
            DoTest<CourseModel, Course, IOrderedQueryable<CourseModel>, IOrderedQueryable<Course>>
            (
                bodyParameter,
                parameterName,
                returnValue => Assert.Equal(4041, returnValue.First().CourseID),
                "$it => $it.OrderBy(a => a.Credits).ThenByDescending(a => a.CourseID)"
            );
        }

        [Fact]
        public void Paging()
        {
            //arrange
            var bodyParameter = new TakeOperatorParameters
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
                                new MemberSelectorOperatorParameters("Assignments", new ParameterOperatorParameters("a")),
                                "a"
                            ),
                            new MemberSelectorOperatorParameters("CourseTitle", new ParameterOperatorParameters("a")),
                            ListSortDirection.Ascending,
                            "a"
                        ),
                        new MemberSelectorOperatorParameters("InstructorID", new ParameterOperatorParameters("a")),
                        ListSortDirection.Ascending,
                        "a"
                    ),
                    1
                ),
                2
            );

            //act
            DoTest<CourseModel, Course, IQueryable<CourseAssignmentModel>, IQueryable<CourseAssignment>>
            (
                bodyParameter,
                parameterName,
                returnValue =>
                {
                    Assert.Equal(2, returnValue.Count());
                    Assert.Equal("Chemistry", returnValue.Last().CourseTitle);
                },
                "$it => $it.SelectMany(a => a.Assignments).OrderBy(a => a.CourseTitle).ThenBy(a => a.InstructorID).Skip(1).Take(2)"
            );
        }

        [Fact]
        public void Select_New()
        {
            //arrange
            var bodyParameter = new SelectOperatorParameters
            (
                new OrderByOperatorParameters
                (
                    new ParameterOperatorParameters(parameterName),
                    new MemberSelectorOperatorParameters("DepartmentID", new ParameterOperatorParameters("a")),
                    ListSortDirection.Descending,
                    "a"
                ),
                new MemberInitOperatorParameters
                (
                    new List<MemberBindingItem>
                    {
                        new MemberBindingItem("ID", new MemberSelectorOperatorParameters("DepartmentID", new ParameterOperatorParameters("a"))),
                        new MemberBindingItem("DepartmentName", new MemberSelectorOperatorParameters("Name", new ParameterOperatorParameters("a"))),
                        new MemberBindingItem("Courses", new MemberSelectorOperatorParameters("Courses", new ParameterOperatorParameters("a")))
                    }
                ),
                "a"
            );

            //act
            DoTest<DepartmentModel, Department, IQueryable<dynamic>, IQueryable<dynamic>>
            (
                bodyParameter,
                parameterName,
                returnValue => Assert.Equal(4, returnValue.First().ID),
                "$it => Convert($it.OrderByDescending(a => a.DepartmentID).Select(a => new AnonymousType() {ID = a.DepartmentID, DepartmentName = a.Name, Courses = a.Courses}))"
            );
        }

        [Fact]
        public void SelectMany()
        {
            //arrange
            var bodyParameter = new SelectManyOperatorParameters
            (
                new ParameterOperatorParameters(parameterName),
                new MemberSelectorOperatorParameters("Assignments", new ParameterOperatorParameters("a")),
                "a"
            );

            //act
            DoTest<CourseModel, Course, IQueryable<CourseAssignmentModel>, IQueryable<CourseAssignment>>
            (
                bodyParameter,
                parameterName,
                returnValue =>
                {
                    Assert.Equal(8, returnValue.Count());
                },
                "$it => $it.SelectMany(a => a.Assignments)"
            );
        }

        [Fact]
        public void Single_Filter_Throws_Exception()
        {
            //arrange
            var bodyParameter = new SingleOperatorParameters
            (
                new ParameterOperatorParameters(parameterName),
                new EqualsBinaryOperatorParameters
                (
                    new MemberSelectorOperatorParameters("DepartmentID", new ParameterOperatorParameters("a")),
                    new ConstantOperatorParameters(-1)
                ),
                "a"
            );

            //act
            Assert.Throws<AggregateException>
            (
                () => DoTest<DepartmentModel, Department, DepartmentModel, Department>
                (
                    bodyParameter,
                    parameterName,
                    returnValue => { },
                    "$it => $it.Single(a => (a.DepartmentID == -1))"
                )
            );
        }

        [Fact]
        public void Single_Filter_Returns_match()
        {
            //arrange
            var bodyParameter = new SingleOperatorParameters
            (
                new ParameterOperatorParameters(parameterName),
                new EqualsBinaryOperatorParameters
                (
                    new MemberSelectorOperatorParameters("DepartmentID", new ParameterOperatorParameters("a")),
                    new ConstantOperatorParameters(1)
                ),
                "a"
            );

            //act
            DoTest<DepartmentModel, Department, DepartmentModel, Department>
            (
                bodyParameter,
                parameterName,
                returnValue => Assert.Equal(1, returnValue.DepartmentID),
                "$it => $it.Single(a => (a.DepartmentID == 1))"
            );
        }

        [Fact]
        public void Single_with_multiple_matches_Throws_Exception()
        {
            //arrange
            var bodyParameter = new SingleOperatorParameters
            (
                new ParameterOperatorParameters(parameterName)
            );

            //act
            Assert.Throws<AggregateException>
            (
                () => DoTest<DepartmentModel, Department, DepartmentModel, Department>
                (
                    bodyParameter,
                    parameterName,
                    returnValue => { },
                    "$it => $it.Single()"
                )
            );
        }

        [Fact]
        public void Sum_Selector()
        {
            //arrange
            var bodyParameter = new SumOperatorParameters
            (
                new ParameterOperatorParameters(parameterName),
                new MemberSelectorOperatorParameters("DepartmentID", new ParameterOperatorParameters("a")),
                "a"
            );

            //act
            DoTest<DepartmentModel, Department, int, int>
            (
                bodyParameter,
                parameterName,
                returnValue => Assert.Equal(10, returnValue),
                "$it => $it.Sum(a => a.DepartmentID)"
            );
        }

        [Fact]
        public void Sum()
        {
            //arrange
            var bodyParameter = new SumOperatorParameters
            (
                new SelectOperatorParameters
                (
                    new ParameterOperatorParameters(parameterName),
                    new MemberSelectorOperatorParameters("DepartmentID", new ParameterOperatorParameters("a")),
                    "a"
                )
            );

            //act
            DoTest<DepartmentModel, Department, int, int>
            (
                bodyParameter,
                parameterName,
                returnValue => Assert.Equal(10, returnValue),
                "$it => $it.Select(a => a.DepartmentID).Sum()"
            );
        }

        [Fact]
        public void ToList()
        {
            //arrange
            var bodyParameter = new ToListOperatorParameters
            (
                new ParameterOperatorParameters(parameterName)
            );

            //act
            DoTest<DepartmentModel, Department, List<DepartmentModel>, List<Department>>
            (
                bodyParameter,
                parameterName,
                returnValue => Assert.Equal(4, returnValue.Count),
                "$it => $it.ToList()"
            );
        }

        [Fact]
        public void Where_with_matches()
        {
            //arrange
            var bodyParameter = new WhereOperatorParameters
            (
                new OrderByOperatorParameters
                (
                    new ParameterOperatorParameters(parameterName),
                    new MemberSelectorOperatorParameters("DepartmentID", new ParameterOperatorParameters("a")),
                    ListSortDirection.Descending,
                    "a"
                ),
                new NotEqualsBinaryOperatorParameters
                (
                    new MemberSelectorOperatorParameters("DepartmentID", new ParameterOperatorParameters("a")),
                    new ConstantOperatorParameters(1)
                ),
                "a"
            );

            //act
            DoTest<DepartmentModel, Department, IQueryable<DepartmentModel>, IQueryable<Department>>
            (
                bodyParameter,
                parameterName,
                returnValue => Assert.Equal(2, returnValue.Last().DepartmentID),
                "$it => $it.OrderByDescending(a => a.DepartmentID).Where(a => (a.DepartmentID != 1))"
            );
        }

        [Fact]
        public void Where_without_matches()
        {
            //arrange
            var bodyParameter = new WhereOperatorParameters
            (
                new ParameterOperatorParameters(parameterName),
                new EqualsBinaryOperatorParameters
                (
                    new MemberSelectorOperatorParameters("DepartmentID", new ParameterOperatorParameters("a")),
                    new ConstantOperatorParameters(-1)
                ),
                "a"
            );

            //act
            DoTest<DepartmentModel, Department, IQueryable<DepartmentModel>, IQueryable<Department>>
            (
                bodyParameter,
                parameterName,
                returnValue => Assert.Empty(returnValue),
                "$it => $it.Where(a => (a.DepartmentID == -1))"
            );
        }

        void DoTest<TModel, TData, TModelReturn, TDataReturn>(IExpressionParameter bodyParameter, string parameterName, Action<TModelReturn> assert, string expectedExpressionString, Parameters.Expansions.SelectExpandDefinitionParameters selectExpandDefinition = null) where TModel : LogicBuilder.Domain.BaseModel where TData : LogicBuilder.Data.BaseData
        {
            //arrange
            IMapper mapper = serviceProvider.GetRequiredService<IMapper>();
            ISchoolRepository repository = serviceProvider.GetRequiredService<ISchoolRepository>();
            IExpressionParameter expressionParameter = GetExpressionParameter<IQueryable<TModel>, TModelReturn>(bodyParameter, parameterName);

            TestExpressionString();
            TestReturnValue();

            void TestReturnValue()
            {
                //act
                TModelReturn returnValue = QueryOperations<TModel, TData, TModelReturn, TDataReturn>.Query
                (
                    repository,
                    mapper,
                    expressionParameter,
                    selectExpandDefinition
                );

                //assert
                assert(returnValue);
            }

            void TestExpressionString()
            {
                //act
                Expression<Func<IQueryable<TModel>, TModelReturn>> expression = QueryOperations<TModel, TData, TModelReturn, TDataReturn>.GetQueryFunc
                (
                    mapper.MapToOperator(expressionParameter)
                );

                //assert
                if (!string.IsNullOrEmpty(expectedExpressionString))
                {
                    AssertFilterStringIsCorrect(expression, expectedExpressionString);
                }
            }
        }
        #endregion Tests

        #region Helpers
        private IExpressionParameter GetExpressionParameter<T, TResult>(IExpressionParameter selectorBody, string parameterName = "$it")
            => new SelectorLambdaOperatorParameters
            (
                selectorBody,
                typeof(T),
                parameterName,
                typeof(TResult)
            );

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
                    cfg.AddProfile<SchoolProfile>();
                    cfg.AddProfile<ExpansionParameterToDescriptorMappingProfile>();
                    cfg.AddProfile<ExpansionDescriptorToOperatorMappingProfile>();
                });
            }
            MapperConfiguration.AssertConfigurationIsValid();
            serviceProvider = new ServiceCollection()
                .AddDbContext<SchoolContext>
                (
                    options => options.UseSqlServer
                    (
                        @"Server=(localdb)\mssqllocaldb;Database=SchoolContext2;ConnectRetryCount=0"
                    ),
                    ServiceLifetime.Transient
                )
                .AddTransient<ISchoolStore, SchoolStore>()
                .AddTransient<ISchoolRepository, SchoolRepository>()
                .AddSingleton<AutoMapper.IConfigurationProvider>
                (
                    MapperConfiguration
                )
                .AddTransient<IMapper>(sp => new Mapper(sp.GetRequiredService<AutoMapper.IConfigurationProvider>(), sp.GetService))
                .BuildServiceProvider();

            SchoolContext context = serviceProvider.GetRequiredService<SchoolContext>();
            //context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            DatabaseSeeder.Seed_Database(serviceProvider.GetRequiredService<ISchoolRepository>()).Wait();
        }
        #endregion Helper
    }
}
