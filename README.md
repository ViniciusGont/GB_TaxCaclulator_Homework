# GB_Homework
GB_Homework

- This project was made using the architecture in 3 layers: Presentation (or Port), Core (or Business) and Shared layer for CrossCutting. 
   For the simplicity of not having calls to APIs or to a database the solution does not have the DAL or Adapters layers, but the solution is ready to add more layers if needed.
   The calculation of taxes and prices could have been passed to another layer, but I left it in the domain to make it simpler. In a different scenario we could take it to another layer.

- The application receives the request, and calls the services layer to carry out the validations and operations, this layer that controls the process, calling the domain to make the calculation operations, then it return a JSON object with the result. 

Exemple:

Request: 

GET: https://localhost:7026/tax/calculateTaxes?regionCode=Austria&VATRate=20&value=100&calculationValueType=NetPrice

Response:

{
    "valueAddedTax": 20.0,
    "netPrice": 100,
    "grossPrice": 120.0
}


Choose the type of value you are sedding to API:
calculationValueType options: NetPrice, Tax or GrossPrice, 

- You can use Swagger or Postman for tests 

- The solution is ready if you need the add more contries or regions

- Added a test project with some basic tests, much more can be added if needed, everything should be testable. 
