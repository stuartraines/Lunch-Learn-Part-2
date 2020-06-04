FROM boxfuse/flyway:latest
COPY ./sql sql

EXPOSE 80/tcp

ENTRYPOINT ["flyway", "migrate"]