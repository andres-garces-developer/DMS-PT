using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPIDMSPruebaTecnica.Dtos.Data;
using WebAPIDMSPruebaTecnica.Dtos.Models;

namespace WebAPIDMSPruebaTecnica.Controllers
{
    //[EnableCors("RulesCorsAPISetting")]
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

        // Método de crear un empleado.
        [HttpPost]
        [Route("create-employee")]
        public IActionResult CreateNewEmployee([FromBody] Employee employee)
        {
            try
            {
                _context.Employees.Add(employee);
                _context.SaveChanges();

                return StatusCode(StatusCodes.Status201Created, new { message = "Empleado Creado Con Éxito." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { message = ex.Message, result = employee });
            }
        }

        // Método para actualizar un empleado.
        [HttpPut]
        [Route("update-employee/ID:int")]
        public IActionResult UpdateEmployee([FromBody] Employee employeeData)
        {
            Employee? employeeUpt = _context.Employees.Find(employeeData.IdEmployee);

            if (employeeUpt == null)
            {
                return BadRequest("Empleado No Encontrado.");
            }

            try
            {
                employeeUpt.NameEmployee = employeeData.NameEmployee is null ? employeeUpt.NameEmployee : employeeData.NameEmployee;
                employeeUpt.LastnameEmployee = employeeData.LastnameEmployee is null ? employeeUpt.LastnameEmployee : employeeData.LastnameEmployee;
                employeeUpt.FkTypeAppointment = employeeData.FkTypeAppointment is Int64 ? employeeUpt.FkTypeAppointment : employeeData.FkTypeAppointment;
                employeeUpt.EmailEmployee = employeeData.EmailEmployee is null ? employeeUpt.EmailEmployee : employeeData.EmailEmployee;
                employeeUpt.DateContratation = employeeData.DateContratation is DateTime ? employeeUpt.DateContratation : employeeData.DateContratation;
                employeeUpt.Avatar = employeeData.Avatar is null ? employeeUpt.Avatar : employeeData.Avatar;

                _context.Employees.Update(employeeUpt);
                _context.SaveChanges();

                return StatusCode(StatusCodes.Status202Accepted, new { message = "Empleado Actualizado Con Éxito." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { message = ex.Message });
            }
        }
    }
}
