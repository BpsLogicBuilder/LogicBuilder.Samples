using AutoMapper;
using Contoso.AutoMapperProfiles;
using Contoso.Forms.Parameters;
using Contoso.Forms.Parameters.Common;
using Contoso.Forms.Parameters.Input;
using Contoso.Forms.View.Input;
using LogicBuilder.Forms.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace UnitTests
{
    public class ValidationTests
    {
        public ValidationTests()
        {
            SetupAutoMapper();
        }
        #region Fields
        static IMapper mapper;
        #endregion Fields

        #region Tests
        [Fact]
        public void Validate_range()
        {
            //Arrange
            InputFormParameters form = new InputFormParameters
            (
                new FormDataParameters
                (
                    "Application Survey",
                    new List<ValidationMessageParameters>
                    {
                        new ValidationMessageParameters
                        (
                            "Height",
                            new List<ValidationMethodParameters>
                            {
                                new ValidationMethodParameters("range", "Height must be between 3 and 8 inclusive")
                            }
                        ),
                        new ValidationMessageParameters
                        (
                            "Width",
                            new List<ValidationMethodParameters>
                            {
                                new ValidationMethodParameters("range", "Width must be between 2 and 7 inclusive")
                            }
                        )
                    }
                ),
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
                                    new InputQuestionParameters<double>(1, "Height", 7.1, new InputDataParameters
                                    (
                                        "Enter float value?",
                                        "col-md-12",
                                        "TextBox",
                                        string.Empty,
                                        string.Empty,
                                        null,
                                        null,
                                        null,
                                        null,
                                        new FormValidationSettingParameters
                                        (
                                            null,
                                            new List<ValidatorDescriptionParameters>
                                            {
                                                new ValidatorDescriptionParameters
                                                (
                                                    "NumberValidators",
                                                    "range",
                                                    new List<ValidatorArgumentParameters>
                                                    {
                                                        new ValidatorArgumentParameters("min", 3),
                                                        new ValidatorArgumentParameters("max", 8)
                                                    }
                                                )
                                            }
                                        )
                                    )),
                                    new InputQuestionParameters<double>(1, "Width", 7.1, new InputDataParameters
                                    (
                                        "Enter float value?",
                                        "col-md-12",
                                        "TextBox",
                                        string.Empty,
                                        string.Empty,
                                        null,
                                        null,
                                        null,
                                        null,
                                        new FormValidationSettingParameters
                                        (
                                            null,
                                            new List<ValidatorDescriptionParameters>
                                            {
                                                new ValidatorDescriptionParameters
                                                (
                                                    "NumberValidators",
                                                    "range",
                                                    new List<ValidatorArgumentParameters>
                                                    {
                                                        new ValidatorArgumentParameters("min", 2),
                                                        new ValidatorArgumentParameters("max", 7)
                                                    }
                                                )
                                            }
                                        )
                                    ))
                                },
                                new List<InputRowParameters> { }
                            )
                        }
                    )
                }
            );

            //Act
            InputFormView vm = mapper.Map<InputFormView>(form);
            vm.Rows.ToList()[0].Columns.ToList()[0].Questions.ToList()[0].Validate(vm.ValidationMessages);
            vm.Rows.ToList()[0].Columns.ToList()[0].Questions.ToList()[1].Validate(vm.ValidationMessages);

            //Assert
            Assert.False(vm.Rows.ToList()[0].Columns.ToList()[0].Questions.ToList()[0].HasErrors);
            Assert.True(vm.Rows.ToList()[0].Columns.ToList()[0].Questions.ToList()[1].HasErrors);
            Assert.Equal("Width must be between 2 and 7 inclusive", vm.Rows.ToList()[0].Columns.ToList()[0].Questions.ToList()[1].Errors[0]);
        }

        [Fact]
        public void Validate_mustBeANumber()
        {
            //Arrange
            InputFormParameters form = new InputFormParameters
            (
                new FormDataParameters
                (
                    "Application Survey",
                    new List<ValidationMessageParameters>
                    {
                        new ValidationMessageParameters
                        (
                            "Height",
                            new List<ValidationMethodParameters>
                            {
                                new ValidationMethodParameters("mustBeANumber", "Height must be a number.")
                            }
                        ),
                        new ValidationMessageParameters
                        (
                            "Width",
                            new List<ValidationMethodParameters>
                            {
                                new ValidationMethodParameters("mustBeANumber", "Width must be a number.")
                            }
                        )
                    }
                ),
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
                                    new InputQuestionParameters<string>(1, "Height", "7.1", new InputDataParameters
                                    (
                                        "Enter float value?",
                                        "col-md-12",
                                        "TextBox",
                                        string.Empty,
                                        string.Empty,
                                        null,
                                        null,
                                        null,
                                        null,
                                        new FormValidationSettingParameters
                                        (
                                            null,
                                            new List<ValidatorDescriptionParameters>
                                            {
                                                new ValidatorDescriptionParameters
                                                (
                                                    "NumberValidators",
                                                    "mustBeANumber"
                                                )
                                            }
                                        )
                                    )),
                                    new InputQuestionParameters<string>(1, "Width", "a string", new InputDataParameters
                                    (
                                        "Enter float value?",
                                        "col-md-12",
                                        "TextBox",
                                        string.Empty,
                                        string.Empty,
                                        null,
                                        null,
                                        null,
                                        null,
                                        new FormValidationSettingParameters
                                        (
                                            null,
                                            new List<ValidatorDescriptionParameters>
                                            {
                                                new ValidatorDescriptionParameters
                                                (
                                                    "NumberValidators",
                                                    "mustBeANumber"
                                                )
                                            }
                                        )
                                    ))
                                },
                                new List<InputRowParameters> { }
                            )
                        }
                    )
                }
            );

            //Act
            InputFormView vm = mapper.Map<InputFormView>(form);
            vm.Rows.ToList()[0].Columns.ToList()[0].Questions.ToList()[0].Validate(vm.ValidationMessages);
            vm.Rows.ToList()[0].Columns.ToList()[0].Questions.ToList()[1].Validate(vm.ValidationMessages);

            //Assert
            Assert.False(vm.Rows.ToList()[0].Columns.ToList()[0].Questions.ToList()[0].HasErrors);
            Assert.True(vm.Rows.ToList()[0].Columns.ToList()[0].Questions.ToList()[1].HasErrors);
            Assert.Equal("Width must be a number.", vm.Rows.ToList()[0].Columns.ToList()[0].Questions.ToList()[1].Errors[0]);
        }

        [Fact]
        public void Validate_min()
        {
            //Arrange
            InputFormParameters form = new InputFormParameters
            (
                new FormDataParameters
                (
                    "Application Survey",
                    new List<ValidationMessageParameters>
                    {
                        new ValidationMessageParameters
                        (
                            "Height",
                            new List<ValidationMethodParameters>
                            {
                                new ValidationMethodParameters("min", "Height must be between 3 and 8 inclusive")
                            }
                        ),
                        new ValidationMessageParameters
                        (
                            "Width",
                            new List<ValidationMethodParameters>
                            {
                                new ValidationMethodParameters("min", "Width must be between 2 and 7 inclusive")
                            }
                        )
                    }
                ),
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
                                    new InputQuestionParameters<double>(1, "Height", 7.1, new InputDataParameters
                                    (
                                        "Enter float value?",
                                        "col-md-12",
                                        "TextBox",
                                        string.Empty,
                                        string.Empty,
                                        null,
                                        null,
                                        null,
                                        null,
                                        new FormValidationSettingParameters
                                        (
                                            null,
                                            new List<ValidatorDescriptionParameters>
                                            {
                                                new ValidatorDescriptionParameters
                                                (
                                                    "Validators",
                                                    "min",
                                                    new List<ValidatorArgumentParameters>
                                                    {
                                                        new ValidatorArgumentParameters("min", 3)
                                                    }
                                                )
                                            }
                                        )
                                    )),
                                    new InputQuestionParameters<double>(1, "Width", 1, new InputDataParameters
                                    (
                                        "Enter float value?",
                                        "col-md-12",
                                        "TextBox",
                                        string.Empty,
                                        string.Empty,
                                        null,
                                        null,
                                        null,
                                        null,
                                        new FormValidationSettingParameters
                                        (
                                            null,
                                            new List<ValidatorDescriptionParameters>
                                            {
                                                new ValidatorDescriptionParameters
                                                (
                                                    "Validators",
                                                    "min",
                                                    new List<ValidatorArgumentParameters>
                                                    {
                                                        new ValidatorArgumentParameters("min", 2)
                                                    }
                                                )
                                            }
                                        )
                                    ))
                                },
                                new List<InputRowParameters> { }
                            )
                        }
                    )
                }
            );

            //Act
            InputFormView vm = mapper.Map<InputFormView>(form);
            vm.Rows.ToList()[0].Columns.ToList()[0].Questions.ToList()[0].Validate(vm.ValidationMessages);
            vm.Rows.ToList()[0].Columns.ToList()[0].Questions.ToList()[1].Validate(vm.ValidationMessages);

            //Assert
            Assert.False(vm.Rows.ToList()[0].Columns.ToList()[0].Questions.ToList()[0].HasErrors);
            Assert.True(vm.Rows.ToList()[0].Columns.ToList()[0].Questions.ToList()[1].HasErrors);
            Assert.Equal("Width must be between 2 and 7 inclusive", vm.Rows.ToList()[0].Columns.ToList()[0].Questions.ToList()[1].Errors[0]);
        }

        [Fact]
        public void Validate_max()
        {
            //Arrange
            InputFormParameters form = new InputFormParameters
            (
                new FormDataParameters
                (
                    "Application Survey",
                    new List<ValidationMessageParameters>
                    {
                        new ValidationMessageParameters
                        (
                            "Height",
                            new List<ValidationMethodParameters>
                            {
                                new ValidationMethodParameters("max", "Height must be between 3 and 8 inclusive")
                            }
                        ),
                        new ValidationMessageParameters
                        (
                            "Width",
                            new List<ValidationMethodParameters>
                            {
                                new ValidationMethodParameters("max", "Width must be between 2 and 7 inclusive")
                            }
                        )
                    }
                ),
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
                                    new InputQuestionParameters<double>(1, "Height", 7.1, new InputDataParameters
                                    (
                                        "Enter float value?",
                                        "col-md-12",
                                        "TextBox",
                                        string.Empty,
                                        string.Empty,
                                        null,
                                        null,
                                        null,
                                        null,
                                        new FormValidationSettingParameters
                                        (
                                            null,
                                            new List<ValidatorDescriptionParameters>
                                            {
                                                new ValidatorDescriptionParameters
                                                (
                                                    "Validators",
                                                    "max",
                                                    new List<ValidatorArgumentParameters>
                                                    {
                                                        new ValidatorArgumentParameters("max", 8)
                                                    }
                                                )
                                            }
                                        )
                                    )),
                                    new InputQuestionParameters<double>(1, "Width", 4, new InputDataParameters
                                    (
                                        "Enter float value?",
                                        "col-md-12",
                                        "TextBox",
                                        string.Empty,
                                        string.Empty,
                                        null,
                                        null,
                                        null,
                                        null,
                                        new FormValidationSettingParameters
                                        (
                                            null,
                                            new List<ValidatorDescriptionParameters>
                                            {
                                                new ValidatorDescriptionParameters
                                                (
                                                    "Validators",
                                                    "max",
                                                    new List<ValidatorArgumentParameters>
                                                    {
                                                        new ValidatorArgumentParameters("max", 2)
                                                    }
                                                )
                                            }
                                        )
                                    ))
                                },
                                new List<InputRowParameters> { }
                            )
                        }
                    )
                }
            );

            //Act
            InputFormView vm = mapper.Map<InputFormView>(form);
            vm.Rows.ToList()[0].Columns.ToList()[0].Questions.ToList()[0].Validate(vm.ValidationMessages);
            vm.Rows.ToList()[0].Columns.ToList()[0].Questions.ToList()[1].Validate(vm.ValidationMessages);

            //Assert
            Assert.False(vm.Rows.ToList()[0].Columns.ToList()[0].Questions.ToList()[0].HasErrors);
            Assert.True(vm.Rows.ToList()[0].Columns.ToList()[0].Questions.ToList()[1].HasErrors);
            Assert.Equal("Width must be between 2 and 7 inclusive", vm.Rows.ToList()[0].Columns.ToList()[0].Questions.ToList()[1].Errors[0]);
        }

        [Fact]
        public void Validate_email()
        {
            //Arrange
            InputFormParameters form = new InputFormParameters
            (
                new FormDataParameters
                (
                    "Application Survey",
                    new List<ValidationMessageParameters>
                    {
                        new ValidationMessageParameters
                        (
                            "HomeEmail",
                            new List<ValidationMethodParameters>
                            {
                                new ValidationMethodParameters("email", "Must be a valid email address.")
                            }
                        ),
                        new ValidationMessageParameters
                        (
                            "WorkEmail",
                            new List<ValidationMethodParameters>
                            {
                                new ValidationMethodParameters("email", "Must be a valid email address.")
                            }
                        )
                    }
                ),
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
                                    new InputQuestionParameters<string>(1, "HomeEmail", "bt@hotmail.com", new InputDataParameters
                                    (
                                        "Enter float value?",
                                        "col-md-12",
                                        "TextBox",
                                        string.Empty,
                                        string.Empty,
                                        null,
                                        null,
                                        null,
                                        null,
                                        new FormValidationSettingParameters
                                        (
                                            null,
                                            new List<ValidatorDescriptionParameters>
                                            {
                                                new ValidatorDescriptionParameters
                                                (
                                                    "Validators",
                                                    "email"
                                                )
                                            }
                                        )
                                    )),
                                    new InputQuestionParameters<string>(1, "WorkEmail", "a string", new InputDataParameters
                                    (
                                        "Enter float value?",
                                        "col-md-12",
                                        "TextBox",
                                        string.Empty,
                                        string.Empty,
                                        null,
                                        null,
                                        null,
                                        null,
                                        new FormValidationSettingParameters
                                        (
                                            null,
                                            new List<ValidatorDescriptionParameters>
                                            {
                                                new ValidatorDescriptionParameters
                                                (
                                                    "Validators",
                                                    "email"
                                                )
                                            }
                                        )
                                    ))
                                },
                                new List<InputRowParameters> { }
                            )
                        }
                    )
                }
            );

            //Act
            InputFormView vm = mapper.Map<InputFormView>(form);
            vm.Rows.ToList()[0].Columns.ToList()[0].Questions.ToList()[0].Validate(vm.ValidationMessages);
            vm.Rows.ToList()[0].Columns.ToList()[0].Questions.ToList()[1].Validate(vm.ValidationMessages);

            //Assert
            Assert.False(vm.Rows.ToList()[0].Columns.ToList()[0].Questions.ToList()[0].HasErrors);
            Assert.True(vm.Rows.ToList()[0].Columns.ToList()[0].Questions.ToList()[1].HasErrors);
            Assert.Equal("Must be a valid email address.", vm.Rows.ToList()[0].Columns.ToList()[0].Questions.ToList()[1].Errors[0]);
        }

        [Fact]
        public void Validate_required()
        {
            //Arrange
            InputFormParameters form = new InputFormParameters
            (
                new FormDataParameters
                (
                    "Application Survey",
                    new List<ValidationMessageParameters>
                    {
                        new ValidationMessageParameters
                        (
                            "HomeEmail",
                            new List<ValidationMethodParameters>
                            {
                                new ValidationMethodParameters("required", "Must be a valid email address.")
                            }
                        ),
                        new ValidationMessageParameters
                        (
                            "WorkEmail",
                            new List<ValidationMethodParameters>
                            {
                                new ValidationMethodParameters("required", "Work email is required.")
                            }
                        )
                    }
                ),
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
                                    new InputQuestionParameters<string>(1, "HomeEmail", "bt@hotmail.com", new InputDataParameters
                                    (
                                        "Enter float value?",
                                        "col-md-12",
                                        "TextBox",
                                        string.Empty,
                                        string.Empty,
                                        null,
                                        null,
                                        null,
                                        null,
                                        new FormValidationSettingParameters
                                        (
                                            null,
                                            new List<ValidatorDescriptionParameters>
                                            {
                                                new ValidatorDescriptionParameters
                                                (
                                                    "Validators",
                                                    "required"
                                                )
                                            }
                                        )
                                    )),
                                    new InputQuestionParameters<string>(1, "WorkEmail", null, new InputDataParameters
                                    (
                                        "Enter float value?",
                                        "col-md-12",
                                        "TextBox",
                                        string.Empty,
                                        string.Empty,
                                        null,
                                        null,
                                        null,
                                        null,
                                        new FormValidationSettingParameters
                                        (
                                            null,
                                            new List<ValidatorDescriptionParameters>
                                            {
                                                new ValidatorDescriptionParameters
                                                (
                                                    "Validators",
                                                    "required"
                                                )
                                            }
                                        )
                                    ))
                                },
                                new List<InputRowParameters> { }
                            )
                        }
                    )
                }
            );

            //Act
            InputFormView vm = mapper.Map<InputFormView>(form);
            vm.Rows.ToList()[0].Columns.ToList()[0].Questions.ToList()[0].Validate(vm.ValidationMessages);
            vm.Rows.ToList()[0].Columns.ToList()[0].Questions.ToList()[1].Validate(vm.ValidationMessages);

            //Assert
            Assert.False(vm.Rows.ToList()[0].Columns.ToList()[0].Questions.ToList()[0].HasErrors);
            Assert.True(vm.Rows.ToList()[0].Columns.ToList()[0].Questions.ToList()[1].HasErrors);
            Assert.Equal("Work email is required.", vm.Rows.ToList()[0].Columns.ToList()[0].Questions.ToList()[1].Errors[0]);
        }

        [Fact]
        public void Validate_requiredTrue()
        {
            //Arrange
            InputFormParameters form = new InputFormParameters
            (
                new FormDataParameters
                (
                    "Application Survey",
                    new List<ValidationMessageParameters>
                    {
                        new ValidationMessageParameters
                        (
                            "HasHomeEmail",
                            new List<ValidationMethodParameters>
                            {
                                new ValidationMethodParameters("required", "Has Home Email must be checked.")
                            }
                        ),
                        new ValidationMessageParameters
                        (
                            "HasWorkEmail",
                            new List<ValidationMethodParameters>
                            {
                                new ValidationMethodParameters("required", "Has Work Email must be checked.")
                            }
                        )
                    }
                ),
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
                                    new InputQuestionParameters<bool>(1, "HasHomeEmail", true, new InputDataParameters
                                    (
                                        "Enter float value?",
                                        "col-md-12",
                                        "TextBox",
                                        string.Empty,
                                        string.Empty,
                                        null,
                                        null,
                                        null,
                                        null,
                                        new FormValidationSettingParameters
                                        (
                                            null,
                                            new List<ValidatorDescriptionParameters>
                                            {
                                                new ValidatorDescriptionParameters
                                                (
                                                    "Validators",
                                                    "requiredTrue"
                                                )
                                            }
                                        )
                                    )),
                                    new InputQuestionParameters<bool>(1, "HasWorkEmail", false, new InputDataParameters
                                    (
                                        "Enter float value?",
                                        "col-md-12",
                                        "TextBox",
                                        string.Empty,
                                        string.Empty,
                                        null,
                                        null,
                                        null,
                                        null,
                                        new FormValidationSettingParameters
                                        (
                                            null,
                                            new List<ValidatorDescriptionParameters>
                                            {
                                                new ValidatorDescriptionParameters
                                                (
                                                    "Validators",
                                                    "requiredTrue"
                                                )
                                            }
                                        )
                                    ))
                                },
                                new List<InputRowParameters> { }
                            )
                        }
                    )
                }
            );

            //Act
            InputFormView vm = mapper.Map<InputFormView>(form);
            vm.Rows.ToList()[0].Columns.ToList()[0].Questions.ToList()[0].Validate(vm.ValidationMessages);
            vm.Rows.ToList()[0].Columns.ToList()[0].Questions.ToList()[1].Validate(vm.ValidationMessages);

            //Assert
            Assert.False(vm.Rows.ToList()[0].Columns.ToList()[0].Questions.ToList()[0].HasErrors);
            Assert.True(vm.Rows.ToList()[0].Columns.ToList()[0].Questions.ToList()[1].HasErrors);
            Assert.Equal("Has Work Email must be checked.", vm.Rows.ToList()[0].Columns.ToList()[0].Questions.ToList()[1].Errors[0]);
        }

        [Fact]
        public void Validate_pattern()
        {
            //Arrange
            InputFormParameters form = new InputFormParameters
            (
                new FormDataParameters
                (
                    "Application Survey",
                    new List<ValidationMessageParameters>
                    {
                        new ValidationMessageParameters
                        (
                            "Height",
                            new List<ValidationMethodParameters>
                            {
                                new ValidationMethodParameters("pattern", "Height must a number.")
                            }
                        ),
                        new ValidationMessageParameters
                        (
                            "Width",
                            new List<ValidationMethodParameters>
                            {
                                new ValidationMethodParameters("pattern", "Width must a number.")
                            }
                        )
                    }
                ),
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
                                    new InputQuestionParameters<string>(1, "Height", "7.1", new InputDataParameters
                                    (
                                        "Enter float value?",
                                        "col-md-12",
                                        "TextBox",
                                        string.Empty,
                                        string.Empty,
                                        null,
                                        null,
                                        null,
                                        null,
                                        new FormValidationSettingParameters
                                        (
                                            null,
                                            new List<ValidatorDescriptionParameters>
                                            {
                                                new ValidatorDescriptionParameters
                                                (
                                                    "Validators",
                                                    "pattern",
                                                    new List<ValidatorArgumentParameters>
                                                    {
                                                        new ValidatorArgumentParameters("pattern", @"^[\d\.]+$")
                                                    }
                                                )
                                            }
                                        )
                                    )),
                                    new InputQuestionParameters<string>(1, "Width", "ABC", new InputDataParameters
                                    (
                                        "Enter float value?",
                                        "col-md-12",
                                        "TextBox",
                                        string.Empty,
                                        string.Empty,
                                        null,
                                        null,
                                        null,
                                        null,
                                        new FormValidationSettingParameters
                                        (
                                            null,
                                            new List<ValidatorDescriptionParameters>
                                            {
                                                new ValidatorDescriptionParameters
                                                (
                                                    "Validators",
                                                    "pattern",
                                                    new List<ValidatorArgumentParameters>
                                                    {
                                                        new ValidatorArgumentParameters("pattern", @"^[\d\.]+$")
                                                    }
                                                )
                                            }
                                        )
                                    ))
                                },
                                new List<InputRowParameters> { }
                            )
                        }
                    )
                }
            );

            //Act
            InputFormView vm = mapper.Map<InputFormView>(form);
            vm.Rows.ToList()[0].Columns.ToList()[0].Questions.ToList()[0].Validate(vm.ValidationMessages);
            vm.Rows.ToList()[0].Columns.ToList()[0].Questions.ToList()[1].Validate(vm.ValidationMessages);

            //Assert
            Assert.False(vm.Rows.ToList()[0].Columns.ToList()[0].Questions.ToList()[0].HasErrors);
            Assert.True(vm.Rows.ToList()[0].Columns.ToList()[0].Questions.ToList()[1].HasErrors);
            Assert.Equal("Width must a number.", vm.Rows.ToList()[0].Columns.ToList()[0].Questions.ToList()[1].Errors[0]);
        }

        [Fact]
        public void Should_throw_InvalidOperationException_for_missing_variable_messages()
        {
            //Arrange
            InputFormParameters form = new InputFormParameters
            (
                new FormDataParameters
                (
                    "Application Survey",
                    new List<ValidationMessageParameters>
                    {
                    }
                ),
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
                                    new InputQuestionParameters<double>(1, "Height", 7.1, new InputDataParameters
                                    (
                                        "Enter float value?",
                                        "col-md-12",
                                        "TextBox",
                                        string.Empty,
                                        string.Empty,
                                        null,
                                        null,
                                        null,
                                        null,
                                        new FormValidationSettingParameters
                                        (
                                            null,
                                            new List<ValidatorDescriptionParameters>
                                            {
                                                new ValidatorDescriptionParameters
                                                (
                                                    "NumberValidators",
                                                    "range",
                                                    new List<ValidatorArgumentParameters>
                                                    {
                                                        new ValidatorArgumentParameters("min", 3),
                                                        new ValidatorArgumentParameters("max", 8)
                                                    }
                                                )
                                            }
                                        )
                                    ))
                                },
                                new List<InputRowParameters> { }
                            )
                        }
                    )
                }
            );

            //Act
            InputFormView vm = mapper.Map<InputFormView>(form);
            Action act = () => vm.Rows.ToList()[0].Columns.ToList()[0].Questions.ToList()[0].Validate(vm.ValidationMessages);

            //Assert
            Assert.Equal("Validation messages are requred for variable Height.", Assert.Throws<InvalidOperationException>(act).Message);
        }

        [Fact]
        public void Should_throw_InvalidOperationException_for_missing_validation_function_messages()
        {
            //Arrange
            InputFormParameters form = new InputFormParameters
            (
                new FormDataParameters
                (
                    "Application Survey",
                    new List<ValidationMessageParameters>
                    {
                        new ValidationMessageParameters
                        (
                            "Height",
                            new List<ValidationMethodParameters>
                            {
                            }
                        )
                    }
                ),
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
                                    new InputQuestionParameters<double>(1, "Height", 7.1, new InputDataParameters
                                    (
                                        "Enter float value?",
                                        "col-md-12",
                                        "TextBox",
                                        string.Empty,
                                        string.Empty,
                                        null,
                                        null,
                                        null,
                                        null,
                                        new FormValidationSettingParameters
                                        (
                                            null,
                                            new List<ValidatorDescriptionParameters>
                                            {
                                                new ValidatorDescriptionParameters
                                                (
                                                    "NumberValidators",
                                                    "range",
                                                    new List<ValidatorArgumentParameters>
                                                    {
                                                        new ValidatorArgumentParameters("min", 3),
                                                        new ValidatorArgumentParameters("max", 8)
                                                    }
                                                )
                                            }
                                        )
                                    ))
                                },
                                new List<InputRowParameters> { }
                            )
                        }
                    )
                }
            );

            //Act
            InputFormView vm = mapper.Map<InputFormView>(form);
            Action act = () => vm.Rows.ToList()[0].Columns.ToList()[0].Questions.ToList()[0].Validate(vm.ValidationMessages);

            //Assert
            Assert.Equal("A validation message is required. Validation Function: range, Variable Name: Height.", Assert.Throws<InvalidOperationException>(act).Message);
        }

        [Fact]
        public void Should_throw_InvalidOperationException_for_if_function_is_not_a_method_in_validation_class()
        {
            //Arrange
            InputFormParameters form = new InputFormParameters
            (
                new FormDataParameters
                (
                    "Application Survey",
                    new List<ValidationMessageParameters>
                    {
                        new ValidationMessageParameters
                        (
                            "Height",
                            new List<ValidationMethodParameters>
                            {
                                new ValidationMethodParameters("range", "Height must be between 3 and 8 inclusive")
                            }
                        )
                    }
                ),
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
                                    new InputQuestionParameters<double>(1, "Height", 7.1, new InputDataParameters
                                    (
                                        "Enter float value?",
                                        "col-md-12",
                                        "TextBox",
                                        string.Empty,
                                        string.Empty,
                                        null,
                                        null,
                                        null,
                                        null,
                                        new FormValidationSettingParameters
                                        (
                                            null,
                                            new List<ValidatorDescriptionParameters>
                                            {
                                                new ValidatorDescriptionParameters
                                                (
                                                    "Validators",
                                                    "range",
                                                    new List<ValidatorArgumentParameters>
                                                    {
                                                        new ValidatorArgumentParameters("min", 3),
                                                        new ValidatorArgumentParameters("max", 8)
                                                    }
                                                )
                                            }
                                        )
                                    ))
                                },
                                new List<InputRowParameters> { }
                            )
                        }
                    )
                }
            );

            //Act
            InputFormView vm = mapper.Map<InputFormView>(form);
            Action act = () => vm.Rows.ToList()[0].Columns.ToList()[0].Questions.ToList()[0].Validate(vm.ValidationMessages);

            //Assert
            Assert.Equal("The validation function: \"range\" does not exist in the class \"Validators\".", Assert.Throws<InvalidOperationException>(act).Message);
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
