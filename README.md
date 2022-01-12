# Dot Net Core Contacts Guide Microservice Project

# Prerequests

### 

You should create two mongoDB containers and RabbitMQ container

```
docker run --name contactsguidemongodb -d -p 27017:27017 mongo

docker run --name contactsguidereportmongodb -d -p 27018:27017 mongo

docker run --name contactsguiderabbitmq -d -p 5672:5672 rabbitmq:3.8.27-management
```

Note: I used RabbitMQ CloudAMQP in this project. You can reset the appsettings.json use by container

After container creation, you should create databases following

- contactsguidemongodb -> ContactsGuideDb
- contactsguidereportmongodb -> ContactsGuideReportDb

# API Explanation

There are two way posting requests to our Restful Api. You can you Swagger UI or Postman

If you want to use swagger you should go to following url:

  ```
  Contact Microservice API => http://localhost:5005/swagger/index.html
  Report Microservice API => http://localhost:5006/swagger/index.html
  ```
 ![alt text](https://github.com/burakhayirli/DotNetCore-Mongo-RabbitMQ-ContactsGuide-Microservices-Project/blob/master/images/ContactApiSwaggerr.PNG)
 ![alt text](https://github.com/burakhayirli/DotNetCore-Mongo-RabbitMQ-ContactsGuide-Microservices-Project/blob/master/images/ReportApiSwagger.PNG)

# Used Technologies and Packages Stack

- Asp.Net Core 5 (.Net 5)
- MongoDb.Driver
- ClosedXML
- RabbitMQ
- Swashbuckle.AspNetCore (Used for Swagger UI)

## Todos

- [ ] Prepare xUnit or nUnit Tests
- [ ] Implement Moq Framework for Unit Tests
- [ ] AutoMapper
- [ ] Use Data Transfer Objects in Controllers
- [ ] Implement Fluent Validation
- [ ] Implement Autofac
- [ ] Implement one of IdentityApi, IdentityServer or Manual JWT Handler
- [ ] Prepare dockerfiles and docker-compose.yml files For Each Microservices
- [ ] Use Adapter Pattern for outer api connections
- [ ] Modify README.md
- [ ] Do not be lazy! Work hard!
- [ ] Do not forget read book!
