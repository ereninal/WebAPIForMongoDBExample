using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;
using WebAPIForMongoDB.DataAccess.Base;
using WebAPIForMongoDB.Entities.MongoDB;
using WebAPIForMongoDB.Models;

namespace WebAPIForMongoDB.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]

    public class ValuesController : ControllerBase
    {
        private readonly ICustomerRepository customerRepository;
        public ValuesController(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }
        [Route("mongo-api-api/create")]
        [HttpPost]
        public IActionResult Create([FromBody] Customer data)
        {
            var result = customerRepository.AddAsync(data).Result;
            return Ok(result);
        }
        [Route("mongo-api/create-range")]
        [HttpPost]
        public IActionResult CreateRangeAsync([FromBody] IList<Customer> data)
        {
            var result = customerRepository.AddRangeAsync(data).Result;
            return Ok(result);
        }
        [Route("mongo-api/update")]
        [HttpPut]
        public IActionResult Update([FromBody] Customer data, string id)
        {
            var result = customerRepository.UpdateAsync(new ObjectId(id), data);
            return Ok(result);
        }
        [Route("mongo-api/patch")]
        [HttpPatch]
        public IActionResult Update([FromBody] IList<Customer> data)
        {
            var result = customerRepository.AddRangeAsync(data).Result;
            return Ok(result);
        }
        [Route("mongo-api/all")]
        [HttpGet]
        public IActionResult GetAll()
        {
            var result = customerRepository.Get().Select(m => new CustomerDto { Id = m.Id.ToString(), Age = m.Age, Name = m.Fullname });
            return Ok(result);
        }
        [Route("mongo-api/get-single-by-id")]
        [HttpGet]
        public IActionResult Get([FromQuery] string id)
        {
            var result = customerRepository.GetById(new ObjectId(id));
            return Ok(result);
        }
        [Route("mongo-api/delete")]
        [HttpDelete]
        public IActionResult Delete([FromQuery] string id)
        {
            var result = customerRepository.DeleteAsync(new ObjectId(id));
            return Ok(result);
        }
        [Route("mongo-api/get-by-name")]
        [HttpGet]
        public IActionResult GetByName([FromQuery] string name)
        {
            var result = customerRepository.GetByName(name);
            return Ok(result);
        }
    }
    /// <summary>
    /// Search Campaign
    /// </summary>
    /// <response code="200">OK</response>
    //[Route("campaign-api/campaigns")]
    //[HttpGet]
    //[Produces("application/json")]
    //[ProducesResponseType(200)]
    //public ActionResult<SearchCampaignResponse> Search([FromQuery] SearchCampaignRequest request)
    //{
    //    validator.ValidateAndThrowException<SearchCampaignRequestValidator, SearchCampaignRequest>(request);

    //    var campaigns = campaignOperations.Search(request.name, request.code, request.objectiveId, (BenefitTypes?)request.benefitType, request.startDate, request.endDate, (CampaignStatus?)request.status, request.sortBy, request.sortDirection, request.pageSize, request.pageNumber, out int totalCount);

    //    SearchCampaignResponse response = new SearchCampaignResponse();
    //    response.Fill(campaigns, LocalizeEnumeration, totalCount);

    //    return new JsonResult(response);
    //}

}
