Identity Managment System. Used best practises mostly, sometimes choosing to save time instead.

In the future consider changing repository async operations to synchronous, depending on database used.


Db code run

`dotnet ef migrations add InitialCreate --project Persistance --startup-project API`

`dotnet ef database update --project Persistance --startup-project API`
`