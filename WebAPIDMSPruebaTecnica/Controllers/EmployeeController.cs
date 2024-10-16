using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPIDMSPruebaTecnica.Dtos.Data;
using WebAPIDMSPruebaTecnica.Dtos.Models;

namespace WebAPIDMSPruebaTecnica.Controllers
{
    [EnableCors("RulesCorsAPISetting")]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class EmployeeController : ControllerBase
    {
        public readonly PruebaTecnicaDMSSoftContext _context;

        public EmployeeController(PruebaTecnicaDMSSoftContext context)
        {
            _context = context;
        }

        // Listado completo de los empleados.
        [HttpGet]
        [Route("list-employee")]
        public IActionResult GetEmployee() 
        {
            List<Employee> employees = new List<Employee>();

            try
            {
                employees = _context.Employees.ToList();
                
                return StatusCode(StatusCodes.Status200OK, new { employees });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status204NoContent, new { message = ex.Message, response = employees });
            }
        }

        // Datos de empleados por ID.
        [HttpGet]
        [Route("list-employee/{ID:int}")]
        public IActionResult GetEmployerById(int ID) 
        {
            Employee? employee = _context.Employees.Find(ID);

            if (employee == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { message = "Empleado No Encontrado", response = employee });
            }

            try
            {
                employee = _context.Employees
                    .Include(ta => ta.FkTypeAppointmentNavigation)
                    .FirstOrDefault();

                return StatusCode(StatusCodes.Status200OK, new { message = "Datos del empleado: ", response = employee });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status204NoContent, new { message = ex.Message, response = employee });
            }
        }
    }
}
