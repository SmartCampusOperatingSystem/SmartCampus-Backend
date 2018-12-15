using System;

namespace SCOS.API.Models
{
    public class DadosSensor
    {
        public int IdDadosSensor { get; set; }

        private string _temperatura;
        public string Temperatura
        {
            get => _temperatura;
            set => _temperatura = value?.Trim();
        }

        private string _umidade;
        public string Umidade
        {
            get => _umidade;
            set => _umidade = value?.Trim();
        }

        public DateTime DataEnvio { get; set; }
    }
}