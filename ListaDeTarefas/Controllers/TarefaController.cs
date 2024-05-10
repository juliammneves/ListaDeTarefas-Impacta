using ListaDeTarefas.Models;
using Microsoft.AspNetCore.Mvc;

namespace ListaDeTarefas.Controllers
{
    public class TarefaController : Controller
    {
        private static List<Tarefa> _tarefas = new List<Tarefa>()
        {
            new Tarefa { ID = 1, Titulo = "Criar apresentação para reunião", Descricao = "Preparar slides e documentos para a reunião com a equipe de projeto.", Status = "A fazer", DataInicio = new DateOnly(2024, 5, 8) },
            new Tarefa { ID = 2, Titulo = "Finalizar relatório mensal", Descricao = "Concluir relatório com os dados do último mês para entrega ao cliente.", Status = "A fazer", DataInicio = new DateOnly(2024, 5, 9) },
            new Tarefa { ID = 3, Titulo = "Agendar reunião com fornecedor", Descricao = "Entrar em contato com o fornecedor para discutir novos termos de contrato.", Status = "A fazer", DataInicio = new DateOnly(2024, 5, 10) }
        };

        public IActionResult Index()
        {
            return View(_tarefas);
        }

        // Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Tarefa tarefa)
        {
            if (ModelState.IsValid) 
            {
                tarefa.ID = _tarefas.Count > 0 ? _tarefas.Max(t => t.ID) + 1 : 1;
                _tarefas.Add(tarefa);
            }
            return RedirectToAction("Index");
        }
        // Fim Create

        // Delete
        public IActionResult Delete(int id) 
        {
            var tarefa = _tarefas.FirstOrDefault(t => t.ID == id);
            if (tarefa == null)
                NotFound();

            _tarefas.Remove(tarefa);
            return RedirectToAction("Index");
        }
        // Fim Delete

        // Edit
        public IActionResult Edit(int id)
        {
            var tarefa = _tarefas.FirstOrDefault(t => t.ID == id);

            if (tarefa == null)
            {
                return NotFound();
            }
            return View(tarefa);
        }

        [HttpPost]
        public IActionResult Edit(Tarefa tarefa)
        {
            if (ModelState.IsValid)
            {
                var existingCliente = _tarefas.FirstOrDefault(t => t.ID == tarefa.ID);
                if (existingCliente != null)
                {
                    existingCliente.Titulo = tarefa.Titulo;
                    existingCliente.Descricao = tarefa.Descricao;
                    existingCliente.Status = tarefa.Status;
                    existingCliente.DataInicio = tarefa.DataInicio;
                    existingCliente.DataConclusao = tarefa.DataConclusao;
                }
                return RedirectToAction("Index");
            }
            return View(tarefa);
        }
        // Fim Edit

        // Details
        public IActionResult Details(int id)
        {
            var tarefa = _tarefas.FirstOrDefault(t => t.ID == id);

            if (tarefa == null)
            {
                return NotFound();
            }
            return View(tarefa);
        }
        // Fim Details

        // Tarefas a fazer
        public IActionResult TarefaFazer()
        {
            var tarefasfazer = _tarefas.Where(t => t.Status == "A fazer").ToList();
            return View(tarefasfazer);
        }
        // Fim Tarefas a fazer

        // Tarefas feitas
        public IActionResult TarefaFeita()
        {
            var tarefasfeitas = _tarefas.Where(t => t.Status == "Concluído").ToList();
            return View(tarefasfeitas);
        }
        // Fim Tarefas a feitas

        // Concluir
        public IActionResult Concluir(int id)
        {
            var tarefa = _tarefas.FirstOrDefault(t => t.ID == id);
            if (tarefa == null)
                NotFound();

            tarefa.Status = "Concluído";
            tarefa.DataConclusao = DateOnly.FromDateTime(DateTime.Now);
            return RedirectToAction("Index");
        }
        // Fim concluir
    }
}
