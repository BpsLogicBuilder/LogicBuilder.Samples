using AutoMapper;
using Contoso.Domain;
using Contoso.Repositories;
using Contoso.Web.Utils;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contoso.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenericController : Controller
    {
        readonly ISchoolRepository _schoolRepository;
        readonly IMapper _mapper;
        public GenericController(ISchoolRepository schoolRepository, IMapper mapper)
        {
            this._schoolRepository = schoolRepository;
            this._mapper = mapper;
        }

        [HttpPost("GetData")]
        public async Task<IActionResult> GetData([FromBody] DataRequest request)
        {
            try
            {
                return Json
                (
                    await request.InvokeGenericMethod<DataSourceResult>("GetData", this._schoolRepository, this._mapper)
                );
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("GetFilterData")]
        public async Task<IActionResult> GetFilterData([FromBody] DataRequest request)
        {
            try
            {
                return Json
                (
                    await request.InvokeGenericMethod<IEnumerable<dynamic>>("GetDynamicSelect", this._schoolRepository, this._mapper)
                );
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("GetSingle")]
        public async Task<IActionResult> GetSingle([FromBody] DataRequest request)
        {
            try
            {
                return Json
                (
                    await request.InvokeGenericMethod<BaseModelClass>("GetSingle", this._schoolRepository, this._mapper)
                );
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Post([FromBody]PostModelRequest postModelRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not Saved");

            try
            {
                if (await postModelRequest.InvokeGenericSaveMethod("Save", postModelRequest.Model, this._schoolRepository))
                {
                    return Created($"/api/[controller]", postModelRequest.Model);
                }
                else
                {
                    return BadRequest("Not Saved");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Update")]
        public async Task<IActionResult> Put([FromBody]PostModelRequest postModelRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not Saved");

            try
            {
                if (await postModelRequest.InvokeGenericSaveMethod("Save", postModelRequest.Model, this._schoolRepository))
                {
                    return NoContent();
                }
                else
                {
                    return BadRequest("Not Saved");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Delete")]
        public async Task<IActionResult> Delete([FromBody]DataRequest request)
        {
            try
            {
                if (await request.InvokeGenericDeleteMethod("Delete", this._schoolRepository))
                {
                    return NoContent();
                }
                else
                {
                    return BadRequest("Not Saved");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}