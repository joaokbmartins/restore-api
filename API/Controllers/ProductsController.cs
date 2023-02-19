using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{

  [ApiController]
  [Route("api/v1/[controller]")]
  public class ProductsController : ControllerBase
  {
    private readonly StoreContext _context;

    public ProductsController(StoreContext context)
    {
      _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<Product>>> GetAll()
    {
      var products = await _context.Products.ToListAsync();
      return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetById(int id)
    {
      var product = await _context.Products.FindAsync(id);
      return product;
    }

    [HttpPost]
    public async Task<ActionResult> Post(Product product)
    {
      try
      {
        var response = await _context.AddAsync(product);
        await _context.SaveChangesAsync();
        return Ok(response.Entity);
      }
      catch (Exception ex)
      {
        if (ex.InnerException.Message.Contains("UNIQUE constraint failed"))
          return StatusCode(400, String.Format("Error: there is already an entry with id: {0}. Please, try another one.", product.Id));
        return StatusCode(500, "There was an error while trying to complete your request. Try again later. " + ex.Message);
      }
    }

    [HttpPut]
    public async Task<ActionResult> Put(Product product)
    {
      try
      {
        var response = _context.Update(product);
        await _context.SaveChangesAsync();
        return NoContent();
      }
      catch (Exception ex)
      {
        if (ex.Message.Contains("The database operation was expected to affect 1 row(s), but actually affected 0 row(s)"))
          return NotFound(String.Format("Attention! Cannot find entry with id: {0}. Verify if it's correct.", product.Id));
        return StatusCode(500, "There was an error while trying to complete your request. Try again later. " + ex.Message);
      }
    }

    [HttpDelete]
    public async Task<ActionResult> Delete(Product product)
    {
      try
      {
        var response = _context.Remove(product);
        await _context.SaveChangesAsync();
        return NoContent();
      }
      catch (Exception ex)
      {
        if (ex.Message.Contains("The database operation was expected to affect 1 row(s), but actually affected 0 row(s)"))
          return NotFound(String.Format("Attention! Cannot find entry with id: {0}. Verify if it's correct.", product.Id));
        return StatusCode(500, "There was an error while trying to complete your request. Try again later. " + ex.Message);
      }
    }

  }
}