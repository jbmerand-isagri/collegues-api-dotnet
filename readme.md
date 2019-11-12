# collegues-api-dotnet

Projet de création d'une RestAPI développée avec .NET Core.

Requêtes :

## [GET] /collegues?nom=Dupuis

Retourne un tableau des matricules des collègues portant ce nom.

## [GET] /collegues/[matricule]

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

## [POST] /collegues
```JSON
{
    "nom": "Dupuis",
    "prenoms": "Jean",
    "email": "jean.dupuis@mail.com",
    "dateDeNaissance": "2000-01-18T00:00:00",
    "photoUrl": "https://img.huffingtonpost.com/asset/5901e5881400002000a9c22f.jpeg?ops=scalefit_720_noupscale"
}
```

Ajoute un collègue à la base de données. Retourne le collègue ajouté avec son matricule. Attention, formats imposés :
```c#
collegue.Nom.Length >= 2 && collegue.Prenoms.Length >= 2 && collegue.Email.Length >= 3
&& collegue.Email.Contains("@") && collegue.PhotoUrl.StartsWith("http") && years >= 18
```

Si succès : `code 200`
```JSON
{
    "matricule": "cfdf8620-f2fc-4526-85e8-37e4df05e008",
    "nom": "Dupuis",
    "prenoms": "Jean",
    "email": "jean.dupuis@mail.com",
    "dateDeNaissance": "2000-01-18T00:00:00",
    "photoUrl": "https://img.huffingtonpost.com/asset/5901e5881400002000a9c22f.jpeg?ops=scalefit_720_noupscale"
}
```

Si échec : `code 400`

## [PATCH] /collegues/[matricule]
```JSON
{
    "email": "jean.dupuis@mail.fr",
    "photoUrl": "http://img.com"
}
```

Modifie un collègue (que email ou photoUrl possible, sinon les deux)

Si succès : `200`
```JSON
{
    "matricule": "acf0ff79-3dba-4e4b-a401-1819023d7ae8",
    "nom": "Dupuis",
    "prenoms": "Jean",
    "email": "jean.dupuis@mail.fr",
    "dateDeNaissance": "2000-01-18T00:00:00",
    "photoUrl": "http://img.com"
}
```

Si échec : `code 400` (requête invalide) ou `code 404` (collègue non trouvé)

