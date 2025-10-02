using EmployeeService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeService.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductsController : ControllerBase
	{
		NorthwindContext _context;

		public ProductsController(NorthwindContext context)
		{
			_context = context;
		}

		// GET: api/Products
		[HttpGet]
		public async Task<ProductDTO> GetProducts()
		{
			
		}
	}
}
