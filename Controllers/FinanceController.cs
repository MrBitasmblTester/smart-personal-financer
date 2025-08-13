using Project_Backend_2024.DTO.TechStack;
using Microsoft.AspNetCore.Mvc;
namespace SmartPersonalFinancer.Controllers;
[ApiController]
[Route("api/expenses")]
public class FinanceController: ControllerBase{
  [HttpGet] public IActionResult Get()=> Ok(new[]{ new{ id=1, category="misc", amount=0 } });
  [HttpPost] public IActionResult Create([FromBody] object dto)=> Created("/api/expenses/1", dto);
}