using Microsoft.AspNetCore.Mvc;
using MusicPortal.BLL.DTO;
using MusicPortal.BLL.Interfaces;
using MusicPortal.BLL.Infrastructure;

namespace MusicPortal.Controllers
{
    public class MusicController : Controller
    {
        private readonly IEntityService<MusicDTO> musicService;

        public MusicController(IEntityService<MusicDTO> serv)
        {
            musicService = serv;
        }

        public async Task<IActionResult> Index()
        {
            return View(await musicService.GetAll());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MusicDTO music, IFormFile uploadedFile)
        {
            if (uploadedFile != null && uploadedFile.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    await uploadedFile.CopyToAsync(ms);
                    music.AudioData = ms.ToArray();
                }
            }
            ModelState.Remove("AudioData");

            if (ModelState.IsValid)
            {
                await musicService.Create(music);
                return RedirectToAction(nameof(Index));
            }

            return View(music);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }
                MusicDTO music = await musicService.Get((int)id);
                return View(music);
            }
            catch (ValidationException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(MusicDTO music, IFormFile? uploadedFile)
        {
            if (uploadedFile != null && uploadedFile.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    await uploadedFile.CopyToAsync(ms);
                    music.AudioData = ms.ToArray();
                }
            }
            else
            {
                var oldData = await musicService.Get(music.Id);
                music.AudioData = oldData.AudioData;
            }

            ModelState.Remove("AudioData");

            if (ModelState.IsValid)
            {
                await musicService.Update(music);
                return RedirectToAction(nameof(Index));
            }
            return View(music);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }
                MusicDTO music = await musicService.Get((int)id);
                return View(music);
            }
            catch (ValidationException ex)
            {
                return NotFound(ex.Message);
            }   
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await musicService.Delete(id);
            return View("~/Views/Music/Index.cshtml", await musicService.GetAll());


        }
    }
}
