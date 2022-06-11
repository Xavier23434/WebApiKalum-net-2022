using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiKalum.Entities;

namespace WebApiKalum.Controllers
{
    [ApiController]
    [Route("v1/KalumManagement/CarrerasTecnicas")]
    public class CarreraTecnicaController : ControllerBase
    {
        private readonly KalumDbContext DbContext;
        private readonly ILogger<CarreraTecnicaController> Logger;
        public CarreraTecnicaController(KalumDbContext _DbContext, ILogger<CarreraTecnicaController> _Logger)
        {
            this.DbContext = _DbContext;
            this.Logger = _Logger;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarreraTecnica>>> Get()
        {
            List<CarreraTecnica> carrerasTecnicas = null;
            Logger.LogDebug("Iniciando Proceso de consulta de carreras tecnicas en la base de datos");
            carrerasTecnicas = await DbContext.CarreraTecnica.Include(c => c.Aspirantes).Include(c => c.Inscripciones).ToListAsync();

            if(carrerasTecnicas == null || carrerasTecnicas.Count == 0)
            {
                Logger.LogWarning("No exsiste carreras técnicas");
                return new NoContentResult();
            }
            return Ok(carrerasTecnicas);
        }
        [HttpGet("{id}", Name = "GetCarreraTecnica")]
        public async Task<ActionResult<CarreraTecnica>> GetCarreraTecnica(string id)
        { 
            Logger.LogDebug("Iniciando el proceso de busqueda con el id "+  id);
            var carrera = await DbContext.CarreraTecnica.Include(c => c.Aspirantes).Include(c => c.Inscripciones).FirstOrDefaultAsync(ct => ct.CarreraId == id);
            if(carrera == null)
            { 
                Logger.LogWarning("No exsiste la carrera técnica con el id " + id);
                return new NoContentResult();
            }
            Logger.LogInformation("Finaliza el proceso de busqueda de forma exsitosa");
            return Ok(carrera);
        } 
        [HttpPost]
        public async Task<ActionResult<CarreraTecnica>> Post([FromBody] CarreraTecnica value){
            Logger.LogDebug("Iniciando el proceso de agregar una carrera tecnica");
            value.CarreraId = Guid.NewGuid().ToString().ToUpper();
            await DbContext.CarreraTecnica.AddAsync(value);
            await DbContext.SaveChangesAsync();
            Logger.LogInformation("Finaliza proceso de agregar carrera tecnica");
            return new CreatedAtRouteResult("GetCarreraTecnica", new {id = value.CarreraId}, value);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<CarreraTecnica>> Delete(string id){
            Logger.LogDebug("Inciando proceso de eliminación del Registro");
            CarreraTecnica carreraTecnica = await DbContext.CarreraTecnica.FirstOrDefaultAsync(ct => ct.CarreraId == id);
            if(carreraTecnica == null){
                Logger.LogWarning($"No se encontro ninguna carrera técnica con id {id}");
                return NotFound();
            } else {
                DbContext.CarreraTecnica.Remove(carreraTecnica);
                await DbContext.SaveChangesAsync();
                Logger.LogInformation($"Carrera Tecnica, {id}, Eliminada Correctamente");
                return carreraTecnica;
            }
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(string id, [FromBody] CarreraTecnica value){
            Logger.LogDebug($"Iniciando el proceso de actualización de la carrrera tecnica con el id {id}");
            CarreraTecnica carreraTecnica = await DbContext.CarreraTecnica.FirstOrDefaultAsync(ct => ct.CarreraId == id);
            if(carreraTecnica == null)
            {
                Logger.LogWarning($"La carrera tecnica con id {id} no exsiste");
                return BadRequest();
            }
            carreraTecnica.Nombre = value.Nombre;
            DbContext.Entry(carreraTecnica).State = EntityState.Modified;
            await DbContext.SaveChangesAsync();
            Logger.LogInformation("Los datos han sido actualizados correctamente");
            return NoContent();
        }
    }
}