# Dot Net Core Contacts Guide Microservice Project

# Prerequests

### 

You should create two mongoDB containers and RabbitMQ container

```
docker run --name contactsguidemongodb -d -p 27017:27017 mongo

docker run --name contactsguidereportmongodb -d -p 27018:27017 mongo

docker run -d --hostname contactsguiderabbithostname --name contactsguiderabbitmq -e RABBITMQ_DEFAULT_USER=admin -e RABBITMQ_DEFAULT_PASS=123456 -p 5672:5672 -p 15672:15672 rabbitmq:3-management
```

After container creation, you should create databases following

- contactsguidemongodb -> ContactsGuideDb
- contactsguidereportmongodb -> ContactsGuideReportDb

# API Explanation

There are two way posting requests to our Restful Api. You can you Swagger UI or Postman

If you want to use swagger you should go to following url:

  ```
  Contact Microservice API => http://localhost:5005/swagger/index.html
  Report Microservice API => http://localhost:5006/swagger/index.html
  Gateway Microservice API => http://localhost:5000
  Core Mvc Web UI => http://localhost:5001
  ```
 ![alt text](https://github.com/burakhayirli/DotNetCore-Mongo-RabbitMQ-ContactsGuide-Microservices-Project/blob/master/images/ContactApiSwaggerr.PNG)
 ![alt text](https://github.com/burakhayirli/DotNetCore-Mongo-RabbitMQ-ContactsGuide-Microservices-Project/blob/master/images/ReportApiSwagger.PNG)

# Unit Tests
 
 - WhenNotExistsPersonIdGiven_GetAsync_ReturnsBadRequestObjectResult
 - WhenExistsPersonIdGiven_GetAsync_ReturnsOkObjectResult
 - WhenReturnsOneOrMorePersonData_GetAllAsync_ReturnsFilledCollection
 - WhenValidInputsAreGiven_Validator_ShouldNotBeReturnErrors
 - WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors

# Used Technologies and Packages Stack

- Asp.Net Core 5 Web API (.Net 5)
- Asp.Net Core 5 MVC
- MongoDb.Driver
- AutoMapper
- Autofac
- Autofac.Extras.DynamicProxy (with Castle)
- Autofac.Extensions.DependencyInjection
- FluentValidation
- ClosedXML
- RabbitMQ
- Moq
- FakeItEasy
- FluentAssertions
- Ocelot
- SignalR
- Swashbuckle.AspNetCore (Used for Swagger UI)

## Todos

- [ ] Do not be lazy! Work hard!
- [ ] Do not forget read book!
