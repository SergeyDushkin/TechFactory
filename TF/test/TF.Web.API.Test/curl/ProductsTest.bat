echo off

cls

echo Test Products endpoint
echo GET
curl http://localhost:5588/odata/Products

echo POST
curl -H content-type:application/json -X POST http://localhost:5588/odata/Products -d "{Id:'5d7b24fd-18d1-4fd4-9247-9497f98a12dd',Type:'REGULAR',Key:'Bolognese REG',Name:'Bolognese regular'}"

echo POST PRICE
curl -H content-type:application/json -X POST http://localhost:5588/odata/Products(5d7b24fd-18d1-4fd4-9247-9497f98a12dd)/Price -d "{Id:'5d7b24fd-18d1-4fd4-9247-9497f98a12dd',Type:'REGULAR',Key:'Bolognese REG',Name:'Bolognese regular'}"

echo POST CATEGORY
curl -H content-type:application/json -X POST http://localhost:5588/odata/Products(5d7b24fd-18d1-4fd4-9247-9497f98a12dd)/Categories -d "{Id:'5d7b24fd-18d1-4fd4-9247-9497f98a12de',CategoryId:'5d7b24fd-18d1-4fd4-9247-9497f98a12df'}"

echo PUT CATEGORY
curl -H content-type:application/json -X POST http://localhost:5588/odata/Products(5d7b24fd-18d1-4fd4-9247-9497f98a12dd)/Categories(5d7b24fd-18d1-4fd4-9247-9497f98a12de) -d "{Id:'5d7b24fd-18d1-4fd4-9247-9497f98a12de',CategoryId:'5d7b24fd-18d1-4fd4-9247-9497f98a12df'}"

echo DELETE
curl -H content-type:application/json -X DELETE http://localhost:5588/odata/Products(5d7b24fd-18d1-4fd4-9247-9497f98a12dd)
