using abcde.vAPI.Controllers.Base;
using abcde.Data.Interfaces;
using abcde.Model;
using abcde.Model.Filters;
using abcde.Model.Summary;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using abcde.Model.Dtos;

namespace abcde.vAPI.Controllers.v1
{
    [AllowAnonymous]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("/api/v{version:apiVersion}/[controller]")]
    public class NotesController : GenericsTenantController<Note, NoteSummary, NoteFilter>
    {
        private readonly IValidator<Note> validator;

        #region

        public NotesController(INoteRepository noteRepository, IValidator<Note> validator) : base(noteRepository)
        {
            this.validator = validator;
        }

        #endregion

        /// <summary>
        /// Add note
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost("CreateNote")]
        public async Task<IActionResult> CreateNote([FromBody] CreateNoteRequest request)
        {
            try
            {
                Note entity = new Note()
                {
                    Id = request.Id,
                    NoteText = request.NoteText,
                    Date = request.Date,
                    UserId = request.UserId,
                    WorkItemId = request.WorkItemId
                };
                var result = await validator.ValidateAsync(entity);

                if (result.IsValid)
                {
                    return await base.Post(entity);
                }

                return BadRequest("Note is missing information, please update");
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);

                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Update note
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPut]
        public override async Task<IActionResult> Put([FromBody] Note entity)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(TenantId))
                {
                    entity.TenantId = TenantId;
                }

                entity.LastModifiedBy = UserId;
                var entityUpdated = await GenericTenantAsyncRepository.UpdateAsync(entity);

                return Ok(entityUpdated);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);

                return BadRequest(ex.Message);
            }
        }
    }
}