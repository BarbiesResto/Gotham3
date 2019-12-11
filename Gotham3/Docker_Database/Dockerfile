# building our new image from the microsft SQL 2017 image
FROM mcr.microsoft.com/mssql/server:latest

#Les crédentials du serveur MSSQL
ENV sa_password=1Secure*Password1				
ENV ACCEPT_EULA=Y

#TODO :
#1) Regarder ce que fait le fichier run.sh
#2) Développer une série d'instructions pour copier le ficher run.sh et script.sql sur dans le container (ressemble beaucoup à l'exercice helloscripts)
#   Note :  On devra faire un chmod +x sur le fichier run.sh
COPY ./Gotham3/Docker_Database/script.sql ./Gotham3/Docker_Database/
COPY ./Gotham3/Docker_Database/run.sh ./Gotham3/Docker_Database/
USER root 
RUN chmod +x ./Gotham3/Docker_Database/script.sql
RUN chmod +x ./Gotham3/Docker_Database/run.sh
#3) Faire en sorte que le fichier run.sh soit exécuté (le shell est "sh")
CMD ["sh", "./Gotham3/Docker_Database/run.sh"]
#4) Spécifiez que /src est le dossier courant dans le container
WORKDIR /src
#4) Exposer le port 1433
EXPOSE 1433
