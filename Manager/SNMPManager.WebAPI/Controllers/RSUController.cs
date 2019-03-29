﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SNMPManager.Core.Entities;
using SNMPManager.Core.Enumerations;
using SNMPManager.Core.Interfaces;
using SNMPManager.Core.Exceptions;
using SNMPManager.WebAPI.Controllers;
using SNMPManager.WebAPI.Models;

namespace SNMPManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RSUController : BaseController
    {
        public RSUController(IContextService contextService, ILogger logger)
            : base(contextService, logger)
        {

        }

        // GET api/values
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        public ActionResult<IEnumerable<RSUModel>> Get(string username, string token)
        {

            var securityProblem = AuthenticateAuthorize(username, token);
            if (securityProblem != null)
                return securityProblem;


            var rsus = _contextService.GetRSU();
            if (rsus == null)
                return NotFound();

            _logger.LogAPICall(username, ManagerOperation.ADMINISTRATION);

            return rsus.Select(r => RSUModel.MaptoModel(r)).ToList();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        public ActionResult<RSUModel> Get(int id, string username, string token)
        {

            var securityProblem = AuthenticateAuthorize(username, token);
            if (securityProblem != null)
                return securityProblem;


            var rsu = _contextService.GetRSU(id);
            if (rsu == null)
                return NotFound(id);

            _logger.LogAPICall(username, ManagerOperation.ADMINISTRATION);

            return RSUModel.MaptoModel(rsu);
        }

        // POST api/values
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(409)]
        [ProducesResponseType(400)]
        public IActionResult Post([FromBody] RSUModel rsu, string username, string token)
        {
            var securityProblem = AuthenticateAuthorize(username, token);
            if (securityProblem != null)
                return securityProblem;

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_contextService.AddRSU(RSUModel.MaptoEntity(rsu)))
                return Conflict();
            else
            {
                _logger.LogAPICall(username, ManagerOperation.ADMINISTRATION);
                return Ok(rsu);
            }
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(400)]
        public IActionResult Put([FromBody] RSUModel rsu, string username, string token)
        {
            var securityProblem = AuthenticateAuthorize(username, token);
            if (securityProblem != null)
                return securityProblem;

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_contextService.UpdateRSU(RSUModel.MaptoEntity(rsu)))
                return NotFound(rsu);
            else
            {
                _logger.LogAPICall(username, ManagerOperation.ADMINISTRATION);
                return Ok(rsu);
            }
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        public IActionResult Delete(int rsuid, string username, string token)
        {
            var securityProblem = AuthenticateAuthorize(username, token);
            if (securityProblem != null)
                return securityProblem;

            if (!_contextService.RemoveRSU(rsuid))
                return NotFound(rsuid);
            else
            {
                _logger.LogAPICall(username, ManagerOperation.ADMINISTRATION);
                return Ok();
            }
        }
    }
}
