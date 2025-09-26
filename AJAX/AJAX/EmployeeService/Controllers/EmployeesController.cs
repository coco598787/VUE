using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmployeeService.Models;
using EmployeeService.DTO;
using Microsoft.AspNetCore.Cors;

namespace EmployeeService.Controllers
{
    [EnableCors("WebFront")]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly NorthwindContext _context;

        public EmployeesController(NorthwindContext context)
        {
            _context = context;
        }

        // POST: api/Employees/Filter
        [HttpPost("Filter")]
        public async Task<IEnumerable<EmployeeDTO>> FilterEmployees([FromBody]EmployeeDTO EmpDTO)
        {
            return _context.Employees.Where(e=>e.EmployeeId==EmpDTO.EmployeeId ||
                                    e.FirstName.Contains(EmpDTO.FirstName) ||
                                    e.LastName.Contains(EmpDTO.LastName) ||
                                    e.Title.Contains(EmpDTO.Title) || 
                                    e.Address.Contains(EmpDTO.Address) ||
                                    e.City.Contains(EmpDTO.City) || 
                                    e.PostalCode.Contains(EmpDTO.PostalCode) ||
                                    e.Country.Contains(EmpDTO.Country) ||
                                    e.HomePhone.Contains(EmpDTO.HomePhone)).Select(e=>new EmployeeDTO
                                    {
                                        EmployeeId = e.EmployeeId,
                                        FirstName = e.FirstName,
                                        LastName = e.LastName,
                                        Title = e.Title,
                                        Address = e.Address,
                                        City = e.City,
                                        PostalCode = e.PostalCode,
                                        Country = e.Country,
                                        HomePhone = e.HomePhone,
                                        BirthDate = e.BirthDate,
                                        HireDate = e.HireDate,
                                        Photo=null,
                                    });           
        }

        // GET: api/Employees
        [HttpGet("GetPhoto/{id}")]
        public async Task<FileResult> GetPhoto(int id)
        {
            string Filename = Path.Combine("images", "Noimage1.jpg");
            Employee? Emp=await _context.Employees.FindAsync(id);
            byte[] ImageContent = Emp?.Photo ?? System.IO.File.ReadAllBytes(Filename);
            return File(ImageContent, "image/jpeg");
        }
        // PUT: api/Employees/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ResultDTO> PutEmployee(int id, EmployeeDTO EmpDTO)
        {
            if (id != EmpDTO.EmployeeId)
            {
                return new ResultDTO
                {
                    Ok = false,
                    Code = 400,
                };  
            }
            Employee Emp = await _context.Employees.FindAsync(id);
            if (Emp == null)
            {
                return new ResultDTO
                {
                    Ok = false,
                    Code = 404,
                };
            }
            else
            {
                Emp.FirstName = EmpDTO.FirstName;
                Emp.LastName = EmpDTO.LastName;
                Emp.Title = EmpDTO.Title;
                Emp.BirthDate = EmpDTO.BirthDate;
                Emp.HireDate = EmpDTO.HireDate;
                Emp.Address = EmpDTO.Address;
                Emp.City = EmpDTO.City;
                Emp.PostalCode = EmpDTO.PostalCode;
                Emp.Country = EmpDTO.Country;
                Emp.HomePhone = EmpDTO.HomePhone;
                if (EmpDTO.Photo != null)
                {
                    using (BinaryReader br = new BinaryReader(EmpDTO.Photo.OpenReadStream()))
                    {
                        Emp.Photo = br.ReadBytes((int)EmpDTO.Photo.Length);
                    }
                }
                _context.Entry(Emp).State = EntityState.Modified;
            }                
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
                {
                    return new ResultDTO
                    {
                        Ok = false,
                        Code = 404,
                    };
                }
                else
                {
                    throw;
                }
            }
            return new ResultDTO
            {
                Ok = true,
                Code = 0,
            }; 
        }

        // POST: api/Employees
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ResultDTO> PostEmployee(EmployeeDTO EmpDTO)
        {
            Employee Emp = new Employee
            {
                FirstName = EmpDTO.FirstName,
                LastName = EmpDTO.LastName,
                Title = EmpDTO.Title,
                BirthDate = EmpDTO.BirthDate,
                HireDate = EmpDTO.HireDate,
                Address = EmpDTO.Address,
                City = EmpDTO.City,
                PostalCode = EmpDTO.PostalCode,
                Country = EmpDTO.Country,
                HomePhone = EmpDTO.HomePhone,
            };
            if (EmpDTO.Photo != null)
            {
                using (BinaryReader br = new BinaryReader(EmpDTO.Photo.OpenReadStream()))
                {
                    Emp.Photo = br.ReadBytes((int)EmpDTO.Photo.Length);
                }                    
            }
            _context.Employees.Add(Emp);
            await _context.SaveChangesAsync();  //寫入資料庫
            EmpDTO.EmployeeId = Emp.EmployeeId;
            return new ResultDTO
            {
                Ok = true,
                Code=0,
            };
        }

        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        public async Task<ResultDTO> DeleteEmployee(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return new ResultDTO
                {
                    Ok = false,
                    Code=404
                };
            }
            try
            {
                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                return new ResultDTO
                {
                    Ok = false,
                    Code = 100
                };
            }            
            return new ResultDTO
            {
                Ok = true,
                Code= 0,
            };
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.EmployeeId == id);
        }
    }
}
