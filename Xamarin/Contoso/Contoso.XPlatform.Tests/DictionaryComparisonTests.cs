using Contoso.XPlatform.Utils;
using System;
using System.Collections.Generic;
using Xunit;

namespace Contoso.XPlatform.Tests
{
    public class DictionaryComparisonTests
    {
        public DictionaryComparisonTests()
        {
            Initialize();
        }

        #region Fields
        private Dictionary<string, object> first;
        private Dictionary<string, object> second;
        #endregion Fields

        [Fact]
        public void ShouldReturnTrueForMatchingLists()
        {
            Dictionary<string, object> first = new Dictionary<string, object>
            {
                ["list"] = new List<int> { 1, 2, 3 }
            };

            Dictionary<string, object> second = new Dictionary<string, object>
            {
                ["list"] = new List<int> { 1, 2, 3 }
            };

            Assert.True(new DictionaryComparer().Equals(first, second));
        }

        [Fact]
        public void ShouldReturnFalseForListsOutOfOrder()
        {
            Dictionary<string, object> first = new Dictionary<string, object>
            {
                ["list"] = new List<int> { 1, 2, 3 }
            };

            Dictionary<string, object> second = new Dictionary<string, object>
            {
                ["list"] = new List<int> { 1, 3, 2 }
            };

            Assert.False(new DictionaryComparer().Equals(first, second));
        }

        [Fact]
        public void ShouldReturnTrueIfAllFieldsMatch()
        {
            Assert.True(new DictionaryComparer().Equals(first, second));
        }

        [Fact]
        public void ShouldReturnFalseValuesDoNotMatch()
        {
            first["ID"] = 3;
            second["ID"] = 4;
            Assert.False(new DictionaryComparer().Equals(first, second));
        }

        [Fact]
        public void ShouldReturnFalseNestedValuesDoNotMatch()
        {
            ((Dictionary<string, object>)first["OfficeAssignment"])["Location"] = "Location1";
            ((Dictionary<string, object>)second["OfficeAssignment"])["Location"] = "Location2";
            Assert.False(new DictionaryComparer().Equals(first, second));
        }

        [Fact]
        public void ShouldReturnFalseForMisMatchInAChildCollectionValue()
        {
            var firstCoursesList = (List<Dictionary<string, object>>)first["Courses"];
            var secondCoursesList = (List<Dictionary<string, object>>)first["Courses"];

            firstCoursesList[0]["CourseTitle"] = "Chemistry";
            secondCoursesList[0]["CourseTitle"] = "Chem";
            Assert.False(new DictionaryComparer().Equals(first, second));
        }

        [Fact]
        public void ShouldReturnFalseForMisMatchInAChildCollectionCount()
        {
            var firstCoursesList = (List<Dictionary<string, object>>)first["Courses"];
            firstCoursesList.Add(new Dictionary<string, object> { });
            Assert.False(new DictionaryComparer().Equals(first, second));
        }

        private void Initialize()
        {
            first = new Dictionary<string, object>
            {
                ["ID"] = 3,
                ["FirstName"] = "John",
                ["LastName"] = "Smith",
                ["HireDate"] = new DateTime(2021, 5, 20),
                ["OfficeAssignment"] = new Dictionary<string, object>
                {
                    ["Location"] = "Location1"
                },
                ["Courses"] = new List<Dictionary<string, object>>
                {
                    new Dictionary<string, object>
                    {
                        ["CourseID"] = 1,
                        ["InstructorID"] = 3,
                        ["CourseTitle"] = "Chemistry"
                    },
                    new Dictionary<string, object>
                    {
                        ["CourseID"] = 2,
                        ["InstructorID"] = 3,
                        ["CourseTitle"] = "Physics"
                    },
                    new Dictionary<string, object>
                    {
                        ["CourseID"] = 3,
                        ["InstructorID"] = 3,
                        ["CourseTitle"] = "Mathematics"
                    }
                }
            };

            second = new Dictionary<string, object>
            {
                ["ID"] = 3,
                ["FirstName"] = "John",
                ["LastName"] = "Smith",
                ["HireDate"] = new DateTime(2021, 5, 20),
                ["OfficeAssignment"] = new Dictionary<string, object>
                {
                    ["Location"] = "Location1"
                },
                ["Courses"] = new List<Dictionary<string, object>>
                {
                    new Dictionary<string, object>
                    {
                        ["CourseID"] = 1,
                        ["InstructorID"] = 3,
                        ["CourseTitle"] = "Chemistry"
                    },
                    new Dictionary<string, object>
                    {
                        ["CourseID"] = 2,
                        ["InstructorID"] = 3,
                        ["CourseTitle"] = "Physics"
                    },
                    new Dictionary<string, object>
                    {
                        ["CourseID"] = 3,
                        ["InstructorID"] = 3,
                        ["CourseTitle"] = "Mathematics"
                    }
                }
            };
        }
    }
}
