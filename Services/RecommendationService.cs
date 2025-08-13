using Project_Backend_2024.DTO.TechStack;
using SmartPersonalFinancer.Models;
namespace SmartPersonalFinancer.Services;
public class RecommendationService{
  public string Advise(IEnumerable<Expense> exp)=> exp.Sum(e=>e.Amount)>0?"Track categories, set goals":"Add expenses first";
}