echo off

cls

echo Test Products endpoint
echo GET
curl http://partner-dev-srv.cloudapp.net/v1/odata/Products

echo POST
curl -H content-type:application/json -X POST http://partner-dev-srv.cloudapp.net/v1/odata/Products -d "{Id:'5d7b24fd-18d1-4fd4-9247-9497f98a12dd',Type:'REGULAR',Key:'Bolognese REG',Name:'Bolognese regular'}"

echo DELETE
curl -H content-type:application/json -X DELETE http://partner-dev-srv.cloudapp.net/v1/odata/Products(Guid'5d7b24fd-18d1-4fd4-9247-9497f98a12dd')
