# Blog - Asp.NET MVC

## préambule

CMS type WordPress pouvant directement être déployé en production et permettant de gérer :

+ gérer des pages qui on pour but de rester statique
+ Gérer des Articles 
+ Un formulaire de contact
+ Des tags liés à plusieurs articles et un article lié à plusieurs pages (M - N)
+ Des catégories liés à plusieurs articles et un article lié à une seule catégorie (1 - N)
+ Un backOffice protégé par Mot de Passe
+ Un listing des utilisateurs
+ ...

* * *
# Model 

## Tags 
### Attribut :

+ TagId (Int)
+ Name (String)
+ Posts (List<Post>)


## Category 
### Attribut :

+ CategoryId (Int)
+ Name (String)
+ Posts (List<Post>)

## Page
### Attribut :

+ PageId (Int)
+ Title (String)
+ Author (String)
+ Date (DateTime)
+ Content (String)
+ Online (Bool)

## Post
### Attribut :

+ PostId (Int)
+ Title (String)
+ Slug (String)
+ Author (String)
+ Tags (List<Post>)
+ Date (DateTime)
+ Content (String)
+ Online (Bool)
+ CategoryId (Int)
+ Categorie (Category)
### Information :
+ categoryId permet de faire la jointure 1 - N
+ Date permet de créer l'archivage
+ Online permet de désactiver un article facilement et sans le supprimer pour autant
+ Slug permet de stocker et gérer plus facilement la réécriture d'url pour l'accès aux articles (SEO frendly)

## Message 
### Attribut :

+ MessageId (Int)
+ Name (String)
+ Site (String)
+ Title (String)
+ Content (String)
+ isRead (Bool)
+ isConfirm (Bool)

### Information :
les deux flags isRead et isConfirm permette de déterminer si le message a été lue par l'admin et si il a été confirmé par l'utilisateur

## Populate
###information 
Routine permettant de peupler la base à chaque modification des models.
Il est aussi possible de forcer l'update de la base de donnée en lançant dans la **Package Manager Console**
>`Update-Database`


## PostModelView
### Attribut :
+ SelectedIds (String[])
+ Items ( IEnumerable<SelectListItem> )
+ currentPost (Post)

### Information :
C'est un model de view permettant à razor de pouvoir générer des selects multiples. Nous nous en servons pour gérer les tags pour un article donné.

***

#Controller
## Règle de nommage :
Le choix a été fait de préfixer les controller par "Admin" pour dissocier la partie front du back

**les détails si dessous ne sont pas exaustif**

###PostController
`public ActionResult getWithLimit(int limit)` permet de récupérer une liste d'articles classé par ordre de parution décroissant

`public ActionResult Navigation(int category, string title)` permet de récupérer la liste des articles liés à une catégorie

`public ActionResult GetArchives(string month, string year)` permet de récupérer la liste des articles pour un mois et une année donnée

`public ActionResult Archive()` Permet de générer l'archive (liste des mois / année ou des articles ont étés parus)

###AdminPostController
La liste des postes est différentes en fonction du Role de l'utilisateur. Si il n'est pas Admin, il ne verra que les posts dont il est l'auteur.

###AdminMessageController
N'affiche que les méssages qui on été confirmé par l'expediteur

###AdminPostController
`public ActionResult Create([Bind(Prefix = "currentPost")] Post post, int CategoryId, int[] SelectedIds)` 

L'ajout de la ModelView ajouter le préfixe currentPost au nom des champs, il faut donc dire à Bind de préfixer les champs pour que la réfléxion fonctionne.