using Microsoft.AspNetCore.Mvc;
using TaskManager.Domain.Entities;
using TaskManager.Domain.IRepositories;
using Microsoft.EntityFrameworkCore;

//Main API Controller

namespace TaskManagerTT.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaskManager : ControllerBase
    {
        
        private IUnitOfWork _unitOfWork;
        private ILogger<TaskManager> _logger;
        public TaskManager(IUnitOfWork unitOfWork, ILogger<TaskManager> logger)
        {
            //Injecting Unit of Work to have access to Repositaries' CRUD operations
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        //Method to get all tasks including projects to which mentioned tasks are linked
        [HttpGet]
        [Route("Tasks")]
        public async Task<ActionResult<IEnumerable<SiTask>>> GetAllTasks() 
        {
            var result = await _unitOfWork.RepoTasks.GetAllQuery().ToListAsync();
            return Ok(result);
        }

        //Method to get a single Task by Id including referenced Project
        [HttpGet]
        [Route("TaskGet")]
        public async Task<ActionResult<SiTask>> TaskGet(int id)
        {
            var result = await _unitOfWork.RepoTasks.GetEntity(id).SingleOrDefaultAsync();
            return Ok(result);
        }

        [HttpPost]
        [Route("TaskAdd")]
        // method to add new task with linking to a existing project
        public async Task<IActionResult> TaskAdd([FromBody] SiTask task) 
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            // Try perform DB transaction
            try
            {
                _unitOfWork.TransactionBegin(); 
                _unitOfWork.RepoTasks.Add(task);
                await _unitOfWork.CommitAsync();
                task.Project = await _unitOfWork.RepoProjects.GetEntity(task.ProjectId).SingleOrDefaultAsync();
                _unitOfWork.TransactionCommit();
                
            }
            // If an error occured, catch error, log error, rollback transaction and return status 500
            catch (Exception ex) 
            {
                _logger.LogError(ex.Message, ex);
                _unitOfWork.TransactionRollback();
                return StatusCode(500);
            }
            return Ok(task);
        }

        // method to edit task with new field values
        [HttpPost]
        [Route("TaskUpdate")]
        public async Task<IActionResult> TaskUpdate([FromBody] SiTask task) 
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                _unitOfWork.TransactionBegin();
                _unitOfWork.RepoTasks.Update(task);
                await _unitOfWork.CommitAsync();
                task.Project = await _unitOfWork.RepoProjects.GetEntity(task.ProjectId).SingleOrDefaultAsync();
                _unitOfWork.TransactionCommit();
            }
            catch (DbUpdateConcurrencyException ex0)
            {
                _logger.LogError(ex0.Message, ex0);
                _unitOfWork.TransactionRollback();
                //Do somethig to inform client about Concurency exception
                return StatusCode(500);
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex.Message, ex);
                _unitOfWork.TransactionRollback();
                return StatusCode(500);
            }
            return Ok(task);
        }
        //method to delete single task
        [HttpDelete]
        [Route("TaskDelete")]
        public async Task<ActionResult<IEnumerable<SiTask>>> TaskDelete([FromBody] SiTask task) 
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                _unitOfWork.TransactionBegin();
                _unitOfWork.RepoTasks.Add(task);
                _unitOfWork.RepoTasks.Remove(task);
                await _unitOfWork.CommitAsync();
                _unitOfWork.TransactionCommit();
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex.Message, ex);
                _unitOfWork.TransactionRollback();
                return StatusCode(500);
            }
            return Ok();
        }

        //method to get all projects with linked tasks
        [HttpGet]
        [Route("Projects")]
        public async Task<ActionResult<IEnumerable<SiTask>>> GetAllProjects()
        {
            var res = await _unitOfWork.RepoProjects.GetAllQuery().ToListAsync();
            
            return Ok(res);
        }
        //method to get single project including linked tasks
        [HttpGet]
        [Route("ProjectGet")]
        public async Task<ActionResult<IEnumerable<SiTask>>> ProjectGet(int id) 
        {
            var res = await _unitOfWork.RepoProjects.GetEntity(id).SingleOrDefaultAsync();
            return Ok(res);
        }

        //method to add new project(without linked tasks or with)
        [HttpPost]
        [Route("ProjectAdd")]
        public async Task<IActionResult> ProjectAdd([FromBody] SiProject project)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                _unitOfWork.TransactionBegin();
                _unitOfWork.RepoProjects.Add(project);
                await _unitOfWork.CommitAsync();
                _unitOfWork.TransactionCommit();
            }
            catch (Exception ex)
            {
                //Log the exception and rollback the transaction
                _logger.LogError(ex.Message, ex);
                _unitOfWork.TransactionRollback();
                return StatusCode(500);
            }

            return Ok(project);
        }
        // method to edit existing project
        [HttpPost]
        [Route("ProjectUpdate")]
        public async Task<IActionResult> ProjectUpdate([FromBody] SiProject project) 
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                _unitOfWork.TransactionBegin();
                _unitOfWork.RepoProjects.Update(project);
                await _unitOfWork.CommitAsync();
                _unitOfWork.TransactionCommit();
            }
            catch (DbUpdateConcurrencyException ex0)
            {
                _logger.LogError(ex0.Message, ex0);
                _unitOfWork.TransactionRollback();
                //Do something to inform client about Concurency exception (further action from clent)
                return StatusCode(500);
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex.Message, ex); 
                _unitOfWork.TransactionRollback();
                return StatusCode(500);
            }
            return Ok(project);
        }
        // method to delete single project including linked tasks
        [HttpDelete]
        [Route("ProjectDelete")]
        public async Task<ActionResult<IEnumerable<SiTask>>> ProjectDelete([FromBody] SiProject project) 
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                _unitOfWork.TransactionBegin();
                _unitOfWork.RepoProjects.Add(project);
                _unitOfWork.RepoProjects.Remove(project);
                await _unitOfWork.CommitAsync();
                _unitOfWork.TransactionCommit();
            }
            catch(Exception ex) 
            {
                //Log the exception and rollback the transaction
                _logger.LogError(ex.Message);
                _unitOfWork.TransactionRollback();
                return StatusCode(500);
            }
            return Ok();
        }
    }
}