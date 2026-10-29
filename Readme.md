# Assure-toi d'avoir le client Ef Core.

# IMPORTANT : Cette activité se fait sans copilot. Tu peux poser des questions à L'IA mais sans y 'domper' du code.

1. Avoir installé le client Ef Core https://learn.microsoft.com/en-us/ef/core/get-started/overview/install#get-the-net-cli-tools
1. Posséder une instance de MariadDB sur ta machine avec l'utilisateur root:root
1. Faire un build de la solution.

Tu vas avoir des erreurs. Pour que le build fonctionne, tu devras mettre en commentaire les useCases qui n'existent pas encore dans DependencyInjection. Tu devras aussi créer un nouveau DTO qui se nomme `CreateTodoDTO`. Cette classe ne contient que deux propriétés qui sont fournies lors de la création d'un DTO. Peux-tu deviner lesquelles?

Tu dois aussi décharger le projet Todo.Tests (clic droit sur le projet, décharger)

## Quand le build fonctionne.

1. Ouvrir une fenêtre console dans visual studio en cliquant droit sur le projet CleanTodo.Infrastructure + terminal
1. Faire la commande ci-dessous

```
dotnet ef database update --startup-project ..\CleanTodo.WebAPI --project .\
```

Ce que ça fait? Ça applique les migrations dans le dossier migrations.

**Q:** : Va voir le fichier InitialCreate. Qu'est-ce qu'il fait, que fait Up? Que fait Down?
**R** :

Si tu vas voir dans MariadDB, tu vas remarquer que deux tables sont créees à partir de notre modèle.

# Pour revenir sur l'architecture

Afin de bien comprendre le fonctionnement de l'architecture, base-toi sur le fonctionnement du FindById et crée la route pour ajouter, modifier et supprimer un todo.

Une fois que c'est fait, tu peux recharger le projet tests et valider si ton application fonctionne adéquatement.

Voici ce que tu auras à faire pour l'ajout. Termine par la mise à jour d'un todo (toggle du statut `IsCompleted`)

1. Ajouter une méthode dans l'interface du Repository qui permet d'ajouter un todo de type Todo. Elle est déjà présente dans la classe TodoRepository.
2. Ajouter un useCase dans le dossier Todo de la couche Application.
3. Appelle la méthode Add dans le UseCase pour créer le nouveau Todo.
4. Validation du Todo.

Afin de valider le TodoDTO, on utilise un validator de la librairie FluentValidation. Voici un extrait de code :
Voici un bout de code pour t'aider.

```C#
using FluentValidation;
using FluentValidation.Results;

namespace CleanTodo.Domain.UseCase;

public class CreateTodoUseCase
{
    private readonly ITodoRepository _todoRepository;
    private readonly IValidator<CreateTodoDto> _validator;

    public CreateTodoUseCase(ITodoRepository todoRepository, IValidator<CreateTodoDto> validator)
    {
        _todoRepository = todoRepository;
        _validator = validator;
    }

    public async Task<TodoDto> Execute(CreateTodoDto createTodoDto)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(createTodoDto);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
```

Attention, le useCase reçoit un CreateTodoDTO en paramètre et retourne un TodoDto. De plus, le repo reçoit un Todo en paramètre. C'est donc au useCase de transformer le Todo en ses différentes formes.

5. Ajoute le useCase dans le dossier DependencyInjection du projet Application. Ça permet de passer le UseCase dans le constructeur et d'y injecter le repo automatiquement.
6. Ajoute le useCase dans le contrôlleur et teste le tout via le swagger. Le code est en commentaire.
7. Fait les mêmes opérations pour la route delete et update. Pour la suppression dans le repo, tu peux demander à ChatGPT comment faire.


Le premier ajout devrait te prendre une heure tout au plus. Les suivants seront plus rapides et simples à faire.
