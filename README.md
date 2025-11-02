Coleções do MongoDB

**Usuarios**
```json
[
  {
    "id": "69066e7f50e348353d1be768",
    "nome": "Arthur",
    "userName": "teste",
    "senha": "***",
    "dataNascimento": "2002-10-12T00:00:00Z"
  },
  {
    "id": "6907a8516949268c19844cae",
    "nome": "Helena Edim",
    "userName": "edimhelena",
    "senha": "senha123",
    "dataNascimento": "2006-03-23T18:51:23.562Z"
  },
  {
    "id": "6907a8dd6949268c19844caf",
    "nome": "Felipe Augusto",
    "userName": "lipelopes",
    "senha": "felipaoDoGrau!!!",
    "dataNascimento": "2002-05-28T18:53:13.369Z"
  },
  {
    "id": "6907a9206949268c19844cb1",
    "nome": "Camily",
    "userName": "camilyyy",
    "senha": "camily13",
    "dataNascimento": "2010-11-02T18:54:55.992Z"
  },
  {
    "id": "6907a9546949268c19844cb2",
    "nome": "Sarah",
    "userName": "sarinha",
    "senha": "blabla123",
    "dataNascimento": "2008-07-04T18:55:40.453Z"
  }
]
```

**Publicacoes**
```json
[
  {
    "id": "6907aaf4221acce35dd14e29",
    "idUsuario": "6907a9546949268c19844cb2",
    "descricao": "post da sarah",
    "data": "2025-11-02T19:02:15.835Z",
    "hashtags": []
  },
  {
    "id": "6907ab4b221acce35dd14e2a",
    "idUsuario": "6907a9206949268c19844cb1",
    "descricao": "post da camily",
    "data": "2025-11-02T19:02:15.835Z",
    "hashtags": [
      "teste"
    ]
  },
  {
    "id": "6907abaf221acce35dd14e2b",
    "idUsuario": "6907a8516949268c19844cae",
    "descricao": "post da helena",
    "data": "2025-11-02T19:02:15.835Z",
    "hashtags": [
      "teste",
      "api"
    ]
  },
  {
    "id": "6907abcd221acce35dd14e2c",
    "idUsuario": "6907a8516949268c19844cae",
    "descricao": "post da helena 2",
    "data": "2025-11-02T19:02:15.835Z",
    "hashtags": [
      "teste",
      "api",
      "bd"
    ]
  },
  {
    "id": "6907ac39221acce35dd14e2d",
    "idUsuario": "6907a8dd6949268c19844caf",
    "descricao": "post do felipao",
    "data": "2025-11-02T19:02:15.835Z",
    "hashtags": [
      "amoOcaram"
    ]
  }
]
```
**Curtidas**
```json
[
  {
    "id": "6907af71221acce35dd14e38",
    "idPublicacao": "6907abcd221acce35dd14e2c",
    "idUsuario": "6907a8dd6949268c19844caf",
    "data": "2025-11-02T19:21:43.841Z"
  },
  {
    "id": "6907af8b221acce35dd14e39",
    "idPublicacao": "6907abcd221acce35dd14e2c",
    "idUsuario": "6907a9206949268c19844cb1",
    "data": "2025-11-02T19:21:43.841Z"
  },
  {
    "id": "6907afba221acce35dd14e3a",
    "idPublicacao": "6907ab4b221acce35dd14e2a",
    "idUsuario": "6907a8dd6949268c19844caf",
    "data": "2025-11-02T19:21:43.841Z"
  },
  {
    "id": "6907afeb221acce35dd14e3b",
    "idPublicacao": "6907aaf4221acce35dd14e29",
    "idUsuario": "6907a8516949268c19844cae",
    "data": "2025-11-02T19:21:43.841Z"
  },
  {
    "id": "6907b018221acce35dd14e3c",
    "idPublicacao": "6907abaf221acce35dd14e2b",
    "idUsuario": "69066e7f50e348353d1be768",
    "data": "2025-11-02T19:21:43.841Z"
  }
]
```
**Comentarios**
```json
[
  {
    "id": "6907acb0221acce35dd14e2e",
    "idUsuario": "69066e7f50e348353d1be768",
    "idPublicacao": "6907ac39221acce35dd14e2d",
    "descricao": "coé Felipão, comentario do arthur",
    "data": "2025-11-02T19:09:31.739Z"
  },
  {
    "id": "6907acdc221acce35dd14e2f",
    "idUsuario": "69066e7f50e348353d1be768",
    "idPublicacao": "6907ac39221acce35dd14e2d",
    "descricao": "coé Felipão, comentario do arthur 2",
    "data": "2025-11-02T19:09:31.739Z"
  },
  {
    "id": "6907ad29221acce35dd14e30",
    "idUsuario": "6907a8516949268c19844cae",
    "idPublicacao": "6907ab4b221acce35dd14e2a",
    "descricao": "comentario da helena no post da camily",
    "data": "2025-11-02T19:09:31.739Z"
  },
  {
    "id": "6907ad80221acce35dd14e31",
    "idUsuario": "6907a8dd6949268c19844caf",
    "idPublicacao": "6907aaf4221acce35dd14e29",
    "descricao": "comentario do felipe no post da sarah",
    "data": "2025-11-02T19:09:31.739Z"
  },
  {
    "id": "6907adb6221acce35dd14e32",
    "idUsuario": "6907a9206949268c19844cb1",
    "idPublicacao": "6907abaf221acce35dd14e2b",
    "descricao": "comentario da camily no post da helena",
    "data": "2025-11-02T19:09:31.739Z"
  }
]
```
**Salvos**
```json
[
  {
    "id": "6907ae20221acce35dd14e33",
    "idUsuario": "6907a8dd6949268c19844caf",
    "idPublicacao": "6907ab4b221acce35dd14e2a"
  },
  {
    "id": "6907ae81221acce35dd14e34",
    "idUsuario": "6907a9546949268c19844cb2",
    "idPublicacao": "6907abaf221acce35dd14e2b"
  },
  {
    "id": "6907aea1221acce35dd14e35",
    "idUsuario": "6907a9546949268c19844cb2",
    "idPublicacao": "6907abcd221acce35dd14e2c"
  },
  {
    "id": "6907aeda221acce35dd14e36",
    "idUsuario": "69066e7f50e348353d1be768",
    "idPublicacao": "6907ac39221acce35dd14e2d"
  },
  {
    "id": "6907af0d221acce35dd14e37",
    "idUsuario": "6907a9206949268c19844cb1",
    "idPublicacao": "6907ac39221acce35dd14e2d"
  }
]
```



