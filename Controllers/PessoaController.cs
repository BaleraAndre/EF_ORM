using Microsoft.AspNetCore.Mvc;
using EF.AppDBcontext;
using Microsoft.EntityFrameworkCore;
using EF.Models.entities;
/**/
[ApiController]
[Route("api/[controller]")]
public class TestController : ControllerBase
{
    private readonly AppDBcontext _context;

    public TestController(AppDBcontext context)
    {
        _context = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Pessoa>> Get()
    {
        try
        {
            var pessoas = _context.pessoas.ToList();
            if (pessoas == null || pessoas.Count == 0) 
            {  
                return NotFound();
            } 
            
            return Ok(pessoas);
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }
  
    
[HttpGet("{id}", Name = "GetPessoa")]
public ActionResult<Pessoa> GetPessoa(int id)
{
    var pessoa = _context.pessoas.Find(id);

    if (pessoa == null)
    {
        return NotFound();
    }

    return Ok(pessoa);
}



[HttpPost]
public ActionResult Post([FromBody] Pessoa pessoa)
{
    if (!ModelState.IsValid || pessoa == null)
    {
        return BadRequest(ModelState);
    }

    _context.pessoas.Add(pessoa);
    _context.SaveChanges();

    return CreatedAtRoute("GetPessoa", new { id = pessoa.id }, pessoa);
}

[HttpPut("{id}")]
    public ActionResult Put(int id, [FromBody] Pessoa updatedPessoa)
    {
        if (!ModelState.IsValid || updatedPessoa == null)
        {
            return BadRequest(ModelState);
        }

        var existingPessoa = _context.pessoas.Find(id);

        if (existingPessoa == null)
        {
            return NotFound();
        }

        existingPessoa.nome = updatedPessoa.nome; 

        _context.SaveChanges();

        return Ok(existingPessoa);
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        var pessoa = _context.pessoas.Find(id);

        if (pessoa == null)
        {
            return NotFound();
        }

        _context.pessoas.Remove(pessoa);
        _context.SaveChanges();

        return NoContent();
    }




}