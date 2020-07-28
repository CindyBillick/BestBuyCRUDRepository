using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using System.Text;

namespace BestBuyCRUDBestPracticeConsoleUI
{
    public class DapperDepartmentRepository : IDepartmentRepository
    {   
        //Field or (local) variable for making queries to the DB
        private readonly IDbConnection _connection;
        public DapperDepartmentRepository(IDbConnection connection)
        {
            //Constructor
            _connection = connection;
        }
        
                
        public IEnumerable<Department> GetAllDepartments()
        {
            return _connection.Query<Department>("SELECT * FROM departments");

            


        }

        public void InsertDepartment(string newDepartmentName)
        {
            _connection.Execute("INSERT INTO DEPARTMENTS(Name)VALUES (@departmentName);",
            new { departmentName = newDepartmentName });    
        }

   }
    }


   
