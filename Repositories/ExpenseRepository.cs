using Project_Backend_2024.DTO.TechStack;
using SmartPersonalFinancer.Interfaces;
using SmartPersonalFinancer.Models;
using SmartPersonalFinancer.Data;
public class ExpenseRepository:IExpenseRepository{
  public IEnumerable<Expense> All()=>InMemoryDb.Expenses;
  public void Add(Expense e)=>InMemoryDb.Expenses.Add(e);
}