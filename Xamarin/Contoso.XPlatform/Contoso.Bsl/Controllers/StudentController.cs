using Contoso.Bsl.Business.Requests;
using Contoso.Bsl.Business.Responses;
using Contoso.Bsl.Flow;
using Contoso.Domain.Entities;
using Contoso.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace Contoso.Bsl.Controllers
{
    [Produces("application/json")]
    [Route("api/Student")]
    public class StudentController : Controller
    {
        private readonly IFlowManager flowManager;
        private readonly ISchoolRepository schoolRepository;
        private readonly ILogger<StudentController> logger;

        public StudentController(IFlowManager flowManager, ISchoolRepository schoolRepository, ILogger<StudentController> logger)
        {
            this.flowManager = flowManager;
            this.schoolRepository = schoolRepository;
            this.logger = logger;
        }

        [HttpPost("Delete")]
        public IActionResult Delete([FromBody] DeleteEntityRequest deleteStudentRequest)
        {
            this.flowManager.FlowDataCache.Request = deleteStudentRequest;
            this.flowManager.Start("deletestudent");
            return Ok(this.flowManager.FlowDataCache.Response);
        }

        [HttpPost("Save")]
        public IActionResult Save([FromBody] SaveEntityRequest saveStudentRequest)
        {
            this.flowManager.FlowDataCache.Request = saveStudentRequest;
            this.flowManager.Start("savestudent");
            return Ok(this.flowManager.FlowDataCache.Response);
        }

        [HttpPost("SaveWithoutRules")]
        public IActionResult SaveWithoutRules([FromBody] SaveEntityRequest saveStudentRequest)
        {
            System.Diagnostics.Stopwatch stopWatch = System.Diagnostics.Stopwatch.StartNew();
            var response = SaveStudentWithoutRules(saveStudentRequest);
            stopWatch.Stop();
            logger.LogWarning("this.SaveStudentWithoutRules: {0}", stopWatch.Elapsed.TotalMilliseconds);
            return Ok(response);
        }

        private SaveEntityResponse SaveStudentWithoutRules(SaveEntityRequest saveStudentRequest)
        {
            Domain.Entities.StudentModel studentModel = (StudentModel)saveStudentRequest.Entity;
            SaveEntityResponse saveStudentResponse = new SaveEntityResponse()
            {
                ErrorMessages = new List<string>()
            };

            #region Validation
            if (string.IsNullOrWhiteSpace(studentModel.FirstName))
                saveStudentResponse.ErrorMessages.Add("First Name is required.");
            if (string.IsNullOrWhiteSpace(studentModel.LastName))
                saveStudentResponse.ErrorMessages.Add("Last Name is required.");
            if (studentModel.EnrollmentDate == default)
                saveStudentResponse.ErrorMessages.Add("Enrollment Date is required.");

            if (saveStudentResponse.ErrorMessages.Any())
            {
                flowManager.CustomActions.WriteToLog("An error occurred saving the request.");
                saveStudentResponse.Success = false;
                return saveStudentResponse;
            }
            #endregion Validation

            #region Save and retrieve
            saveStudentResponse.Success = schoolRepository.SaveGraphAsync<Domain.Entities.StudentModel, Data.Entities.Student>(studentModel).Result;
            if (!saveStudentResponse.Success) return saveStudentResponse;

            studentModel = schoolRepository.GetAsync<Domain.Entities.StudentModel, Data.Entities.Student>
            (
                f => f.ID == studentModel.ID,
                null,
                new LogicBuilder.Expressions.Utils.Expansions.SelectExpandDefinition
                {
                    ExpandedItems = new List<LogicBuilder.Expressions.Utils.Expansions.SelectExpandItem>
                    {
                        new LogicBuilder.Expressions.Utils.Expansions.SelectExpandItem { MemberName = "enrollments" }
                    }
                }
            ).Result.SingleOrDefault();

            saveStudentResponse.Entity = studentModel;
            #endregion Save and retrieve

            #region Log Enrollments
            int Iteration_Index = 0;

            flowManager.CustomActions.WriteToLog
            (
                string.Format
                (
                    "EnrollmentCount: {0}. Index: {1}",
                    new object[]
                    {
                        studentModel.Enrollments.Count,
                        Iteration_Index
                    }
                )
            );

            Domain.Entities.EnrollmentModel enrollmentModel = null;
            while (Iteration_Index < studentModel.Enrollments.Count)
            {
                enrollmentModel = studentModel.Enrollments.ElementAt(Iteration_Index);
                Iteration_Index = Iteration_Index + 1;
                flowManager.CustomActions.WriteToLog
                (
                    string.Format
                    (
                        "Student Id:{0} is enrolled in {1}.",
                        new object[]
                        {
                            studentModel.ID,
                            enrollmentModel.CourseTitle
                        }
                    )
                );
            }
            #endregion Log Enrollments

            return saveStudentResponse;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "a", "b"};
        }
    }
}
