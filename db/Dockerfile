FROM mcr.microsoft.com/mssql/server

ENV ACCEPT_EULA Y

# copy from host to container
COPY ./setup.sh setup.sh
COPY ./create.sh create.sh
COPY ./script.sql script.sql

# RUN sudo chmod +x ./setup.sh
# RUN sudo chmod +x ./create.sh

ENTRYPOINT ./setup.sh

# build command: docker build -t helloservedb -e database_name=helloserve -e login_user=helloserve -e login_name=helloserve -e login_password=helloserve -e sa_password=Passw0rd .
# run command: docker run --name helloservedb -p 44402:1433 helloservedb
