using Microsoft.AspNetCore.Mvc;

namespace RedeSocialTBD.Models.Relatorios
{
    public class RelatorioCurtidas
    {
        public string IdPublicacao { get; set; } = string.Empty;
        public int TotalCurtidas { get; set; }

    }
}
