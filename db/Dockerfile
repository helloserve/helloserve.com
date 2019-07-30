FROM microsoft/mssql-server-linux

ENV ACCEPT_EULA Y

# copy from host to container
COPY ./entrypoint.sh entrypoint.sh
COPY ./create.sh create.sh
COPY ./script.sql script.sql

RUN chmod +x ./entrypoint.sh
RUN chmod +x ./create.sh

ENTRYPOINT ./entrypoint.sh

# build command: docker build -t l10ndb -e database_name=l10n -e login_user=l10n -e login_name=l10n -e login_password=l10n -e sa_password=Passw0rd .
# run command: docker run --name l10ndb -p 44402:1433 l10ndb
