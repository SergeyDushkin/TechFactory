echo off

cls

echo Test Products endpoint
REM echo GET
REM curl http://localhost:5588/odata/Products

curl -H content-type:application/json -X POST http://localhost:5588/odata/Orders(abb00ddf-cc1c-420f-9efc-b2046ee6c892)/NoodleService.Confirm -d "{}"

echo POST
REM curl -H content-type:application/json -X POST http://localhost:5588/odata/Products -d "{Id:'5d7b24fd-18d1-4fd4-9247-9497f98a12dd',Type:'REGULAR',Key:'Bolognese REG',Name:'Bolognese regular'}"

echo POST PRICE
curl -H content-type:application/json -X POST http://localhost:5588/odata/Products(5d7b24fd-18d1-4fd4-9247-9497f98a12dd)/Price -d "{ProductId:'5d7b24fd-18d1-4fd4-9247-9497f98a12dd', Price:10}"

echo PUT PRICE
curl -H content-type:application/json -X PUT http://localhost:5588/odata/Products(5d7b24fd-18d1-4fd4-9247-9497f98a12dd)/Price -d "{ProductId:'5d7b24fd-18d1-4fd4-9247-9497f98a12dd', Price:15}"

echo POST CATEGORY
curl -H content-type:application/json -X POST http://localhost:5588/odata/Products(5d7b24fd-18d1-4fd4-9247-9497f98a12dd)/Categories -d "{Id:'5d7b24fd-18d1-4fd4-9247-9497f98a12de',Key:'TEST_CAT',Name:'TEST_NAME'}"

echo PUT CATEGORY
curl -H content-type:application/json -X PUT http://localhost:5588/odata/Products(5d7b24fd-18d1-4fd4-9247-9497f98a12dd)/Categories(5d7b24fd-18d1-4fd4-9247-9497f98a12de) -d "{Id:'5d7b24fd-18d1-4fd4-9247-9497f98a12de'}"

REM echo GET CATEGORIES
REM curl -H content-type:application/json -X GET http://localhost:5588/odata/Products(5d7b24fd-18d1-4fd4-9247-9497f98a12dd)/Categories

REM echo GET CATEGORY
REM curl -H content-type:application/json -X GET http://localhost:5588/odata/Products(5d7b24fd-18d1-4fd4-9247-9497f98a12dd)/Categories(5d7b24fd-18d1-4fd4-9247-9497f98a12de)

REM echo DELETE
REM curl -H content-type:application/json -X DELETE http://localhost:5588/odata/Products(5d7b24fd-18d1-4fd4-9247-9497f98a12dd)
