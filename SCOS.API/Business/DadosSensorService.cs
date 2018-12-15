using System;
using System.Collections.Generic;
using System.Linq;
using SCOS.API.Data;
using SCOS.API.Models;

namespace SCOS.API.Business
{
    public class DadosSensorService
    {
        private ApplicationDbContext _context;

        public DadosSensorService(ApplicationDbContext context)
        {
            _context = context;
        }

        public DadosSensor Obter(int idDadosSensor)
        {
            if (idDadosSensor > 0)
            {
                return _context.DadosSensor.Where(
                    p => p.IdDadosSensor == idDadosSensor).FirstOrDefault();
            }
            else
                return null;
        }

        // public IEnumerable<DadosSensor> ListarTodos()
        // {
        //     return resultado;
        //     return _context.DadosSensor
        //         .OrderBy(p => p.Nome).ToList();
        // }

        public Resultado Incluir(DadosSensor dadosDadosSensor)
        {
            dadosDadosSensor.DataEnvio = DateTime.Now;

            Resultado resultado = DadosValidos(dadosDadosSensor);
            resultado.Acao = "Inclusão de DadosSensor";

            if (resultado.Inconsistencias.Count == 0)
            {
                _context.DadosSensor.Add(dadosDadosSensor);
                _context.SaveChanges();
            }

            return resultado;
        }

        public Resultado Atualizar(DadosSensor dadosDadosSensor)
        {
            Resultado resultado = DadosValidos(dadosDadosSensor);
            resultado.Acao = "Atualização de DadosSensor";

            resultado.Inconsistencias.Add("Ação não disponível");

            return resultado;
        }

        public Resultado Excluir(string idDadosSensor)
        {
            Resultado resultado = new Resultado();
            resultado.Acao = "Exclusão de DadosSensor";

            DadosSensor DadosSensor = Obter(Convert.ToInt32(idDadosSensor));
            if (DadosSensor == null)
            {
                resultado.Inconsistencias.Add(
                    "DadosSensor não encontrado");
            }
            else
            {
                _context.DadosSensor.Remove(DadosSensor);
                _context.SaveChanges();
            }
                
            return resultado;
        }

        private Resultado DadosValidos(DadosSensor DadosSensor)
        {
            var resultado = new Resultado();
            if (DadosSensor == null)
            {
                resultado.Inconsistencias.Add(
                    "Preencha os Dados do DadosSensor");
            }
            else
            {
                if (String.IsNullOrWhiteSpace(DadosSensor.Temperatura))
                {
                    resultado.Inconsistencias.Add(
                        "Informe a temperatura");
                }
                if (String.IsNullOrWhiteSpace(DadosSensor.Umidade))
                {
                    resultado.Inconsistencias.Add(
                        "Informe a umidade");
                }
                if (DadosSensor.DataEnvio < DateTime.Now.AddDays(-1))
                {
                    resultado.Inconsistencias.Add(
                        "Data de envio muito antiga");
                }
            }

            return resultado;
        }
    }
}