using AutoMapper;
using Backend.Repositorios.AperturaCampanaElectoral;

namespace Backend.Repositorios.Cuadrillas
{
    public class RepositorioCuadrillas : IRepositorioCuadrillas
    {
        private readonly ILogger<RepositorioCuadrillas> logger;
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public RepositorioCuadrillas(ILogger<RepositorioCuadrillas> logger, ApplicationDbContext context, IMapper mapper)
        {
            this.logger = logger;
            this.context = context;
            this.mapper = mapper;
        }
    }
}
