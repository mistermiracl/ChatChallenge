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

1. Run ```docker-compose up``` it will launch the RabbitMQ server needed
2. On the root of the cloned repo run ```dotnet restore```, it will compute the dependency tree and download or compile the required dependencies
3. Navigate to the ```ChatChallenge.StocksBot``` folder and run ```dotnet publish -c Release -o out```
4. Navigate to the ```ChatChallenge.StocksBot/out``` folder and run ```dotnet ChatChallenge.StocksBot.dll```, the StocksNot will start running, it will spawn a http and https server take note of the https port
5. On a different shell navigate to the root of the solution  again but this time go to ```ChatChallenge.Presentation``` folder
6. Here run ```npm install``` then ```npm run build```
7. Go to the appSettings.json on the "Apis" objects change the "StocksBot" port according to the stocks bot one
8. Now run ```dotnet publish -c Release -o out```
9. After that navigate to the ``òut`` folder run ```dotnet ChatChallenge.Presentation.dll``` it will also spawn and show a http and https server
10. Lastly navigate the the spawn https server and follow the steps presented in the webapp

