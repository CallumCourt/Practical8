
using Microsoft.AspNetCore.Mvc;

using SMS.Data.Entities;
using SMS.Data.Services;

namespace SMS.Web.Controllers;
public class StudentController : Controller
{
    private IStudentService svc;

    public StudentController()
    {
        svc = new StudentServiceDb();            
    }

    // GET /student
    public IActionResult Index()
    {
        // TBC - load students using service and pass to view
        var data = svc.GetStudents();
        
        return View(data);
    }

    // GET /student/details/{id}
    public IActionResult Details(int id)
    {
        // retrieve the student with specifed id from the service
        var s = svc.GetStudent(id);
      
        // TBC check if s is null and return NotFound()  

        // pass student as parameter to the view
        return View(s);
    }

    // GET: /student/create
    public IActionResult Create()
    {
        // display blank form to create a student
        return View();
    }

    // POST /student/create
    [HttpPost]
    public IActionResult Create(Student s)
    {
        Console.WriteLine($"Post: {s}");
        
        // complete POST action to add student
        if (ModelState.IsValid)
        {
            // TBC call service AddStudent method using data in s
            s = svc.AddStudent(s);
            if (s is not null)
            {
                return RedirectToAction(nameof(Details), new { Id = s.Id});
            }
        }
        
        // redisplay the form for editing as there are validation errors
        return View(s);
    }

    // GET /student/edit/{id}
    public IActionResult Edit(int id)
    {
        // load the student using the service
        var s = svc.GetStudent(id);

        // TBC check if s is null and return NotFound()
        if (s is null)
        {
            return NotFound();
        }  

        // pass student to view for editing
        return View(s);
    }

    // POST /student/edit/{id}
    [HttpPost]
    public IActionResult Edit(int id, Student s)
    {
        // complete POST action to save student changes
        if (ModelState.IsValid)
        {
            svc.UpdateStudent(s);

            return RedirectToAction(nameof(Index));
        }

        // redisplay the form for editing as validation errors
        return View(s);
    }

    // GET / student/delete/{id}
    public IActionResult Delete(int id)
    {
        // load the student using the service
        var s = svc.GetStudent(id);
        // check the returned student is not null and if so return NotFound()
        if (s == null)
        {
            return NotFound();
        }     
        
        // pass student to view for deletion confirmation
        return View(s);
    }

    // POST /student/delete/{id}
    [HttpPost]
    public IActionResult DeleteConfirm(int id)
    {
        // TBC delete student via service
        svc.DeleteStudent(id);
        
        // redirect to the index view
        return RedirectToAction(nameof(Index));
    }

     // ============== Student ticket management ==============

    // GET /student/ticketcreate/{id}
    public IActionResult TicketCreate(int id)
    {
        var s = svc.GetStudent(id);
        if (s == null)
        {
            return NotFound();
        }

        // create a ticket view model and set foreign key
        var ticket = new Ticket { StudentId = id }; 
        // render blank form
        return View( ticket );
    }

    // POST /student/ticketcreate
    [HttpPost]
    public IActionResult TicketCreate(Ticket t)
    {
        if (ModelState.IsValid)
        {                
            var ticket = svc.CreateTicket(t.StudentId, t.Issue);
            return RedirectToAction(nameof(Details), new { Id = ticket.StudentId });
        }
        // redisplay the form for editing
        return View(t);
    }

    // GET /student/ticketdelete/{id}
    public IActionResult TicketDelete(int id)
    {
        // load the ticket using the service
        var ticket = svc.GetTicket(id);
        // check the returned Ticket is not null and if so return NotFound()
        if (ticket == null)
        {
            return NotFound();
        }     
        
        // pass ticket to view for deletion confirmation
        return View(ticket);
    }

    // POST /student/ticketdeleteconfirm/{id}
    [HttpPost]
    public IActionResult TicketDeleteConfirm(int id)
    {
        // TBC 
        
        // delete student via service
            
        // Q4 replace with redirect to List of students       
        // Q5 replace redirect to List of students with redirect to current student 
        return NotFound();
    }

}