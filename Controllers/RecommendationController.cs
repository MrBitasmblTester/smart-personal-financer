using Project_Backend_2024.DTO.TechStack;
using Microsoft.AspNetCore.Mvc;
namespace SmartPersonalFinancer.Controllers;
[ApiController]
[Route("api/recommendations")]
public class RecommendationController:ControllerBase{
  [HttpGet] public IActionResult Get()=> Ok(new{ message="Start saving 10%"});
}