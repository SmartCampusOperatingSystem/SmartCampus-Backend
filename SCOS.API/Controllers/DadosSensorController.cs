using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SCOS.API.Business;
using SCOS.API.Models;
using System;
using Microsoft.AspNetCore.Authorization;

namespace SCOS.API.Controllers
{
    //[Authorize("Bearer")]
    [Route("api/[controller]")]
    public class DadosSensorController : Controller
    {
        private DadosSensorService _service;

        public DadosSensorController(DadosSensorService service)
        {
            _service = service;
        }

        // [HttpGet]
        // public IEnumerable<DadosSensor> Get()
        // {
        //     return _service.ListarTodos();
        // }

        [HttpGet("{codigoBarras}")]
        public IActionResult Get(string idDadosSensor)
        {
            var DadosSensor = _service.Obter(Convert.ToInt32(idDadosSensor));
            if (DadosSensor != null)
                return new ObjectResult(DadosSensor);
            else
                return NotFound();
        }

        [HttpPost]
        public Resultado Post([FromBody]DadosSensor DadosSensor)
        {
            return _service.Incluir(DadosSensor);
        }

        [HttpPut]
        public Resultado Put([FromBody]DadosSensor DadosSensor)
        {
            return _service.Atualizar(DadosSensor);
        }

        [HttpDelete("{idDadosSensor}")]
        public Resultado Delete(string idDadosSensor)
        {
            return _service.Excluir(idDadosSensor);
        }
    }
}