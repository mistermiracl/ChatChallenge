# Chat Challenge

This repository contains two main apps:
* The ChatChallenge.StocksBot, an API that accepts stock codes and returns quotes through a RabbitMQ message queue
* The ChatChallenge.Presentation, a webapp that allows you to chat with mutiple users in several chatrooms, it requests stock data to the StocksBot and receives it through through the RabbitMQ message queue

## Testing

In order to test this solution first ```git clone``` this repo then follow the next steps

## Requirements

* Docker
* .NET 6
* NodeJS (for assets building only)

## Steps

1. On the root of the cloned solution run ```docker-compose up``` it will launch the RabbitMQ server needed
2. Spawn another shell On the root of the cloned solution and run ```dotnet restore```, it will compute the dependency tree and download or compile the required dependencies
3. Navigate to the ```ChatChallenge.StocksBot``` folder and run ```dotnet publish -c Release -o out```
4. Navigate to the ```ChatChallenge.StocksBot/out``` folder and run ```dotnet ChatChallenge.StocksBot.dll```, the StocksBot will start running, it will spawn a http and https server, take note of the https port
5. On a different shell navigate to the root of the solution
6. Run ``dotnet tool install --global dotnet-ef``
7. Now run the following command ``dotnet ef database update --project ChatChallenge.Infrastructure --startup-project ChatChallenge.Presentation`` it will run all migrations which will define the structure of the database
8. Now navigate to ``ChatChallenge.Presentation`` folder
9. Here run ```npm install``` then ```npm run build``` it will build some assets for the frontend
10. Go to the appSettings.json on the "InternalApis" object change the "StocksBotUrl" port according to the the StocksBot one spawned in the previous step
11. Now run ```dotnet publish -c Release -o out```
12. After that navigate to the ``òut`` folder run ```dotnet ChatChallenge.Presentation.dll --urls https://0.0.0.0:5003``` it will also spawn a https server on the port 5003
13. Lastly navigate the to ``https://localhost:5003`` ignore/accept the certificate warning and follow the steps presented in the webapp

