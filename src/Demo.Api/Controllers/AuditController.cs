using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Audit.Core;
using Demo.Api.Models;
using Demo.Audit.DynamicProxy;
using Microsoft.AspNetCore.Mvc;
using Nest;

namespace Demo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuditController : Controller
    {
        private readonly IElasticClient _elasticClient;

        public AuditController(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }

        [HttpGet]
        public async Task<IActionResult> List([FromQuery] AuditSearchRequest request)
        {
            return Json(await GetEventsAsync(request));
        }


        [HttpGet("{index}/{id}")]
        public async Task<IActionResult> GetAuditEvent(string index, Guid id)
        {
            var response = await _elasticClient.GetAsync(new DocumentPath<AuditEvent>(id), x => x.Index(index));

            if (response.IsValid && response.Found)
            {
                return Json(response.Source);
            }

            return NotFound();
        }

        private async Task<IEnumerable<AuditEventDescriptor>> GetEventsAsync(AuditSearchRequest request)
        {
            QueryContainer query = new QueryContainer();

            if (!string.IsNullOrWhiteSpace(request.EventType))
            {
                query &= new MatchPhraseQuery { Field = "eventType", Query = request.EventType };
            }

            if (request.StartDate.HasValue || request.EndDate.HasValue)
            {
                query &= new DateRangeQuery { GreaterThanOrEqualTo = request.StartDate, LessThanOrEqualTo = request.EndDate };
            }

            var response = await _elasticClient.SearchAsync<AuditEventIntercept>(s => s
                .Index("demoapi*")
                .From(request.From)
                .Size(request.Size)
                .Query(q => q
                    .Bool(b => b                        
                        .Filter(f => f
                            .MatchAll() && query))));

            return response.Hits.Select(MapHit);
        }

        private static AuditEventDescriptor MapHit(IHit<AuditEventIntercept> hit)
        {
            return new AuditEventDescriptor
            {
                AuditEventId = Guid.Parse(hit.Id),
                Index = hit.Index,
                EventType = hit.Source.EventType,
                StartDate = hit.Source.StartDate,
                EndDate = hit.Source.EndDate,
                Duration = hit.Source.Duration,
                Success = hit.Source.InterceptEvent.Success
            };
        }     
    }
}