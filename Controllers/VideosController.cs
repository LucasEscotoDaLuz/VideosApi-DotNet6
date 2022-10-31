using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace VideosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideosController : ControllerBase
    {
        public readonly DataContext _context;

        public VideosController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Videos>>> Get()
        {


            return Ok(await _context.TableVideos.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Videos>> Get(int id)
        {
            //CONFERIR ESTAS VARIÁVEIS PARA VERIFICAR A ADIÇÃO DE QUE TODOS OS CAMPOS DEVEM SER PREENCHIDOS ANTES DE QUAISQUER COMANDOS EXECUTADOS. NÃO PODE TER CAMPO NULO
            var links = await _context.TableVideos.FindAsync(id);
            if (links == null)
                return BadRequest("Video não encontrado.");
            return Ok(await _context.TableVideos.ToListAsync());
        }

        [HttpPost]

        public async Task<ActionResult<List<Videos>>> AddVideo(Videos links)
        {

            _context.TableVideos.Add(links);
            await _context.SaveChangesAsync();

            return Ok(await _context.TableVideos.ToListAsync());
        }

        [HttpPut]

        public async Task<ActionResult<List<Videos>>> UpdateVideo(Videos request)
        {

            var dbVideos = await _context.TableVideos.FindAsync(request.Id);
            if (dbVideos == null)
                return BadRequest("Video não encontrado.");

            dbVideos.Titulo = request.Titulo;
            dbVideos.Descricao = request.Descricao;
            dbVideos.Url = request.Url;

            await _context.SaveChangesAsync();

            return Ok(await _context.TableVideos.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Videos>> Delete(int id)
        {
            var dbVideos = await _context.TableVideos.FindAsync(id);
            if (dbVideos == null)
                return BadRequest("Video não encontrado.");

            _context.TableVideos.Remove(dbVideos);
            
            await _context.SaveChangesAsync();

            return Ok(await _context.TableVideos.ToListAsync());
        }


    }
}
