using AutoMapper;
using Enrollment.AutoMapperProfiles;
using Enrollment.Forms.Parameters;
using Enrollment.Forms.Parameters.Input;
using Enrollment.Forms.View.Input;
using LogicBuilder.Forms.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace UnitTests
{
    public class InputFormMappingTests
    {
        public InputFormMappingTests(Xunit.Abstractions.ITestOutputHelper output)
        {
            this.output = output;
            SetupAutoMapper();
        }

        #region Fields
        //int listCount = 1000000;
        readonly int listCount = 100;
        static IMapper mapper;
        private readonly Xunit.Abstractions.ITestOutputHelper output;
        #endregion Fields

        #region Tests
        [Fact]
        public void Map_InputFormParameters_To_InputFormView()
        {
            //Arrange
            InputFormParameters form = new InputFormParameters
                (
                    new FormDataParameters("Application Survey"),
                    new List<InputRowParameters>
                    {
                        new InputRowParameters
                        (
                            1, new RowDataParameters("Row 1", true, "Row 1 tool tip"),
                            new List<InputColumnParameters>
                            {
                                new InputColumnParameters
                                (
                                    11, new ColumnDataParameters("Column 11", true, "col-md-12", "Column 11 tool tip"),
                                    new List<BaseInputQuestionParameters>
                                    {
                                        new InputQuestionParameters<Boolean>(1, "IsAtHome", true, new InputDataParameters
                                        (
                                            "Is person at home?",
                                            "col-md-12",
                                            "ChackBox",
                                            null,
                                            null,
                                            null,
                                            null
                                        )),
                                        new InputQuestionParameters<Boolean>(2, "OtherVariable", true, new InputDataParameters
                                        (
                                            "Is person at home?",
                                            "col-md-12",
                                            "ChackBox",
                                            null,
                                            null,
                                            null,
                                            null
                                        ))
                                    },
                                    new List<InputRowParameters> { }
                                ),
                                new InputColumnParameters
                                (
                                    12, new ColumnDataParameters("Column 12", true, "col-md-12", "Column 12 tool tip"),
                                    new List<BaseInputQuestionParameters>
                                    {
                                        new InputQuestionParameters<Boolean>(3, "IsAtSchool", true, new InputDataParameters
                                        (
                                            "Is person at home?",
                                            "col-md-12",
                                            "ChackBox",
                                            null,
                                            null,
                                            null,
                                            null
                                        )),
                                        new InputQuestionParameters<Boolean>(4, "FourthVariable", true, new InputDataParameters
                                        (
                                            "Is person at home?",
                                            "col-md-12",
                                            "ChackBox",
                                            null,
                                            null,
                                            null,
                                            null
                                        ))
                                    },
                                    new List<InputRowParameters> { }
                                ),
                                new InputColumnParameters
                                (
                                    11, new ColumnDataParameters("Column 11", true, "col-md-12", "Column 11 tool tip"),
                                    new List<BaseInputQuestionParameters>
                                    {
                                        new InputQuestionParameters<Boolean>(1, "IsAtHome", true, new InputDataParameters
                                        (
                                            "Is person at home?",
                                            "col-md-12",
                                            "ChackBox",
                                            null,
                                            null,
                                            null,
                                            null
                                        )),
                                        new InputQuestionParameters<Boolean>(2, "OtherVariable", true, new InputDataParameters
                                        (
                                            "Is person at home?",
                                            "col-md-12",
                                            "ChackBox",
                                            null,
                                            null,
                                            null,
                                            null
                                        ))
                                    },
                                    new List<InputRowParameters> { }
                                ),
                                new InputColumnParameters
                                (
                                    12, new ColumnDataParameters("Column 12", true, "col-md-12", "Column 12 tool tip"),
                                    new List<BaseInputQuestionParameters>
                                    {
                                        new InputQuestionParameters<Boolean>(3, "IsAtSchool", true, new InputDataParameters
                                        (
                                            "Is person at home?",
                                            "col-md-12",
                                            "ChackBox",
                                            null,
                                            null,
                                            null,
                                            null
                                        )),
                                        new InputQuestionParameters<Boolean>(4, "FourthVariable", true, new InputDataParameters
                                        (
                                            "Is person at home?",
                                            "col-md-12",
                                            "ChackBox",
                                            null,
                                            null,
                                            null,
                                            null
                                        )),
                                        new InputQuestionParameters<ICollection<Enrollment.Domain.Entities.StateLivedInModel>>(5, "FifthVariable", new System.Collections.ObjectModel.Collection<Enrollment.Domain.Entities.StateLivedInModel> { new Enrollment.Domain.Entities.StateLivedInModel { State = "OH" } }, new InputDataParameters
                                        (
                                            "Is person at home?",
                                            "col-md-12",
                                            "ChackBox",
                                            null,
                                            null,
                                            null,
                                            null
                                        ))
                                    },
                                    new List<InputRowParameters> { }
                                )
                            }
                        )
                    }
                );

            System.Diagnostics.Stopwatch stopWatch = System.Diagnostics.Stopwatch.StartNew();

            //Act
            InputFormView vm = null;
            for (int i = 0; i < listCount; i++)
                vm = mapper.Map<InputFormView>(form);
            stopWatch.Stop();
            this.output.WriteLine("Map_InputFormParameters_To_InputFormView = {0}", stopWatch.Elapsed.TotalMilliseconds);

            //Assert
            Assert.Single(vm.Rows);
            Assert.Equal("Application Survey", vm.Title);
            Assert.Equal("Row 1", vm.Rows.ToList()[0].Title);
            Assert.True(vm.Rows.ToList()[0].Columns.Count == 4);
            Assert.Equal("Column 11", vm.Rows.ToList()[0].Columns.ToList()[0].Title);
            Assert.True(vm.Rows.ToList()[0].Columns.ToList()[0].Questions.Count == 2);
            Assert.True(vm.Rows.ToList()[0].Columns.ToList()[0].Questions.ToList()[0].ToolTipText == "ChackBox");
        }

        [Fact]
        public void Map_InputFormView_To_InputFormParameters()
        {
            //Arrange
            InputFormView vm = new InputFormView
            {
                Title = "Application Survey",
                Rows = new List<InputRowView>
                {
                    new InputRowView
                    {
                        Id = 1,
                        Title ="Row 1",
                        ShowTitle = true,
                        ToolTipText = "Row 1 tool tip",
                        Columns = new List<InputColumnView>
                        {
                            new InputColumnView
                            {
                                Id = 11,
                                ColumnShare="col-md-12",
                                Title="Column 11",
                                ShowTitle = true,
                                ToolTipText="Column 11 tool tip",
                                Questions = new List<BaseInputView>
                                {
                                    new InputView<bool>
                                    {
                                        Id = 111,
                                        Text = "Are you at school?",
                                        ToolTipText = "Are you at school tool tip",
                                        ClassAttribute = "col-md-2",
                                        CurrentValue = true,
                                        Directives = null,
                                        VariableName = "IsAtSchool"
                                    },
                                    new InputView<bool>
                                    {
                                        Id = 112,
                                        Text = "Are you at home?",
                                        ToolTipText = "Is it true tool tip",
                                        ClassAttribute = "col-md-2",
                                        CurrentValue = true,
                                        Directives = null,
                                        VariableName = "IsAtHome"
                                    }
                                }
                            },
                            new InputColumnView
                            {
                                Id = 12,
                                ColumnShare="col-md-12",
                                Title="Column 12",
                                ShowTitle = true,
                                ToolTipText="Column 12 tool tip",
                                Questions = new List<BaseInputView>
                                {
                                    new InputView<bool>
                                    {
                                        Id = 121,
                                        Text = "Third Question?",
                                        ToolTipText = "Is it true tool tip",
                                        ClassAttribute = "col-md-2",
                                        CurrentValue = true,
                                        Directives = new List<Enrollment.Forms.View.Common.DirectiveView>
                                        {
                                            new Enrollment.Forms.View.Common.DirectiveView
                                            {
                                                ConditionGroup = new Enrollment.Forms.View.Common.ConditionGroupView
                                                {
                                                    Conditions = new List<Enrollment.Forms.View.Common.ConditionView>
                                                    {
                                                        new Enrollment.Forms.View.Common.ConditionView
                                                        {
                                                            LeftVariable = "NNN",
                                                                Operator= "eqyals",
                                                                Value = "VVV"
                                                        },
                                                        new Enrollment.Forms.View.Common.ConditionView
                                                        {
                                                            LeftVariable = "NNN",
                                                                Operator= "eqyals",
                                                                Value = "VVV"
                                                        }
                                                    }
                                                }
                                            }
                                        },
                                        VariableName = "IsThirdTrue"
                                    },
                                    new InputView<bool>
                                    {
                                        Id = 122,
                                        Text = "Fourth Question?",
                                        ToolTipText = "Is it true tool tip",
                                        ClassAttribute = "col-md-2",
                                        CurrentValue = true,
                                        Directives = null,
                                        VariableName = "IsFourthTrue"
                                    }
                                }
                            },
                            new InputColumnView
                            {
                                Id = 11,
                                ColumnShare="col-md-12",
                                Title="Column 11",
                                ShowTitle = true,
                                ToolTipText="Column 11 tool tip",
                                Questions = new List<BaseInputView>
                                {
                                    new InputView<bool>
                                    {
                                        Id = 111,
                                        Text = "Are you at school?",
                                        ToolTipText = "Are you at school tool tip",
                                        ClassAttribute = "col-md-2",
                                        CurrentValue = true,
                                        Directives = null,
                                        VariableName = "IsAtSchool"
                                    },
                                    new InputView<bool>
                                    {
                                        Id = 112,
                                        Text = "Are you at home?",
                                        ToolTipText = "Is it true tool tip",
                                        ClassAttribute = "col-md-2",
                                        CurrentValue = true,
                                        Directives = null,
                                        VariableName = "IsAtHome"
                                    }
                                }
                            },
                            new InputColumnView
                            {
                                Id = 12,
                                ColumnShare="col-md-12",
                                Title="Column 12",
                                ShowTitle = true,
                                ToolTipText="Column 12 tool tip",
                                Questions = new List<BaseInputView>
                                {
                                    new InputView<bool>
                                    {
                                        Id = 121,
                                        Text = "Third Question?",
                                        ToolTipText = "Is it true tool tip",
                                        ClassAttribute = "col-md-2",
                                        CurrentValue = true,
                                        Directives = new List<Enrollment.Forms.View.Common.DirectiveView>
                                        {
                                            new Enrollment.Forms.View.Common.DirectiveView
                                            {
                                                ConditionGroup = new Enrollment.Forms.View.Common.ConditionGroupView
                                                {
                                                    Conditions = new List<Enrollment.Forms.View.Common.ConditionView>
                                                    {
                                                        new Enrollment.Forms.View.Common.ConditionView
                                                        {
                                                            LeftVariable = "NNN",
                                                                Operator= "eqyals",
                                                                Value = "VVV"
                                                        },
                                                        new Enrollment.Forms.View.Common.ConditionView
                                                        {
                                                            LeftVariable = "NNN",
                                                                Operator= "eqyals",
                                                                Value = "VVV"
                                                        }
                                                    }
                                                }
                                            }
                                        },
                                        VariableName = "IsThirdTrue"
                                    },
                                    new InputView<bool>
                                    {
                                        Id = 122,
                                        Text = "Fourth Question?",
                                        ToolTipText = "Is it true tool tip",
                                        ClassAttribute = "col-md-2",
                                        CurrentValue = true,
                                        Directives = null,
                                        VariableName = "IsFourthTrue"
                                    }
                                }
                            }
                        }
                    }
                }
            };

            System.Diagnostics.Stopwatch stopWatch = System.Diagnostics.Stopwatch.StartNew();

            //Act
            InputFormParameters form = null;
            for (int i = 0; i < listCount; i++)
                form = mapper.Map<InputFormParameters>(vm);
            stopWatch.Stop();
            this.output.WriteLine("Map_InputFormViewModel_To_InputFormParameterss = {0}", stopWatch.Elapsed.TotalMilliseconds);

            //Assert
            Assert.Single(form.Rows);
            Assert.Equal("Application Survey", ((FormDataParameters)form.FormData).Title);
            Assert.Equal("Row 1", ((RowDataParameters)form.Rows.ToList()[0].RowData).Title);
            Assert.True(form.Rows.ToList()[0].Columns.Count == 4);
            Assert.Equal("Column 11", ((ColumnDataParameters)form.Rows.ToList()[0].Columns.ToList()[0].ColumnData).Title);
            Assert.True(form.Rows.ToList()[0].Columns.ToList()[0].Questions.Count == 2);
            Assert.True(((BaseDataParameters)form.Rows.ToList()[0].Columns.ToList()[0].Questions.ToList()[0].QuestionData).ToolTipText == "Are you at school tool tip");


        }
        #endregion Tests

        #region Methods
        private static void SetupAutoMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(typeof(InputFormMappingProfile));
                cfg.AddMaps(typeof(InputVariablesMappingProfile));
            });

            config.AssertConfigurationIsValid<InputFormMappingProfile>();
            config.AssertConfigurationIsValid<InputVariablesMappingProfile>();
            mapper = config.CreateMapper();
        }
        #endregion Methods
    }
}
