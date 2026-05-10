using Business.Abstract;
using DAL.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles="Parent")]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _service;
        private readonly IUnitOfWork _unitOfWork;

        public ReportController(IReportService service, IUnitOfWork unitOfWork)
        {
            _service = service;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetMyStudentReport()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var parent = (await _unitOfWork.Parents
                .GetWhereAsync(x => x.UserId == userId))
                .FirstOrDefault();

            if (parent == null)
                return BadRequest("Parent tapilmadi");

            var student = (await _unitOfWork.Students
                .GetWhereAsync(x => x.ParentId == parent.Id))
                .FirstOrDefault();

            if (student == null)
                return BadRequest("Student tapilmadi");

            var result = await _service.GetStudentMonthlyReport(student.Id);

            return Ok(result);
        }
    }
}
