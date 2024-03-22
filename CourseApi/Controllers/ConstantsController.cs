using CourseApi.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CourseApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConstantsController : ControllerBase
    {
        [HttpGet]
        [Route("statuses")]
        public IActionResult GetCourseStatuses()
        {
            List<string> statuses = new List<string>()
            {
                CourseStatuses.Free,
                CourseStatuses.Subscription,
                CourseStatuses.Locked,
                CourseStatuses.Paid
            };

            return Ok(statuses);
        }

        [HttpGet]
        [Route("skillLevels")]
        public IActionResult GetCourseSkillLevels()
        {
            List<string> skillLevels = new List<string>()
            {
                SkillLevels.Beginner,
                SkillLevels.Intermediate,
                SkillLevels.Advanced,
                SkillLevels.Expert
            };

            return Ok(skillLevels);
        }
    }
}
