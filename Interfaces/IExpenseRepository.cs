using Project_Backend_2024.DTO.TechStack;
using SmartPersonalFinancer.Models;
namespace SmartPersonalFinancer.Interfaces;
public interface IExpenseRepository{ IEnumerable<Expense> All(); void Add(Expense e); }