using System;
using System.Linq;
using ServiceStack;
using GDIApps.ServiceModel;
using BusinessRules;
using ServiceStack.Auth;
using ServiceStack.Data;
using System.Globalization;

namespace GDIApps.ServiceInterface;

public class TodosServices : Service
{
    public IAutoQueryData AutoQuery { get; set; }

    static readonly PocoDataSource<Todo> Todos = PocoDataSource.Create(new Todo[]
    {
        new () { Id = 1, Text = "Learn" },
        new () { Id = 2, Text = "Vue", IsFinished = true },
        new () { Id = 3, Text = "Vite!" },
    }, nextId: x => x.Select(e => e.Id).Max());

    public object Get(QueryTodos query)
    {
        var db = Todos.ToDataSource(query, Request);
        return AutoQuery.Execute(query, AutoQuery.CreateQuery(query, Request, db), db);
    }

    public Todo Post(CreateTodo request)
    {
        var newTodo = new Todo { Id = Todos.NextId(), Text = request.Text };
        Todos.Add(newTodo);
        return newTodo;
    }
    public CreateClaimResponse Post(CreateOvertimeDraft request)
    {
        CreateClaimResponse response= new CreateClaimResponse();
        foreach(var empId in request.EmployeeIds)
        {
            var itemResponse = new CreateClaimItemResponse();
            try
            {
                DateTime dt=DateTime.ParseExact(request.OtDate,"dd-MMM-yyyy",CultureInfo.GetCultureInfo("en-US"));
                string newOtNuumber = _rules.CreateNewOvertimeDraft(dt, empId);
                itemResponse.Success= true;
                itemResponse.OtNumber = newOtNuumber;
                
            }
            catch (Exception ex)
            {
                itemResponse.Success = false;
                itemResponse.ErrorMessage = ex.Message;
            }
            response.Items.Add(itemResponse);
        }
        return response;
       
    }
    public Todo Put(UpdateTodo request)
    {
        var todo = request.ConvertTo<Todo>();
        Todos.TryUpdateById(todo, todo.Id);
        return todo;
    }
    LogicRules _rules = null;
    private LogicRules Rules
    {
        get
        {
            if (_rules == null)
            {

                var dbcnf = this.Resolve<IDbConnectionFactory>();
                var session = SessionAs<AuthUserSession>();
                IUserAuth user2 = AuthRepository.GetUserAuth(session.UserAuthId);
                var authSession = this.GetSession();
                var externalData=this.TryResolve<IExternalData>();
                if (authSession != null)
                {
                    var user = this.GetSession().GetUserAuthName();
                    var userData = AuthRepository.GetUserAuthByUserName(user);
                    _rules = new LogicRules(dbcnf, userData,externalData);
                }
                else
                {
                    _rules = new LogicRules(dbcnf, null,externalData);
                }

            }
            return _rules;
        }
    }

    // Handles Deleting the Todo item
    public void Delete(DeleteTodo request) => Todos.TryDeleteById(request.Id);

    public void Delete(DeleteTodos request) => Todos.TryDeleteByIds(request.Ids);
    //public object Any(Hello request)
    //{
    //    return new HelloResponse { Result = $"Hello, {request.Name}!" };
    //}
}
