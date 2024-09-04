using Microsoft.AspNetCore.Mvc;
using primeiraAPI.Context;
using primeiraAPI.Models;

namespace primeiraAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContatoController : ControllerBase
    {
        private readonly AgendaContext _context;
        public ContatoController(AgendaContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Create(Contato contato)
        {
            _context.Add(contato);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetByID), new { id = contato.Id}, contato);
        }

        [HttpGet("{id}")]
        public IActionResult GetByID(int id)
        {
            var contato = _context.Contatos.Find(id);

            if(contato == null) return NotFound();
            return Ok(contato);
        }

        [HttpGet("GetByNome/{nome}")]
        public IActionResult GetByNome(string nome)
        {
            var contato = _context.Contatos.Where(x => x.Nome.Contains(nome));

            if(contato == null) return NotFound();
            return Ok(contato);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var contato = _context.Contatos.Find(id);
            _context.Remove(contato);
            _context.SaveChanges();

            if(contato == null) return NotFound();
            return Ok(contato);
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, Contato contato)
        {
            var contatoBanco = _context.Contatos.Find(id);

            if(contatoBanco == null) return NotFound();

            contatoBanco.Nome = contato.Nome;
            contatoBanco.Telefone = contato.Telefone;
            contatoBanco.isActive = contato.isActive;

            _context.Contatos.Update(contatoBanco);
            _context.SaveChanges();
            return Ok(contatoBanco);
        }

        
    }
}