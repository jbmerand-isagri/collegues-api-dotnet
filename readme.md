# collegues-api-dotnet

Projet de création d'une RestAPI développée avec .NET Core.

Requêtes :

[GET] /collegues?nom=Dupuis

Retourne un tableau des matricules des collègues portant ce nom.

[GET] /collegues/[matricule]

Retourne un JSON du collègue trouvé ou une erreur 404 si non trouvé.

Code `200` si réussite avec réponse :

```JSON
{
    "matricule": "cfdf8620-f2fc-4526-85e8-37e4df05e008",
    "nom": "Dupuis",
    "prenoms": "Jean",
    "email": "jean.dupuis@mail.com",
    "dateDeNaissance": "1980-01-18T00:00:00",
    "photoUrl": "https://img.huffingtonpost.com/asset/5901e5881400002000a9c22f.jpeg?ops=scalefit_720_noupscale"
}
```